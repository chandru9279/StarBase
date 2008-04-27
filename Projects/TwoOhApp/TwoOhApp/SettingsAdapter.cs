using System;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace TwoOhApp
{
    /// <summary>
    /// A configuration section for web.config.
    /// </summary>
    /// <remarks>
    /// In the config section you can get or persist the settings that the user has selected to the App.config file
    /// </remarks>
    public class SettingsAdapter : System.Configuration.AppSettingsReader
    {
        public string ConfigFileName = String.Empty;
        private XmlNode appSettingsNode = null;
        public static SettingsAdapter Instance;

        /// <summary>
        /// Constructor that initializes the static variable Instance
        /// </summary>        
        public SettingsAdapter()
        {
            Instance = this;
        }

        /// <summary>
        /// Adds a new name/value pair in appSettings collection or modifies existing one
        /// </summary>
        /// <returns>A value indicating if the operation was successfull or not</returns>
        public bool SetValue(string key, string value)
        {
            XmlDocument ConfigToObject = new XmlDocument();
            LoadConfigToObject(ConfigToObject);
            appSettingsNode = ConfigToObject.SelectSingleNode("//appSettings");
            if (appSettingsNode == null)
                throw new System.InvalidOperationException("appSettings section not found");
            try
            {
                // XPath
                XmlElement addElement = (XmlElement)appSettingsNode.SelectSingleNode("//add[@key='" + key + "']");
                if (addElement != null)
                    addElement.SetAttribute("value", value);
                else
                {
                    XmlElement entry = ConfigToObject.CreateElement("add");
                    entry.SetAttribute("key", key);
                    entry.SetAttribute("value", value);
                    appSettingsNode.AppendChild(entry);
                }
                SaveObjectToConfig(ConfigToObject, ConfigFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a name/value pair in appSettings collection
        /// </summary>
        /// <returns>A value indicating if the operation was successfull or not</returns>
        public bool RemoveElement(string elementKey)
        {
            try
            {
                XmlDocument ConfigToObject = new XmlDocument();
                LoadConfigToObject(ConfigToObject);
                appSettingsNode = ConfigToObject.SelectSingleNode("//appSettings");
                if (appSettingsNode == null)
                    throw new System.InvalidOperationException("appSettings section not found");
                // XPath
                appSettingsNode.RemoveChild(appSettingsNode.SelectSingleNode("//add[@key='" + elementKey + "']"));
                SaveObjectToConfig(ConfigToObject, ConfigFileName);
                return true;                
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves an <see cref="System.Xml.XmlDocument"/> object to the App.config file
        /// </summary>
        /// <exception cref="Exception">Throws exception if the file has problems</exception>
        private void SaveObjectToConfig(XmlDocument ConfigToObject, string FileName)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(FileName, null);
                writer.Formatting = Formatting.Indented;
                ConfigToObject.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Loads an <see cref="System.Xml.XmlDocument"/> object from the App.config file
        /// </summary>
        /// <exception cref="Exception">Throws exception if the file has problems</exception>
        private XmlDocument LoadConfigToObject(XmlDocument ConfigToObject)
        {
            try
            {
                ConfigFileName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                ConfigFileName += ".exe.config";
                ConfigToObject.Load(ConfigFileName);
                return ConfigToObject;
            }
            catch
            {
                throw;
            }
        }

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

