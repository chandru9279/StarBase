using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.XmlProvider
{ 
  public partial class XmlStorageProvider : StorageProvider
  { 
    public override Page SelectPage(Guid id)
    {
      string fileName = _Folder + "Pages" + Path.DirectorySeparatorChar + id.ToString() + ".xml";
      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);

      Page page = new Page();

      page.Title = doc.SelectSingleNode("page/title").InnerText;
      page.Description = doc.SelectSingleNode("page/description").InnerText;
      page.Content = doc.SelectSingleNode("page/content").InnerText;
      page.Keywords = doc.SelectSingleNode("page/keywords").InnerText;

      if (doc.SelectSingleNode("page/parent") != null)
        page.Parent = new Guid(doc.SelectSingleNode("page/parent").InnerText);

      if (doc.SelectSingleNode("page/isfrontpage") != null)
        page.IsFrontPage = bool.Parse(doc.SelectSingleNode("page/isfrontpage").InnerText);

      if (doc.SelectSingleNode("page/showinlist") != null)
        page.ShowInList = bool.Parse(doc.SelectSingleNode("page/showinlist").InnerText);

      if (doc.SelectSingleNode("page/ispublished") != null)
        page.IsPublished = bool.Parse(doc.SelectSingleNode("page/ispublished").InnerText);

      page.DateCreated = DateTime.Parse(doc.SelectSingleNode("page/datecreated").InnerText);
      page.DateModified = DateTime.Parse(doc.SelectSingleNode("page/datemodified").InnerText);

      return page;
    }
         
    public override void InsertPage(Page page)
    {
      if (!Directory.Exists(_Folder + "Pages"))
        Directory.CreateDirectory(_Folder + "Pages");

      string fileName = _Folder + "Pages" + Path.DirectorySeparatorChar + page.Id.ToString() + ".xml";
      XmlWriterSettings settings = new XmlWriterSettings();
      settings.Indent = true;

      using (XmlWriter writer = XmlWriter.Create(fileName, settings))
      {
        writer.WriteStartDocument(true);
        writer.WriteStartElement("page");

        writer.WriteElementString("title", page.Title);
        writer.WriteElementString("description", page.Description);
        writer.WriteElementString("content", page.Content);
        writer.WriteElementString("keywords", page.Keywords);
        writer.WriteElementString("parent", page.Parent.ToString());
        writer.WriteElementString("isfrontpage", page.IsFrontPage.ToString());
        writer.WriteElementString("showinlist", page.ShowInList.ToString());
        writer.WriteElementString("ispublished", page.IsPublished.ToString());
        writer.WriteElementString("datecreated", page.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
        writer.WriteElementString("datemodified", page.DateModified.ToString("yyyy-MM-dd HH:mm:ss"));        
        writer.WriteEndElement();
      }
    }

    public override void UpdatePage(Page page)
    {
      InsertPage(page);
    }

    public override void DeletePage(Page page)
    {
      string fileName = _Folder + "Pages" + Path.DirectorySeparatorChar + page.Id.ToString() + ".xml";
      if (File.Exists(fileName))
        File.Delete(fileName);

      if (Page.Pages.Contains(page))
        Page.Pages.Remove(page);
    }

    public override List<Page> FillPages()
    {
      string folder = _Folder + "Pages" + Path.DirectorySeparatorChar;
      List<Page> pages = new List<Page>();

      foreach (string file in Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly))
      {
        FileInfo info = new FileInfo(file);
        string id = info.Name.Replace(".xml", string.Empty);
        Page page = Page.Load(new Guid(id));
        pages.Add(page);
      }

      return pages;
    }

  }
}
