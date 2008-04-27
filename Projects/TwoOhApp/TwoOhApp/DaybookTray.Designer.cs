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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DaybookTray));
            this.Menu = new System.Windows.Forms.TabControl();
            this.Contactstab = new System.Windows.Forms.TabPage();
            this.lblHideTasksNotDue = new System.Windows.Forms.Label();
            this.lblHideCompTasks = new System.Windows.Forms.Label();
            this.lblCustomViewDays = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.ContactsGridView = new System.Windows.Forms.DataGridView();
            this.ContactsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Taskstab = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TasksGridView = new System.Windows.Forms.DataGridView();
            this.TasksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.Scheduledeventstab = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.EventsGridView = new System.Windows.Forms.DataGridView();
            this.EventsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Settingstab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTryContinuously = new System.Windows.Forms.CheckBox();
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Login = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginStatusPicture = new System.Windows.Forms.PictureBox();
            this.LoginStatus = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.LoginTimer = new System.Windows.Forms.Timer(this.components);
            this.SoapIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClubsTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.SettingTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.TSSOne = new System.Windows.Forms.ToolStripSeparator();
            this.ExitTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            this.Contactstab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsBindingSource)).BeginInit();
            this.Taskstab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TasksGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).BeginInit();
            this.Scheduledeventstab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsBindingSource)).BeginInit();
            this.Settingstab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginStatusPicture)).BeginInit();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Menu.Controls.Add(this.Contactstab);
            this.Menu.Controls.Add(this.Taskstab);
            this.Menu.Controls.Add(this.Scheduledeventstab);
            this.Menu.Controls.Add(this.Settingstab);
            this.Menu.Location = new System.Drawing.Point(140, 65);
            this.Menu.Multiline = true;
            this.Menu.Name = "Menu";
            this.Menu.SelectedIndex = 0;
            this.Menu.Size = new System.Drawing.Size(444, 381);
            this.Menu.TabIndex = 0;
            // 
            // Contactstab
            // 
            this.Contactstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Contactstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Contactstab.Controls.Add(this.lblHideTasksNotDue);
            this.Contactstab.Controls.Add(this.lblHideCompTasks);
            this.Contactstab.Controls.Add(this.lblCustomViewDays);
            this.Contactstab.Controls.Add(this.lblName);
            this.Contactstab.Controls.Add(this.label6);
            this.Contactstab.Controls.Add(this.label5);
            this.Contactstab.Controls.Add(this.label4);
            this.Contactstab.Controls.Add(this.label1);
            this.Contactstab.Controls.Add(this.pictureBox3);
            this.Contactstab.Controls.Add(this.ContactsGridView);
            this.Contactstab.ImageKey = "(none)";
            this.Contactstab.Location = new System.Drawing.Point(23, 4);
            this.Contactstab.Name = "Contactstab";
            this.Contactstab.Padding = new System.Windows.Forms.Padding(3);
            this.Contactstab.Size = new System.Drawing.Size(417, 373);
            this.Contactstab.TabIndex = 0;
            this.Contactstab.Text = "    Contacts    ";
            this.Contactstab.ToolTipText = "View your contacts here. Any updates must be done in your account at www.daybook." +
                "in and these updates will be reflected here.";
            // 
            // lblHideTasksNotDue
            // 
            this.lblHideTasksNotDue.AutoSize = true;
            this.lblHideTasksNotDue.Location = new System.Drawing.Point(185, 101);
            this.lblHideTasksNotDue.Name = "lblHideTasksNotDue";
            this.lblHideTasksNotDue.Size = new System.Drawing.Size(40, 13);
            this.lblHideTasksNotDue.TabIndex = 10;
            this.lblHideTasksNotDue.Text = "<data>";
            // 
            // lblHideCompTasks
            // 
            this.lblHideCompTasks.AutoSize = true;
            this.lblHideCompTasks.Location = new System.Drawing.Point(185, 72);
            this.lblHideCompTasks.Name = "lblHideCompTasks";
            this.lblHideCompTasks.Size = new System.Drawing.Size(40, 13);
            this.lblHideCompTasks.TabIndex = 9;
            this.lblHideCompTasks.Text = "<data>";
            // 
            // lblCustomViewDays
            // 
            this.lblCustomViewDays.AutoSize = true;
            this.lblCustomViewDays.Location = new System.Drawing.Point(185, 45);
            this.lblCustomViewDays.Name = "lblCustomViewDays";
            this.lblCustomViewDays.Size = new System.Drawing.Size(40, 13);
            this.lblCustomViewDays.TabIndex = 8;
            this.lblCustomViewDays.Text = "<data>";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(185, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(40, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "<data>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "HideTasksNotDueRecently : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "HideCompletedTasks : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "CustomViewDays : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name : ";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::TwoOhApp.DaybookTrayResources.UserPictureMale;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(329, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(83, 83);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // ContactsGridView
            // 
            this.ContactsGridView.AutoGenerateColumns = false;
            this.ContactsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ContactsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ContactsGridView.DataSource = this.ContactsBindingSource;
            this.ContactsGridView.Location = new System.Drawing.Point(1, 160);
            this.ContactsGridView.Name = "ContactsGridView";
            this.ContactsGridView.Size = new System.Drawing.Size(410, 210);
            this.ContactsGridView.TabIndex = 0;
            // 
            // Taskstab
            // 
            this.Taskstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Taskstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Taskstab.Controls.Add(this.pictureBox1);
            this.Taskstab.Controls.Add(this.TasksGridView);
            this.Taskstab.Controls.Add(this.monthCalendar1);
            this.Taskstab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Taskstab.Location = new System.Drawing.Point(23, 4);
            this.Taskstab.Name = "Taskstab";
            this.Taskstab.Padding = new System.Windows.Forms.Padding(3);
            this.Taskstab.Size = new System.Drawing.Size(417, 373);
            this.Taskstab.TabIndex = 1;
            this.Taskstab.Text = "    Tasks   ";
            this.Taskstab.ToolTipText = "View your tasks here. Any updates must be done in your account at www.daybook.in " +
                "and these updates will be reflected here.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::TwoOhApp.DaybookTrayResources.Task;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(331, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 63);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // TasksGridView
            // 
            this.TasksGridView.AutoGenerateColumns = false;
            this.TasksGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.TasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TasksGridView.DataSource = this.TasksBindingSource;
            this.TasksGridView.Location = new System.Drawing.Point(1, 160);
            this.TasksGridView.Name = "TasksGridView";
            this.TasksGridView.Size = new System.Drawing.Size(410, 210);
            this.TasksGridView.TabIndex = 1;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(-2, -2);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // Scheduledeventstab
            // 
            this.Scheduledeventstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Scheduledeventstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Scheduledeventstab.Controls.Add(this.pictureBox2);
            this.Scheduledeventstab.Controls.Add(this.EventsGridView);
            this.Scheduledeventstab.Location = new System.Drawing.Point(23, 4);
            this.Scheduledeventstab.Name = "Scheduledeventstab";
            this.Scheduledeventstab.Size = new System.Drawing.Size(417, 373);
            this.Scheduledeventstab.TabIndex = 2;
            this.Scheduledeventstab.Text = "   Scheduled Events   ";
            this.Scheduledeventstab.ToolTipText = "View your scheduled events here. Any updates must be done in your account at www." +
                "daybook.in and these updates will be reflected here.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::TwoOhApp.DaybookTrayResources.Date;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(331, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(76, 63);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // EventsGridView
            // 
            this.EventsGridView.AutoGenerateColumns = false;
            this.EventsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.EventsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventsGridView.DataSource = this.EventsBindingSource;
            this.EventsGridView.Location = new System.Drawing.Point(1, 160);
            this.EventsGridView.Name = "EventsGridView";
            this.EventsGridView.Size = new System.Drawing.Size(410, 210);
            this.EventsGridView.TabIndex = 1;
            // 
            // Settingstab
            // 
            this.Settingstab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Settingstab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Settingstab.Controls.Add(this.groupBox3);
            this.Settingstab.Controls.Add(this.groupBox2);
            this.Settingstab.Controls.Add(this.groupBox1);
            this.Settingstab.Location = new System.Drawing.Point(23, 4);
            this.Settingstab.Name = "Settingstab";
            this.Settingstab.Padding = new System.Windows.Forms.Padding(3);
            this.Settingstab.Size = new System.Drawing.Size(417, 373);
            this.Settingstab.TabIndex = 3;
            this.Settingstab.Text = "          Settings          ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkTryContinuously);
            this.groupBox3.Controls.Add(this.chkAutostart);
            this.groupBox3.Location = new System.Drawing.Point(6, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 170);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Login);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 84);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(296, 15);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(80, 58);
            this.Login.TabIndex = 4;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(108, 53);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(165, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(108, 15);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(165, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username :";
            // 
            // LoginStatusPicture
            // 
            this.LoginStatusPicture.BackColor = System.Drawing.Color.Transparent;
            this.LoginStatusPicture.BackgroundImage = global::TwoOhApp.DaybookTrayResources.NotLogggedIn;
            this.LoginStatusPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LoginStatusPicture.Location = new System.Drawing.Point(12, 12);
            this.LoginStatusPicture.Name = "LoginStatusPicture";
            this.LoginStatusPicture.Size = new System.Drawing.Size(76, 63);
            this.LoginStatusPicture.TabIndex = 1;
            this.LoginStatusPicture.TabStop = false;
            this.LoginStatusPicture.Click += new System.EventHandler(this.LoginStatusPicture_Click);
            // 
            // LoginStatus
            // 
            this.LoginStatus.AutoSize = true;
            this.LoginStatus.BackColor = System.Drawing.Color.Transparent;
            this.LoginStatus.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginStatus.ForeColor = System.Drawing.Color.Red;
            this.LoginStatus.Location = new System.Drawing.Point(94, 29);
            this.LoginStatus.Name = "LoginStatus";
            this.LoginStatus.Size = new System.Drawing.Size(126, 20);
            this.LoginStatus.TabIndex = 2;
            this.LoginStatus.Text = "Not Logged In";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMessage.Location = new System.Drawing.Point(381, 44);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(191, 16);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Login through the Settings Tab!";
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
            this.SoapIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SoapIcon.Icon")));
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
            this.RightClickMenu.Size = new System.Drawing.Size(200, 125);
            // 
            // OpenTSMI
            // 
            this.OpenTSMI.Name = "OpenTSMI";
            this.OpenTSMI.Size = new System.Drawing.Size(199, 22);
            this.OpenTSMI.Text = "Open Daybook Console";
            this.OpenTSMI.Click += new System.EventHandler(this.OpenTSMI_Click);
            // 
            // ClubsTSCB
            // 
            this.ClubsTSCB.Items.AddRange(new object[] {
            "My Club",
            "My Other Club"});
            this.ClubsTSCB.Name = "ClubsTSCB";
            this.ClubsTSCB.Size = new System.Drawing.Size(121, 23);
            this.ClubsTSCB.Text = "Clubs";
            // 
            // SettingTSMI
            // 
            this.SettingTSMI.Name = "SettingTSMI";
            this.SettingTSMI.Size = new System.Drawing.Size(199, 22);
            this.SettingTSMI.Text = "Settings ...";
            this.SettingTSMI.Click += new System.EventHandler(this.SettingTSMI_Click);
            // 
            // AboutTSMI
            // 
            this.AboutTSMI.Name = "AboutTSMI";
            this.AboutTSMI.Size = new System.Drawing.Size(199, 22);
            this.AboutTSMI.Text = "About";
            this.AboutTSMI.Click += new System.EventHandler(this.AboutTSMI_Click);
            // 
            // TSSOne
            // 
            this.TSSOne.Name = "TSSOne";
            this.TSSOne.Size = new System.Drawing.Size(196, 6);
            // 
            // ExitTSMI
            // 
            this.ExitTSMI.Name = "ExitTSMI";
            this.ExitTSMI.Size = new System.Drawing.Size(199, 22);
            this.ExitTSMI.Text = "Exit";
            this.ExitTSMI.Click += new System.EventHandler(this.ExitTSMI_Click);
            // 
            // DaybookTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TwoOhApp.DaybookTrayResources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(584, 444);
            this.ContextMenuStrip = this.RightClickMenu;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.LoginStatus);
            this.Controls.Add(this.LoginStatusPicture);
            this.Controls.Add(this.Menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 480);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "DaybookTray";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DaybookTray";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DaybookTray_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DaybookTray_FormClosing);
            this.Menu.ResumeLayout(false);
            this.Contactstab.ResumeLayout(false);
            this.Contactstab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsBindingSource)).EndInit();
            this.Taskstab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TasksGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).EndInit();
            this.Scheduledeventstab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventsBindingSource)).EndInit();
            this.Settingstab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginStatusPicture)).EndInit();
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Menu;
        private System.Windows.Forms.TabPage Contactstab;
        private System.Windows.Forms.TabPage Taskstab;
        private System.Windows.Forms.TabPage Scheduledeventstab;
        private System.Windows.Forms.TabPage Settingstab;
        private System.Windows.Forms.PictureBox LoginStatusPicture;
        private System.Windows.Forms.Label LoginStatus;
        private System.Windows.Forms.DataGridView TasksGridView;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer LoginTimer;
        private System.Windows.Forms.DataGridView ContactsGridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView EventsGridView;
        private System.Windows.Forms.BindingSource ContactsBindingSource;
        private System.Windows.Forms.BindingSource EventsBindingSource;
        private System.Windows.Forms.BindingSource TasksBindingSource;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHideTasksNotDue;
        private System.Windows.Forms.Label lblHideCompTasks;
        private System.Windows.Forms.Label lblCustomViewDays;
        private System.Windows.Forms.Label lblName;
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