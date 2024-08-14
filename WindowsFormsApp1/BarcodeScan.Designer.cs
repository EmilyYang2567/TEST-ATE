namespace BarCode_FormsApp
{
    partial class BarcodeScan
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
            this.TextBox_wono = new System.Windows.Forms.TextBox();
            this.label_wono = new System.Windows.Forms.Label();
            this.TextBox_opid = new System.Windows.Forms.TextBox();
            this.label_opid = new System.Windows.Forms.Label();
            this.TextBox_scanMac = new System.Windows.Forms.TextBox();
            this.label_scanMac = new System.Windows.Forms.Label();
            this.TextBox_scanSn = new System.Windows.Forms.TextBox();
            this.label_scanSn = new System.Windows.Forms.Label();
            this.TextBox_scanCsn = new System.Windows.Forms.TextBox();
            this.label_scanCsn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_wono
            // 
            this.TextBox_wono.BackColor = System.Drawing.Color.White;
            this.TextBox_wono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBox_wono.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_wono.Location = new System.Drawing.Point(156, 5);
            this.TextBox_wono.MaxLength = 16;
            this.TextBox_wono.Name = "TextBox_wono";
            this.TextBox_wono.Size = new System.Drawing.Size(278, 40);
            this.TextBox_wono.TabIndex = 15;
            this.TextBox_wono.Visible = false;
            this.TextBox_wono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_wono_KeyPress);
            // 
            // label_wono
            // 
            this.label_wono.AutoSize = true;
            this.label_wono.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_wono.ForeColor = System.Drawing.Color.Blue;
            this.label_wono.Location = new System.Drawing.Point(33, 5);
            this.label_wono.Name = "label_wono";
            this.label_wono.Size = new System.Drawing.Size(102, 33);
            this.label_wono.TabIndex = 14;
            this.label_wono.Text = "WoNo. :";
            this.label_wono.Visible = false;
            // 
            // TextBox_opid
            // 
            this.TextBox_opid.BackColor = System.Drawing.Color.White;
            this.TextBox_opid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBox_opid.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_opid.Location = new System.Drawing.Point(156, 42);
            this.TextBox_opid.MaxLength = 6;
            this.TextBox_opid.Name = "TextBox_opid";
            this.TextBox_opid.Size = new System.Drawing.Size(278, 40);
            this.TextBox_opid.TabIndex = 13;
            this.TextBox_opid.Visible = false;
            this.TextBox_opid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_opid_KeyPress);
            // 
            // label_opid
            // 
            this.label_opid.AutoSize = true;
            this.label_opid.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_opid.ForeColor = System.Drawing.Color.Blue;
            this.label_opid.Location = new System.Drawing.Point(50, 45);
            this.label_opid.Name = "label_opid";
            this.label_opid.Size = new System.Drawing.Size(85, 33);
            this.label_opid.TabIndex = 12;
            this.label_opid.Text = "OPID :";
            this.label_opid.Visible = false;
            // 
            // TextBox_scanMac
            // 
            this.TextBox_scanMac.BackColor = System.Drawing.Color.White;
            this.TextBox_scanMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_scanMac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBox_scanMac.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_scanMac.Location = new System.Drawing.Point(156, 123);
            this.TextBox_scanMac.MaxLength = 12;
            this.TextBox_scanMac.Name = "TextBox_scanMac";
            this.TextBox_scanMac.Size = new System.Drawing.Size(278, 40);
            this.TextBox_scanMac.TabIndex = 11;
            this.TextBox_scanMac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_scanMac_KeyPress);
            // 
            // label_scanMac
            // 
            this.label_scanMac.AutoSize = true;
            this.label_scanMac.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_scanMac.ForeColor = System.Drawing.Color.Blue;
            this.label_scanMac.Location = new System.Drawing.Point(53, 125);
            this.label_scanMac.Name = "label_scanMac";
            this.label_scanMac.Size = new System.Drawing.Size(82, 33);
            this.label_scanMac.TabIndex = 10;
            this.label_scanMac.Text = "MAC :";
            // 
            // TextBox_scanSn
            // 
            this.TextBox_scanSn.BackColor = System.Drawing.Color.White;
            this.TextBox_scanSn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBox_scanSn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_scanSn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TextBox_scanSn.Location = new System.Drawing.Point(156, 82);
            this.TextBox_scanSn.MaxLength = 15;
            this.TextBox_scanSn.Name = "TextBox_scanSn";
            this.TextBox_scanSn.Size = new System.Drawing.Size(278, 40);
            this.TextBox_scanSn.TabIndex = 9;
            this.TextBox_scanSn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_scanSn_KeyPress);
            // 
            // label_scanSn
            // 
            this.label_scanSn.AutoSize = true;
            this.label_scanSn.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_scanSn.ForeColor = System.Drawing.Color.Blue;
            this.label_scanSn.Location = new System.Drawing.Point(68, 85);
            this.label_scanSn.Name = "label_scanSn";
            this.label_scanSn.Size = new System.Drawing.Size(67, 33);
            this.label_scanSn.TabIndex = 8;
            this.label_scanSn.Text = "S/N :";
            // 
            // TextBox_scanCsn
            // 
            this.TextBox_scanCsn.BackColor = System.Drawing.Color.White;
            this.TextBox_scanCsn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_scanCsn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBox_scanCsn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_scanCsn.Location = new System.Drawing.Point(156, 161);
            this.TextBox_scanCsn.MaxLength = 12;
            this.TextBox_scanCsn.Name = "TextBox_scanCsn";
            this.TextBox_scanCsn.Size = new System.Drawing.Size(278, 40);
            this.TextBox_scanCsn.TabIndex = 17;
            this.TextBox_scanCsn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_scanCsn_KeyPress);
            // 
            // label_scanCsn
            // 
            this.label_scanCsn.AutoSize = true;
            this.label_scanCsn.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_scanCsn.ForeColor = System.Drawing.Color.Blue;
            this.label_scanCsn.Location = new System.Drawing.Point(63, 163);
            this.label_scanCsn.Name = "label_scanCsn";
            this.label_scanCsn.Size = new System.Drawing.Size(72, 33);
            this.label_scanCsn.TabIndex = 16;
            this.label_scanCsn.Text = "CSN:";
            // 
            // BarcodeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 249);
            this.Controls.Add(this.TextBox_scanCsn);
            this.Controls.Add(this.label_scanCsn);
            this.Controls.Add(this.TextBox_wono);
            this.Controls.Add(this.label_wono);
            this.Controls.Add(this.TextBox_opid);
            this.Controls.Add(this.label_opid);
            this.Controls.Add(this.TextBox_scanMac);
            this.Controls.Add(this.label_scanMac);
            this.Controls.Add(this.TextBox_scanSn);
            this.Controls.Add(this.label_scanSn);
            this.Name = "BarcodeScan";
            this.Text = "Bar-Code";
            this.Load += new System.EventHandler(this.BarcodeScan_Load);
            this.Shown += new System.EventHandler(this.BarcodeScan_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BarcodeScan_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_wono;
        private System.Windows.Forms.Label label_wono;
        private System.Windows.Forms.TextBox TextBox_opid;
        private System.Windows.Forms.Label label_opid;
        private System.Windows.Forms.TextBox TextBox_scanMac;
        private System.Windows.Forms.Label label_scanMac;
        private System.Windows.Forms.TextBox TextBox_scanSn;
        private System.Windows.Forms.Label label_scanSn;
        private System.Windows.Forms.TextBox TextBox_scanCsn;
        private System.Windows.Forms.Label label_scanCsn;
    }
}