using BarCode_FormsApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ClassStruct;
using static OptStruct;
using static WindowsFormsApp1.Form_option;
using WindowsFormsApp1;

namespace MainForm_App 
{
    public partial class Form1 : Form
    {
        #region declare form objects
        public Form1  MainForm;
        public Form_option OptionForm; // 宣告 Form_option 類型的字段 OptionForm
        public BarcodeScan BarCodeForm = new BarcodeScan();// 初始化 BarcodeScan 類別
        public Shopflow Shopflow;
        public CmdExe cmdExe;
        public OptPriv_T OptPriv;
        public Priv_T Priv;


        #endregion

        #region 宣告 message font size, font, color
        public static float fMsgFontSize = 9F;
        public static string sMsgFont = "Verdana";
        public static Color clrMsgColor = Color.Silver;

        public static float fTitleFontSize = 12F;
        public static string sTitleFont = "Arial";
        public static Color clrTitleColor = Color.Aqua;

        public static Color clrCurrMsgColor = Color.White;
        #endregion

        #region 宣告 The start and length of the old message
        public static int iCmdMsgSelBgn = 0;
        public static int iCmdMsgSelLen = 0;
        #endregion
        
        #region 宣告 The start and length of the old message
        public static int iFlowMsgSelBgn = 0;
        public static int iFlowMsgSelLen = 0;
        #endregion

        #region txt_result.Text test msg function
        public void MF_Msg(string s)
        {
            // 將消息顯示在 txt_result
            txt_result.Text += $"{s}{Environment.NewLine}";
            txt_result.ScrollBars = ScrollBars.Vertical;
            txt_result.SelectionStart = txt_result.Text.Length;
            txt_result.ScrollToCaret();
        }
        #endregion

        #region 使用 Windows API 獲取當前應用程序的主窗口
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        #endregion

        #region 載入shopflow *.dll
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransStart();
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransClose();
        [DllImport("SajetConnect.dll")]
        public static extern bool SajetTransData(int f_iCommandNo, char[] f_pData, ref int f_pLen);
        #endregion

        #region  start test
        public Form1()
        {
            
            InitializeComponent();//初始化
            BarcodeScan BarCodeForm = new BarcodeScan();
            LoadForm();//載入之前視窗資訊
            OptionForm = new Form_option(this,BarCodeForm,Shopflow);//創建一個新的 Form_option 類別實例，並將 BarCodeForm /Shopflow參數傳遞給它的構造函數
            Shopflow = new Shopflow();
            Priv = new Priv_T(BarCodeForm);
            OptPriv = new OptPriv_T(BarCodeForm);
            Button_Stop.Visible = false;
            OptPriv.bStopFlag = false;
            Button_Start.Visible = true;
        }
#endregion

        #region  AbocomShopFlowFunction_Initial check
        private bool AbocomShopFlowFunction_InitialCheck()
        {
            char[] UpdateTempString = new char[100];
            int DataLength;
            int fLen;

            // 清空 UpdateTempString

            UpdateTempString = Enumerable.Repeat('\0', 100).ToArray();

            // 設定 UpdateTempString
            string testString = "TEST";
            char[] testCharArray = testString.ToCharArray();
            Array.Copy(testCharArray, UpdateTempString, Math.Min(testCharArray.Length, UpdateTempString.Length));

            DataLength = UpdateTempString.Length;
            fLen = DataLength;

            if (!SajetTransData(7, UpdateTempString, ref fLen))
            {

                MF_Msg($"COMMAND-7:{new string(UpdateTempString)}");
                return false;
            }
            return true;
        }
        #endregion

        #region toolMenu
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OptionForm.ShowDialog(this);


        }
    
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OptionForm.ShowDialog(this);

        }
        #endregion

        #region Start Scan Bar Code and Select Scan Mode 
        private async void button1_Click(object sender, EventArgs e)
        {
            
            bool bResult;
            long lTestTimeTic;
            string sNowTime;
            string MODEL_FILENAME = "model.ini";
            Stopwatch stopWatch;
            SetupIniIP ini = new SetupIniIP();
           
            Button_Start.Visible = false;
            Button_Stop.Visible = true;
            textBox_SN.Clear();
            textBox_MAC.Clear();
            textBox_Csn.Clear();
        TestStart:
            OptionForm.ModelRead(MODEL_FILENAME);
            OptionForm.Apply();
            // Form_option.SelectedOption?.Function.Invoke();
         
            BarCodeForm.BARCODE_Scan((int)SCAN_MODE.SNANDMAC);//BarCodeForm  Show Slect Scan Mode 
            
            textBox_Fwono.Text = OptStruct.Priv.opt_Ate.WoNoDef;
            textBox_Fopid.Text = OptStruct.Priv.opt_Ate.OpIdDef;
            textBox_SN.Text += BarCodeForm.BARCODE_GetSn();
            textBox_MAC.Text += BarCodeForm.BARCODE_GetMac();
            textBox_Csn.Text += BarCodeForm.BARCODE_GetCSn();


            stopWatch = Stopwatch.StartNew();   // 開始記錄執行時間
          
            sNowTime = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}";
            OptPriv.sStartTime = sNowTime;
            MF_Msg($"Start test at {sNowTime}");


            OptPriv.bStopFlag = false;
            Button_Stop.Visible = true;

            if (IsStopTest())
            {
                MF_Msg("\r\n!!!!! Abort test !!!!!");
                goto TestEnd;
            }
            if (GetShopFlowEnable())
            {

                if (AbocomShopFlowFunction_InitialCheck())
                {
                    MF_Msg("Found Shopflow server");

                }
                else
                {
                    MF_Msg("Can't Found Shopflow server");
                }
            }

            if (OptionForm != null)
            {
                try
                {
                    // 確保等待 RunSelectedTests 非同步方法執行完畢
                    await OptionForm.RunSelectedTests();
                }
                catch (Exception ex)
                {
                    // 處理可能發生的錯誤
                    MessageBox.Show($"An error occurred while running tests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 顯示錯誤提示 OptionForm 尚未初始化
                MessageBox.Show("No tests are selected or OptionForm is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            /*if (OptionForm != null)
            {
               await OptionForm.RunSelectedTests();
                 //OptionForm.RunSelectedTests();
            }
            else
            {
                MessageBox.Show("No tests are selected or OptionForm is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            //MessageBox.Show("test result is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (IsStopTest())
            {
                MF_Msg("\r\n!!!!! Abort test !!!!!");
                bResult = false;
            }
            else
            {
                // test time
                lTestTimeTic = stopWatch.ElapsedMilliseconds / 1000;
                OptPriv.sTestTime = $"{(lTestTimeTic / 60):D2}:{(lTestTimeTic % 60):D2}";

                MF_Msg("Test finished.");
                MF_Msg($"Test time : {OptPriv.sTestTime}");
            }
        TestEnd:
            Button_Start.Visible = true;
            Button_Start.Focus();
            Button_Stop.Visible = false;
            if ((OptStruct.Priv.opt_Ate.bAutoShowScanBox) && !IsStopTest())
            { 
                BarCodeForm.ShowDialog(this);
             }
        }
        #endregion

        #region the function to check if test terminated.
        /*------------------------------------------------------------------------
         * Function: IsStopTest
         *  Purpose: the function to check if test terminated.
         *  Returns: variable MfGlobal.Pub.bStopFlag.
         *     Note: Needs to modify.
        *------------------------------------------------------------------------
         */
        public bool IsStopTest()
        {
            return OptPriv.bStopFlag;
        }
        #endregion

        #region Get option shopflow enable
        private bool GetShopFlowEnable()
        {
            return OptStruct.Priv.Shopflow.ShopFlowEnable;

        }
        #endregion

        #region form show
        private void Form1_Shown(object sender, EventArgs e)
        {
           /*int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = (Point)new Size(x, y);*/
            textBox_SN.Clear();
            textBox_MAC.Clear();
            textBox_Csn.Clear();
        }
        #endregion
        
        #region form load 
        private void Form1_Load(object sender, EventArgs e)
        {
            string MODEL_FILENAME = "model.ini";
            SetupIniIP ini = new SetupIniIP();
            OptionForm.ModelRead(MODEL_FILENAME);
            OptionForm.Apply();
            if (OptStruct.Priv.Shopflow.ShopFlowEnable)
            {
                if (!SajetTransStart())
                {
                    //  Console.WriteLine("\nError: SajetTransStart()");
                    MessageBox.Show("Error: ShopFloor Connect Error");
                    return;
                }
            }
            
        }
        #endregion
        
        #region  保存Form 大小與座標
        private void SaveForm()
        {
            // 紀錄座標
            WindowsFormsApp1.Properties.Settings.Default.FormLocationX = this.Location.X;
            WindowsFormsApp1.Properties.Settings.Default.FormLocationY = this.Location.Y;

            // 紀錄 大小
            WindowsFormsApp1.Properties.Settings.Default.FormSizeW = this.Size.Width;
            WindowsFormsApp1.Properties.Settings.Default.FormSizeH = this.Size.Height;

            WindowsFormsApp1.Properties.Settings.Default.Save();
        }
        #endregion
        
        #region Load Main Form 
        private void LoadForm()
        {
            int x = WindowsFormsApp1.Properties.Settings.Default.FormLocationX;
            int y = WindowsFormsApp1.Properties.Settings.Default.FormLocationY;

            int w = WindowsFormsApp1.Properties.Settings.Default.FormSizeW;
            int h = WindowsFormsApp1.Properties.Settings.Default.FormSizeH;

            // 檢查是否有有效的座標
            if (x >= 0 && y >= 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Size = new Size(w, h);
                this.Location = new System.Drawing.Point(x, y);
            }
        }
        #endregion
        
        #region Form Close 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OptStruct.Priv.Shopflow.ShopFlowEnable)
            { 
                SajetTransClose(); 
            }
            SaveForm();
        }
        #endregion

        #region Stop Btn Click 
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            Button_Stop.Visible = false;
            OptPriv.bStopFlag = false;
            Button_Start.Visible = true;
            MF_Msg("\r\n!!!!! Abort test !!!!!");
        }
        #endregion

        #region Add Test Message
        /*------------------------------------------------------------------------
          * Function: AddMsg
          *  Purpose: the function to add a string to the RichTextBox_Info 
          *           using input message font settings.
          *  Returns: None.
          *     Note: Do not need to modify.
          *           You can set whether the message requires the keep style.
          *           You can set whether the message needs to be added on a new line.
          *------------------------------------------------------------------------
          */
        private void AddMsg(
            string sMsg,           // [Input] Shown message string
            string sFontName,      // [Input] Shown message string font fame settings
            Color clrFontColor,     // [Input] Shown message string font color settings
            FontStyle fFontStyle,   // [Input] Shown message string font style settings
            float fFontSize,        // [Input] Shown message string font size settings
            bool bKeepStyle,        // [Input] The messages don't need to be keep style
            bool bNewLine           // [Input] The message is added on a new line
            )
        {
            Font fDefFontStyle;
            RichTextBox pRichTextBox;

            fDefFontStyle = // 設置預設文字為字型、字型大小與粗體字
                new Font(sMsgFont, fMsgFontSize, FontStyle.Bold);
            pRichTextBox = RichTextBox_FlowMsg;

            Console.WriteLine($"newMsg:{sMsg}");

            // 更新 RichTextBox 的文字顯示
            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    pRichTextBox.Refresh();

                    if (pRichTextBox.Text.Length == 0)
                    {
                        iFlowMsgSelBgn = 0;
                        iFlowMsgSelLen = 0;
                    }
                    Console.WriteLine($"非同步");
                    Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                    Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");

                    // 捲動到新增的最後一行
                    pRichTextBox.ScrollToCaret();
                }));
            }
            else
            {
                pRichTextBox.Refresh();

                // 如果已經在 UI 線程上，則直接更新 UI 元素
                if (pRichTextBox.Text.Length == 0)
                {
                    iFlowMsgSelBgn = 0;
                    iFlowMsgSelLen = 0;
                }
                Console.WriteLine($"同步");
                Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");

                // 捲動到新增的最後一行
                pRichTextBox.ScrollToCaret();
            }

            if (iFlowMsgSelLen > 0)
            {
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        pRichTextBox.Select(iFlowMsgSelBgn, iFlowMsgSelLen);
                        string sLastLine = pRichTextBox.SelectedText;
                        pRichTextBox.SelectionColor = clrMsgColor;  // 設置文字顏色
                        pRichTextBox.SelectionFont = fDefFontStyle; // 設置文字為字型、字型大小與粗體字
                        pRichTextBox.Select(0, 0);
                        Console.WriteLine($"非同步");
                        Console.WriteLine($"sLastLine:{sLastLine}");
                        Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                        Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    pRichTextBox.Select(iFlowMsgSelBgn, iFlowMsgSelLen);
                    string sLastLine = pRichTextBox.SelectedText;
                    pRichTextBox.SelectionColor = clrMsgColor;  // 設置文字顏色
                    pRichTextBox.SelectionFont = fDefFontStyle; // 設置文字為字型、字型大小與粗體字
                    pRichTextBox.Select(0, 0);
                    Console.WriteLine($"同步");
                    Console.WriteLine($"sLastLine:{sLastLine}");
                    Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                    Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                }
            }

            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    iFlowMsgSelBgn = pRichTextBox.Text.Length;
                    Console.WriteLine($"非同步");
                    Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                    Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                iFlowMsgSelBgn = pRichTextBox.Text.Length;
                Console.WriteLine($"同步");
                Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
            }

            if (bNewLine)
            {
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        pRichTextBox.AppendText($"{sMsg}\n");
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    pRichTextBox.AppendText($"{sMsg}\n");
                }
            }
            else
            {
                int iLastIndex;
                int iStart;
                int iLength;
                string sLastLine;

                iFlowMsgSelBgn -= 1;   // include "\n"
                Console.WriteLine($"flowMsg");
                Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        iLastIndex = pRichTextBox.Lines.Length - 1;
                        iStart = pRichTextBox.GetFirstCharIndexFromLine(iLastIndex - 1); // 倒數第二行的起始字符索引
                        iLength = pRichTextBox.Lines[iLastIndex - 1].Length;             // 倒數第二行的字符長度
                        pRichTextBox.Select(iStart, iLength);
                        sLastLine = pRichTextBox.SelectedText;
                        pRichTextBox.SelectedText = sLastLine + sMsg;
                        pRichTextBox.Select(0, 0);
                        Console.WriteLine($"非同步");
                        Console.WriteLine($"sLastLine:{sLastLine}");
                        Console.WriteLine($"sLastLine + sMsg:{sLastLine + sMsg}");
                        Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                        Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    iLastIndex = pRichTextBox.Lines.Length - 1;
                    iStart = pRichTextBox.GetFirstCharIndexFromLine(iLastIndex - 1); // 倒數第二行的起始字符索引
                    iLength = pRichTextBox.Lines[iLastIndex - 1].Length;             // 倒數第二行的字符長度
                    pRichTextBox.Select(iStart, iLength);
                    sLastLine = pRichTextBox.SelectedText;
                    pRichTextBox.SelectedText = sLastLine + sMsg;
                    pRichTextBox.Select(0, 0);
                    Console.WriteLine($"同步");
                    Console.WriteLine($"sLastLine:{sLastLine}");
                    Console.WriteLine($"sLastLine + sMsg:{sLastLine + sMsg}");
                    Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                    Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
                }
            }

            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    iFlowMsgSelLen = pRichTextBox.Text.Length - iFlowMsgSelBgn;

                    pRichTextBox.Select(iFlowMsgSelBgn, iFlowMsgSelLen);
                    pRichTextBox.SelectionColor = clrFontColor;   // 設置文字顏色
                    pRichTextBox.SelectionFont =                // 設置文字為字型、字型大小與粗體字
                        new Font(sFontName, fFontSize, fFontStyle);
                    Console.WriteLine($"非同步");
                    Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                    Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");

                    pRichTextBox.Select(iFlowMsgSelBgn + iFlowMsgSelLen, 0);
                    // 捲動到新增的最後一行
                    pRichTextBox.ScrollToCaret();
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                iFlowMsgSelLen = pRichTextBox.Text.Length - iFlowMsgSelBgn;

                pRichTextBox.Select(iFlowMsgSelBgn, iFlowMsgSelLen);
                pRichTextBox.SelectionColor = clrFontColor;   // 設置文字顏色
                pRichTextBox.SelectionFont =                // 設置文字為字型、字型大小與粗體字
                    new Font(sFontName, fFontSize, fFontStyle);
                Console.WriteLine($"同步");
                Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
                Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");

                pRichTextBox.Select(iFlowMsgSelBgn + iFlowMsgSelLen + 1, 0);
                // 捲動到新增的最後一行
                pRichTextBox.ScrollToCaret();
            }

            if (bKeepStyle)
            {
                iFlowMsgSelBgn = 0;
                iFlowMsgSelLen = 0;
            }
            Console.WriteLine($"endMsg");
            Console.WriteLine($"iFlowMsgSelBgn:{iFlowMsgSelBgn}");
            Console.WriteLine($"iFlowMsgSelLen:{iFlowMsgSelLen}");
        }
        #endregion

        #region Add Test Message FlowMsg new line
        /*------------------------------------------------------------------------
        * Function: MsgFontSize
        *  Purpose: the function to add a new line of string to the RichTextBox_Info 
        *           using normal message font settings.
        *  Returns: None.
        *     Note: Do not need to modify.
        *------------------------------------------------------------------------
        */
        public void FlowMsg(
            string sMsg // [Input] Shown message string
            )
        {
            AddMsg(
                sMsg,
                sMsgFont,
                clrCurrMsgColor,
                FontStyle.Bold,
                fMsgFontSize,
                false,
                true
                );
        }
        #endregion

        #region Append Test Message FlowMsgLink 
        /*------------------------------------------------------------------------
        * Function: FlowMsgLink
        *  Purpose: the function to Add a string to the last line of the message 
        *           in the RichTextBox_Info using the input message font settings.
        *  Returns: None.
        *     Note: Do not need to modify.
        *------------------------------------------------------------------------
        */
        public void FlowMsgLink(
            bool bIsOkMsg,  // [Input] Shown message string font settings (PS: true:normal font / false:fail font)
            string sMsg     // [Input] Shown message string
            )
        {
            Color clrColor;
            FontStyle fFontStyle;
            bool bKeepStyle;

            if (bIsOkMsg)
            {
                clrColor = clrCurrMsgColor;
                bKeepStyle = false;
                fFontStyle = FontStyle.Bold;
            }
            else
            {
                clrColor = Color.Red;
                bKeepStyle = true;
                fFontStyle = (FontStyle.Bold | FontStyle.Italic);
            }

            AddMsg(
                sMsg,
                sMsgFont,
                clrColor,
                fFontStyle,
                fMsgFontSize,
                bKeepStyle,
                false
                );
        }
        #endregion

        #region 刪除設定字數字串的功能
        /*------------------------------------------------------------------------
           * Function: FlowMsgDelLastLine
           *  Purpose: the function to delete the set word count string 
           *           in the last line of the RichTextBox_Info.
           *  Returns: None.
           *     Note: Do not need to modify.
           *------------------------------------------------------------------------
           */
        public void FlowMsgDel(
            int iLen    // [Input] The number of deleted words in the message
            )
        {
            int iLastIndex = 0;
            int iLastLineStart;
            int iLastLineLen;
            string sLastLine;

            if (RichTextBox_FlowMsg.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                RichTextBox_FlowMsg.Invoke((MethodInvoker)(() => {
                    iLastIndex = RichTextBox_FlowMsg.Lines.Length - 2;
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                iLastIndex = RichTextBox_FlowMsg.Lines.Length - 2;
            }
            if (iLastIndex < 0)
            {
                return;
            }

            if (RichTextBox_FlowMsg.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                RichTextBox_FlowMsg.Invoke((MethodInvoker)(() => {
                    RichTextBox_FlowMsg.Refresh();

                    iLastLineStart = RichTextBox_FlowMsg.GetFirstCharIndexFromLine(iLastIndex); // 倒數第二行的起始字符索引
                    iLastLineLen = RichTextBox_FlowMsg.Lines[iLastIndex].Length;                // 倒數第二行的字符長度

                    if (iLastLineLen < iLen)
                    {
                        RichTextBox_FlowMsg.Select(iLastLineStart - 1, iLastLineLen + 2);
                        RichTextBox_FlowMsg.SelectedText = "\n";
                    }
                    else
                    {
                        RichTextBox_FlowMsg.Select(iLastLineStart, iLastLineLen);
                        sLastLine = RichTextBox_FlowMsg.SelectedText;
                        RichTextBox_FlowMsg.SelectedText = sLastLine.Substring(0, iLastLineLen - iLen);
                    }

                    // 捲動到新增的最後一行
                    RichTextBox_FlowMsg.ScrollToCaret();
                    RichTextBox_FlowMsg.Refresh();
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                RichTextBox_FlowMsg.Refresh();

                iLastLineStart = RichTextBox_FlowMsg.GetFirstCharIndexFromLine(iLastIndex); // 倒數第二行的起始字符索引
                iLastLineLen = RichTextBox_FlowMsg.Lines[iLastIndex].Length;                // 倒數第二行的字符長度

                if (iLastLineLen < iLen)
                {
                    RichTextBox_FlowMsg.Select(iLastLineStart - 1, iLastLineLen + 2);
                    RichTextBox_FlowMsg.SelectedText = "\n";
                }
                else
                {
                    RichTextBox_FlowMsg.Select(iLastLineStart, iLastLineLen);
                    sLastLine = RichTextBox_FlowMsg.SelectedText;
                    RichTextBox_FlowMsg.SelectedText = sLastLine.Substring(0, iLastLineLen - iLen);
                }

                // 捲動到新增的最後一行
                RichTextBox_FlowMsg.ScrollToCaret();
                RichTextBox_FlowMsg.Refresh();
            }
        }
        #endregion

        #region add CMD test message       
        public void FlowCmdMsg(string sMsg)
        {
            AddCmdMsg(
                sMsg,
                sMsgFont,
                clrCurrMsgColor,
                FontStyle.Bold,
                fMsgFontSize,
                false,
                true
                );
        }
        /*------------------------------------------------------------------------
                   * Function: AddCmdMsg
                   *  Purpose: the function to add a string to the RichTextBox_CmdMsg 
                   *           using input message font settings.
                   *  Returns: None.
                   *     Note: Do not need to modify.
                   *           You can set whether the message requires the keep style.
                   *           You can set whether the message needs to be added on a new line.
                   *------------------------------------------------------------------------
                   */
        private void AddCmdMsg(
            string sMsg,           // [Input] Shown message string
            string sFontName,      // [Input] Shown message string font fame settings
            Color clrFontColor,     // [Input] Shown message string font color settings
            FontStyle fFontStyle,   // [Input] Shown message string font style settings
            float fFontSize,        // [Input] Shown message string font size settings
            bool bKeepStyle,        // [Input] The messages don't need to be keep style
            bool bNewLine           // [Input] The message is added on a new line
            )
        {
            Font fDefFontStyle;
            RichTextBox pRichTextBox;

            fDefFontStyle = // 設置預設文字為字型、字型大小與粗體字
                new Font(sMsgFont, fMsgFontSize, FontStyle.Bold);
            pRichTextBox = RichTextBox_CmdMsg;

            // 更新 RichTextBox 的文字顯示
            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    if (pRichTextBox.Text.Length == 0)
                    {
                        iCmdMsgSelBgn = 0;
                        iCmdMsgSelLen = 0;
                    }

                    // 捲動到新增的最後一行
                    pRichTextBox.ScrollToCaret();
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                if (pRichTextBox.Text.Length == 0)
                {
                    iCmdMsgSelBgn = 0;
                    iCmdMsgSelLen = 0;
                }

                // 捲動到新增的最後一行
                pRichTextBox.ScrollToCaret();
            }

            if (iCmdMsgSelLen > 0)
            {
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        pRichTextBox.Select(iCmdMsgSelBgn, iCmdMsgSelLen);
                        pRichTextBox.SelectionColor = clrMsgColor;  // 設置文字顏色
                        pRichTextBox.SelectionFont = fDefFontStyle;  // 設置文字為字型、字型大小與粗體字
                        pRichTextBox.Select(0, 0);
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    pRichTextBox.Select(iCmdMsgSelBgn, iCmdMsgSelLen);
                    pRichTextBox.SelectionColor = clrMsgColor;  // 設置文字顏色
                    pRichTextBox.SelectionFont = fDefFontStyle;  // 設置文字為字型、字型大小與粗體字
                    pRichTextBox.Select(0, 0);
                }
            }

            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    iCmdMsgSelBgn = pRichTextBox.Text.Length;
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                iCmdMsgSelBgn = pRichTextBox.Text.Length;
            }

            if (bNewLine)
            {
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        pRichTextBox.AppendText($"{sMsg}\n");
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    pRichTextBox.AppendText($"{sMsg}\n");
                }
            }
            else
            {
                int iLastIndex;
                int iStart;
                int iLength;
                string sLastLine;

                iCmdMsgSelBgn -= 1;   // include "\n"
                if (pRichTextBox.InvokeRequired)
                {
                    // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                    pRichTextBox.Invoke((MethodInvoker)(() => {
                        iLastIndex = pRichTextBox.Lines.Length - 1;
                        iStart = pRichTextBox.GetFirstCharIndexFromLine(iLastIndex - 1); // 倒數第二行的起始字符索引
                        iLength = pRichTextBox.Lines[iLastIndex - 1].Length;             // 倒數第二行的字符長度
                        pRichTextBox.Select(iStart, iLength);
                        sLastLine = pRichTextBox.SelectedText;
                        pRichTextBox.SelectedText = sLastLine + sMsg;
                        pRichTextBox.Select(0, 0);
                    }));
                }
                else
                {
                    // 如果已經在 UI 線程上，則直接更新 UI 元素
                    iLastIndex = pRichTextBox.Lines.Length - 1;
                    iStart = pRichTextBox.GetFirstCharIndexFromLine(iLastIndex - 1); // 倒數第二行的起始字符索引
                    iLength = pRichTextBox.Lines[iLastIndex - 1].Length;             // 倒數第二行的字符長度
                    pRichTextBox.Select(iStart, iLength);
                    sLastLine = pRichTextBox.SelectedText;
                    pRichTextBox.SelectedText = sLastLine + sMsg;
                    pRichTextBox.Select(0, 0);
                }
            }

            if (pRichTextBox.InvokeRequired)
            {
                // 如果不在 UI 線程上，則使用 Invoke 方法來在 UI 線程上執行更新操作
                pRichTextBox.Invoke((MethodInvoker)(() => {
                    iCmdMsgSelLen = pRichTextBox.Text.Length - iCmdMsgSelBgn;

                    pRichTextBox.Select(iCmdMsgSelBgn, iCmdMsgSelLen);
                    pRichTextBox.SelectionColor = clrFontColor; // 設置文字顏色
                    pRichTextBox.SelectionFont =                // 設置文字為字型、字型大小與粗體字
                        new Font(sFontName, fFontSize, fFontStyle);

                    pRichTextBox.Select(iCmdMsgSelBgn + iCmdMsgSelBgn + 1, 0);
                    // 捲動到新增的最後一行
                    pRichTextBox.ScrollToCaret();
                }));
            }
            else
            {
                // 如果已經在 UI 線程上，則直接更新 UI 元素
                iCmdMsgSelLen = pRichTextBox.Text.Length - iCmdMsgSelBgn;

                pRichTextBox.Select(iCmdMsgSelBgn, iCmdMsgSelBgn);
                pRichTextBox.SelectionColor = clrFontColor; // 設置文字顏色
                pRichTextBox.SelectionFont =                // 設置文字為字型、字型大小與粗體字
                    new Font(sFontName, fFontSize, fFontStyle);

                pRichTextBox.Select(iCmdMsgSelBgn + iCmdMsgSelBgn + 1, 0);
                // 捲動到新增的最後一行
                pRichTextBox.ScrollToCaret();
            }

            if (bKeepStyle)
            {
                iCmdMsgSelBgn = 0;
                iCmdMsgSelLen = 0;
            }
        }
        #endregion

        #region add error test message  
        /*------------------------------------------------------------------------
            * Function: ErrMsg
            *  Purpose: the function to add a new line of string to the RichTextBox_Info 
            *           using error message font settings.
            *  Returns: None.
            *     Note: Do not need to modify.
            *------------------------------------------------------------------------
            */
        public void ErrMsg(string sMsg) // [Input] Shown message string 
        {
            AddMsg(
                sMsg,
                sMsgFont,
                Color.Red,
                FontStyle.Bold,
                fTitleFontSize,
                true,
                true
                );
        }
        #endregion

        # region 在 RichTextBox_Info 中新增空字串的函數。
        /*------------------------------------------------------------------------
            * Function: MsgNextSection
            *  Purpose: the function to add an empty string in the RichTextBox_Info.
            *  Returns: None.
            *     Note: Do not need to modify.
            *------------------------------------------------------------------------
            */
        public void MsgNextSection()
        {
            AddMsg(
                null,
                sMsgFont,
                Color.Blue,
                FontStyle.Regular,
                6,
                true,
                true
                );
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}

       






