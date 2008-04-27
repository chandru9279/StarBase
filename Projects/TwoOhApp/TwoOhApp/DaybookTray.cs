using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;

namespace TwoOhApp
{
    public partial class DaybookTray : Form
    {
        #region Constructor

        /// <summary>
        /// Default constructor for initializing controls on the form
        /// </summary>
        /// <remarks>
        /// Creates singleton instances of supplementary adapter classes.
        /// </remarks>
        public DaybookTray()
        {
            InitializeComponent();
            new ServiceAdapter(this);
            new SettingsAdapter();                       
        }

        /// <summary>
        /// Event handler for DaybookTray Load event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void DaybookTray_Load(object sender, EventArgs e)
        {
            TasksGridView.AutoGenerateColumns = true;
            EventsGridView.AutoGenerateColumns = true;
            ContactsGridView.AutoGenerateColumns = true;
            RealizeSettings();
            SoapIcon.ShowBalloonTip(1000);
        }

        #endregion


        #region MainFormControls

        /// <summary>
        /// Event handler for LoginStatusPicture Click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void LoginStatusPicture_Click(object sender, EventArgs e)
        {
            if(!ServiceAdapter.Instance.IsAuthenticated)
            MessageBox.Show("Go to Settings tab to enter your username and password & click Login");
        }

        /// <summary>
        /// Event handler for LoginTimer's Tick event.        
        /// </summary>
        /// <remarks>
        /// Calls the Login button's click event handler with dummy arguments.
        /// </remarks>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void LoginTimer_Tick(object sender, EventArgs e)
        {
            Login_Click(this, new EventArgs());
        }

        /// <summary>
        /// Looks through the setting persisted in app.config and realizes them semantically.
        /// <remarks>
        /// Called at every time the form loads and app starts.
        /// </remarks>
        /// </summary>
        private void RealizeSettings()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["CheckContinuously"].ToLowerInvariant()))
                LoginTimer.Enabled = true;
            try
            {
                SettingsAdapter.RegistryStore();
                if (ConfigurationManager.AppSettings["autostart"].Equals("true", StringComparison.InvariantCultureIgnoreCase))
                    SettingsAdapter.RegistryChange(true);
                else
                    SettingsAdapter.RegistryChange(false);
            }
            catch
            {
                lblMessage.Text = "Problem accessing the registry.";
            } 
        }

        /// <summary>
        /// Callback function used by the ServiceAdapter class to inform UI(me) of results of ServiceAdapter.Instance.TryGetInformation() call.
        /// <seealso cref="ServiceAdapter"/>
        /// </summary>
        /// <param name="Result">Indicates the success/failure of the call</param>
        public void GotAllData(bool Result)
        {
            if (Result)
            {
                ContactsBindingSource.DataSource = ServiceAdapter.contacts;
                TasksBindingSource.DataSource = ServiceAdapter.tasks;
                EventsBindingSource.DataSource = ServiceAdapter.events;
            }
        }

        /// <summary>
        /// Event handler for DaybookTray FormClosing event
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured FormClosingEvent arguments</param>
        private void DaybookTray_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// Event handler for SoapIcon - system tray item double click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void SoapIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// Event handler for ExitTSMI Context menu item click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void ExitTSMI_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        /// <summary>
        /// Event handler for SettingTSMI Context menu item click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void SettingTSMI_Click(object sender, EventArgs e)
        {
            this.Menu.SelectedIndex = 3;
            this.Show();
        }

        /// <summary>
        /// Event handler for OpenTSMI Context menu item click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void OpenTSMI_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// Event handler for AboutTSMI Context menu item click event.        
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void AboutTSMI_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About box opens here");
        }
        #endregion


        #region ContactsTabControls
        /// <summary>
        /// Callback function used by the ServiceAdapter class to inform UI(me) of results of ServiceAdapter.Instance.TryGetProfile() call.
        /// <seealso cref="ServiceAdapter"/>
        /// </summary>
        /// <param name="Result">Indicates the success/failure of the call</param>
        public void GotProfile(bool Result)
        {
            if (Result)
            {
                lblName.Text = ServiceAdapter.Instance.username;                
                for (int i = 0; i < ServiceAdapter.profiledata.Length-1; i++)
			    {
                    if (ServiceAdapter.profiledata[i].Key.Equals("Calendar.CustomViewDays", StringComparison.InvariantCultureIgnoreCase))
                        lblCustomViewDays.Text = ServiceAdapter.profiledata[i].Value.ToString();                    
                    if (ServiceAdapter.profiledata[i].Key.Equals("Tasks.HideTasksNotDueRecently", StringComparison.InvariantCultureIgnoreCase))
                        lblHideTasksNotDue.Text = ServiceAdapter.profiledata[i].Value.ToString();
                    if (ServiceAdapter.profiledata[i].Key.Equals("Tasks.HideCompletedTasks", StringComparison.InvariantCultureIgnoreCase))
                        lblHideCompTasks.Text = ServiceAdapter.profiledata[i].Value.ToString();
			    }
                
            }
        }
        #endregion


        #region ScheduledEventsTabControls

        #endregion


        #region TasksTabControls

        #endregion


        #region SettingsTabControls
        
        /// <summary>
        /// Event handler for Login button click.
        /// Also called by LoginTimer's Tick event handler.
        /// </summary>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void Login_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Trying to login ...";
            ServiceAdapter.Instance.TryLogin(txtUsername.Text, txtPassword.Text);
        }

        /// <summary>
        /// Callback function used by the ServiceAdapter class to inform UI(me) of results of ServiceAdapter.Instance.TryLogin(string username, string password) call.
        /// <seealso cref="ServiceAdapter"/>
        /// </summary>
        /// <param name="Result">Indicates the success/failure of the call</param>
        public void LoginCompleted(bool Result)
        {
            if (Result)
            {
                lblMessage.Text = "Logged in successfully.";
                LoginStatusPicture.BackgroundImage = DaybookTrayResources.LoggedIn;
                LoginStatus.Text = "Welcome , " + ServiceAdapter.Instance.username + "!";
                LoginTimer.Enabled = false;
                ServiceAdapter.Instance.TryGetInformation();
                ServiceAdapter.Instance.TryGetProfile();
            }
            else
            {
                lblMessage.Text = "Login failed.";
            }
        }

        /// <summary>
        /// Event handler for chkAutostart CheckedChanged event.       
        /// </summary>
        /// <remarks>
        /// Modifies the application configuration file/registry.
        /// </remarks>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void chkAutostart_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutostart.Checked)
                    SettingsAdapter.Instance.SetValue("autostart", "true");
                else
                    SettingsAdapter.Instance.SetValue("autostart", "false");
                SettingsAdapter.RegistryChange(chkAutostart.Checked);                
            }
            catch 
            { 
                lblMessage.Text = "Problem accessing the registry.";
                MessageBox.Show("Registry could not be accesed."+Environment.NewLine+"An Antivirus or security application or security feature of the OS may be blocking me !"+Environment.NewLine+"Try after disabling it.");
                if (chkAutostart.Checked) chkAutostart.Checked = false; else chkAutostart.Checked = true;
            }
        }

        /// <summary>
        /// Event handler for chkTryContinuously CheckedChanged event.        
        /// </summary>
        /// <remarks>
        /// Modifies the application configuration file/registry.
        /// </remarks>
        /// <param name="sender">Object on which event was caused</param>
        /// <param name="e">The occured events arguments</param>
        private void chkTryContinuously_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTryContinuously.Checked)
                SettingsAdapter.Instance.SetValue("CheckContinuously", "true");
            else
                SettingsAdapter.Instance.SetValue("CheckContinuously", "false");
        }

        #endregion
    }
}
