using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aveo.Daybook.TrayClient;

namespace TwoOhApp
{
    /// <summary>
    /// The Main MDI Form : Parent form for the entire App
    /// </summary>
    public partial class DaybookOnTray : Form
    {
        private int childFormNumber = 0;

        /// <summary>
        /// The Zero Argument Constructor for the MDI Form
        /// </summary>
        public DaybookOnTray()
        {
            InitializeComponent();
            new ServiceAdapter();
            new SettingsAdapter();  
        }

        #region Standard Menu & Toolbar Items

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            MainContainer.ContentPanel.Controls.Add(childForm);            
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }        

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Essential Menu & Toolbar Items

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DaybookToolStrip.Visible = toolBarToolStripMenuItem.Checked;
            StandardToolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        private void DaybookOnTray_Load(object sender, EventArgs e)
        {
            LoadForm(new Home());
        }        

        private void tsbSignIn_Click(object sender, EventArgs e)
        {
            LoadForm(new Login());
        }
        
        private void LoadForm(Form formtype)
        {
            formtype.MdiParent = this;
            MainContainer.ContentPanel.Controls.Add(formtype);
            formtype.Show();
            SwitchWindowCombo.Items.Add(formtype.Text);
            formtype.GotFocus += new EventHandler(formtype_GotFocus);
            formtype.FormClosed += new FormClosedEventHandler(formtype_FormClosed);
        }

        void formtype_FormClosed(object sender, FormClosedEventArgs e)
        {
            SwitchWindowCombo.Items.Remove(((Form)sender).Text);
        }

        void formtype_GotFocus(object sender, EventArgs e)
        {
            SwitchWindowCombo.SelectedItem = formtype.Text;
        }       
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Home)
                    MessageBox.Show(frm.Location.ToString());
            }
        }
    }
}
