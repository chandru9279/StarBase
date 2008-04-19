namespace ThonSettingsSet
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.LoadSettings = new System.Windows.Forms.Button();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.SettingsNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.nameValueBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.SettingsGrid = new System.Windows.Forms.DataGridView();
            this.Memos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoadBlogSettings = new System.Windows.Forms.Button();
            this.SaveBlogSettings = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingsSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsNavigator)).BeginInit();
            this.SettingsNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadSettings
            // 
            this.LoadSettings.Location = new System.Drawing.Point(12, 418);
            this.LoadSettings.Name = "LoadSettings";
            this.LoadSettings.Size = new System.Drawing.Size(90, 25);
            this.LoadSettings.TabIndex = 0;
            this.LoadSettings.Text = "LoadSettings";
            this.LoadSettings.UseVisualStyleBackColor = true;
            this.LoadSettings.Click += new System.EventHandler(this.LoadSettings_Click);
            // 
            // SaveSettings
            // 
            this.SaveSettings.Location = new System.Drawing.Point(108, 418);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(90, 24);
            this.SaveSettings.TabIndex = 2;
            this.SaveSettings.Text = "SaveSettings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // SettingsNavigator
            // 
            this.SettingsNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.SettingsNavigator.BindingSource = this.SettingsSource;
            this.SettingsNavigator.CountItem = this.bindingNavigatorCountItem;
            this.SettingsNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.SettingsNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.nameValueBindingNavigatorSaveItem});
            this.SettingsNavigator.Location = new System.Drawing.Point(0, 0);
            this.SettingsNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.SettingsNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.SettingsNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.SettingsNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.SettingsNavigator.Name = "SettingsNavigator";
            this.SettingsNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.SettingsNavigator.Size = new System.Drawing.Size(627, 25);
            this.SettingsNavigator.TabIndex = 3;
            this.SettingsNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // nameValueBindingNavigatorSaveItem
            // 
            this.nameValueBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nameValueBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("nameValueBindingNavigatorSaveItem.Image")));
            this.nameValueBindingNavigatorSaveItem.Name = "nameValueBindingNavigatorSaveItem";
            this.nameValueBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.nameValueBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // SettingsGrid
            // 
            this.SettingsGrid.AutoGenerateColumns = false;
            this.SettingsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SettingsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SettingsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.SettingsGrid.DataSource = this.SettingsSource;
            this.SettingsGrid.Location = new System.Drawing.Point(0, 28);
            this.SettingsGrid.Name = "SettingsGrid";
            this.SettingsGrid.Size = new System.Drawing.Size(627, 300);
            this.SettingsGrid.TabIndex = 3;
            // 
            // Memos
            // 
            this.Memos.Location = new System.Drawing.Point(12, 331);
            this.Memos.Name = "Memos";
            this.Memos.Size = new System.Drawing.Size(568, 46);
            this.Memos.TabIndex = 4;
            this.Memos.Text = resources.GetString("Memos.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ThonSettings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 402);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "BlogSettings";
            // 
            // LoadBlogSettings
            // 
            this.LoadBlogSettings.Location = new System.Drawing.Point(465, 417);
            this.LoadBlogSettings.Name = "LoadBlogSettings";
            this.LoadBlogSettings.Size = new System.Drawing.Size(90, 24);
            this.LoadBlogSettings.TabIndex = 8;
            this.LoadBlogSettings.Text = "LoadSettings";
            this.LoadBlogSettings.UseVisualStyleBackColor = true;
            this.LoadBlogSettings.Click += new System.EventHandler(this.LoadBlogSettings_Click);
            // 
            // SaveBlogSettings
            // 
            this.SaveBlogSettings.Location = new System.Drawing.Point(369, 417);
            this.SaveBlogSettings.Name = "SaveBlogSettings";
            this.SaveBlogSettings.Size = new System.Drawing.Size(90, 25);
            this.SaveBlogSettings.TabIndex = 7;
            this.SaveBlogSettings.Text = "SaveSettings";
            this.SaveBlogSettings.UseVisualStyleBackColor = true;
            this.SaveBlogSettings.Click += new System.EventHandler(this.SaveBlogSettings_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // SettingsSource
            // 
            this.SettingsSource.DataSource = typeof(ThonSettingsSet.NameValue);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(509, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Thon Settings and Blog Settings are changed to throw exact errors instead of Conf" +
                "igurationErrorsException";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 463);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoadBlogSettings);
            this.Controls.Add(this.SaveBlogSettings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Memos);
            this.Controls.Add(this.SettingsGrid);
            this.Controls.Add(this.SettingsNavigator);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.LoadSettings);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.SettingsNavigator)).EndInit();
            this.SettingsNavigator.ResumeLayout(false);
            this.SettingsNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadSettings;
        private System.Windows.Forms.Button SaveSettings;
        private System.Windows.Forms.BindingSource SettingsSource;
        private System.Windows.Forms.BindingNavigator SettingsNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton nameValueBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView SettingsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label Memos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoadBlogSettings;
        private System.Windows.Forms.Button SaveBlogSettings;
        private System.Windows.Forms.Label label3;
    }
}

