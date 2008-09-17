namespace TwoOhApp
{
    partial class DaybookTray
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
            System.Windows.Forms.TabControl Menu;
            this.Contactstab = new System.Windows.Forms.TabPage();
            this.ContactsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Taskstab = new System.Windows.Forms.TabPage();
            this.Scheduledeventstab = new System.Windows.Forms.TabPage();
            this.Settingstab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTryContinuously = new System.Windows.Forms.CheckBox();
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TasksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EventsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LoginTimer = new System.Windows.Forms.Timer(this.components);
            this.SoapIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClubsTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.SettingTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.TSSOne = new System.Windows.Forms.ToolStripSeparator();
            this.ExitTSMI = new System.Windows.Forms.ToolStripMenuItem();
            Menu = new System.Windows.Forms.TabControl();
            Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsBindingSource)).BeginInit();
            this.Settingstab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsBindingSource)).BeginInit();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            Menu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            Menu.Controls.Add(this.Contactstab);
            Menu.Controls.Add(this.Taskstab);
            Menu.Controls.Add(this.Scheduledeventstab);
            Menu.Controls.Add(this.Settingstab);
            Menu.HotTrack = true;
            Menu.Location = new System.Drawing.Point(138, 63);
            Menu.Name = "Menu";
            Menu.Padding = new System.Drawing.Point(6, 10);
            Menu.SelectedIndex = 0;
            Menu.Size = new System.Drawing.Size(444, 381);
            Menu.TabIndex = 0;
            // 
            // Contactstab
            // 
            this.Contactstab.BackColor = System.Drawing.Color.White;
            this.Contactstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Contactstab.ImageKey = "(none)";
            this.Contactstab.Location = new System.Drawing.Point(4, 39);
            this.Contactstab.Name = "Contactstab";
            this.Contactstab.Padding = new System.Windows.Forms.Padding(3);
            this.Contactstab.Size = new System.Drawing.Size(436, 338);
            this.Contactstab.TabIndex = 0;
            this.Contactstab.Text = "    Contacts    ";
            this.Contactstab.ToolTipText = "View your contacts here. Any updates must be done in your account at www.daybook." +
                "in and these updates will be reflected here.";
            // 
            // Taskstab
            // 
            this.Taskstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Taskstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Taskstab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Taskstab.Location = new System.Drawing.Point(4, 39);
            this.Taskstab.Name = "Taskstab";
            this.Taskstab.Padding = new System.Windows.Forms.Padding(3);
            this.Taskstab.Size = new System.Drawing.Size(436, 338);
            this.Taskstab.TabIndex = 1;
            this.Taskstab.Text = "    Tasks   ";
            this.Taskstab.ToolTipText = "View your tasks here. Any updates must be done in your account at www.daybook.in " +
                "and these updates will be reflected here.";
            // 
            // Scheduledeventstab
            // 
            this.Scheduledeventstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Scheduledeventstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Scheduledeventstab.Location = new System.Drawing.Point(4, 39);
            this.Scheduledeventstab.Name = "Scheduledeventstab";
            this.Scheduledeventstab.Size = new System.Drawing.Size(436, 338);
            this.Scheduledeventstab.TabIndex = 2;
            this.Scheduledeventstab.Text = "   Scheduled Events   ";
            this.Scheduledeventstab.ToolTipText = "View your scheduled events here. Any updates must be done in your account at www." +
                "daybook.in and these updates will be reflected here.";
            // 
            // Settingstab
            // 
            this.Settingstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Settingstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Settingstab.Controls.Add(this.groupBox3);
            this.Settingstab.Controls.Add(this.groupBox2);
            this.Settingstab.Location = new System.Drawing.Point(4, 39);
            this.Settingstab.Name = "Settingstab";
            this.Settingstab.Padding = new System.Windows.Forms.Padding(3);
            this.Settingstab.Size = new System.Drawing.Size(436, 338);
            this.Settingstab.TabIndex = 3;
            this.Settingstab.Text = "          Settings          ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkTryContinuously);
            this.groupBox3.Controls.Add(this.chkAutostart);
            this.groupBox3.Location = new System.Drawing.Point(6, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 90);
            this.groupBox3.TabIndex = 7;
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
            this.chkTryContinuously.CheckedChanged += new System.EventHandler(this.chkTryContinuously_CheckedChanged);
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
            this.chkAutostart.CheckedChanged += new System.EventHandler(this.chkAutostart_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(6, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 82);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connectivity";
            // 
            // LoginTimer
            // 
            this.LoginTimer.Interval = 3600000;
            this.LoginTimer.Tick += new System.EventHandler(this.LoginTimer_Tick);
            // 
            // SoapIcon
            // 
            this.SoapIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.SoapIcon.BalloonTipText = "See the reflection of your Daybook here !";
            this.SoapIcon.BalloonTipTitle = "Aveo Daybook !";
            this.SoapIcon.ContextMenuStrip = this.RightClickMenu;
            this.SoapIcon.Text = "Aveo Daybook!";
            this.SoapIcon.Visible = true;
            this.SoapIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SoapIcon_MouseDoubleClick);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenTSMI,
            this.ClubsTSCB,
            this.SettingTSMI,
            this.AboutTSMI,
            this.TSSOne,
            this.ExitTSMI});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.RightClickMenu.Size = new System.Drawing.Size(187, 123);
            // 
            // OpenTSMI
            // 
            this.OpenTSMI.Name = "OpenTSMI";
            this.OpenTSMI.Size = new System.Drawing.Size(186, 22);
            this.OpenTSMI.Text = "Open Daybook Console";
            this.OpenTSMI.Click += new System.EventHandler(this.OpenTSMI_Click);
            // 
            // ClubsTSCB
            // 
            this.ClubsTSCB.Items.AddRange(new object[] {
            "My Club",
            "My Other Club"});
            this.ClubsTSCB.Name = "ClubsTSCB";
            this.ClubsTSCB.Size = new System.Drawing.Size(121, 21);
            this.ClubsTSCB.Text = "Clubs";
            // 
            // SettingTSMI
            // 
            this.SettingTSMI.Name = "SettingTSMI";
            this.SettingTSMI.Size = new System.Drawing.Size(186, 22);
            this.SettingTSMI.Text = "Settings ...";
            this.SettingTSMI.Click += new System.EventHandler(this.SettingTSMI_Click);
            // 
            // AboutTSMI
            // 
            this.AboutTSMI.Name = "AboutTSMI";
            this.AboutTSMI.Size = new System.Drawing.Size(186, 22);
            this.AboutTSMI.Text = "About";
            this.AboutTSMI.Click += new System.EventHandler(this.AboutTSMI_Click);
            // 
            // TSSOne
            // 
            this.TSSOne.Name = "TSSOne";
            this.TSSOne.Size = new System.Drawing.Size(183, 6);
            // 
            // ExitTSMI
            // 
            this.ExitTSMI.Name = "ExitTSMI";
            this.ExitTSMI.Size = new System.Drawing.Size(186, 22);
            this.ExitTSMI.Text = "Exit";
            this.ExitTSMI.Click += new System.EventHandler(this.ExitTSMI_Click);
            // 
            // DaybookTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TwoOhApp.DaybookTrayResources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(592, 446);
            this.ContextMenuStrip = this.RightClickMenu;
            this.Controls.Add(Menu);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 480);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "DaybookTray";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daybook Tray";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DaybookTray_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DaybookTray_FormClosing);
            Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContactsBindingSource)).EndInit();
            this.Settingstab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsBindingSource)).EndInit();
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Contactstab;
        private System.Windows.Forms.TabPage Taskstab;
        private System.Windows.Forms.TabPage Scheduledeventstab;
        private System.Windows.Forms.TabPage Settingstab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer LoginTimer;
        private System.Windows.Forms.BindingSource ContactsBindingSource;
        private System.Windows.Forms.BindingSource EventsBindingSource;
        private System.Windows.Forms.BindingSource TasksBindingSource;
        private System.Windows.Forms.NotifyIcon SoapIcon;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenTSMI;
        private System.Windows.Forms.ToolStripComboBox ClubsTSCB;
        private System.Windows.Forms.ToolStripMenuItem AboutTSMI;
        private System.Windows.Forms.ToolStripSeparator TSSOne;
        private System.Windows.Forms.ToolStripMenuItem ExitTSMI;
        private System.Windows.Forms.ToolStripMenuItem SettingTSMI;
        private System.Windows.Forms.CheckBox chkTryContinuously;
        private System.Windows.Forms.CheckBox chkAutostart;
    }
}