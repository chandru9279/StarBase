namespace TwoOhApp
{
    partial class Home
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
            this.LoginStatusPicture = new System.Windows.Forms.PictureBox();
            this.LoginStatus = new System.Windows.Forms.Label();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lnklblOpenDaybook = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LoginStatusPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginStatusPicture
            // 
            this.LoginStatusPicture.BackColor = System.Drawing.Color.Transparent;
            this.LoginStatusPicture.BackgroundImage = global::TwoOhApp.DaybookTrayResources.NotLogggedIn;
            this.LoginStatusPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LoginStatusPicture.Location = new System.Drawing.Point(138, 136);
            this.LoginStatusPicture.Name = "LoginStatusPicture";
            this.LoginStatusPicture.Size = new System.Drawing.Size(39, 37);
            this.LoginStatusPicture.TabIndex = 2;
            this.LoginStatusPicture.TabStop = false;
            // 
            // LoginStatus
            // 
            this.LoginStatus.AutoSize = true;
            this.LoginStatus.BackColor = System.Drawing.Color.Transparent;
            this.LoginStatus.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginStatus.ForeColor = System.Drawing.Color.Red;
            this.LoginStatus.Location = new System.Drawing.Point(182, 145);
            this.LoginStatus.Name = "LoginStatus";
            this.LoginStatus.Size = new System.Drawing.Size(126, 20);
            this.LoginStatus.TabIndex = 3;
            this.LoginStatus.Text = "Not Logged In";
            // 
            // lblMessage1
            // 
            this.lblMessage1.AutoEllipsis = true;
            this.lblMessage1.AutoSize = true;
            this.lblMessage1.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMessage1.Location = new System.Drawing.Point(12, 310);
            this.lblMessage1.MaximumSize = new System.Drawing.Size(500, 16);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(500, 16);
            this.lblMessage1.TabIndex = 4;
            this.lblMessage1.Text = "Daybook On Tray brings your valuable business information right on to the desktop" +
                ".";
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoEllipsis = true;
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblMessage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMessage2.Location = new System.Drawing.Point(12, 335);
            this.lblMessage2.MaximumSize = new System.Drawing.Size(500, 16);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(405, 16);
            this.lblMessage2.TabIndex = 5;
            this.lblMessage2.Text = "It saves you time, by giving you easy access to your daybook data!";
            // 
            // lnklblOpenDaybook
            // 
            this.lnklblOpenDaybook.AutoSize = true;
            this.lnklblOpenDaybook.BackColor = System.Drawing.Color.Transparent;
            this.lnklblOpenDaybook.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnklblOpenDaybook.LinkColor = System.Drawing.Color.PapayaWhip;
            this.lnklblOpenDaybook.Location = new System.Drawing.Point(416, 49);
            this.lnklblOpenDaybook.Name = "lnklblOpenDaybook";
            this.lnklblOpenDaybook.Size = new System.Drawing.Size(88, 13);
            this.lnklblOpenDaybook.TabIndex = 6;
            this.lnklblOpenDaybook.TabStop = true;
            this.lnklblOpenDaybook.Text = "Go to daybook.in";
            this.lnklblOpenDaybook.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblOpenDaybook_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(12, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(407, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sign into your daybook using the blue Daybook button in the toolbar";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TwoOhApp.DaybookTrayResources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(516, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnklblOpenDaybook);
            this.Controls.Add(this.lblMessage2);
            this.Controls.Add(this.lblMessage1);
            this.Controls.Add(this.LoginStatus);
            this.Controls.Add(this.LoginStatusPicture);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LoginStatusPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LoginStatusPicture;
        private System.Windows.Forms.Label LoginStatus;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.LinkLabel lnklblOpenDaybook;
        private System.Windows.Forms.Label label1;
    }
}