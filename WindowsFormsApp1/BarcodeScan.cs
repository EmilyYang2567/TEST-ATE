using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainForm_App; // 添加這一行
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
/// <summary>
/// This is scan bare code.cs
/// Author Name: Emily
/// Create Date: 05/28/2024
/// </summary>
public enum SCAN_MODE
{
    SN = 1,               // Scan WONO, OPID and S/N 
    MAC,                  // Scan WONO, OPID and MAC 
    SNANDMAC,             // Scan WONO, OPID, S/N and MAC 
    SN_MACANDCSN          // Scan WONO, OPID , S/N , MAC and CSN
}

namespace BarCode_FormsApp
{

    public partial class BarcodeScan : Form
    {
        // Define Length 
        const int MAC_LEN = 12; 
        const int SN_LEN = 12;
        const int CSN_LEN = 12;
        const int Wono_LEN = 20;

        public Form_option OptionForm;


        public BarcodeScan()
        {
            Shown += new EventHandler(BarcodeScan_Shown);//is an instance of a Form or any other class with a Shown event

            InitializeComponent();//for initializing and configuring the visual components of the form

            //Barcode form text clear
           // TextBox_wono.Clear();
           // TextBox_opid.Clear();   
            TextBox_scanSn.Clear(); 
            TextBox_scanMac.Clear();
            TextBox_scanCsn.Clear();


            this.KeyPreview = true;//it allows the form to receive key events before the event is passed to the control that has focus. 
        }
        //---------------------------------------------------------------------------

        //---------------------------------------------------------------------------
        void __showErrMsgBox(string msg)
        {
            MessageBox.Show(msg, "ErrMsg", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        //---------------------------------------------------------------------------
        bool __CharIsHex(char ch)
        {
            if (((ch >= '0') && (ch <= '9')) ||
                 ((ch >= 'A') && (ch <= 'F')) ||
                 ((ch >= 'a') && (ch <= 'f')))
            {
                return true;
            }
            
            return false;
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        bool __StrIsHex(string str)
        {
            char ch;
            int len;

            len = str.Length;
            if (len <= 0)
            {
                return false;
            }

            for (int i = 0; i < len; i++)
            {
                ch = Convert.ToChar(str.Substring(i, 1));
                if (!__CharIsHex(ch))
                {
                    return false;
                }
            }

            return true;
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// Show BARCODE_Scan Form.
        /// </summary>
        /// <param name="Mode"></param>
     
        public void BARCODE_Scan(int Mode)
        {
            ClassStruct.bc_Priv.scanMode = Mode;

            this.ShowDialog();
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// Set BARCODE_Scan Form UI
        /// </summary>
        /// <param name="Mode"></param>
        void SetScanModeUI(int Mode)
        {
            switch (Mode)
            {
                case (int)SCAN_MODE.SN:
                    label_scanSn.Visible = true;
                    TextBox_scanSn.Visible = true;
                    label_scanMac.Visible = false;
                    TextBox_scanMac.Visible = false;
                    label_scanCsn.Visible = false;
                    TextBox_scanCsn.Visible = false;

                    label_scanSn.Top = label_opid.Top + 27;
                    TextBox_scanSn.Top = TextBox_opid.Top + 27;
                    label_scanSn.Text = "S/N";
                    TextBox_scanSn.ReadOnly = false;
                    TextBox_scanSn.MaxLength = SN_LEN;
                    break;
                case (int)SCAN_MODE.MAC:
                    label_scanSn.Visible = false;
                    TextBox_scanSn.Visible = false;
                    label_scanMac.Visible = true;
                    TextBox_scanMac.Visible = true;
                    label_scanCsn.Visible = false;
                    TextBox_scanCsn.Visible = false;

                    label_scanMac.Top = label_opid.Top + 27;
                    TextBox_scanMac.Top = TextBox_opid.Top + 27;
                    label_scanMac.Text = "MAC";
                    TextBox_scanMac.ReadOnly = false;
                    TextBox_scanMac.MaxLength = MAC_LEN;
                    break;

                case (int)SCAN_MODE.SNANDMAC:
                    label_scanSn.Visible = true;
                    TextBox_scanSn.Visible = true;
                    label_scanMac.Visible = true;
                    TextBox_scanMac.Visible = true;
                    label_scanCsn.Visible = false;
                    TextBox_scanCsn.Visible = false;

                    label_scanSn.Top = label_opid.Top + 27;
                    TextBox_scanSn.Top = TextBox_opid.Top + 27;
                    label_scanMac.Top = label_opid.Top + 54;
                    TextBox_scanMac.Top = TextBox_opid.Top + 54;
                    label_scanSn.Text = "S/N";
                    TextBox_scanSn.ReadOnly = false;
                    TextBox_scanSn.MaxLength = SN_LEN;
                    label_scanMac.Text = "MAC";
                    TextBox_scanMac.ReadOnly = false;
                    TextBox_scanMac.MaxLength = MAC_LEN;
                    break;
                case (int)SCAN_MODE.SN_MACANDCSN:
                    label_scanSn.Visible = true;
                    TextBox_scanSn.Visible = true;
                    label_scanMac.Visible = true;
                    TextBox_scanMac.Visible = true;
                    label_scanCsn.Visible = true;
                    TextBox_scanCsn.Visible = true;

                    label_scanSn.Top = label_opid.Top + 27;
                    TextBox_scanSn.Top = TextBox_opid.Top + 27;
                    label_scanMac.Top = label_opid.Top + 54;
                    TextBox_scanMac.Top = TextBox_opid.Top + 54;
                    label_scanCsn.Top = label_opid.Top + 81;
                    TextBox_scanCsn.Top = TextBox_opid.Top + 81;

                    label_scanSn.Text = "S/N";
                    TextBox_scanSn.ReadOnly = false;
                    TextBox_scanSn.MaxLength = SN_LEN;
                    label_scanMac.Text = "MAC";
                    TextBox_scanMac.ReadOnly = false;
                    TextBox_scanMac.MaxLength = MAC_LEN;
                    label_scanCsn.Text = "CSN";
                    TextBox_scanCsn.ReadOnly = false;
                    TextBox_scanCsn.MaxLength = CSN_LEN;
                    break;
            }
        }

        private void BarcodeScan_Load(object sender, EventArgs e)
        {
          
        }
        
        private void TextBox_wono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Return == e.KeyChar || (char)Keys.Space == e.KeyChar)
            {
                e.KeyChar = (char)Keys.Return;
                
                TextBox_opid.Focus();
            }
            TextBox_wono.Text = TextBox_wono.Text.ToUpper();
        }

        private void TextBox_opid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Return == e.KeyChar || (char)Keys.Space == e.KeyChar)
            {
                e.KeyChar = (char)Keys.Return;
                
                TextBox_scanSn.Focus();
            }

            else if ((int)SCAN_MODE.MAC == ClassStruct.bc_Priv.scanMode)
            {
                  TextBox_scanMac.Focus();
               
            }
            TextBox_opid.Text = TextBox_opid.Text.ToUpper();

        }

        private void TextBox_scanSn_KeyPress(object sender, KeyPressEventArgs e)
        {
          

            if ((char)Keys.Space == e.KeyChar)
            {
                e.KeyChar = (char)Keys.Return;
                

            }
            else if ((char)Keys.Return != e.KeyChar)
            {
                return;
            }
            TextBox_scanSn.Text = TextBox_scanSn.Text.ToUpper();
            /* Check S/N length */
            if (TextBox_scanSn.Text.Length != SN_LEN)
            {
                __showErrMsgBox("S/N Length incorrect !");
                TextBox_scanSn.Clear();
                TextBox_scanSn.Focus();
                ClassStruct.bc_Priv.bSNOk = false;
                return;
            }
            if (!TextBox_scanSn.Text.StartsWith(ClassStruct.bc_Priv.sSnPrefix))
            {
                __showErrMsgBox("S/N Prefix incorrect !");
                TextBox_scanSn.Clear();
                TextBox_scanSn.Focus();
                ClassStruct.bc_Priv.bSNOk = false;
                return;
            }

            ClassStruct.bc_Priv.bSNOk = true;

            if ((int)SCAN_MODE.SNANDMAC == ClassStruct.bc_Priv.scanMode || (int)SCAN_MODE.SN_MACANDCSN == ClassStruct.bc_Priv.scanMode)
            {
                TextBox_scanMac.Focus();
                return;
            }
            else
            {   
                this.Close();
            }

        }

        private void TextBox_scanMac_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((char)Keys.Space == e.KeyChar)
            {
                e.KeyChar = (char)Keys.Return;
            }
            else if ((char)Keys.Return != e.KeyChar)
            {
                return;
            }
            if (!TextBox_scanSn.Text.StartsWith(ClassStruct.bc_Priv.sSnPrefix))
            {
                __showErrMsgBox("S/N Prefix incorrect !");
                TextBox_scanSn.Clear();
                TextBox_scanSn.Focus();
                ClassStruct.bc_Priv.bSNOk = false;
                return;
            }
            TextBox_scanMac.Text = TextBox_scanMac.Text.ToUpper();
            
            if (!TextBox_scanMac.Text.StartsWith(ClassStruct.bc_Priv.sMAC_Prefix))
            {
                __showErrMsgBox("MAC Prefix incorrect !");
                TextBox_scanMac.Clear();
                TextBox_scanMac.Focus();
                ClassStruct.bc_Priv.bMACOk = false;
                return;
            }
            /* Check MAC length */
            if (TextBox_scanMac.Text.Length != MAC_LEN)
            {
                __showErrMsgBox("MAC length incorrect !");
                TextBox_scanMac.Clear();
                TextBox_scanMac.Focus();
                ClassStruct.bc_Priv.bMACOk = false;
                return;
            }

            /* Check MAC format */
            if (!__StrIsHex(TextBox_scanMac.Text))
            {
                __showErrMsgBox("MAC format incorrect !");

                TextBox_scanMac.Clear();
                TextBox_scanMac.Focus();
                ClassStruct.bc_Priv.bMACOk = false;
                return;
            }
            if ((int)SCAN_MODE.SN_MACANDCSN == ClassStruct.bc_Priv.scanMode )
            {
                TextBox_scanCsn.Focus();
                return;
            }
            ClassStruct.bc_Priv.bMACOk = true;
            ClassStruct.bc_Priv.bScanOK = true;

            this.Close();

        }

        private void TextBox_scanCsn_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((char)Keys.Space == e.KeyChar)
            {
                e.KeyChar = (char)Keys.Return;
                TextBox_scanCsn.Text = TextBox_scanCsn.Text.ToUpper();

            }
            else if ((char)Keys.Return != e.KeyChar)
            {
                return;
            }


            ClassStruct.bc_Priv.bScanOK = true;

           this.Close();
        }
        private void BarcodeScan_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        /// <summary>
        /// Set Form StartPosition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarcodeScan_Shown(object sender, EventArgs e)
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; 
            this.Location = (Point)new Size(x, y);         
            ClassStruct.bc_Priv.bScanOK = false;
            SetScanModeUI(ClassStruct.bc_Priv.scanMode);
          

            TextBox_scanSn.Clear();
            TextBox_scanMac.Clear();
            TextBox_scanCsn.Clear();

        }
        /// <summary>
        /// Get Bare code WONO.
        /// </summary>
        /// <returns></returns>
        public string BARCODE_GetWONo()
        {
            ClassStruct.bc_Priv.currWONO = TextBox_wono.Text;
            return ClassStruct.bc_Priv.currWONO;
        }
        /// <summary>
        /// Get Bare code OPID.
        /// </summary>
        /// <returns></returns>
        public string BARCODE_GetOpID()
        {
            ClassStruct.bc_Priv.currOPID = TextBox_opid.Text;
            return ClassStruct.bc_Priv.currOPID;
        }
        /// <summary>
        /// Get Bare code SN.
        /// </summary>
        /// <returns></returns>
        public string BARCODE_GetSn()
        {

            ClassStruct.bc_Priv.currSN = TextBox_scanSn.Text;
            if (ClassStruct.bc_Priv.currSN.Length <= 0)
            {
                ClassStruct.bc_Priv.currSN = "(EMPTY)\0";
            }

            return ClassStruct.bc_Priv.currSN;
        }
        /// <summary>
        /// Get Bare code MAC.
        /// </summary>
        /// <returns></returns>
        public string BARCODE_GetMac()
        {
            ClassStruct.bc_Priv.currMAC = TextBox_scanMac.Text;
            if (ClassStruct.bc_Priv.currMAC.Length <= 0)
            {
                ClassStruct.bc_Priv.currMAC = "(EMPTY)\0";
            }

            return ClassStruct.bc_Priv.currMAC;
        }

        #region Get Bare code CSN.
        public string BARCODE_GetCSn()
        {
            ClassStruct.bc_Priv.currCSN = TextBox_scanCsn.Text;
            if (ClassStruct.bc_Priv.currCSN.Length <= 0)
            {
                ClassStruct.bc_Priv.currCSN = "(EMPTY)\0";
            }

            return ClassStruct.bc_Priv.currCSN;
        }
        #endregion
    }
}


