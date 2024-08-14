// ProcessManager.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Management;

namespace MainForm_App // MainForm_App Option_FormsApp
{
    // 自定義的 delegate
    public delegate void FlowMsg(string sMsg);
    public delegate void MsgNextSection();
    public delegate void FlowMsgLink(bool bIsOkMsg, string sMsg);
    public delegate void FlowMsgLinkEx(bool bBold, bool bItalic, Color cColor, string sMsg);
    public delegate void ErrMsg(string sMsg);
    public delegate void FlowMsgDel(int nLen);
    public delegate void FlowAddMsg(string sMsg);
    public delegate bool IsStopTest();

    public class ProcessManager : IDisposable
    {
        private SafeFileHandle jobHandle;//
        private bool disposed = false; //
        private List<Process> childProcesses = new List<Process>();
        // Import the TerminateProcess function from kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateJobObject(IntPtr lpJobAttributes, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetInformationJobObject(SafeFileHandle hJob, JOBOBJECTINFOCLASS JobObjectInfoClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AssignProcessToJobObject(SafeFileHandle hJob, IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool TerminateJobObject(SafeFileHandle hJob, uint uExitCode);

        enum JOBOBJECTINFOCLASS
        {
            JobObjectBasicLimitInformation = 2,
            JobObjectExtendedLimitInformation = 9
        }

        [StructLayout(LayoutKind.Sequential)]
        struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public long PerProcessUserTimeLimit;
            public long PerJobUserTimeLimit;
            public JOBOBJECT_LIMIT_FLAGS LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public uint ActiveProcessLimit;
            public long Affinity;
            public uint PriorityClass;
            public uint SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct IO_COUNTERS
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        }

        [Flags]
        enum JOBOBJECT_LIMIT_FLAGS : uint
        {
            JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000
        }
        public class CommandExecutionResult
        {
            public bool Success { get; set; }
            public double ExecutionTimeInSeconds { get; set; }
            public string Output { get; set; }
            public string Error { get; set; }
            public string ErrorMessage { get; set; }
            public bool TimedOut { get; set; }
            public int ExitCode { get; set; }
            public bool HasExited { get; set; }
        }
        public enum CmdExeResult
        {
            RunInit,
            Success,
            Failure,
            Timeout,
            RunAbort
        }
        public ProcessManager()
        {
            jobHandle = CreateJobObject(IntPtr.Zero, null);
            if (jobHandle.IsInvalid)
            {
                throw new InvalidOperationException("Unable to create job object.");
            }

            var info = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
            {
                BasicLimitInformation = { LimitFlags = JOBOBJECT_LIMIT_FLAGS.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE }
            };

            int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            IntPtr infoPtr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(info, infoPtr, false);

            SetInformationJobObject(jobHandle, JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation, infoPtr, (uint)length);
            Marshal.FreeHGlobal(infoPtr);
        }

        public void AssignProcess(Process process)
        {
            if (disposed) throw new ObjectDisposedException("ProcessManager");

            AssignProcessToJobObject(jobHandle, process.Handle);
            childProcesses.Add(process);
        }
        public async Task<CommandExecutionResult> RunCmdAsync(FlowMsg FlowMsg, string program, string command, int timeoutMilliseconds)
        {
            CommandExecutionResult executionResult = new CommandExecutionResult();
            var cancellationTokenSource = new CancellationTokenSource();
            //childProcesses.Clear();
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = program,
                        Arguments = $"/c {command}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    };

                    StringBuilder outputBuilder = new StringBuilder();
                    StringBuilder errorBuilder = new StringBuilder();

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    AssignProcess(process);

                    process.OutputDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            outputBuilder.AppendLine(args.Data);
                            FlowMsg(args.Data + Environment.NewLine);

                        }
                    };

                    process.ErrorDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            errorBuilder.AppendLine(args.Data);
                            FlowMsg(args.Data + Environment.NewLine);
                        }
                    };

                    Task timeoutTask = Task.Delay(timeoutMilliseconds, cancellationTokenSource.Token).ContinueWith(async t =>
                    {
                        if (!process.HasExited)
                        {
                            executionResult.TimedOut = true;
                            process.Kill();
                            await WaitForExitAsync(process);
                            FlowMsg($"Command execution timed out after {stopwatch.Elapsed.TotalMilliseconds} ms" + Environment.NewLine);
                        }
                    }, cancellationTokenSource.Token);

                    await Task.WhenAny(timeoutTask, WaitForExitAsync(process));

                    if (process.HasExited)
                    {
                        stopwatch.Stop();
                        executionResult.Success = true;
                        executionResult.ExecutionTimeInSeconds = stopwatch.Elapsed.TotalSeconds;
                        executionResult.Output = outputBuilder.ToString();
                        executionResult.Error = errorBuilder.ToString();
                        executionResult.ExitCode = process.ExitCode;
                        executionResult.HasExited = true;
                    }
                    else
                    {
                        stopwatch.Stop();
                        executionResult.TimedOut = true;
                        executionResult.Success = false;
                    }

                    if (!executionResult.Success)
                    {
                        executionResult.ErrorMessage = errorBuilder.ToString();
                    }

                    cancellationTokenSource.Dispose();
                }
            }
            catch (Exception ex)
            {
                executionResult.Success = false;
                executionResult.ErrorMessage = ex.Message;
            }
            return executionResult;
        }

        /*private void AppendText(string text)
        {
            if (!mainForm.txt_result.IsDisposed && !Form1.txt_result.Disposing)
            {
                if (Form1.txt_result.InvokeRequired)
                {
                    Form1.txt_result.Invoke(new Action<string>(AppendText), text);
                }
                else
                {
                    Form1.txt_result.AppendText(text);
                }
            }
        }*/
        public async Task TerminateAllProcesses()
        {
            foreach (Process process in childProcesses)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    await WaitForExitAsync(process);
                }
            }
        }

        public Task WaitForExitAsync(Process process)
        {
            var tcs = new TaskCompletionSource<bool>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.SetResult(true);
            return tcs.Task;
        }
        public void Dispose()
        {
            if (!disposed)
            {
                if (jobHandle != null && !jobHandle.IsInvalid)
                {
                    TerminateJobObject(jobHandle, 0);
                    jobHandle.Dispose();
                }
                disposed = true;
            }
        }
  
    }
}
