using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class SfGlobal
{
    public struct BaseInformation_T
    {
        public string sSerialNumber;    // Serial number
        public string sMachineName;     // 測試設備名稱
        public string sProgramName;     // 程式名稱
        public string sProgramVer;      // 程式版本
        public string sTestResult;      // 測試結果 (Pass輸入值:Y / Fail輸入值:N)
        public string sRetest;          // 重新測試 (重測入值:Y / 不重測入值:N)
        public string sOpId;            // Operator identity
        public int iTestCnt;            // 測試項目數量
        public int iErrCnt;             // 測試結果為Fail的測試項目數量
        public string sErrCode;         // Error Code
        // public string sBatch;           // Batch, 分批上傳, Batch-Fir, Batch-Con, Batch-End
        public string sCartonBoxNo;    // Carton Name
    };

    public struct TestItemResult_T
    {
        public string sTestItemFieldName;
        public string sTestLog;
        public int iTestResult;  // 0:FAIL 1:PASS 2:NONE_TEST
    };

    public struct Relationship_T
    {
        public bool bCommand4;         // 寫入SN & MAC 之間的關聯
        public bool bCommand5;         // 驗證SN & MAC 之間的關聯

        public bool bCommand6;         // SN轉換為最上階SN
        public bool bCommand9;         // Carton update shopflow byteData
        public bool bCommand14;        // 寫入SN & MAC & CSN 之間的關聯

        public string sMacAddress;     // 寫入關聯的MAC
        public string sCsn;            // 寫入關聯的CSN
    };

    public struct ShopFlowStruct_T
    {
        public BaseInformation_T BaseInfo;
        public TestItemResult_T[] TestItemResult;
        public Relationship_T Relationship;
        public string sErrMsg;
    }

    public static ShopFlowStruct_T Pub;
}

namespace MainForm_App
{
    public class Shopflow
    {
        // DllImport shopflow function
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransStart();    // Shopflow initialization and startup function
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransClose();    // Shopflow close function
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransData(int f_iCommandNo, byte[] f_pData, ref Int32 f_pLen);   // Shopflow send byteData function

        public MsgBox MsgBoxForm = new MsgBox();

        /*------------------------------------------------------------------------
            * Function: InitialCheck
            *  Purpose: the function to check whether the connection to the shopflow is correct.
            *  Returns: 1. TRUE - Connection shopflow successfully.
            *           2. FALSE - Connection shopflow failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool InitialCheck(
            ref string sRtnMsg   // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            sUpdateTemp = "Test";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(7, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-7:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-7:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine($"COMMAND-7:{sResult}"); // Debug print
            Console.WriteLine("COMMAND-7 check OK");    // Debug print
            sRtnMsg = $"COMMAND-7 check OK";
            return true;
        }

        /*------------------------------------------------------------------------
            * Function: InitialCheckNoRtnMsg
            *  Purpose: the function to check whether the connection to the shopflow is correct.
            *  Returns: 1. TRUE - Connection shopflow successfully.
            *           2. FALSE - Connection shopflow failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool InitialCheckNoRtnMsg()
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;

            sUpdateTemp = "Test";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(7, byteData, ref int32Len))
            {
                return false;
            }

            Console.WriteLine("COMMAND-7 check OK");    // Debug print
            return true;
        }

        /*------------------------------------------------------------------------
            * Function: SendData
            *  Purpose: the function to send message to shopflow.
            *  Returns: 1. TRUE - send message to shopflow successfully.
            *           2. FALSE - send message to shopflow failed.
            *     Note: Undone.
            *------------------------------------------------------------------------
            */
        public bool SendData(
            FlowMsg FlowMsg,    // [Input] 顯示訊息的主頁面副程式
            ErrMsg ErrMsg       // [Input] 顯示訊息的主頁面副程式
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;
            int iCmd = 3;

            // Informatiokn Check Area -----------------------------------------------------------------------------------------

            // STEP 1: Check AbocomShopFlow Structure
            // Test Count must more than 0
            if (SfGlobal.Pub.BaseInfo.iTestCnt == 0)
            {
                SfGlobal.Pub.sErrMsg = "Test Count == 0";
                return false;
            }

            // Serial Number must can not be NULL string
            if (String.IsNullOrEmpty(SfGlobal.Pub.BaseInfo.sSerialNumber))
            {
                SfGlobal.Pub.sErrMsg = "Serial Number == 0";
                return false;
            }

            // Operator identity must can not be NULL string
            if (String.IsNullOrEmpty(SfGlobal.Pub.BaseInfo.sOpId))
            {
                SfGlobal.Pub.sErrMsg = "Operator identity == 0";
                return false;
            }

/*
            // STEP 2: Check error count
            SfGlobal.Pub.BaseInfo.iErrCnt = 0;
            for (int i = 0; i < InfoGlobal.AteItemNo; i++)
            {
                if (i >= SfGlobal.Pub.BaseInfo.iTestCnt)
                    break;
                if (SfGlobal.Pub.TestItemResult[i].iTestResult == 0)
                    SfGlobal.Pub.BaseInfo.iErrCnt++;
            }*/ //emily

            /* if (SfGlobal.Pub.BaseInfo.sBatch == "Batch-End") // mark 批量上拋功能
            {
                if (SfGlobal.Pub.BaseInfo.iErrCnt == 0)                     // Test sResult is PASS.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "Y";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                    SfGlobal.Pub.BaseInfo.sErrCode = "N/A";

                    if (SfGlobal.Pub.Relationship.bCommand9 == true)
                        iCmd = 9;
                }

                // Some test station that have only one test item
                // don't need to show Retest ask message box(for example : UpdateOS)
                else if (SfGlobal.Pub.BaseInfo.iTestCnt == 1)                 // Test sResult is FAIL and only have one test item.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "N";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                }

                else                                                                    // Test sResult is FAIL and have many test items.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "N";
                    if (MsgBoxForm.MsgBox_Confirm("Retest DUT ?"))
                        SfGlobal.Pub.BaseInfo.sRetest = "Y";
                    else
                    {
                        if (MsgBoxForm.MsgBox_Confirm("Make sure again!!\nRetest DUT ?"))
                            SfGlobal.Pub.BaseInfo.sTestResult = "Y";
                        else
                            SfGlobal.Pub.BaseInfo.sRetest = "N";
                    }
                    ErrMsg("Error Code:%s", SfGlobal.Pub.BaseInfo.sErrCode);
                }
            }
            else if (SfGlobal.Pub.BaseInfo.sBatch == "Batch-Fir" || SfGlobal.Pub.BaseInfo.sBatch == "Batch-Con")
            {
                if (SfGlobal.Pub.BaseInfo.iErrCnt == 0)
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "B";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                    SfGlobal.Pub.BaseInfo.sTestResult = "N/A";

                    if (SfGlobal.Pub.Relationship.bCommand9 == true)
                        iCmd = 9;
                }
                else
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "B";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                }
            } */    // mark 批量上拋功能
            // else    // 非批量上拋shopflow data
            {
                if (SfGlobal.Pub.Relationship.bCommand9 == true)
                    iCmd = 9;

                if (SfGlobal.Pub.BaseInfo.iErrCnt == 0)         // Test sResult is PASS.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "Y";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                    SfGlobal.Pub.BaseInfo.sErrCode = "N/A";
                }
                // Some test station that have only one test item
                // don't need to show Retest ask message box(for example : UpdateOS)
                else if (SfGlobal.Pub.BaseInfo.iTestCnt == 1)     // Test sResult is FAIL and only have one test item.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "N";
                    SfGlobal.Pub.BaseInfo.sRetest = "N";
                }
                else                                                // Test sResult is FAIL and have many test items.
                {
                    SfGlobal.Pub.BaseInfo.sTestResult = "N";
                    if (MsgBoxForm.MsgBox_Confirm("Retest DUT ?"))
                        SfGlobal.Pub.BaseInfo.sRetest = "Y";
                    else
                    {
                        if (MsgBoxForm.MsgBox_Confirm("Make sure again!!\nRetest DUT ?"))
                            SfGlobal.Pub.BaseInfo.sRetest = "Y";
                        else
                            SfGlobal.Pub.BaseInfo.sRetest = "N";
                    }

                    ErrMsg($"Error Code:{SfGlobal.Pub.BaseInfo.sErrCode}");
                }
            }

            // Command List Area -----------------------------------------------------------------------------------------


            // Check shopflow connect status
            if (!InitialCheckNoRtnMsg())
            {
                string sRtnMsg = "";
                if (AddToTable(ref sRtnMsg))
                {
                    FlowMsg($"{sRtnMsg}");
                }
                else
                {
                    ErrMsg($"{sRtnMsg}");
                }

                sRtnMsg = "";
                if (SfGlobal.Pub.Relationship.bCommand4 == true)
                {
                    sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber};{SfGlobal.Pub.Relationship.sMacAddress}";

                    if (AddToTableForMac(sUpdateTemp, ref sRtnMsg))
                    {
                        FlowMsg($"{sRtnMsg}");
                    }
                    else
                    {
                        ErrMsg($"{sRtnMsg}");
                    }
                }

                sRtnMsg = "";
                if (SfGlobal.Pub.Relationship.bCommand14 == true)
                {
                    sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sOpId};{SfGlobal.Pub.BaseInfo.sSerialNumber};{SfGlobal.Pub.Relationship.sCsn};{SfGlobal.Pub.Relationship.sMacAddress}";

                    if (AddToTableForCsn(sUpdateTemp, ref sRtnMsg))
                    {
                        FlowMsg($"{sRtnMsg}");
                    }
                    else
                    {
                        ErrMsg($"{sRtnMsg}");
                    }
                }

                SfGlobal.Pub.sErrMsg = "Can not connect to shopflow server!!";
                return false;
            }

            // STEP 3: COMMAND_6 (Transfer Assembly Serial Number to Serial Number)

            if (SfGlobal.Pub.Relationship.bCommand6 == true)
            {
                sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber}";
                byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
                Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                int32Len = byteUpdateTemp.Length;

                if (!SajetTransData(6, byteData, ref int32Len))
                {
                    sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                    Console.WriteLine($"COMMAND-6:{sResult}"); // Debug print
                    SfGlobal.Pub.sErrMsg = $"COMMAND-6:{sResult}";
                    // mf_AddToTable(AbocomShopFlow);
                    return false;
                }
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

                string[] cutting = sResult.Split(';');
                SfGlobal.Pub.BaseInfo.sSerialNumber = cutting[0];
                Console.WriteLine($"SfGlobal.Pub.BaseInfo.sSerialNumber={SfGlobal.Pub.BaseInfo.sSerialNumber}");
                Console.WriteLine("Command 6 check OK.");

                FlowMsg("Command 6 check OK.");
            }


            // STEP 4: COMMAND_5 (Check SN & MAC relation)
            if (SfGlobal.Pub.Relationship.bCommand5 == true)
            {
                sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber}";
                byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
                Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                int32Len = byteUpdateTemp.Length;

                if (!SajetTransData(5, byteData, ref int32Len))
                {
                    sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                    Console.WriteLine($"COMMAND-5:{sResult}"); // Debug print
                    SfGlobal.Pub.sErrMsg = $"COMMAND-5:{sResult}";
                    // mf_AddToTable(AbocomShopFlow);
                    return false;
                }

                FlowMsg("Command 5 check OK.");
            }

            // STEP 5: COMMAND_2 (Check OPID)
            sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber}";
            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            if (!SajetTransData(2, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-2:{sResult}"); // Debug print
                SfGlobal.Pub.sErrMsg = $"COMMAND-2:{sResult}";
                // mf_AddToTable(AbocomShopFlow);
                return false;
            }

            FlowMsg("Command 2 check OK.");


            // STEP 6: COMMAND_1 (Check Seriral Number)
            sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber}";
            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            if (!SajetTransData(1, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-1:{sResult}"); // Debug print
                SfGlobal.Pub.sErrMsg = $"COMMAND-1:{sResult}";
                // mf_AddToTable(AbocomShopFlow);
                return false;
            }

            FlowMsg("Command 1 check OK.");


            // STEP 7: COMMAND_4 (Set up SN & MAC relation)
            if (SfGlobal.Pub.Relationship.bCommand4 == true)
            {
                sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sSerialNumber};{SfGlobal.Pub.Relationship.sMacAddress}";
                FlowMsg(sUpdateTemp);
                byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
                Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                int32Len = byteUpdateTemp.Length;

                if (!SajetTransData(4, byteData, ref int32Len))
                {
                    sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                    Console.WriteLine($"COMMAND-4:{sResult}"); // Debug print
                    SfGlobal.Pub.sErrMsg = $"COMMAND-4:{sResult}";
                    return false;
                }
                FlowMsg("Command 4 check OK.");
            }


            // STEP 8: COMMAND_14 (Set up SN & CSN relation & Mac relation)
            if (SfGlobal.Pub.Relationship.bCommand14 == true)
            {
                sUpdateTemp = $"{SfGlobal.Pub.BaseInfo.sOpId};{SfGlobal.Pub.BaseInfo.sSerialNumber};{SfGlobal.Pub.Relationship.sCsn};{SfGlobal.Pub.Relationship.sMacAddress}";
                FlowMsg(sUpdateTemp);
                byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
                Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                int32Len = byteUpdateTemp.Length;

                if (!SajetTransData(14, byteData, ref int32Len))
                {
                    sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                    Console.WriteLine($"COMMAND-14:{sResult}"); // Debug print
                    SfGlobal.Pub.sErrMsg = $"COMMAND-14:{sResult}";
                    return false;
                }
                FlowMsg("Command 14 check OK.");
            }

            // STEP 9: COMMAND_3 or COMMAND_9 (Upload Data)
            sUpdateTemp =
                    SfGlobal.Pub.BaseInfo.sSerialNumber + ';' +     // Serial Number
                    SfGlobal.Pub.BaseInfo.sMachineName + ';' +      // Machine Name
                    SfGlobal.Pub.BaseInfo.sProgramName + ';' +      // Program Name
                    SfGlobal.Pub.BaseInfo.sProgramVer + ';' +       // Program Version
                    SfGlobal.Pub.BaseInfo.sTestResult + ';' +       // Test Result
                    SfGlobal.Pub.BaseInfo.sRetest + ';' +           // Retest
                    SfGlobal.Pub.BaseInfo.sOpId + ';' +             // Operator Identity
                    SfGlobal.Pub.BaseInfo.iTestCnt + ';' +          // Test Counter
                    SfGlobal.Pub.BaseInfo.iErrCnt + ';' +           // Error Counter
                    SfGlobal.Pub.BaseInfo.sErrCode;                 // Error Code

            /* if (SfGlobal.Pub.BaseInfo.sBatch.Length > 0) // mark 批量上拋功能
            {
                sUpdateTemp += ";" + SfGlobal.Pub.BaseInfo.sBatch;  // Batch
            } */

            if (SfGlobal.Pub.Relationship.bCommand9 == true && iCmd == 9)
            {
                string sRtnMsg = "";

                if (!CheckContainerNo(SfGlobal.Pub.BaseInfo.sCartonBoxNo, ref sRtnMsg))
                {
                    SfGlobal.Pub.sErrMsg = $"In COMMAND 8:Carton No. Error!";
                    return false;
                }

                sUpdateTemp += ';' + SfGlobal.Pub.BaseInfo.sCartonBoxNo;
            }

            for (int i = 0; i < SfGlobal.Pub.BaseInfo.iTestCnt; i++)
            {
                sUpdateTemp += ';' + SfGlobal.Pub.TestItemResult[i].sTestItemFieldName;
                sUpdateTemp += ';' + SfGlobal.Pub.TestItemResult[i].sTestLog;
            }

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            FlowMsg($"Shopflow string int32Len ={int32Len}");

            // Application->MessageBoxA(sUpdateTemp, 0);

            if (!SajetTransData(iCmd, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-{iCmd}:{sResult}"); // Debug print
                SfGlobal.Pub.sErrMsg = $"COMMAND-{iCmd}:{sUpdateTemp}";
                // mf_AddToTable(AbocomShopFlow);
                return false;
            }

            FlowMsg($"Command {iCmd} check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: Start
            *  Purpose: the function to shopflow dll initialization and startup.
            *  Returns: 1. TRUE - Shopflow dll startup successfully.
            *           2. FALSE - Shopflow dll startup failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool Start()
        {
            return SajetTransStart();
        }

        /*------------------------------------------------------------------------
            * Function: Close
            *  Purpose: the function to shopflow dll close.
            *  Returns: 1. TRUE - Shopflow dll close successfully.
            *           2. FALSE - Shopflow dll close failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool Close()
        {
            return SajetTransClose();
        }

        //---------------------------------------------------------------------------
        public bool SendTableToServer(ref string sErrMsg)
        {
            string sFilePath;
            string sFilePathToday;

            string sLogDir;

            List<string> sStrListOld = new List<string>();
            List<string> sStrListSuccess = new List<string>();


            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTable.csv";
            sFilePathToday = $"{sLogDir}\\{DateTime.Now:yyyyMMddHHmmss}-ShopFlowTable.csv";

            try
            {
                // 使用 StreamReader 打開文件
                using (StreamReader sr = new StreamReader(sFilePath))
                {
                    string sLine;

                    // 逐行讀取並顯示文件內容
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        byte[] byteUpdateTemp;
                        byte[] byteData = new byte[512];

                        Int32 int32Len;

                        byteUpdateTemp = Encoding.ASCII.GetBytes(sLine);
                        Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                        int32Len = byteUpdateTemp.Length;

                        Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
                        if (!SajetTransData(3, byteData, ref int32Len))
                        {
                            sStrListOld.Add(sLine);
                            return false;
                        }

                        sStrListSuccess.Add(sLine);
                    }
                }
            }
            catch (Exception e)
            {
                sErrMsg = $"Read file error：{e.Message}";
            }

            // Save sFilePathToday file
            if (File.Exists(sFilePathToday))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePathToday))
                    {
                        foreach (string sStr in sStrListSuccess)
                        {
                            swWriter.WriteLine($"{sStr}");
                        }
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    File.WriteAllLines(sFilePathToday, sStrListSuccess);
                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            if (!sStrListOld.Any())
            {
                File.Delete(sFilePath);
            }
            else
            {
                try
                {
                    // 寫入 List<string> 中的每行內容至檔案中（覆蓋原有內容）
                    File.WriteAllLines(sFilePath, sStrListOld);
                    Console.WriteLine($"File overwriting Successfully :{sFilePath}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File overwriting error :{ex.Message}.");
                    sErrMsg = $"File overwriting error :{ex.Message}.";
                }
                return false;
            }

            return true;
        }

        //---------------------------------------------------------------------------
        public bool SendMacTableToServer(ref string sErrMsg)
        {
            string sFilePath;
            string sFilePathToday;

            string sLogDir;

            List<string> sStrListOld = new List<string>();
            List<string> sStrListSuccess = new List<string>();


            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTableMac.csv";
            sFilePathToday = $"{sLogDir}\\{DateTime.Now:yyyyMMddHHmmss}-ShopFlowTableMac.csv";

            try
            {
                // 使用 StreamReader 打開文件
                using (StreamReader sr = new StreamReader(sFilePath))
                {
                    string sLine;

                    // 逐行讀取並顯示文件內容
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        byte[] byteUpdateTemp;
                        byte[] byteData = new byte[512];

                        Int32 int32Len;

                        byteUpdateTemp = Encoding.ASCII.GetBytes(sLine);
                        Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                        int32Len = byteUpdateTemp.Length;

                        Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
                        if (!SajetTransData(4, byteData, ref int32Len))
                        {
                            sStrListOld.Add(sLine);
                            return false;
                        }

                        sStrListSuccess.Add(sLine);
                    }
                }
            }
            catch (Exception e)
            {
                sErrMsg = $"Read file error：{e.Message}";
            }

            // Save sFilePathToday file
            if (File.Exists(sFilePathToday))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePathToday))
                    {
                        foreach (string sStr in sStrListSuccess)
                        {
                            swWriter.WriteLine($"{sStr}");
                        }
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    File.WriteAllLines(sFilePathToday, sStrListSuccess);
                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            if (!sStrListOld.Any())
            {
                File.Delete(sFilePath);
            }
            else
            {
                try
                {
                    // 寫入 List<string> 中的每行內容至檔案中（覆蓋原有內容）
                    File.WriteAllLines(sFilePath, sStrListOld);
                    Console.WriteLine($"File overwriting Successfully :{sFilePath}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File overwriting error :{ex.Message}.");
                    sErrMsg = $"File overwriting error :{ex.Message}.";
                }
                return false;
            }

            return true;
        }

        //---------------------------------------------------------------------------
        public bool SendCsnTableToServer(ref string sErrMsg)
        {
            string sFilePath;
            string sFilePathToday;

            string sLogDir;

            List<string> sStrListOld = new List<string>();
            List<string> sStrListSuccess = new List<string>();


            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTableCsn.csv";
            sFilePathToday = $"{sLogDir}\\{DateTime.Now:yyyyMMddHHmmss}-ShopFlowTableCsn.csv";

            try
            {
                // 使用 StreamReader 打開文件
                using (StreamReader sr = new StreamReader(sFilePath))
                {
                    string sLine;

                    // 逐行讀取並顯示文件內容
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        byte[] byteUpdateTemp;
                        byte[] byteData = new byte[512];

                        Int32 int32Len;

                        byteUpdateTemp = Encoding.ASCII.GetBytes(sLine);
                        Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
                        int32Len = byteUpdateTemp.Length;

                        Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
                        if (!SajetTransData(14, byteData, ref int32Len))
                        {
                            sStrListOld.Add(sLine);
                            return false;
                        }

                        sStrListSuccess.Add(sLine);
                    }
                }
            }
            catch (Exception e)
            {
                sErrMsg = $"Read file error：{e.Message}";
            }

            // Save sFilePathToday file
            if (File.Exists(sFilePathToday))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePathToday))
                    {
                        foreach (string sStr in sStrListSuccess)
                        {
                            swWriter.WriteLine($"{sStr}");
                        }
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    File.WriteAllLines(sFilePathToday, sStrListSuccess);
                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            if (!sStrListOld.Any())
            {
                File.Delete(sFilePath);
            }
            else
            {
                try
                {
                    // 寫入 List<string> 中的每行內容至檔案中（覆蓋原有內容）
                    File.WriteAllLines(sFilePath, sStrListOld);
                    Console.WriteLine($"File overwriting Successfully :{sFilePath}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File overwriting error :{ex.Message}.");
                    sErrMsg = $"File overwriting error :{ex.Message}.";
                }
                return false;
            }

            return true;
        }

        //---------------------------------------------------------------------------
        bool AddToTable(ref string sErrMsg)
        {
            string sFilePath;
            string sLogDir;
            string sUpdateTemp;

            sUpdateTemp =
                    SfGlobal.Pub.BaseInfo.sSerialNumber + ';' +         // ISNO
                    SfGlobal.Pub.BaseInfo.sMachineName + ';' +          // MACHINE NAME
                    SfGlobal.Pub.BaseInfo.sProgramName + ';' +          // PROGRAM NAME
                    SfGlobal.Pub.BaseInfo.sProgramVer + ';' +           // PROGRAM VERSION
                    SfGlobal.Pub.BaseInfo.sTestResult + ';' +           // TEST RESULT
                    SfGlobal.Pub.BaseInfo.sRetest + ';' +               // RETEST
                    SfGlobal.Pub.BaseInfo.sOpId + ';' +                 // OPID
                    SfGlobal.Pub.BaseInfo.iTestCnt + ';' +              // Test counter
                    SfGlobal.Pub.BaseInfo.iErrCnt + ';' +               // Error counter
                    SfGlobal.Pub.BaseInfo.sErrCode;                     // ErrorCode

            if (SfGlobal.Pub.Relationship.bCommand9 == true)
            {
                sUpdateTemp += ';' + SfGlobal.Pub.BaseInfo.sCartonBoxNo;
            }

            for (int i = 0; i < SfGlobal.Pub.BaseInfo.iTestCnt; i++)
            {
                sUpdateTemp += ';' + SfGlobal.Pub.TestItemResult[i].sTestItemFieldName;
                sUpdateTemp += ';' + SfGlobal.Pub.TestItemResult[i].sTestLog;
            }

            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTable.csv";
            if (File.Exists(sFilePath))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePath))
                    {
                        swWriter.WriteLine($"{sUpdateTemp}");
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    using (StreamWriter swWriter = new StreamWriter(sFilePath))
                    {
                        swWriter.WriteLine($"{sUpdateTemp}");
                    }

                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            sErrMsg = "Write data to ShopflowTable file!!\nBecause can not connect to Shopflow server!!";
            return true;
        }

        //---------------------------------------------------------------------------
        bool AddToTableForMac(string sSendString, ref string sErrMsg)
        {
            string sFilePath;
            string sLogDir;

            if (String.IsNullOrEmpty(sSendString))
                return false;

            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTableMac.csv";

            if (File.Exists(sFilePath))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePath))
                    {
                        swWriter.WriteLine($"{sSendString}");
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    using (StreamWriter swWriter = new StreamWriter(sFilePath))
                    {
                        swWriter.WriteLine($"{sSendString}");
                    }

                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            sErrMsg = "Write data to ShopFlowTableMac file!!\nBecause can not connect to Shopflow server!!";
            return true;
        }

        /*------------------------------------------------------------------------
            * Function: AddToTableForCsn
            *  Purpose: the function to the binding command between the serial number and 
            *           the client serial number are stored in the file.
            *  Returns: 1. TRUE - Write command save to file successfully.
            *           2. FALSE - Write command save to file failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        bool AddToTableForCsn(string sSendString, ref string sErrMsg)
        {
            string sFilePath;
            string sLogDir;

            if (String.IsNullOrEmpty(sSendString))
                return false;

            // Create log file folder
            sLogDir = $"{Directory.GetCurrentDirectory()}\\log";

            // 確認目錄存在，如果不存在則創建目錄
            if (!Directory.Exists(sLogDir))
            {
                // 創建多層次文件夾結構
                Directory.CreateDirectory(sLogDir);
                Console.WriteLine($"Created directory: {sLogDir}");
                if (!Directory.Exists(sLogDir))
                {
                    sErrMsg = "Create log directory fail.";
                    return false;
                }
            }

            // Open file
            sFilePath = $"{sLogDir}\\ShopFlowTableCsn.csv";

            if (File.Exists(sFilePath))
            {
                try
                {
                    using (StreamWriter swWriter = File.AppendText(sFilePath))
                    {
                        swWriter.WriteLine($"{sSendString}");
                    }

                    Console.WriteLine("Log entry has been appended to the file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    using (StreamWriter swWriter = new StreamWriter(sFilePath))
                    {
                        swWriter.WriteLine($"{sSendString}");
                    }

                    Console.WriteLine("Log entry has been written to the new file.");
                }
                catch (Exception ex)
                {
                    sErrMsg = $"An error occurred: {ex.Message}";
                    return false;
                }
            }

            sErrMsg = "Write data to ShopFlowTableCsn file!!\nBecause can not connect to Shopflow server!!";
            return true;
        }

        /*------------------------------------------------------------------------
            * Function: CheckWonoOpidSn
            *  Purpose: the function to check whether the work order, 
            *           operator identity and serial number are correct in the system.
            *  Returns: 1. TRUE - Shopflow dll close successfully.
            *           2. FALSE - Shopflow dll close failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool CheckWonoOpidSn(
            string sWoNo,       // [Input] Work order string 
            string sOpId,       // [Input] Operator identity string 
            string sSn,         // [Input] Serial number string
            ref string sRtnMsg  // [Output] Return message
            )
        {
            string sUpdateTemp;
            string sWoNoUpTemp;
            string sOpIdUpTemp;
            string sSnUpTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sWoNo))
                return false;
            if (String.IsNullOrEmpty(sOpId))
                return false;
            if (String.IsNullOrEmpty(sSn))
                return false;

            sWoNoUpTemp = $"{sWoNo.ToUpper()}";
            sOpIdUpTemp = $"{sOpId.ToUpper()}";
            sSnUpTemp = $"{sSn.ToUpper()}";

            sUpdateTemp = $"{sSnUpTemp};{sWoNoUpTemp}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(1, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-1:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-1:{sResult}";
                return false;
            }
            Console.WriteLine("Command 1 check OK.");


            byteUpdateTemp = Encoding.ASCII.GetBytes(sOpIdUpTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(2, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-2:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-2:{sResult}";
                return false;
            }
            Console.WriteLine("Command 2 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: CheckContainerNo
            *  Purpose: the function to check container number is available 
            *           in the shopflow.
            *  Returns: 1. TRUE - Check container number successfully.
            *           2. FALSE - Check container number failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool CheckContainerNo(
            string sContainerNo, // [Input] Container number string
            ref string sRtnMsg   // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sContainerNo))
                return false;

            // STEP 1: COMMAND_8 (Check Container No)
            sUpdateTemp = $"{sContainerNo}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(8, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-8:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-8:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 8 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: EndContainerNo
            *  Purpose: the function to end the container number in the shopflow.
            *  Returns: 1. TRUE - End the container number successfully.
            *           2. FALSE - End the container number failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool EndContainerNo(
            string sContainerNo,    // [Input] Container number string
            string sOpId,           // [Input] Operator identity string 
            ref string sRtnMsg      // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sContainerNo))
                return false;

            if (String.IsNullOrEmpty(sOpId))
                return false;

            // STEP 1: COMMAND_10 (End BOX)
            sUpdateTemp = $"{sContainerNo};{sOpId}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
            if (!SajetTransData(10, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-10:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-10:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 10 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: GetSnRelatedMacCsn
            *  Purpose: the function to search sn or mac or csn based on the input String.
            *  Returns: 1. TRUE - search successfully.
            *           2. FALSE - search failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool GetSnRelatedMacCsn(
            string sMacCsn,     // [Input] Search string (serial number or mac address or client serial number)
            string sRtnType,    // [Input] Return string type (serial number or mac address or client serial number)
            ref string sRtnMsg  // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sMacCsn))
                return false;

            // STEP 1: COMMAND_17 (Get Serial Number)
            sUpdateTemp = $"{sMacCsn};;;;{sRtnType}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
            if (!SajetTransData(17, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-17:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-17:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 17 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: GetChildPartsSn
            *  Purpose: the function to search sn based on input string and 
            *           child parts number.
            *  Returns: 1. TRUE - search successfully.
            *           2. FALSE - search failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool GetChildPartsSn(
            string sSnMacCsn,       // [Input] Search string (serial number or mac address or client serial number)
            string sChildPartsNo,   // [Input] Child parts number string 
            string sIndex,          // [Input] Record index string
            ref string sRtnMsg      // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sSnMacCsn))
                return false;

            // STEP 1: COMMAND_18 (Get Child Parts Number Sn)
            sUpdateTemp = $"{sSnMacCsn};{sChildPartsNo};{sIndex}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
            if (!SajetTransData(18, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-18:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-18:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 18 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: GetItemValue
            *  Purpose: the function to search past test byteData using serial number.
            *  Returns: 1. TRUE - search successfully.
            *           2. FALSE - search failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool GetItemValue(
            string sSn,             // [Input] Serial number string
            string sItemName,       // [Input] Item name string 
            string sProcessName,    // [Input] Station name string
            ref string sRtnMsg      // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sSn))
                return false;

            // STEP 1: COMMAND_19 (Get Item Value)
            sUpdateTemp = $"{sSn};{sItemName};{sProcessName}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");       // Debug print
            if (!SajetTransData(19, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-19:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-19:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 19 check OK.");

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: GetPartNo
            *  Purpose: the function to search for part number based on the serial number.
            *  Returns: 1. TRUE - search successfully.
            *           2. FALSE - search failed.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool GetPartNo(
            string sSn,         // [Input] Search string (serial number or work order)
            ref string sRtnMsg  // [Output] Return message
            )
        {
            string sUpdateTemp;
            byte[] byteUpdateTemp;
            byte[] byteData = new byte[512];
            Int32 int32Len;
            string sResult;

            if (String.IsNullOrEmpty(sSn))
                return false;

            // STEP 1: COMMAND_27 (Get Part Number)
            sUpdateTemp = $"{sSn}";

            byteUpdateTemp = Encoding.ASCII.GetBytes(sUpdateTemp);
            Array.Copy(byteUpdateTemp, byteData, byteUpdateTemp.Length);
            int32Len = byteUpdateTemp.Length;

            Console.WriteLine("\n--" + Encoding.ASCII.GetString(byteUpdateTemp) + "--");    // Debug print
            if (!SajetTransData(27, byteData, ref int32Len))
            {
                sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
                Console.WriteLine($"COMMAND-27:{sResult}"); // Debug print
                sRtnMsg = $"COMMAND-27:{sResult}";
                return false;
            }

            sResult = Encoding.ASCII.GetString(byteData, 0, int32Len);
            Console.WriteLine("SajetTransData()=" + int32Len.ToString() + "\n--" + sResult + "--");    // Debug print

            string[] cutting = sResult.Split(';');
            sRtnMsg = cutting[0];
            Console.WriteLine($"sRtnMsg={sRtnMsg}");
            Console.WriteLine("Command 27 check OK.");

            return true;
        }
    }
}
