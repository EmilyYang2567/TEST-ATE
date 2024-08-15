using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainForm_App;
using Microsoft.Win32.SafeHandles;



namespace MainForm_App
{
    public class CmdExe
    {

        #region 宣告載入的類別
        public Form1 MainForm;
        // 構造函數
        public CmdExe(Form1 form)
        {
            MainForm = form;
        }
        #endregion

        #region DLL 載入
        [DllImport("kernel32.dll", SetLastError = true)]
    static extern SafeFileHandle CreateJobObject(IntPtr lpJobAttributes, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetInformationJobObject(SafeFileHandle hJob, JOBOBJECTINFOCLASS JobObjectInfoClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool AssignProcessToJobObject(SafeFileHandle hJob, IntPtr hProcess);

    [DllImport("kernel32.dll", SetLastError = true)]
        #endregion

        #region TerminateJobObject 外部終止作業對象中的所有進程
        static extern bool TerminateJobObject(SafeFileHandle hJob, uint uExitCode);
        #endregion

        public List<Process> childProcesses = new List<Process>();

        #region 指定獲取或設置作業對象信息的類型
        enum JOBOBJECTINFOCLASS
        {
            JobObjectBasicLimitInformation = 2, //用於獲取或設置基本限制信息。
            JobObjectExtendedLimitInformation = 9 //用於獲取或設置擴展限制信息。
        }
        #endregion

        [StructLayout(LayoutKind.Sequential)]

        #region 定義活動進行的進程
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
        #endregion
        [StructLayout(LayoutKind.Sequential)]
        #region 包含了進行中的進程 I/O 
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
        #endregion
        [Flags]

        #region 定義主進程關閉時，終止其中所有的進程。
        enum JOBOBJECT_LIMIT_FLAGS : uint
        {
           JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000
        }
        #endregion

        #region CMD執行結果存放
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
        #endregion

        #region 執行cmd command
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
                            AppendText(args.Data + Environment.NewLine);
                        }
                    };

                    process.ErrorDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            errorBuilder.AppendLine(args.Data);
                            AppendText(args.Data + Environment.NewLine);
                        }
                    };

                    Task timeoutTask = Task.Delay(timeoutMilliseconds, cancellationTokenSource.Token).ContinueWith(async t =>
                    {
                        if (!process.HasExited)
                        {
                            executionResult.TimedOut = true;
                            process.Kill();
                            await WaitForExitAsync(process);

                            AppendText($"Command execution timed out after {stopwatch.Elapsed.TotalMilliseconds} ms" + Environment.NewLine);
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
                AppendText($"Error: {ex.Message}" + Environment.NewLine);
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
        #endregion

        #region  等待 Process 的退出
        public Task WaitForExitAsync(Process process)
        {
            var tcs = new TaskCompletionSource<bool>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.SetResult(true);
            return tcs.Task;
        }
        #endregion

        #region 終止所有執行Process
        public async Task TerminateAllProcesses()
        {
            foreach (Process process in childProcesses)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        await WaitForExitAsync(process);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error terminating process: {ex.Message}");
                }
            }
        }
        #endregion
        public void AppendText(string text)
        {
            if (!MainForm.txt_result.IsDisposed && !MainForm.txt_result.Disposing)
            {
                if (MainForm.txt_result.InvokeRequired)
                {
                    MainForm.txt_result.Invoke(new Action<string>(AppendText), text);
                }
                else
                {
                    MainForm.txt_result.AppendText(text);
                    
                }
            }
        }
    }
}
