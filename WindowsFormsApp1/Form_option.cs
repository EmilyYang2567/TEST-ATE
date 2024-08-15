using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarCode_FormsApp;
using Option_FormsApp;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using MainForm_App;


namespace WindowsFormsApp1
{

    public partial class Form_option : Form
    {
        #region declare form objects
        public Form1 MainForm;
        public BarcodeScan BarCodeForm;
        public static Form_option SelectedOption;
        public static string MODEL_FILENAME = "model.ini";
        private static bool itemsInitialized = false; // Static flag to prevent multiple initializations
        private SetupIniIP ini;
        private int lockedIndex = 0; // 例如，锁定第1项
        public static List<Form_option> TestOptions = new List<Form_option>();
        private List<TestItem> testItems = new List<TestItem>();      
        private CmdExe cmdExe; // Add this
        public delegate void TestFunction();
        public event EventHandler ModelSaved;
        private readonly object initializationLock = new object();
        public string TestName { get; set; }
        public TestFunction Function { get; set; }
        #endregion
        public Form_option()
        {
            InitializeComponent();
            this.FormClosing += Form_option_FormClosing;

        }
        public Form_option(Form1 MainFormForm, BarcodeScan BarCodeForm, Shopflow Shopflow)
        {
            {
                InitializeComponent();
                ini = new SetupIniIP(); // Initialize the ini object
                checkedListBoxTests.ItemCheck += checkedListBoxTests_ItemCheck;
                cmdExe = new CmdExe(MainFormForm);
                this.BarCodeForm = BarCodeForm;
                
                ModelRead(MODEL_FILENAME);
                Apply();

                if (!itemsInitialized)
                {
                    InitializeTestItems();
                    itemsInitialized = true;
                }

                InitializeCustomComponents();
                // Initialize SelectedOption if not already initialized
                if (SelectedOption == null)
                {
                    SelectedOption = this;
                }
                if (SelectedOption == null)
                {
                    SelectedOption = this;
                }
            }
        }
        private void InitializeTestItems()
        {

            // Initialize test items and add them to the form

            lock (initializationLock)
            {
                if (!itemsInitialized)
                {
                    // Initialize test items and add them to the form
                    this.AddTestItem(new TestItem("Test1", TestFunction1));
                    this.AddTestItem(new TestItem("Test2", TestFunction2));
                    this.AddTestItem(new TestItem("Test3", TestFunction3));

                    itemsInitialized = true;
                }
            }

        }
        private void InitializeCustomComponents()
        {
            checkedListBoxTests.Items.Clear();

            // Add test items to the checked list box
            foreach (var item in testItems)
            {
                checkedListBoxTests.Items.Add(item.TestName, item.IsChecked);
            }
        }

       /* private void InitializeCommonComponents()
        {
            ini = new SetupIniIP();
            ModelRead(MODEL_FILENAME);
            Apply();

            if (!itemsInitialized)
            {
                InitializeTestItems();
                itemsInitialized = true;
            }

            InitializeCustomComponents();
        }*/
        public class SetupIniIP
        {
            public string path;
            public SetupIniIP() { }
            public string TestName { get; set; }
            public TestFunction Function { get; set; }

            public SetupIniIP(string testName, TestFunction function)
            {
                TestName = testName;
                Function = function;
            }
            #region 定義 ini 檔案 dll
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public void IniWriteValue(string Section, string Key, string Value, string inipath)
            {
                WritePrivateProfileString(Section, Key, Value.ToString(), Application.StartupPath + "\\" + inipath);
            }

            public string IniReadValue(string Section, string Key, string defaultValue, string inipath)
            {
                StringBuilder temp = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, defaultValue, temp, 255, Application.StartupPath + "\\" + inipath);
                return temp.ToString();
            }
            #endregion
        }
        #region ini save function
        public void ModelSave(string file)
        {
            ini.IniWriteValue("ATE", "opid", textBox_opid.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "part", textBox_part.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "MachineName", MachineName.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "wono", textBox_wono.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "ScanSN", CheckBox_ateScanSN.Checked.ToString(), MODEL_FILENAME);
            ini.IniWriteValue("ATE", "SN_Prefix", SN_Prefix.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "ScanMAC", CheckBox_ateScanMAC.Checked.ToString(), MODEL_FILENAME);
            ini.IniWriteValue("ATE", "MAC_Prefix", MAC_Prefix.Text, MODEL_FILENAME);
            ini.IniWriteValue("ATE", "ShopFlowEnable", ShopFlowEnable.Checked.ToString(), MODEL_FILENAME);

            // Save test item states from checkedListBoxTests
            for (int i = 0; i < checkedListBoxTests.Items.Count; i++)
            {
                string testName = checkedListBoxTests.Items[i].ToString();
                bool isChecked = checkedListBoxTests.GetItemChecked(i);
                ini.IniWriteValue("TEST_ITEMS", testName, isChecked.ToString(), MODEL_FILENAME);

                // Update testItems list to reflect the current state
                testItems[i].IsChecked = isChecked;
            }
            // ModelSaved?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region read ini function
        public bool ModelRead(string file)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\" + MODEL_FILENAME))
                {
                    textBox_opid.Text = ini.IniReadValue("ATE", "opid", "NULL", MODEL_FILENAME);
                    textBox_part.Text = ini.IniReadValue("ATE", "part", "NULL", MODEL_FILENAME);
                    MachineName.Text = ini.IniReadValue("ATE", "MachineName", "NULL", MODEL_FILENAME);
                    textBox_wono.Text = ini.IniReadValue("ATE", "wono", "NULL", MODEL_FILENAME);
                    SN_Prefix.Text = ini.IniReadValue("ATE", "SN_Prefix", "NULL", MODEL_FILENAME);
                    MAC_Prefix.Text = ini.IniReadValue("ATE", "MAC_Prefix", "NULL", MODEL_FILENAME);
                    CheckBox_ateScanSN.Checked = Convert.ToBoolean(ini.IniReadValue("ATE", "ScanSN", "False", MODEL_FILENAME));
                    CheckBox_ateScanMAC.Checked = Convert.ToBoolean(ini.IniReadValue("ATE", "ScanMAC", "False", MODEL_FILENAME));
                    ShopFlowEnable.Checked = Convert.ToBoolean(ini.IniReadValue("ATE", "ShopFlowEnable", "False", MODEL_FILENAME));
                    // Read test item states
                    foreach (var testItem in testItems)
                    {
                        string state = ini.IniReadValue("TEST_ITEMS", testItem.TestName, "False", MODEL_FILENAME);
                        testItem.IsChecked = Convert.ToBoolean(state);
                    }

                    // Update the checkedListBoxTests to reflect the current state
                    InitializeCustomComponents();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        #endregion

        private async Task<bool> TestFunction1(CmdExe cmdExe)
        {
            string message = "Executing TestFunction1:\n";
            string command = "ping 8.8.8.8";
            int timeoutMilliseconds;

            if (SelectedOption == null)
            {
                MessageBox.Show("SelectedOption is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                timeoutMilliseconds = int.Parse(SelectedOption.timeoutTextBox1.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing timeout value: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var result = await cmdExe.RunCmdAsync("cmd.exe", command, timeoutMilliseconds);
            // Process result
            if (result.Success)
            {
                // Log or handle successful result
                MessageBox.Show(message, "OK TestFunction1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                // Log or handle failure
                MessageBox.Show(message, "NG TestFunction1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
           
        }



        //public async Task<bool> TestFunction2()
        private async Task<bool> TestFunction2(CmdExe cmdExe)
        {
            string message = "Executing TestFunction2:\n";
            MessageBox.Show(message, "Executing TestFunction2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        // public async Task<bool> TestFunction3()
        private async Task<bool> TestFunction3(CmdExe cmdExe)
        {
            string message = "Executing TestFunction3:\n";
            MessageBox.Show(message, "Executing TestFunction3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private void Form_option_Shown(object sender, EventArgs e)
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = (Point)new Size(x, y);
        }

        private void Form_option_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Test item parameters";
            tabPage2.Text = "ATE Set up";
            ModelRead(MODEL_FILENAME);
        }
        #region cancel btn function
        private void SpeedButton_cancel_Click(object sender, EventArgs e)
        {
            ModelRead(MODEL_FILENAME);
        }
        #endregion

        #region save ini file btn function 
        private void SpeedButton_ok_Click(object sender, EventArgs e)
        {
            ModelSave(MODEL_FILENAME);
            this.Close();
        }
        #endregion

        #region option 變數載入
        public void Apply()
        {
            OptStruct.Priv.Shopflow.ShopFlowEnable = ShopFlowEnable.Checked;
            OptStruct.Priv.Shopflow.PartNmb = textBox_part.Text;
            OptStruct.Priv.Shopflow.TestStationNo = MachineName.Text;

            OptStruct.Priv.opt_Ate.ScanSN = CheckBox_ateScanSN.Checked;
            OptStruct.Priv.opt_Ate.ScanMAC = CheckBox_ateScanMAC.Checked;

            OptStruct.Priv.opt_Ate.WoNoDef = textBox_wono.Text.Trim();
            OptStruct.Priv.opt_Ate.OpIdDef = textBox_opid.Text.Trim();
            OptStruct.Priv.opt_Ate.bAutoShowScanBox = CheckBox_AteAutoScan.Checked;

            OptStruct.Priv.bIsParamsOK = true;

            ClassStruct.bc_Priv.sSnPrefix = SN_Prefix.Text.Trim();
            ClassStruct.bc_Priv.sMAC_Prefix = MAC_Prefix.Text.Trim();

        }
        #endregion

        #region Test item class
        public class TestItem
        {
            public string TestName { get; set; }
            //public Func<Task<bool>> TestFunction { get; set; }
            public Func<CmdExe, Task<bool>> TestFunction { get; set; }
            public bool IsChecked { get; set; }
            public string Name { get; set; }
            public TestItem(string testName, Func<CmdExe, Task<bool>> testFunction)
            {
                TestName = testName;
                TestFunction = testFunction;
                IsChecked = false;
            }
        }
        #endregion

        #region Add test item

        public void AddTestItem(TestItem testItem)
        {
            testItems.Add(testItem);
        }
        #endregion
        
        /*public void RunSelectedTests(List<string> selectedTestNames)

        {
            // var selectedTestNames = checkedListBoxTests.CheckedItems.Cast<string>().ToList();
            foreach (var testItem in testItems)
            {
                if (selectedTestNames.Contains(testItem.Name))
                {
                    Console.WriteLine($"Running test: {testItem.TestName}");
                    //testItem.TestFunction(); // Call the test function delegate
                    testItem.TestFunction.Invoke();
                }
            }
        }*/

        #region option test item test btn
        private async void button_testitem_Click(object sender, EventArgs e)
        {
            var selectedTestNames = checkedListBoxTests.CheckedItems.Cast<string>().ToList();


            // Display a message box listing all selected test items
            if (selectedTestNames.Count > 0)
            {
                string message = "Selected Test Items:\n" + string.Join("\n", selectedTestNames);
                MessageBox.Show(message, "Selected Tests", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No test items selected.", "Selected Tests", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Run the selected test items
            await RunSelectedTests(); // Await the asynchronous method
            // RunSelectedTests(selectedTestNames);
            //   RunSelectedTests();
        }
        #endregion

        #region Run selected test items
        public async Task RunSelectedTests()

        {
            var selectedTestNames = checkedListBoxTests.CheckedItems.Cast<string>().ToList();


            foreach (var testItem in testItems)
            {
                if (selectedTestNames.Contains(testItem.TestName))
                {
                    Console.WriteLine($"Running test: {testItem.TestName}");
                    // Invoke the test function and pass the CmdExe instance
                    bool success = await testItem.TestFunction(cmdExe); // Ensure cmdExe is accessible

                    if (success)
                    {
                        Console.WriteLine($"{testItem.TestName} passed.");
                    }
                    else
                    {
                        Console.WriteLine($"{testItem.TestName} failed.");
                    }
                    //testItem.TestFunction.Invoke();
                    //await testItem.TestFunction.Invoke(); // Await the async function
                    //  await Task.Run(() => testItem.TestFunction.Invoke());
                    //await testItem.TestFunction();
                }
            }

        }
        #endregion

        #region 檢查目前的索引是否鎖定
        private void checkedListBoxTests_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 检查当前项的索引是否为锁定项
            if (e.Index == lockedIndex && e.NewValue == CheckState.Unchecked)
            {
                // 如果是锁定项且用户尝试取消选中，阻止操作
                e.NewValue = CheckState.Checked;
            }
        }
        #endregion

        #region Form_option_FormClosing
        private async void Form_option_FormClosing(object sender, FormClosingEventArgs e)
        {
            await cmdExe.TerminateAllProcesses();
        }
        #endregion
    }
}