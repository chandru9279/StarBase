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
    #region Categories    
    // Gets a Category based on a Guid    
    public override Category SelectCategory(Guid id)
    {
      List<Category> categories = Category.Categories;
      Category category = new Category();
      foreach (Category cat in categories)
      {
        if (cat.Id == id)
          category = cat;
      }
      category.MarkOld();
      return category;
    }

    // Inserts a Category
    public override void InsertCategory(Category category)
    {
      List<Category> categories = Category.Categories;
      categories.Add(category);
      string fileName = _Folder + "Categories.xml";

      using (XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.UTF8))
      {
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("categories");

        foreach (Category cat in categories)
        {
          writer.WriteStartElement("category");
          writer.WriteAttributeString("id", cat.Id.ToString());
          writer.WriteAttributeString("description", cat.Description);
          writer.WriteValue(cat.Title);
          writer.WriteEndElement();
          cat.MarkOld();
        }

        writer.WriteEndElement();
      }

    }
    
    // Updates a Category
    public override void UpdateCategory(Category category)
    {
      List<Category> categories = Category.Categories;
      categories.Remove(category);
      categories.Add(category);
      string fileName = _Folder + "Categories.xml";

      using (XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.UTF8))
      {
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("categories");

        foreach (Category cat in categories)
        {
          writer.WriteStartElement("category");
          writer.WriteAttributeString("id", cat.Id.ToString());
          writer.WriteAttributeString("description", cat.Description);
          writer.WriteValue(cat.Title);
          writer.WriteEndElement();
          cat.MarkOld();
        }

        writer.WriteEndElement();
      }
    }
    
    /// <summary>
    /// Deletes a Category
    /// </summary>
    /// <param name="category">Which category? - The Category reference</param>
    public override void DeleteCategory(Category category)
    {
      List<Category> categories = Category.Categories;
      categories.Remove(category);
      string fileName = _Folder + "Categories.xml";

      if (File.Exists(fileName))
        File.Delete(fileName);

      if (Category.Categories.Contains(category))
        Category.Categories.Remove(category);

      using (XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.UTF8))
      {
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("categories");

        foreach (Category cat in categories)
        {
          writer.WriteStartElement("category");
          writer.WriteAttributeString("id", cat.Id.ToString());
          writer.WriteAttributeString("description", cat.Description);
          writer.WriteValue(cat.Title);
          writer.WriteEndElement();
          cat.MarkOld();
        }

        writer.WriteEndElement();
      }

    }
         
    /// <summary>
    /// Fills an unsorted list of categories.
    /// </summary>
    /// <returns>A list of Category objects got from the XML data document</returns>
    public override List<Category> FillCategories()
    {

      string fileName = _Folder + "Categories.xml";
      if (!File.Exists(fileName))
        return null;

      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      List<Category> categories = new List<Category>();

      foreach (XmlNode node in doc.SelectNodes("categories/category"))
      {
        Category category = new Category();

        category.Id = new Guid(node.Attributes["id"].InnerText);
        category.Title = node.InnerText;
				if (node.Attributes["description"] != null)
					category.Description = node.Attributes["description"].InnerText;
				else
					category.Description = string.Empty;
        categories.Add(category);
        category.MarkOld();
      }

      return categories;
    }

    #endregion
  }
}
