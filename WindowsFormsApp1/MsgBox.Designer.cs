namespace MainForm_App
{
    partial class MsgBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Button_Yes = new System.Windows.Forms.Button();
            this.Button_No = new System.Windows.Forms.Button();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Label_Msg = new System.Windows.Forms.Label();
            this.Timer_Msg = new System.Windows.Forms.Timer(this.components);
            this.PictureBox_Icon2 = new System.Windows.Forms.PictureBox();
            this.PictureBox_Icon1 = new System.Windows.Forms.PictureBox();
            this.PictureBox_Default2 = new System.Windows.Forms.PictureBox();
            this.PictureBox_Default1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Default2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Default1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_Yes
            // 
            this.Button_Yes.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Italic);
            this.Button_Yes.Location = new System.Drawing.Point(196, 16);
            this.Button_Yes.Name = "Button_Yes";
            this.Button_Yes.Size = new System.Drawing.Size(155, 40);
            this.Button_Yes.TabIndex = 2;
            this.Button_Yes.Text = "&Yes";
            this.Button_Yes.UseVisualStyleBackColor = true;
            this.Button_Yes.Click += new System.EventHandler(this.Button_Yes_Click);
            // 
            // Button_No
            // 
            this.Button_No.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Italic);
            this.Button_No.Location = new System.Drawing.Point(196, 62);
            this.Button_No.Name = "Button_No";
            this.Button_No.Size = new System.Drawing.Size(155, 40);
            this.Button_No.TabIndex = 3;
            this.Button_No.Text = "&No";
            this.Button_No.UseVisualStyleBackColor = true;
            this.Button_No.Click += new System.EventHandler(this.Button_No_Click);
            // 
            // Button_Ok
            // 
            this.Button_Ok.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Italic);
            this.Button_Ok.Location = new System.Drawing.Point(196, 108);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(155, 40);
            this.Button_Ok.TabIndex = 4;
            this.Button_Ok.Text = "&OK";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Label_Msg
            // 
            this.Label_Msg.AutoSize = true;
            this.Label_Msg.BackColor = System.Drawing.SystemColors.WindowText;
            this.Label_Msg.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Msg.ForeColor = System.Drawing.Color.Silver;
            this.Label_Msg.Location = new System.Drawing.Point(12, 102);
            this.Label_Msg.Name = "Label_Msg";
            this.Label_Msg.Size = new System.Drawing.Size(69, 22);
            this.Label_Msg.TabIndex = 7;
            this.Label_Msg.Text = "Label1";
            // 
            // Timer_Msg
            // 
            this.Timer_Msg.Tick += new System.EventHandler(this.Timer_Msg_Tick);
            // 
            // PictureBox_Icon2
            // 
            this.PictureBox_Icon2.Location = new System.Drawing.Point(46, 12);
            this.PictureBox_Icon2.Name = "PictureBox_Icon2";
            this.PictureBox_Icon2.Size = new System.Drawing.Size(28, 29);
            this.PictureBox_Icon2.TabIndex = 6;
            this.PictureBox_Icon2.TabStop = false;
            this.PictureBox_Icon2.Visible = false;
            // 
            // PictureBox_Icon1
            // 
            this.PictureBox_Icon1.Location = new System.Drawing.Point(12, 12);
            this.PictureBox_Icon1.Name = "PictureBox_Icon1";
            this.PictureBox_Icon1.Size = new System.Drawing.Size(28, 29);
            this.PictureBox_Icon1.TabIndex = 5;
            this.PictureBox_Icon1.TabStop = false;
            // 
            // PictureBox_Default2
            // 
           
            this.PictureBox_Default2.Location = new System.Drawing.Point(98, 128);
            this.PictureBox_Default2.Name = "PictureBox_Default2";
            this.PictureBox_Default2.Size = new System.Drawing.Size(80, 80);
            this.PictureBox_Default2.TabIndex = 1;
            this.PictureBox_Default2.TabStop = false;
            this.PictureBox_Default2.Visible = false;
            // 
            // PictureBox_Default1
            // 
            
            this.PictureBox_Default1.Location = new System.Drawing.Point(12, 128);
            this.PictureBox_Default1.Name = "PictureBox_Default1";
            this.PictureBox_Default1.Size = new System.Drawing.Size(80, 80);
            this.PictureBox_Default1.TabIndex = 0;
            this.PictureBox_Default1.TabStop = false;
            this.PictureBox_Default1.Visible = false;
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(363, 213);
            this.Controls.Add(this.Label_Msg);
            this.Controls.Add(this.PictureBox_Icon2);
            this.Controls.Add(this.PictureBox_Icon1);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.Button_No);
            this.Controls.Add(this.Button_Yes);
            this.Controls.Add(this.PictureBox_Default2);
            this.Controls.Add(this.PictureBox_Default1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MsgBox";
            this.Text = "MsgBox";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsgBox_FormClosed);
            this.Load += new System.EventHandler(this.MsgBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Default2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Default1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_Default1;
        private System.Windows.Forms.PictureBox PictureBox_Default2;
        private System.Windows.Forms.Button Button_Yes;
        private System.Windows.Forms.Button Button_No;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.PictureBox PictureBox_Icon1;
        private System.Windows.Forms.PictureBox PictureBox_Icon2;
        private System.Windows.Forms.Label Label_Msg;
        private System.Windows.Forms.Timer Timer_Msg;
    }
}