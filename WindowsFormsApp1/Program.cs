using BarCode_FormsApp;
using MainForm_App;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Option_FormsApp
{
    internal static class Program
    {
        [STAThread]

        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // 啟動應用程式
            Application.Run(new Form1());  // 啟動 Form1 表單
        }
    
          //  Application.Run(new Form1(Form1,BarcodeScan));
        }
    }

