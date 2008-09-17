namespace TwoOhApp
{
    partial class Contacts
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
            this.lblHideTasksNotDue = new System.Windows.Forms.Label();
            this.lblHideCompTasks = new System.Windows.Forms.Label();
            this.lblCustomViewDays = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ContactsGridView = new System.Windows.Forms.DataGridView();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHideTasksNotDue
            // 
            this.lblHideTasksNotDue.AutoSize = true;
            this.lblHideTasksNotDue.Location = new System.Drawing.Point(196, 113);
            this.lblHideTasksNotDue.Name = "lblHideTasksNotDue";
            this.lblHideTasksNotDue.Size = new System.Drawing.Size(40, 13);
            this.lblHideTasksNotDue.TabIndex = 20;
            this.lblHideTasksNotDue.Text = "<data>";
            // 
            // lblHideCompTasks
            // 
            this.lblHideCompTasks.AutoSize = true;
            this.lblHideCompTasks.Location = new System.Drawing.Point(196, 84);
            this.lblHideCompTasks.Name = "lblHideCompTasks";
            this.lblHideCompTasks.Size = new System.Drawing.Size(40, 13);
            this.lblHideCompTasks.TabIndex = 19;
            this.lblHideCompTasks.Text = "<data>";
            // 
            // lblCustomViewDays
            // 
            this.lblCustomViewDays.AutoSize = true;
            this.lblCustomViewDays.Location = new System.Drawing.Point(196, 57);
            this.lblCustomViewDays.Name = "lblCustomViewDays";
            this.lblCustomViewDays.Size = new System.Drawing.Size(40, 13);
            this.lblCustomViewDays.TabIndex = 18;
            this.lblCustomViewDays.Text = "<data>";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(196, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(40, 13);
            this.lblName.TabIndex = 17;
            this.lblName.Text = "<data>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "HideTasksNotDueRecently : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "HideCompletedTasks : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "CustomViewDays : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name : ";
            // 
            // ContactsGridView
            // 
            this.ContactsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ContactsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ContactsGridView.Location = new System.Drawing.Point(12, 172);
            this.ContactsGridView.Name = "ContactsGridView";
            this.ContactsGridView.Size = new System.Drawing.Size(410, 210);
            this.ContactsGridView.TabIndex = 11;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::TwoOhApp.DaybookTrayResources.UserPictureMale;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(340, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(83, 83);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // Contacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 426);
            this.Controls.Add(this.lblHideTasksNotDue);
            this.Controls.Add(this.lblHideCompTasks);
            this.Controls.Add(this.lblCustomViewDays);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ContactsGridView);
            this.Name = "Contacts";
            this.Text = "Contacts";
            ((System.ComponentModel.ISupportInitialize)(this.ContactsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHideTasksNotDue;
        private System.Windows.Forms.Label lblHideCompTasks;
        private System.Windows.Forms.Label lblCustomViewDays;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.DataGridView ContactsGridView;
    }
}