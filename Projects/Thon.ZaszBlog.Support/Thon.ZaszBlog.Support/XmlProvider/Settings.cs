using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;
using System.Web;

namespace Thon.ZaszBlog.Support.XmlProvider
{  
  public partial class XmlStorageProvider : StorageProvider
  {    
    public override StringDictionary LoadSettings()
    {
      string filename = HttpContext.Current.Server.MapPath("~/App_Data/ZaszBlog/ZaszBlogSettings.xml");
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
    
    public override void SaveSettings(StringDictionary settings)
    {
      if (settings == null)
        throw new ArgumentNullException("settings");
      string filename = HttpContext.Current.Server.MapPath("~/App_Data/ZaszBlog/ZaszBlogSettings.xml");
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

  }
}
