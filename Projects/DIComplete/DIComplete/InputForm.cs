using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DIComplete
{
    public partial class InputForm : Form
    {
        private Input inp;   
        public InputForm()
        {
            InitializeComponent();            
        }

        public Input INP 
        {
            get { return inp; }
            set { inp = value; }
        }

        public void UpdateDS()
        {
            inputBindingSource.DataSource = inp.transactions;
            inputDataGridView.Refresh();
        }

        private void gridViewStyleSetter()
        {
            DataGridViewCellStyle cs = new DataGridViewCellStyle();
            cs.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
            cs.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            cs.SelectionBackColor = System.Drawing.Color.FromArgb(0, 150, 0);
            cs.SelectionForeColor = System.Drawing.Color.FromArgb(0, 0, 0);            
            inputDataGridView.AlternatingRowsDefaultCellStyle = cs;
            inputDataGridView.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBox1.Text = inp.principal.ToString();
            gridViewStyleSetter();
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    switch (toolStripComboBox1.SelectedIndex)
                    {
                        case 0:
                            inp.principal = double.Parse(toolStripTextBox1.Text);
                            break;
                        case 1:
                            inp.startRate = double.Parse(toolStripTextBox1.Text);
                            break;
                        case 2:
                            inp.startDate = DateTime.Parse(toolStripTextBox1.Text);
                            break;
                        case 3:
                            inp.endDate = DateTime.Parse(toolStripTextBox1.Text);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Value Unacceptable");   
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    toolStripTextBox1.Text = inp.principal.ToString();
                    break;
                case 1:                 
                    toolStripTextBox1.Text = inp.startRate.ToString();
                    break;
                case 2:
                    toolStripTextBox1.Text = inp.startDate.ToString("d");
                    break;
                case 3:
                    toolStripTextBox1.Text = inp.endDate.ToString("d");
                    break;
                default:
                    break;
            }
        }      
    }
}
