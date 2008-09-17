using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;

namespace Aveo.Daybook.TrayClient
{
    /// <summary>
    /// A configuration section for web.config.
    /// </summary>
    /// <remarks>
    /// In the config section you can get or persist the settings that the user has selected to the App.config file
    /// </remarks>
    public partial class SettingsAdapter : System.Configuration.AppSettingsReader
    {
        /// <summary>
        /// This method creates sub keys and key-value pairs in the registry for Application 
        /// settings that need to be stored in the registry such as autorun
        /// </summary>
        /// <remarks>
        /// This may throw an exception due to antivirus programs interfering
        /// with applications that modify registry such as this one
        /// </remarks>
        public static void RegistryStore()
        {
            //Create a folder if its not already there..            
            if (!new List<string>(Registry.LocalMachine.OpenSubKey("SOFTWARE").GetSubKeyNames()).Contains("AveoInfotech"))
            {
                Registry.LocalMachine.OpenSubKey("SOFTWARE", true).CreateSubKey("AveoInfotech");
            }
            //If folder is there but autostarttrayapp key isnt there then create it and set default value..
            if (!new List<string>(Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AveoInfotech").GetValueNames()).Contains("autostarttrayapp"))
            {
                Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AveoInfotech", true).SetValue("autostarttrayapp", "yes", RegistryValueKind.String);
            }

            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AveoInfotech");
            RegistryKey runkey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Run", true);
            string[] running = runkey.GetValueNames();
            string mypath = System.Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + Process.GetCurrentProcess().ProcessName;

            // This condition reflects the option user has set into actual autorun registry directory            
            if (key.GetValue("autostarttrayapp").ToString().Equals("yes") & (!new List<string>(running).Contains("AveoDaybook")))
            {
                runkey.SetValue("AveoDaybook", mypath, RegistryValueKind.String);
            }
            if (key.GetValue("autostarttrayapp").ToString().Equals("no") & (new List<string>(running).Contains("AveoDaybook")))
            {
                runkey.SetValue("AveoDaybook", mypath, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// This makes changes only to our applications registry key.
        /// Actual windows autorun registry keys are added/removed only at restart of the application
        /// </summary>
        /// <param name="autostart">Given true it creates an entry that 
        /// makes the app start along with windows.
        /// Given false, it deletes the entry, if present
        /// </param>
        public static void RegistryChange(bool autostart)
        {
            if (autostart)
            {
                //Enable it
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AveoInfotech", true);
                key.SetValue("autostarttrayapp", "yes", RegistryValueKind.String);
            }
            else
            {
                //Disable autostart
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AveoInfotech", true);
                key.SetValue("autostarttrayapp", "no", RegistryValueKind.String);
            }
        }
    }
}