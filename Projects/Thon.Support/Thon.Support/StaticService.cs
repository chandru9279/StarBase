using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Xml;

namespace Thon.Support
{
    
    // FULLY STATIC - so sync is necessary.
    public static class StaticService
    {

        #region Settings

        // Loads the settings from the StorageLocation._SettingsFolder\ThonSettings.xml 
        // _SettingsFolder will have :
        // C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\
        // and returns them in a StringDictionary for the ThonSettings class to use.        
        public static StringDictionary LoadSettings()
        {
            string filename = StorageLocation._SettingsFolder + "ThonSettings.xml";
            StringDictionary dic = new StringDictionary();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            foreach (XmlNode settingsNode in doc.SelectSingleNode("settings").ChildNodes)
            {
                string name = settingsNode.Name;
                string value = settingsNode.InnerText;
                dic.Add(name, value);
            }
            return dic;
        }

        
        // Save the settings to the file (See above comment).        
        public static void SaveSettings(StringDictionary settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            string filename = StorageLocation._SettingsFolder + "ThonSettings.xml";
            XmlWriterSettings writerSettings = new XmlWriterSettings(); ;
            writerSettings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filename, writerSettings))
            {
                writer.WriteStartElement("settings");
                foreach (string key in settings.Keys)
                {
                    writer.WriteElementString(key, settings[key]);
                }
                writer.WriteEndElement();
            }
        }

        #endregion

    }
}
