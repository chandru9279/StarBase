using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TwoOhApp
{
    public partial class Home : Form
    {
        /// <summary>
        /// The Zero Argument Constructor for the Home Form
        /// </summary>
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.Location = new Point(20, 30);
        }

        private void lnklblOpenDaybook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.daybook.in");
        }
    }
}
