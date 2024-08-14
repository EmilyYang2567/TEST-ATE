namespace WindowsFormsApp1
{
    partial class Form_option
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
            this.SpeedButton_ok = new System.Windows.Forms.Button();
            this.SpeedButton_cancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.timeoutTextBox3 = new System.Windows.Forms.TextBox();
            this.timeoutTextBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timeoutTextBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkedListBoxTests = new System.Windows.Forms.CheckedListBox();
            this.button_testitem = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CheckBox_AteAutoScan = new System.Windows.Forms.CheckBox();
            this.ShopFlowEnable = new System.Windows.Forms.CheckBox();
            this.MachineName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_part = new System.Windows.Forms.TextBox();
            this.textBox_opid = new System.Windows.Forms.TextBox();
            this.textBox_wono = new System.Windows.Forms.TextBox();
            this.MAC_Prefix = new System.Windows.Forms.TextBox();
            this.SN_Prefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBox_ateScanSN = new System.Windows.Forms.CheckBox();
            this.CheckBox_ateScanMAC = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpeedButton_ok
            // 
            this.SpeedButton_ok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedButton_ok.Location = new System.Drawing.Point(677, 262);
            this.SpeedButton_ok.Name = "SpeedButton_ok";
            this.SpeedButton_ok.Size = new System.Drawing.Size(112, 49);
            this.SpeedButton_ok.TabIndex = 0;
            this.SpeedButton_ok.Text = "Save";
            this.SpeedButton_ok.UseVisualStyleBackColor = true;
            this.SpeedButton_ok.Click += new System.EventHandler(this.SpeedButton_ok_Click);
            // 
            // SpeedButton_cancel
            // 
            this.SpeedButton_cancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedButton_cancel.Location = new System.Drawing.Point(805, 262);
            this.SpeedButton_cancel.Name = "SpeedButton_cancel";
            this.SpeedButton_cancel.Size = new System.Drawing.Size(112, 49);
            this.SpeedButton_cancel.TabIndex = 1;
            this.SpeedButton_cancel.Text = "Cancel";
            this.SpeedButton_cancel.UseVisualStyleBackColor = true;
            this.SpeedButton_cancel.Click += new System.EventHandler(this.SpeedButton_cancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(940, 232);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.timeoutTextBox3);
            this.tabPage1.Controls.Add(this.timeoutTextBox2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.timeoutTextBox1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.checkedListBoxTests);
            this.tabPage1.Controls.Add(this.button_testitem);
            this.tabPage1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(932, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TestItem";
            // 
            // timeoutTextBox3
            // 
            this.timeoutTextBox3.Location = new System.Drawing.Point(421, 68);
            this.timeoutTextBox3.Name = "timeoutTextBox3";
            this.timeoutTextBox3.Size = new System.Drawing.Size(66, 28);
            this.timeoutTextBox3.TabIndex = 12;
            this.timeoutTextBox3.Text = "3000";
            // 
            // timeoutTextBox2
            // 
            this.timeoutTextBox2.Location = new System.Drawing.Point(421, 34);
            this.timeoutTextBox2.Name = "timeoutTextBox2";
            this.timeoutTextBox2.Size = new System.Drawing.Size(66, 28);
            this.timeoutTextBox2.TabIndex = 11;
            this.timeoutTextBox2.Text = "3000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(256, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(265, 21);
            this.label7.TabIndex = 10;
            this.label7.Text = "TEST2 Timeout                     ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 21);
            this.label6.TabIndex = 9;
            this.label6.Text = "TEST3 Timeout                     ms";
            // 
            // timeoutTextBox1
            // 
            this.timeoutTextBox1.Location = new System.Drawing.Point(421, 3);
            this.timeoutTextBox1.Name = "timeoutTextBox1";
            this.timeoutTextBox1.Size = new System.Drawing.Size(66, 28);
            this.timeoutTextBox1.TabIndex = 7;
            this.timeoutTextBox1.Text = "3000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "TEST1 Timeout                     ms";
            // 
            // checkedListBoxTests
            // 
            this.checkedListBoxTests.CheckOnClick = true;
            this.checkedListBoxTests.FormattingEnabled = true;
            this.checkedListBoxTests.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxTests.Name = "checkedListBoxTests";
            this.checkedListBoxTests.Size = new System.Drawing.Size(232, 204);
            this.checkedListBoxTests.TabIndex = 3;
            this.checkedListBoxTests.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxTests_ItemCheck);
            // 
            // button_testitem
            // 
            this.button_testitem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_testitem.Location = new System.Drawing.Point(260, 145);
            this.button_testitem.Name = "button_testitem";
            this.button_testitem.Size = new System.Drawing.Size(146, 49);
            this.button_testitem.TabIndex = 1;
            this.button_testitem.Text = "test";
            this.button_testitem.UseVisualStyleBackColor = true;
            this.button_testitem.Click += new System.EventHandler(this.button_testitem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.CheckBox_AteAutoScan);
            this.tabPage2.Controls.Add(this.ShopFlowEnable);
            this.tabPage2.Controls.Add(this.MachineName);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox_part);
            this.tabPage2.Controls.Add(this.textBox_opid);
            this.tabPage2.Controls.Add(this.textBox_wono);
            this.tabPage2.Controls.Add(this.MAC_Prefix);
            this.tabPage2.Controls.Add(this.SN_Prefix);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.CheckBox_ateScanSN);
            this.tabPage2.Controls.Add(this.CheckBox_ateScanMAC);
            this.tabPage2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(932, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ATE_Parameters";
            // 
            // CheckBox_AteAutoScan
            // 
            this.CheckBox_AteAutoScan.AutoSize = true;
            this.CheckBox_AteAutoScan.Location = new System.Drawing.Point(345, 128);
            this.CheckBox_AteAutoScan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckBox_AteAutoScan.Name = "CheckBox_AteAutoScan";
            this.CheckBox_AteAutoScan.Size = new System.Drawing.Size(337, 25);
            this.CheckBox_AteAutoScan.TabIndex = 110;
            this.CheckBox_AteAutoScan.Text = "Auto show scan box after test finished";
            this.CheckBox_AteAutoScan.UseVisualStyleBackColor = true;
            // 
            // ShopFlowEnable
            // 
            this.ShopFlowEnable.AutoSize = true;
            this.ShopFlowEnable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShopFlowEnable.Location = new System.Drawing.Point(345, 94);
            this.ShopFlowEnable.Name = "ShopFlowEnable";
            this.ShopFlowEnable.Size = new System.Drawing.Size(239, 25);
            this.ShopFlowEnable.TabIndex = 14;
            this.ShopFlowEnable.Text = "Enable Shopflow function";
            this.ShopFlowEnable.UseVisualStyleBackColor = true;
            // 
            // MachineName
            // 
            this.MachineName.Location = new System.Drawing.Point(185, 125);
            this.MachineName.MaxLength = 2;
            this.MachineName.Name = "MachineName";
            this.MachineName.Size = new System.Drawing.Size(127, 28);
            this.MachineName.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "PaetNo.: ";
            // 
            // textBox_part
            // 
            this.textBox_part.Location = new System.Drawing.Point(124, 91);
            this.textBox_part.MaxLength = 20;
            this.textBox_part.Name = "textBox_part";
            this.textBox_part.Size = new System.Drawing.Size(188, 28);
            this.textBox_part.TabIndex = 11;
            // 
            // textBox_opid
            // 
            this.textBox_opid.Location = new System.Drawing.Point(185, 26);
            this.textBox_opid.MaxLength = 6;
            this.textBox_opid.Name = "textBox_opid";
            this.textBox_opid.Size = new System.Drawing.Size(127, 28);
            this.textBox_opid.TabIndex = 10;
            // 
            // textBox_wono
            // 
            this.textBox_wono.Location = new System.Drawing.Point(124, 56);
            this.textBox_wono.MaxLength = 20;
            this.textBox_wono.Name = "textBox_wono";
            this.textBox_wono.Size = new System.Drawing.Size(188, 28);
            this.textBox_wono.TabIndex = 9;
            // 
            // MAC_Prefix
            // 
            this.MAC_Prefix.Location = new System.Drawing.Point(735, 53);
            this.MAC_Prefix.MaxLength = 6;
            this.MAC_Prefix.Name = "MAC_Prefix";
            this.MAC_Prefix.Size = new System.Drawing.Size(188, 28);
            this.MAC_Prefix.TabIndex = 8;
            // 
            // SN_Prefix
            // 
            this.SN_Prefix.Location = new System.Drawing.Point(735, 19);
            this.SN_Prefix.MaxLength = 2;
            this.SN_Prefix.Name = "SN_Prefix";
            this.SN_Prefix.Size = new System.Drawing.Size(188, 28);
            this.SN_Prefix.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "TestStationNo :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "OPID: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "WONo.: ";
            // 
            // CheckBox_ateScanSN
            // 
            this.CheckBox_ateScanSN.AutoSize = true;
            this.CheckBox_ateScanSN.Location = new System.Drawing.Point(345, 22);
            this.CheckBox_ateScanSN.Name = "CheckBox_ateScanSN";
            this.CheckBox_ateScanSN.Size = new System.Drawing.Size(353, 25);
            this.CheckBox_ateScanSN.TabIndex = 3;
            this.CheckBox_ateScanSN.Text = "Scan WONO / OPID / SN   ,  SN Prefix :";
            this.CheckBox_ateScanSN.UseVisualStyleBackColor = true;
            // 
            // CheckBox_ateScanMAC
            // 
            this.CheckBox_ateScanMAC.AutoSize = true;
            this.CheckBox_ateScanMAC.Location = new System.Drawing.Point(345, 56);
            this.CheckBox_ateScanMAC.Name = "CheckBox_ateScanMAC";
            this.CheckBox_ateScanMAC.Size = new System.Drawing.Size(366, 25);
            this.CheckBox_ateScanMAC.TabIndex = 0;
            this.CheckBox_ateScanMAC.Text = "Scan WONO / OPID / MAC , MAC Prefix: ";
            this.CheckBox_ateScanMAC.UseVisualStyleBackColor = true;
            // 
            // Form_option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 330);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.SpeedButton_cancel);
            this.Controls.Add(this.SpeedButton_ok);
            this.Name = "Form_option";
            this.Text = "Form_option";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_option_FormClosing);
            this.Load += new System.EventHandler(this.Form_option_Load);
            this.Shown += new System.EventHandler(this.Form_option_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SpeedButton_ok;
        private System.Windows.Forms.Button SpeedButton_cancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox CheckBox_ateScanSN;
        private System.Windows.Forms.CheckBox CheckBox_ateScanMAC;
        private System.Windows.Forms.TextBox MAC_Prefix;
        private System.Windows.Forms.TextBox SN_Prefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_part;
        private System.Windows.Forms.TextBox textBox_opid;
        private System.Windows.Forms.TextBox textBox_wono;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ShopFlowEnable;
        private System.Windows.Forms.TextBox MachineName;
        private System.Windows.Forms.Button button_testitem;
        private System.Windows.Forms.CheckedListBox checkedListBoxTests;
        private System.Windows.Forms.TextBox timeoutTextBox3;
        private System.Windows.Forms.TextBox timeoutTextBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox timeoutTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CheckBox_AteAutoScan;
    }
}