using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using xlexport;

namespace DIComplete
{    
	public partial class Window1
	{
        bool outputgot;
        bool enteringdata;
        int TabStatus;
        InputTransaction transaction;
        Input input;
        Output output;
        System.Windows.Forms.OpenFileDialog o;
        InputForm ifo;
        int totaltransactions = 0;
		public Window1()
		{   
			this.InitializeComponent();
            enteringdata = false;
            TabStatus = 0;
            outputgot = false;
		}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initiate();
        }
        private void initiate()
        {
            List<StartingGrid> list = new List<StartingGrid>();
            ifo = new InputForm();
            list.Add(new StartingGrid("Hi,", ""));
            list.Add(new StartingGrid("This", "is"));
            list.Add(new StartingGrid("THE", ""));
            list.Add(new StartingGrid("Daily", "Interest"));
            list.Add(new StartingGrid("Calculator", ""));
            list.Add(new StartingGrid("Press", "START"));
            list.Add(new StartingGrid("to", "begin!"));
            GreenTab.SelectedIndex = 0;
            InLabel1.Content = "Transaction Amount (Rs.)";
            InLabel2.Content = "Transaction date (d/m/yyyy)";            
            GreenTabCHANGER(false);
            InGridCHANGER(false);
            OutGridCHANGER(false);            
            o = new System.Windows.Forms.OpenFileDialog();
            MyMediaControl.mediaelement.Stretch = Stretch.UniformToFill;
            MyMediaControl.mediaelement.Play();
            //  <Comment to Blend>
            gridViewStyleSetter();
            gridViewConcerning(list, list);
            MultiButton.initobject(this);
            //  </Comment to Blend>
        }
        internal class StartingGrid
        {
            public StartingGrid(string n1, string n2)
            {
                this.n1 = n1;
                this.n2 = n2;
            }
            private string n1,n2;

            public string Daily
            {
                get { return n1; }
                set { n1 = value; }
            }
            public string Calculator
            {
                get { return n2; }
                set { n2 = value; }
            }
        }        
        private void GlowButton_Click(object sender, RoutedEventArgs e)
        {
            if (enteringdata)
            {              
                GlowButton.Content = "START";
                MyMediaControl.Visibility = Visibility.Visible;
                MyMediaControl.mediaelement.Play();                
                OutGridCHANGER(false);
                GreenTabCHANGER(false);
                InGridCHANGER(false);
                enteringdata = false;
                emptyalltextboxes();
                ActualStatus.Content = "Cancelled ...";
                totaltransactions = 0;
                outputgot = false;
                GlowButton.IsTabStop = true;
            }
            else
            {
                MyMediaControl.mediaelement.Stop();
                MyMediaControl.Visibility = Visibility.Hidden;
                GlowButton.Content = "Cancel";
                OutGridCHANGER(true);
                enteringdata = true;
                ActualStatus.Content = "Start ...";
                input = new Input();
                input.transactions = new List<InputTransaction>();
                outputgot = false;
                GlowButton.IsTabStop = false;
            }
        }
        private void GlowNext_Click(object sender, RoutedEventArgs e)
        {
            transaction = new InputTransaction();
            if (OutgridValidate())
            {
                OutGridCHANGER(false);
                InGridCHANGER(true);
                GreenTabCHANGER(true);                
                ActualStatus.Content = "Waiting for transaction details ...";
                System.Windows.Controls.TabItem i =(System.Windows.Controls.TabItem)GreenTab.Items[0];
                i.Focus();
            }
            else
                System.Windows.MessageBox.Show("Enter Correct Details"); 
        }
        public void MultibuttonAddClick()
        {
            if (IngridValidate())
            {
                emptyalltextboxes();
                totaltransactions++;
                switch (TabStatus)
                {
                    case 0:
                        transaction.type = TransactionType.Credit;
                        ActualStatus.Content = "Credit Transaction Added ... Total :" + totaltransactions.ToString();
                        break;
                    case 1:
                        transaction.type = TransactionType.Debit;
                        ActualStatus.Content = "Debit Transaction Added ..." + totaltransactions.ToString();
                        break;
                    case 2:
                        transaction.type = TransactionType.Rate;
                        ActualStatus.Content = "Rate Transaction Added ..." + totaltransactions.ToString();
                        break;
                }
                input.transactions.Add(transaction);                
                transaction = new InputTransaction();
            }
            else
            {
                System.Windows.MessageBox.Show("Enter Correct Details");
                ActualStatus.Content = "Validation failed ...";
            }
        }
        internal void MultibuttonBackClick()
        {
            InputTransaction tb;
            try
            {
                tb = input.transactions[input.transactions.Count - 1];
                InText1.Text = tb.amount.ToString();
                InText2.Text = tb.date.ToString();
                switch (tb.type)
                {
                    case TransactionType.Credit:
                        GreenTab.SelectedIndex = 0;
                        break;
                    case TransactionType.Debit:
                        GreenTab.SelectedIndex = 1;
                        break;
                    case TransactionType.Rate:
                        GreenTab.SelectedIndex = 2;
                        break;
                }
                input.transactions.RemoveAt(input.transactions.Count - 1);
                totaltransactions--;
                ActualStatus.Content = "Deleted the most recent ... Total :" + totaltransactions.ToString();
            }            
            catch (Exception)
            {
                RoutedEventArgs w= new RoutedEventArgs();
                GlowButton_Click(this,w);
            }
        }
        public void MultibuttonFinishClick()
        {
            try
            {
                output = MainProgram.execMain(input);
                DisplayOutput(output);
                outputgot = true;
                emptyalltextboxes();
            }
            catch (Exception)
            {System.Windows.Forms.MessageBox.Show("Unknown Exception");}
            GlowButton.Content = "START";
            enteringdata = false;
            GreenTabCHANGER(false);
            InGridCHANGER(false);
            OutGridCHANGER(false);            
            MyMediaControl.Visibility = Visibility.Visible;            
            MyMediaControl.mediaelement.Play();
            ActualStatus.Content = "Output !";
            totaltransactions = 0;
            
        }
        private bool IngridValidate()
        {
            if (InText1.Text.Equals("") || InText2.Text.Equals(""))
            {
                ActualStatus.Content = "Validation failed : Some fields are empty";
                return false;
            }
            try
            {transaction.amount = double.Parse(InText1.Text);}
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : Amount"; return false; }
            try
            { transaction.date = DateTime.Parse(InText2.Text); }
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : Date"; return false; } 
            return true;
        }
        private bool OutgridValidate()
        {
            if (OutText1.Text.Equals("") || OutText2.Text.Equals("") || OutText3.Text.Equals("") || OutText4.Text.Equals(""))
            {
                ActualStatus.Content = "Validation failed : Some fields are empty";
                return false;
            }
            try
            {input.principal = double.Parse(OutText1.Text);}
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : Principal"; return false; }
            try
            { input.startRate = double.Parse(OutText2.Text); }
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : StartRate"; return false; } 
            try
            { input.startDate = DateTime.Parse(OutText3.Text); }
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : StartDate"; return false; }
            try
            { input.endDate = DateTime.Parse(OutText4.Text); }
            catch (Exception parseException)
            { ActualStatus.Content = "Validation failed : EndDate"; return false; }
            return true;
        }
        private void emptyalltextboxes()
        {
            OutText1.Clear();
            OutText2.Clear();
            OutText3.Clear();
            OutText4.Clear();
            InText1.Clear();
            InText2.Clear();
        }
        private void GreenTabCHANGER(bool show)
        {
            if (show)
            {
                GreenTab.Visibility = Visibility.Visible;
                GreenTab.IsEnabled = true;
            }
            else
            {
                GreenTab.Visibility = Visibility.Hidden;
                GreenTab.IsEnabled = false;
            }
        }
        private void InGridCHANGER(bool show)
        {
            if (show)
            {
                InGrid.Visibility = Visibility.Visible;
                InGrid.IsEnabled = true;
                MultiButton.IsEnabled = true;
                InText1.IsEnabled = true;
                InText2.IsEnabled = true;
                    
            }
            else
            {
                InGrid.Visibility = Visibility.Hidden;
                InGrid.IsEnabled = false;
                MultiButton.IsEnabled = false;
                InText1.IsEnabled = false;
                InText2.IsEnabled = false;
            }
        }
        private void OutGridCHANGER(bool show)
        {
            if (show)
            {
                OutGrid.Visibility = Visibility.Visible;
                OutGrid.IsEnabled = true;
                OutText1.IsEnabled = true;
                OutText2.IsEnabled = true;
                OutText3.IsEnabled = true;
                OutText4.IsEnabled = true;
                GlowNext.IsEnabled = true;                
            }
            else
            {
                OutGrid.Visibility = Visibility.Hidden;
                OutGrid.IsEnabled = false;
                OutText1.IsEnabled = false;
                OutText2.IsEnabled = false;
                OutText3.IsEnabled = false;
                OutText4.IsEnabled = false;
                GlowNext.IsEnabled = false;
            }
        }        
        private void GreenTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabStatus = GreenTab.SelectedIndex;
            if (TabStatus == 2)
            {
                InLabel1.Content = "New Daily Interest Rate (%)";
                InLabel2.Content = "Rate Change Date (m/d/yyyy)";
            }
            else
            {
                InLabel1.Content = "Transaction Amount (Rs.)";
                InLabel2.Content = "Transaction date (m/d/yyyy)";
            }
        }
        private void DisplayOutput(Output output)
        {
            ActualBalance.Content = output.balancePrincipal.ToString();           
            //IEnumerator<OutputTransaction> s = from ot in output.transactions
            //                                   join oi in output.interimInterests 
            //                                   on 
            gridViewConcerning(output.interimInterests, output.transactions);
        }

        private bool own = false, too = false, thee = false, foar = false;
        private void OutText1_MouseDoubleClick(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            own = true;
            if ((own && too) || (thee && foar) || (too && foar) || (thee && own) || (own && foar))
            {   
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MyMediaControl.mediaelement.Stop();
                    MyMediaControl.mediaelement.Source = new Uri(o.FileName);                    
                    MyMediaControl.mediaelement.Play();
                }                
            }
        }
        private void OutText2_MouseDoubleClick(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            too = true;
            if ((own && too) || (thee && foar) || (too && foar) || (thee && own) || (own && foar))
            {
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MyMediaControl.mediaelement.Stop();
                    MyMediaControl.mediaelement.Source = new Uri(o.FileName);
                    MyMediaControl.mediaelement.Play();
                }
            }

        }
        private void OutText3_MouseDoubleClick(object sender, 
            System.Windows.Input.MouseButtonEventArgs e)
        {
            thee = true;
            if ((own && too) || (thee && foar) || (too && foar) || (thee && own) || (own && foar))
            {
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MyMediaControl.mediaelement.Stop();
                    MyMediaControl.mediaelement.Source = new Uri(o.FileName);
                    MyMediaControl.mediaelement.Play();
                }
            }

        }
        private void OutText4_MouseDoubleClick(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            foar = true;
            if ((own && too) || (thee && foar) || (too && foar) || (thee && own) || (own && foar))
            {
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MyMediaControl.mediaelement.Stop();
                    MyMediaControl.mediaelement.Source = new Uri(o.FileName);
                    MyMediaControl.mediaelement.Play();
                }
            }

        }

        private void gridViewConcerning(object dataSource1, object dataSource2)
        {
            gridView1.AutoGenerateColumns = true;
            gridView2.AutoGenerateColumns = true;
            gridView1.DataSource = dataSource1;            
            gridView1.Refresh();
            gridView2.DataSource = dataSource2;
            gridView2.Refresh();
        }
        private void gridViewStyleSetter()
        {
            DataGridViewCellStyle cs = new DataGridViewCellStyle();
            cs.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
            cs.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            cs.SelectionBackColor = System.Drawing.Color.FromArgb(0, 150, 0);
            cs.SelectionForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            gridView1.AlternatingRowsDefaultCellStyle = cs;
            gridView2.AlternatingRowsDefaultCellStyle = cs;
        }
        //XL_Click Contains the TestInput() call
        private void XL_Click(object sender, RoutedEventArgs e)
        {
            /*Test EXCEL*/
            //input = TestInput();
            //output = MainProgram.execMain(input);
            //DisplayOutput(output);
            //outputgot = true;
            //ActualStatus.Content = "Testing";
            /*Test*/

            if (outputgot)
            {
                string fname;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.OverwritePrompt = true;
                sfd.AddExtension = true;
                //The filter string must contain a description of the filter, followed by the vertical bar (|) and the filter pattern. The strings for different filtering options must also be separated by the vertical bar. Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                sfd.Filter = "Excel Compatible XML for DIC(*.xml)|*.xml|Text files (*.txt)|*.txt";
                sfd.FilterIndex = 0;
                sfd.CheckPathExists = true;
                sfd.DereferenceLinks = true;
                sfd.ShowHelp = false;
                sfd.SupportMultiDottedExtensions = false;
                sfd.Title = "Choose a location for your Excel file ..";
                sfd.ValidateNames = true;
                DialogResult r = sfd.ShowDialog();
                if (!(r.ToString().Equals("Cancel")))
                {
                    fname = sfd.FileName;
                    int ret = ExportToExcel(input, output, fname);
                    if (ret == -1) System.Windows.Forms.MessageBox.Show("Problem with conversion!");
                    if (ret == -2) System.Windows.Forms.MessageBox.Show("Problem with filename or path!");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Get an output first!");
                ActualStatus.Content = "No data available. Can't export...";
            }
        }
        private Input TestInput()
        {
            Input testinput = new Input();
            testinput.transactions = new List<InputTransaction>();
            testinput.endDate = new DateTime(2007, 11, 3);
            testinput.principal = 1240000;
            testinput.startDate = new DateTime(2007, 8, 1);
            testinput.startRate = 14.75;
            InputTransaction it = new InputTransaction();
            it.type = TransactionType.Credit;
            it.amount = 10000;
            it.date = new DateTime(2007, 8, 10);
            testinput.transactions.Add(it);
            it = new InputTransaction();
            it.type = TransactionType.Credit;
            it.amount = 10000;
            it.date = new DateTime(2007, 8, 18);
            testinput.transactions.Add(it);
            it = new InputTransaction();
            it.type = TransactionType.Credit;
            it.amount = 10000;
            it.date = new DateTime(2007, 8, 27);
            testinput.transactions.Add(it);
            return testinput;
        }
        private int ExportToExcel(Input input, Output output,string fname)
        {
            int retcode = 0;
            Types.Input inp = new Types.Input();
            Types.Output outp = new Types.Output();
            fill(ref inp,ref outp, input, output);
            retcode = new xlexport.xlexport().export(inp, outp, fname);
            return retcode;            
        }
        private void fill(ref Types.Input i,ref Types.Output o, Input input, Output output)
        {
            i.transactions = new List<Types.InputTransaction>();
            o.transactions = new List<Types.OutputTransaction>();
            o.interimInterests = new List<Types.InterimInterest>();
            Types.InterimInterest tii;
            Types.InputTransaction ti;
            Types.OutputTransaction tot;
            i.endDate = input.endDate;
            i.principal = input.principal;
            i.startDate = input.startDate;
            i.startRate = input.startRate;
            foreach (InputTransaction item in input.transactions)
            {
                ti = new Types.InputTransaction();
                ti._date = item.date;
                ti.amount = item.amount;
                switch (item.type)
                {
                    case TransactionType.Cmt:
                        ti.TransactionType = Types.TransactionType.Cmt;
                        break;
                    case TransactionType.Credit:
                        ti.TransactionType = Types.TransactionType.Credit;
                        break;
                    case TransactionType.Debit:
                        ti.TransactionType = Types.TransactionType.Debit;
                        break;
                    case TransactionType.Int:
                        ti.TransactionType = Types.TransactionType.Int;
                        break;
                    case TransactionType.Invalid:
                        ti.TransactionType = Types.TransactionType.Invalid;
                        break;
                    case TransactionType.Rate:
                        ti.TransactionType = Types.TransactionType.Rate;
                        break;
                }

                i.transactions.Add(ti);
            }
            o.balancePrincipal = output.balancePrincipal;
            foreach (InterimInterest item in output.interimInterests)
            {
                tii = new Types.InterimInterest();
                tii.amount = item.amount;
                tii.endDate = item.endDate;
                tii.principal = item.principal;
                tii.rate = item.rate;
                tii.startDate = item.startDate;
                o.interimInterests.Add(tii);
            }
            foreach (OutputTransaction item in output.transactions)
            {
                tot = new Types.OutputTransaction();
                tot._date = item.date;
                tot.balancePrincipal = item.balancePrincipal;
                tot.initialPrincipal = item.initialPrincipal;
                switch (item.type)
                {
                    case TransactionType.Cmt:
                        tot.type = Types.TransactionType.Cmt;
                        break;
                    case TransactionType.Credit:
                        tot.type = Types.TransactionType.Credit;
                        break;
                    case TransactionType.Debit:
                        tot.type = Types.TransactionType.Debit;
                        break;
                    case TransactionType.Int:
                        tot.type = Types.TransactionType.Int;
                        break;
                    case TransactionType.Invalid:
                        tot.type = Types.TransactionType.Invalid;
                        break;
                    case TransactionType.Rate:
                        tot.type = Types.TransactionType.Rate;
                        break;
                }
                o.transactions.Add(tot);
            }
        }
        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            /*Test MODIFY INPUT*/
            //input = TestInput();
            //output = MainProgram.execMain(input);
            //DisplayOutput(output);
            //ActualStatus.Content = "Testing";
            //InputForm ifo = new InputForm();
            //ifo.INP = input;
            //ifo.UpdateDS();
            //ifo.ShowInTaskbar = true;
            //ifo.ShowDialog();            //Blocking Call
            //output = MainProgram.execMain(input);
            //DisplayOutput(output);
            /*Test*/
            if (outputgot)
            {
                ActualStatus.Content = "Modifying input ...";                
                ifo.INP = input;
                ifo.UpdateDS();
                ifo.ShowInTaskbar = true;
                ifo.ShowDialog();            //Blocking Call
                output = MainProgram.execMain(input);
                DisplayOutput(output); 
            }
        }
    }
}
