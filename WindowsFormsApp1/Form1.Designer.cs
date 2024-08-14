namespace MainForm_App
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Button_Start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_Csn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_Fwono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Fopid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_MAC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_SN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RichTextBox_CmdMsg = new System.Windows.Forms.RichTextBox();
            this.RichTextBox_FlowMsg = new System.Windows.Forms.RichTextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Button_Start.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Start.Location = new System.Drawing.Point(709, 365);
            this.Button_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(170, 87);
            this.Button_Start.TabIndex = 0;
            this.Button_Start.Text = "Start";
            this.Button_Start.UseVisualStyleBackColor = false;
            this.Button_Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 313);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 145);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlText;
            this.label1.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(609, 115);
            this.label1.TabIndex = 0;
            this.label1.Text = "O";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_Csn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.textBox_Fwono);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_Fopid);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_MAC);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_SN);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(636, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 254);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // textBox_Csn
            // 
            this.textBox_Csn.BackColor = System.Drawing.Color.White;
            this.textBox_Csn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Csn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Csn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Csn.Location = new System.Drawing.Point(70, 163);
            this.textBox_Csn.MaxLength = 12;
            this.textBox_Csn.Name = "textBox_Csn";
            this.textBox_Csn.ReadOnly = true;
            this.textBox_Csn.Size = new System.Drawing.Size(227, 35);
            this.textBox_Csn.TabIndex = 9;
            this.textBox_Csn.Text = "00120E123456";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "CSN :";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(73, 206);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 41);
            this.button2.TabIndex = 5;
            this.button2.Text = "END CARTON";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_Fwono
            // 
            this.textBox_Fwono.BackColor = System.Drawing.Color.White;
            this.textBox_Fwono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Fwono.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Fwono.Location = new System.Drawing.Point(68, 29);
            this.textBox_Fwono.MaxLength = 16;
            this.textBox_Fwono.Name = "textBox_Fwono";
            this.textBox_Fwono.ReadOnly = true;
            this.textBox_Fwono.Size = new System.Drawing.Size(227, 35);
            this.textBox_Fwono.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "WO :";
            // 
            // textBox_Fopid
            // 
            this.textBox_Fopid.BackColor = System.Drawing.Color.White;
            this.textBox_Fopid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Fopid.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Fopid.Location = new System.Drawing.Point(68, 62);
            this.textBox_Fopid.MaxLength = 6;
            this.textBox_Fopid.Name = "textBox_Fopid";
            this.textBox_Fopid.ReadOnly = true;
            this.textBox_Fopid.Size = new System.Drawing.Size(227, 35);
            this.textBox_Fopid.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "OP :";
            // 
            // textBox_MAC
            // 
            this.textBox_MAC.BackColor = System.Drawing.Color.White;
            this.textBox_MAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_MAC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_MAC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MAC.Location = new System.Drawing.Point(70, 131);
            this.textBox_MAC.MaxLength = 12;
            this.textBox_MAC.Name = "textBox_MAC";
            this.textBox_MAC.ReadOnly = true;
            this.textBox_MAC.Size = new System.Drawing.Size(227, 35);
            this.textBox_MAC.TabIndex = 3;
            this.textBox_MAC.Text = "00120E123456";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "MAC :";
            // 
            // textBox_SN
            // 
            this.textBox_SN.BackColor = System.Drawing.Color.White;
            this.textBox_SN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_SN.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_SN.Location = new System.Drawing.Point(70, 94);
            this.textBox_SN.MaxLength = 15;
            this.textBox_SN.Name = "textBox_SN";
            this.textBox_SN.ReadOnly = true;
            this.textBox_SN.Size = new System.Drawing.Size(227, 35);
            this.textBox_SN.TabIndex = 1;
            this.textBox_SN.Text = "P1234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "SN :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(780, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(162, 40);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RichTextBox_CmdMsg);
            this.groupBox3.Controls.Add(this.RichTextBox_FlowMsg);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.txt_result);
            this.groupBox3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(613, 258);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Procedure";
            // 
            // RichTextBox_CmdMsg
            // 
            this.RichTextBox_CmdMsg.BackColor = System.Drawing.SystemColors.MenuText;
            this.RichTextBox_CmdMsg.Location = new System.Drawing.Point(6, 194);
            this.RichTextBox_CmdMsg.Name = "RichTextBox_CmdMsg";
            this.RichTextBox_CmdMsg.Size = new System.Drawing.Size(589, 58);
            this.RichTextBox_CmdMsg.TabIndex = 10;
            this.RichTextBox_CmdMsg.Text = "";
            // 
            // RichTextBox_FlowMsg
            // 
            this.RichTextBox_FlowMsg.BackColor = System.Drawing.SystemColors.MenuText;
            this.RichTextBox_FlowMsg.Location = new System.Drawing.Point(6, 108);
            this.RichTextBox_FlowMsg.Name = "RichTextBox_FlowMsg";
            this.RichTextBox_FlowMsg.Size = new System.Drawing.Size(589, 93);
            this.RichTextBox_FlowMsg.TabIndex = 9;
            this.RichTextBox_FlowMsg.Text = "";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.Lime;
            this.textBox7.Location = new System.Drawing.Point(7, 22);
            this.textBox7.MaxLength = 15;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(135, 40);
            this.textBox7.TabIndex = 8;
            this.textBox7.Text = "M01";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_result
            // 
            this.txt_result.BackColor = System.Drawing.SystemColors.MenuText;
            this.txt_result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_result.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.ForeColor = System.Drawing.SystemColors.Info;
            this.txt_result.Location = new System.Drawing.Point(6, 64);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ReadOnly = true;
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(600, 47);
            this.txt_result.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 31);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(86, 27);
            this.toolStripMenuItem1.Text = "Option";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(78, 27);
            this.toolStripMenuItem2.Text = "About";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox6);
            this.groupBox4.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(631, 49);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(308, 54);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Error Code";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Yellow;
            this.textBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.Red;
            this.textBox6.Location = new System.Drawing.Point(114, 14);
            this.textBox6.MaxLength = 15;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(188, 35);
            this.textBox6.TabIndex = 2;
            this.textBox6.Text = "PLC001";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.Black;
            this.textBox8.Font = new System.Drawing.Font("Arial Narrow", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.Yellow;
            this.textBox8.Location = new System.Drawing.Point(613, 3);
            this.textBox8.MaxLength = 15;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(161, 53);
            this.textBox8.TabIndex = 9;
            this.textBox8.Text = "F1-1";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Black;
            this.textBox9.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.ForeColor = System.Drawing.Color.Yellow;
            this.textBox9.Location = new System.Drawing.Point(190, 3);
            this.textBox9.MaxLength = 15;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(429, 53);
            this.textBox9.TabIndex = 10;
            this.textBox9.Text = "Model Name";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Button_Stop
            // 
            this.Button_Stop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Button_Stop.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Stop.ForeColor = System.Drawing.Color.Red;
            this.Button_Stop.Location = new System.Drawing.Point(709, 365);
            this.Button_Stop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(170, 87);
            this.Button_Stop.TabIndex = 11;
            this.Button_Stop.Text = "STOP";
            this.Button_Stop.UseVisualStyleBackColor = false;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 483);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button_Start);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "F1 ATE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_SN;
        private System.Windows.Forms.TextBox textBox_Fopid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_MAC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Fwono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox_Csn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.RichTextBox RichTextBox_FlowMsg;
        private System.Windows.Forms.RichTextBox RichTextBox_CmdMsg;
    }
}

