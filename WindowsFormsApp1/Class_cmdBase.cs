using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using Option_FormsApp;
namespace WindowsFormsApp1
{
    public class Class_cmdBase
    {

        private Form1 form1;
        private List<Process> childProcesses = new List<Process>();

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

        class CommandExecutionResult
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
        private Task WaitForExitAsync(Process process)
        {
            var tcs = new TaskCompletionSource<bool>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.SetResult(true);
            return tcs.Task;
        }
        public async Task<CommandExecutionResult> RunCmdAsync(string program, string command, int timeoutMilliseconds)
        {
            CommandExecutionResult executionResult = new CommandExecutionResult();
            var cancellationTokenSource = new CancellationTokenSource();

            SafeFileHandle jobHandle = CreateJobObject(IntPtr.Zero, null);
            if (jobHandle.IsInvalid)
            {
                throw new InvalidOperationException("Unable to create job object.");
            }

            IntPtr infoPtr = IntPtr.Zero;

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

                    JOBOBJECT_EXTENDED_LIMIT_INFORMATION info = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
                    info.BasicLimitInformation.LimitFlags = JOBOBJECT_LIMIT_FLAGS.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;

                    int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
                    infoPtr = Marshal.AllocHGlobal(length);
                    Marshal.StructureToPtr(info, infoPtr, false);

                    SetInformationJobObject(jobHandle, JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation, infoPtr, (uint)length);

                    StringBuilder outputBuilder = new StringBuilder();
                    StringBuilder errorBuilder = new StringBuilder();

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    AssignProcessToJobObject(jobHandle, process.Handle);
                    childProcesses.Add(process);

                    process.OutputDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            outputBuilder.AppendLine(args.Data);
                            form1.MF_Msg(args.Data + Environment.NewLine);
                        }
                    };

                    process.ErrorDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            errorBuilder.AppendLine(args.Data);
                            form1.MF_Msg(args.Data + Environment.NewLine);
                        }
                    };

                    Task timeoutTask = Task.Delay(timeoutMilliseconds, cancellationTokenSource.Token).ContinueWith(async t =>
                    {
                        if (!process.HasExited)
                        {
                            executionResult.TimedOut = true;
                            process.Kill();
                            await WaitForExitAsync(process);

                            form1.MF_Msg($"Command execution timed out after {stopwatch.Elapsed.TotalMilliseconds} ms" + Environment.NewLine);
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
                        childProcesses.Remove(process);
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
            finally
            {
                if (executionResult.TimedOut && !jobHandle.IsInvalid)
                {
                    TerminateJobObject(jobHandle, 0);
                }

                if (infoPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(infoPtr);
                }
            }
            return executionResult;
        }
    }
}