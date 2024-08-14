using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm_App
{
    public class TestItem
    {
        // declare form object
        public MainForm MainFormForm;
        public CfmBox CfmBoxForm = new CfmBox();
        public MsgBox MsgBoxForm = new MsgBox();

        /*------------------------------------------------------------------------
            * Function: TestItem
            *  Purpose: the function to link to MainForm form object passed 
            *           in from other Forms.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public TestItem(MainForm MainFormForm)
        {
            this.MainFormForm = MainFormForm;

            // Set Icon
            CfmBoxForm.Icon = MainFormForm.Icon;

            // 設定子表單不顯示在工作列
            CfmBoxForm.ShowInTaskbar = false;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem1
            *  Purpose: the function to Set the test process for TestItem1.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem1()
        {
            string sCommand;            // 要執行的DOS指令
            int iTimeoutMilliseconds;   // 設置超時時間(毫秒)
            WinCmd.CmdExeResult result = WinCmd.CmdExeResult.RunInit;

            // sCommand = "Ping 127.0.0.1";
            sCommand = "Ping 8.8.8.8";
            // timeout 時間單位從秒轉換成毫秒
            iTimeoutMilliseconds = 10 * 1000;

            // 建立Stopwatch 計算運行時間
            Stopwatch stopwatch = new Stopwatch();

            // 計時開始
            stopwatch.Start();

            MainFormForm.FlowMsg("TEST1");

            try // 非同步執行指令，並等待結果
            {
                result = await WinCmd.CmdExeAsync(
                    MainFormForm.FlowMsg,
                    MainFormForm.FlowMsgLink,
                    MainFormForm.FlowMsgLinkEx,
                    MainFormForm.ErrMsg,
                    MainFormForm.MsgNextSection,
                    MainFormForm.FlowMsgDel,
                    MainFormForm.FlowCmdMsg,
                    MainFormForm.IsStopTest,
                    sCommand,
                    sCommand,
                    iTimeoutMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command execution failed: {ex.Message}");
                MainFormForm.ErrMsg($"Command execution failed: {ex.Message}");
            }

            // 計時停止
            stopwatch.Stop();

            // 取得運行時間
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // 使用浮點數轉換成字串，取得毫秒數轉成秒數後並取到小數點第一位
            string elapsedTimeSeconds = $"{(elapsedTime.TotalMilliseconds / 1000):F1}";

            // 輸出運行時間
            Console.WriteLine($"Command execution Time: {elapsedTimeSeconds} s");
            MainFormForm.FlowMsg($"Command execution Time: {elapsedTimeSeconds} s");

            switch (result)
            {
                case WinCmd.CmdExeResult.RunInit:
                    Console.WriteLine("Not command execution.");
                    MainFormForm.ErrMsg("Not command execution.");
                    MfGlobal.LogRlt.sInitDUT = "Fail:Not command execution.";
                    return false;
                case WinCmd.CmdExeResult.Success:
                    Console.WriteLine("Command executed successfully.");
                    MainFormForm.FlowMsg("Command executed successfully.");
                    MfGlobal.LogRlt.sInitDUT = "Pass";
                    break;
                case WinCmd.CmdExeResult.Timeout:
                    Console.WriteLine("Command execution timed out.");
                    MainFormForm.ErrMsg("Command execution timed out.");
                    MfGlobal.LogRlt.sInitDUT = "Fail:Command execution timed out.";
                    return false;
                case WinCmd.CmdExeResult.Failure:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sInitDUT = "Fail:Command execution failed.";
                    return false;
                case WinCmd.CmdExeResult.RunAbort:
                    Console.WriteLine("Command execution abort.");
                    MainFormForm.ErrMsg("Command execution abort.");
                    MfGlobal.LogRlt.sInitDUT = "Fail:Command execution abort.";
                    return false;
                default:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sInitDUT = "Fail:Command execution failed.";
                    return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem2
            *  Purpose: the function to Set the test process for TestItem2.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem2()
        {
            string sCommand;            // 要執行的DOS指令
            int iTimeoutMilliseconds;   // 設置超時時間(毫秒)
            WinCmd.CmdExeResult result = WinCmd.CmdExeResult.RunInit;

            sCommand = "Ping 127.0.0.1";
            // timeout 時間單位從秒轉換成毫秒
            iTimeoutMilliseconds = 10 * 1000;

            // 建立Stopwatch 計算運行時間
            Stopwatch stopwatch = new Stopwatch();

            // 計時開始
            stopwatch.Start();

            MainFormForm.FlowMsg("TEST2");

            try // 非同步執行指令，並等待結果
            {
                result = await WinCmd.CmdExeAsync(
                    MainFormForm.FlowMsg,
                    MainFormForm.FlowMsgLink,
                    MainFormForm.FlowMsgLinkEx,
                    MainFormForm.ErrMsg,
                    MainFormForm.MsgNextSection,
                    MainFormForm.FlowMsgDel,
                    MainFormForm.FlowCmdMsg,
                    MainFormForm.IsStopTest,
                    sCommand,
                    sCommand,
                    iTimeoutMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command execution failed: {ex.Message}");
                MainFormForm.ErrMsg($"Command execution failed: {ex.Message}");
            }

            // 計時停止
            stopwatch.Stop();

            // 取得運行時間
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // 使用浮點數轉換成字串，取得毫秒數轉成秒數後並取到小數點第一位
            string elapsedTimeSeconds = $"{(elapsedTime.TotalMilliseconds / 1000):F1}";

            // 輸出運行時間
            Console.WriteLine($"Command execution Time: {elapsedTimeSeconds} s");
            MainFormForm.FlowMsg($"Command execution Time: {elapsedTimeSeconds} s");

            switch (result)
            {
                case WinCmd.CmdExeResult.RunInit:
                    Console.WriteLine("Not command execution.");
                    MainFormForm.ErrMsg("Not command execution.");
                    MfGlobal.LogRlt.sTestItem2 = "Fail:Not command execution.";
                    return false;
                case WinCmd.CmdExeResult.Success:
                    Console.WriteLine("Command executed successfully.");
                    MainFormForm.FlowMsg("Command executed successfully.");
                    MfGlobal.LogRlt.sTestItem2 = "Pass";
                    break;
                case WinCmd.CmdExeResult.Timeout:
                    Console.WriteLine("Command execution timed out.");
                    MainFormForm.ErrMsg("Command execution timed out.");
                    MfGlobal.LogRlt.sTestItem2 = "Fail:Command execution timed out.";
                    return false;
                case WinCmd.CmdExeResult.Failure:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sTestItem2 = "Fail:Command execution failed.";
                    return false;
                case WinCmd.CmdExeResult.RunAbort:
                    Console.WriteLine("Command execution abort.");
                    MainFormForm.ErrMsg("Command execution abort.");
                    MfGlobal.LogRlt.sTestItem2 = "Fail:Command execution abort.";
                    return false;
                default:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sTestItem2 = "Fail:Command execution failed.";
                    return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem3
            *  Purpose: the function to Set the test process for TestItem3.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem3()
        {
            MainFormForm.FlowMsg("TEST3");
            await Task.Delay(2000);
            MfGlobal.LogRlt.sTestItem3 = "Pass";
            return true;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem4
            *  Purpose: the function to Set the test process for TestItem4.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem4()
        {
            string sCommand;            // 要執行的DOS指令
            int iTimeoutMilliseconds;   // 設置超時時間(毫秒)
            WinCmd.CmdExeResult result = WinCmd.CmdExeResult.RunInit;

            sCommand = "Ping 192.168.178.2";
            // timeout 時間單位從秒轉換成毫秒
            iTimeoutMilliseconds = 30 * 1000;

            // 建立Stopwatch 計算運行時間
            Stopwatch stopwatch = new Stopwatch();

            // 計時開始
            stopwatch.Start();

            MainFormForm.FlowMsg("TEST1");

            try // 非同步執行指令，並等待結果
            {
                result = await WinCmd.CmdExeAsync(
                    MainFormForm.FlowMsg,
                    MainFormForm.FlowMsgLink,
                    MainFormForm.FlowMsgLinkEx,
                    MainFormForm.ErrMsg,
                    MainFormForm.MsgNextSection,
                    MainFormForm.FlowMsgDel,
                    MainFormForm.FlowCmdMsg,
                    MainFormForm.IsStopTest,
                    sCommand,
                    sCommand,
                    iTimeoutMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command execution failed: {ex.Message}");
                MainFormForm.ErrMsg($"Command execution failed: {ex.Message}");
            }

            // 計時停止
            stopwatch.Stop();

            // 取得運行時間
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // 使用浮點數轉換成字串，取得毫秒數轉成秒數後並取到小數點第一位
            string elapsedTimeSeconds = $"{(elapsedTime.TotalMilliseconds / 1000):F1}";

            // 輸出運行時間
            Console.WriteLine($"Command execution Time: {elapsedTimeSeconds} s");
            MainFormForm.FlowMsg($"Command execution Time: {elapsedTimeSeconds} s");

            switch (result)
            {
                case WinCmd.CmdExeResult.RunInit:
                    Console.WriteLine("Not command execution.");
                    MainFormForm.ErrMsg("Not command execution.");
                    MfGlobal.LogRlt.sTestItem4 = "Fail:Not command execution.";
                    return false;
                case WinCmd.CmdExeResult.Success:
                    Console.WriteLine("Command executed successfully.");
                    MainFormForm.FlowMsg("Command executed successfully.");
                    MfGlobal.LogRlt.sTestItem4 = "Pass";
                    break;
                case WinCmd.CmdExeResult.Timeout:
                    Console.WriteLine("Command execution timed out.");
                    MainFormForm.ErrMsg("Command execution timed out.");
                    MfGlobal.LogRlt.sTestItem4 = "Fail:Command execution timed out.";
                    return false;
                case WinCmd.CmdExeResult.Failure:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sTestItem4 = "Fail:Command execution failed.";
                    return false;
                case WinCmd.CmdExeResult.RunAbort:
                    Console.WriteLine("Command execution abort.");
                    MainFormForm.ErrMsg("Command execution abort.");
                    MfGlobal.LogRlt.sTestItem4 = "Fail:Command execution abort.";
                    return false;
                default:
                    Console.WriteLine("Command execution failed.");
                    MainFormForm.ErrMsg("Command execution failed.");
                    MfGlobal.LogRlt.sTestItem4 = "Fail:Command execution failed.";
                    return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem5
            *  Purpose: the function to Set the test process for TestItem5.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem5()
        {
            MainFormForm.FlowMsg("TEST5");
            bool bRtn;
            
            bRtn = CfmBoxForm.ShowTestDlg("Make sure the LED is flashing");
            
            if (bRtn)
            {
                MfGlobal.LogRlt.sTestItem5 = "Pass";
                MsgBoxForm.MsgBox_Info("Pass"); ;
            }
            else
            {
                MfGlobal.LogRlt.sTestItem5 = "Fail";
                MsgBoxForm.MsgBox_Info("Fail"); ;
            }

            return bRtn;
        }

        /*------------------------------------------------------------------------
            * Function: TestItem6
            *  Purpose: the function to Set the test process for TestItem6.
            *  Returns: 1. TRUE - Test process successfully.
            *           2. FALSE - Test process fail.
            *     Note: Use asynchronous functions.
            *           function 目前是Debug 狀態。
            *------------------------------------------------------------------------
            */
        public async Task<bool> TestItem6()
        {
            MainFormForm.FlowMsg("TEST6");
            await Task.Delay(4000);
            MfGlobal.LogRlt.sTestItem6 = "Pass";
            return true;
        }
    }
}
