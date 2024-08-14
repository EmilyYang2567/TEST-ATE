using System;
using System.Windows.Forms;

namespace MainForm_App
{
    public partial class MsgBox : Form
    {
        // InputBox form parameter
        private const int PicDefaultInterval = 500;   // Picture change default interval
        private static bool bChangeFlag = true;

        // Declare form event parameters.
        private const int WM_SYSCOMMAND = 0x112;
        private const long SC_MAXIMIZE = 0xF030;
        private const long SC_MINIMIZE = 0xF020;
        private const long SC_CLOSE = 0xF060;

        // MsgBox form global variables

        private struct MsgBoxPriv_T
        {
            public bool bBtnClicked;
            public int iBtnValue;
        }

        private static MsgBoxPriv_T MsgBoxPriv;

        private enum BtnFlag
        {
            mbOk,               // OK按鈕
            mbOkCancel,         // OK, Cancel按鈕
            mbAbortRetryIgnore, // Abort, Retry, Ignore按鈕
            mbYesNoCancel,      // Yes, No, Cancel按鈕
            mbYesNo,            // Yes, No按鈕
            mbRetryCancel,      // Retry, Cancel按鈕
        }

        private enum BtnPress
        {
            mrOk,   // 按下 OK按鈕
            mrYes,  // 按下 Yes按鈕
            mrNo,   // 按下 No按鈕
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox
            *  Purpose: the function to initialize components and parameter settings.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public MsgBox()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            Timer_Msg.Enabled = false;
            Timer_Msg.Interval = PicDefaultInterval;

            MsgBoxPriv.bBtnClicked = false;
        }

        /*------------------------------------------------------------------------
            * Function: Button_Yes_Click
            *  Purpose: the function to after the Button_Yes is clicked, 
            *           the parameters are set and the form is closed.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void Button_Yes_Click(object sender, EventArgs e)
        {
            MsgBoxPriv.bBtnClicked = true;
            MsgBoxPriv.iBtnValue = (int)BtnPress.mrYes;
            // Form_msg->Close();
            this.Close();
        }

        /*------------------------------------------------------------------------
            * Function: Button_No_Click
            *  Purpose: the function to after the Button_No is clicked, 
            *           the parameters are set and the form is closed.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void Button_No_Click(object sender, EventArgs e)
        {
            MsgBoxPriv.bBtnClicked = true;
            MsgBoxPriv.iBtnValue = (int)BtnPress.mrNo;
            // Form_msg->Close();
            this.Close();
        }

        /*------------------------------------------------------------------------
            * Function: Button_Ok_Click
            *  Purpose: the function to after the Button_Ok is clicked, 
            *           the parameters are set and the form is closed.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void Button_Ok_Click(object sender, EventArgs e)
        {
            MsgBoxPriv.bBtnClicked = true;
            MsgBoxPriv.iBtnValue = (int)BtnPress.mrOk;
            // Form_msg->Close();
            this.Close();
        }

        /*------------------------------------------------------------------------
            * Function: FormResize
            *  Purpose: the function to set the size of the form seat when 
            *           the MsgBox form is shown.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void FormResize(
            int iDlgType    // [Input] display form type
            )
        {
            const int Th = 30;  // Title Height
            const int Bs = 32;  // Border Size

            // F = Form,   I = Image,  L = Label, B = Button
            // h = height, w = width,  t = top,   l = left

            int Fh, Fw, Ih, Iw, Lh, Lw, Bh, Bw;
            int It, Il, Lt, Ll, Bt, Bl;


            Ih = PictureBox_Icon1.Height;
            Iw = PictureBox_Icon1.Width;
            Lh = Label_Msg.Height;
            Lw = Label_Msg.Width;

            switch (iDlgType)
            {
                case (int)BtnFlag.mbYesNo:
                    Bh = Button_Yes.Height;
                    Bw = Button_No.Width + Button_Yes.Width + Bs;
                    break;

                case (int)BtnFlag.mbOk:
                default:
                    Bh = Button_Ok.Height;
                    Bw = Button_Ok.Width;
                    break;
            }

            Fh = ((Ih >= Lh) ? Ih : Lh) + Bs * 4 + Bh;
            Fw = ((Bw >= (Iw + Bs + Lw)) ? Bw : (Iw + Bs + Lw)) + Bs * 2;

            Height = Fh + Th;
            Width = Fw + 5;


            // On Screen Center
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2 - 20;
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;


            It = Bs + (Fh - Bs * 3 - Bh - Ih) / 2;
            Lt = Bs + (Fh - Bs * 3 - Bh - Lh) / 2;
            Il = Bs;
            Ll = Bs * 2 + Iw;


            switch (iDlgType)
            {
                case (int)BtnFlag.mbYesNo:
                    Bt = Fh - Bs - Bh;
                    Button_Yes.Top = Bt;
                    Button_No.Top = Bt;
                    Bl = (Fw - Bw) / 2;
                    Button_Yes.Left = Bl;
                    Bl = Bl + Button_Yes.Width + Bs;
                    Button_No.Left = Bl;
                    break;

                case (int)BtnFlag.mbOk:
                default:
                    Bt = Fh - Bs - Bh;
                    Bl = (Fw - Bw) / 2;
                    Button_Ok.Top = Bt;
                    Button_Ok.Left = Bl;
                    break;
            }


            PictureBox_Icon1.Top = It;
            PictureBox_Icon1.Left = Il;
            PictureBox_Icon2.Top = It;
            PictureBox_Icon2.Left = Il;
            Label_Msg.Top = Lt;
            Label_Msg.Left = Ll;
        }

        /*------------------------------------------------------------------------
            * Function: Timer_Msg_Tick
            *  Purpose: the function to toggle showing icon.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void Timer_Msg_Tick(object sender, EventArgs e)
        {
            PictureBox_Icon1.Visible = !bChangeFlag;
            PictureBox_Icon2.Visible = bChangeFlag;
            bChangeFlag = !bChangeFlag;
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox_Dlg
            *  Purpose: the function to Icon, button type, title and content settings,
            *           also call FormResize function.
            *  Returns: Return key press.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private int MsgBox_Dlg(
            string sStrTitle,                       // [Input] Title String
            string sStrMsg,                         // [Input] Message String
            int iDlgType,                           // [Input] Dialog type: MB_OK or MB_YESNO
            System.Windows.Forms.PictureBox pPic1,  // [Input] TPicture pointer
            System.Windows.Forms.PictureBox pPic2,  // [Input] TPicture pointer
            int iPicInterval                        // [Input] Picture change interval
            )
        {
            bool bIsAnimation = false;

            // Set String
            this.Text = sStrTitle;
            Label_Msg.Text = sStrMsg;


            // to determine Animation or not
            if (null == pPic1 && null == pPic2)
            {
                bIsAnimation = true;
                PictureBox_Icon1.Image = PictureBox_Default1.Image;
                PictureBox_Icon1.Width = PictureBox_Default1.Width + 20;
                PictureBox_Icon1.Height = PictureBox_Default1.Height + 20;

                PictureBox_Icon2.Image = PictureBox_Default2.Image;
                PictureBox_Icon2.Width = PictureBox_Default2.Width + 20;
                PictureBox_Icon2.Height = PictureBox_Default2.Height + 20;
            }
            else if (null == pPic1 && null != pPic2)
            {
                bIsAnimation = false;
                PictureBox_Icon1.Visible = false;

                PictureBox_Icon2.Visible = true;
                PictureBox_Icon2.Image = pPic2.Image;
                PictureBox_Icon2.Width = pPic2.Width + 10;
                PictureBox_Icon2.Height = pPic2.Height + 10;
            }
            else if (null != pPic1 && null == pPic2)
            {
                bIsAnimation = false;
                PictureBox_Icon1.Visible = true;
                PictureBox_Icon1.Image = pPic1.Image;
                PictureBox_Icon1.Width = pPic1.Width + 10;
                PictureBox_Icon1.Height = pPic1.Height + 10;

                PictureBox_Icon2.Visible = false;
            }
            else if (null != pPic1 && null != pPic2)
            {
                bIsAnimation = true;
                PictureBox_Icon1.Image = pPic1.Image;
                PictureBox_Icon1.Width = pPic1.Width + 10;
                PictureBox_Icon1.Height = pPic1.Height + 10;

                PictureBox_Icon2.Image = pPic2.Image;
                PictureBox_Icon2.Width = pPic2.Width + 10;
                PictureBox_Icon2.Height = pPic2.Height + 10;
            }

            // Set Pic interval
            if (bIsAnimation)
            {
                if (iPicInterval <= 100)
                {
                    iPicInterval = PicDefaultInterval;
                }
                Timer_Msg.Interval = iPicInterval;
            }


            // Visable Buttons
            Button_No.Visible = false;
            Button_Ok.Visible = false;
            Button_Yes.Visible = false;
            switch (iDlgType)
            {
                case (int)BtnFlag.mbYesNo:
                    Button_No.Visible = true;
                    Button_Yes.Visible = true;
                    break;

                case (int)BtnFlag.mbOk:
                default:
                    Button_Ok.Visible = true;
                    break;
            }


            // Show Form
            MsgBoxPriv.bBtnClicked = false;

            FormResize(iDlgType);
            Timer_Msg.Enabled = bIsAnimation;

            // Form_msg->ShowModal();
            this.ShowDialog();
            Timer_Msg.Enabled = false;

            return MsgBoxPriv.iBtnValue;
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox_Info
            *  Purpose: the function to show reminder form.
            *  Returns: Return true.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool MsgBox_Info(
            string sStrMsg  // [Input] Message String
            )
        {
            MsgBox_Dlg("", sStrMsg, (int)BtnFlag.mbOk, null, null, 0);

            return true;
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox_Confirm
            *  Purpose: the function to show inquiry form.
            *  Returns: 1. TRUE - Only press YES button.
            *           2. FALSE - Press a button other than the YES button.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        public bool MsgBox_Confirm(
            string sStrMsg  // [Input] Message String
            )
        {
            int ret;

            ret = MsgBox_Dlg("", sStrMsg, (int)BtnFlag.mbYesNo, null, null, 0);

            return ((int)BtnPress.mrYes == ret);
        }

        /*------------------------------------------------------------------------
            * Function: WndProc
            *  Purpose: the function to retrieve form menu commands.
            *  Returns: None.
            *     Note: Cancel the maximize button, minimize button or close button action.
            *------------------------------------------------------------------------
            */
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt64() == SC_MAXIMIZE)
                {
                    // MessageBox.Show("MAXIMIZE ");
                    return;
                }
                if (m.WParam.ToInt64() == SC_MINIMIZE)
                {
                    // MessageBox.Show("MINIMIZE ");
                    return;
                }
                if (m.WParam.ToInt64() == SC_CLOSE)
                {
                    // MessageBox.Show("CLOSE ");
                    return;
                }
            }
            base.WndProc(ref m);
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox_Load
            *  Purpose: the function to after opening the program, 
            *           set MsgBox form position.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void MsgBox_Load(object sender, EventArgs e)
        {
           
        }

        /*------------------------------------------------------------------------
            * Function: MsgBox_FormClosed
            *  Purpose: the function to save MsgBox form position.
            *  Returns: None.
            *     Note: None.
            *------------------------------------------------------------------------
            */
        private void MsgBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
