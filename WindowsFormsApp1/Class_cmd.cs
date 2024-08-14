using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using Option_FormsApp;

namespace WindowsFormsApp1
{
    public class Class_cmd : Class_cmdBase
    {
        private Form1 form1;

        // 設置 Form1 實例的方法
        public void SetForm1Instance(Form1 formInstance)
        {
            form1 = formInstance;
        }
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
        public async Task ExecuteCommandAsync(string commandName, string command, int timeoutMilliseconds)
        {
            form1.MF_Msg($"===== {commandName} Start ==============================={Environment.NewLine}");

            try
            {
                childProcesses.Clear();

                CommandExecutionResult result = await RunCmdAsync("cmd.exe", command, timeoutMilliseconds);

                form1.MF_Msg(result.TimedOut ? $"TIMEOUT{Environment.NewLine}" : result.Success ? $"PASS{Environment.NewLine}" : $"NG{Environment.NewLine}");
                form1.MF_Msg($"Cmd {commandName} TestTime: {result.ExecutionTimeInSeconds:F2}s{Environment.NewLine}");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Command execution cancelled.");
            }
            finally
            {
                form1.MF_Msg($"===== {commandName} End ================================={Environment.NewLine}");
            }
        }

        private async Task TerminateAllProcesses()
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
     
    }
}
