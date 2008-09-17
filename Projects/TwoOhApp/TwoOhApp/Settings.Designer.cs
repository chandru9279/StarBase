namespace TwoOhApp
{
    partial class Settings
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTryContinuously = new System.Windows.Forms.CheckBox();
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkTryContinuously);
            this.groupBox3.Controls.Add(this.chkAutostart);
            this.groupBox3.Location = new System.Drawing.Point(12, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 90);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Application";
            // 
            // chkTryContinuously
            // 
            this.chkTryContinuously.AutoSize = true;
            this.chkTryContinuously.Location = new System.Drawing.Point(6, 55);
            this.chkTryContinuously.Name = "chkTryContinuously";
            this.chkTryContinuously.Size = new System.Drawing.Size(120, 17);
            this.chkTryContinuously.TabIndex = 1;
            this.chkTryContinuously.Text = "Keep trying till Login";
            this.chkTryContinuously.UseVisualStyleBackColor = true;
            // 
            // chkAutostart
            // 
            this.chkAutostart.AutoSize = true;
            this.chkAutostart.Location = new System.Drawing.Point(6, 32);
            this.chkAutostart.Name = "chkAutostart";
            this.chkAutostart.Size = new System.Drawing.Size(134, 17);
            this.chkAutostart.TabIndex = 0;
            this.chkAutostart.Text = "Autostart with windows";
            this.chkAutostart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 82);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connectivity";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 348);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkTryContinuously;
        private System.Windows.Forms.CheckBox chkAutostart;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}