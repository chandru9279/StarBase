using System;
using System.Xml;
using System.Reflection;

namespace Aveo.Daybook.TrayClient
{
    /// <summary>
    /// A Singleton class that allows persistance of settings in the windows 
    /// registry and in the XML configuration files.
    /// </summary>
    /// <remarks>
    /// In the config section you can get or persist the settings that the user has selected to the App.config file
    /// and as keys in the windows registry.
    /// </remarks>
    public partial class SettingsAdapter : System.Configuration.AppSettingsReader
    {
        /// <summary>
        /// Leaving unassigned is recommended. The default value is taken
        /// as (AssemblyName).dll.config file. If a name of a file is specified here,
        /// then all application settings other than Service Addresses are stored in 
        /// this file.
        /// </summary>
        public string ConfigFileName = String.Empty;
        
        
        private XmlNode appSettingsNode = null;
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

        #region Save to config file
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
        #endregion

        #region Load from config file
        /// <summary>
        /// Loads an <see cref="System.Xml.XmlDocument"/> object from the App.config file
        /// </summary>
        /// <exception cref="Exception">Throws exception if the file has problems</exception>
        private XmlDocument LoadConfigToObject(XmlDocument ConfigToObject)
        {
            try
            {
                if (string.IsNullOrEmpty(ConfigFileName))
                {
                    ConfigFileName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                    ConfigFileName += ".exe.config";
                }
                ConfigToObject.Load(ConfigFileName);
                return ConfigToObject;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}