using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.XmlProvider
{
  public partial class XmlStorageProvider : StorageProvider
  {
    
    // Loads the ping services into a StringCollection
    public override StringCollection LoadPingServices()
    {
      string fileName = _Folder + "PingServices.xml";
      if (!File.Exists(fileName))
        return new StringCollection();

      StringCollection col = new StringCollection();
      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);

      foreach (XmlNode node in doc.SelectNodes("services/service"))
      {
        if (!col.Contains(node.InnerText))
          col.Add(node.InnerText);
      }
      return col;
    }

    // Saves the ping services into pingservices.xml
    public override void SavePingServices(StringCollection services)
    {
      if (services == null)
        throw new ArgumentNullException("services");
      string fileName = _Folder + "PingServices.xml";
      using (XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.UTF8))
      {
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("services");
        foreach (string service in services)
        {
          writer.WriteElementString("service", service);
        }
        writer.WriteEndElement();
      }
    }

  }
}
