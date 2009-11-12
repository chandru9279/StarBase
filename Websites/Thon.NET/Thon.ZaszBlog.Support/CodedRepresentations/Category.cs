using System;
using System.Collections.Generic;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
  
  // Categories are a way to organize posts. A post can be in multiple categories.
  [Serializable]
  public class Category : BusinessBase<Category, Guid>, IComparable<Category>
  {
    public Category()
    {
      Id = Guid.NewGuid();
    }

    public Category(string title, string description)
    {
      this.Id = Guid.NewGuid();
      this._Title = title;
      this._Description = description;
    }

    #region Properties

    private string _Title;    
    public string Title
    {
      get { return _Title; }
      set
      {
        if (_Title != value) MarkChanged("Title");
        _Title = value;
      }
    }

    private string _Description;
    public string Description
    {
      get { return _Description; }
      set
      {
        if (_Description != value) MarkChanged("Description");
        _Description = value;
      }
    }
    
    public static Category GetCategory(Guid id)
    {
      foreach (Category category in Categories)
      {
        if (category.Id == id)
          return category;
      }

      return null;
    }

    private static object _SyncRoot = new object();
    private static List<Category> _Categories;
    public static List<Category> Categories
    {
      get
      {
        if (_Categories == null)
        {
          lock (_SyncRoot)
          {
            if (_Categories == null)
						{
              _Categories = StaticDataService.FillCategories();
							_Categories.Sort();
						}
          }
        }

        return _Categories;
      }
    }

    #endregion

    #region Base overrides
    
    protected override void ValidationRules()
    {
      AddRule("Title", "Title must be set", string.IsNullOrEmpty(Title));
    }

    
    protected override Category DataSelect(Guid id)
    {
      return StaticDataService.SelectCategory(id);
    }
   
    protected override void DataUpdate()
    {
      if (IsChanged)
        StaticDataService.UpdateCategory(this);
    }

    protected override void DataInsert()
    {
      if (IsNew)
        StaticDataService.InsertCategory(this);
    }
    
    protected override void DataDelete()
    {
      if (IsDeleted)
        StaticDataService.DeleteCategory(this);
      if (Categories.Contains(this))
        Categories.Remove(this);
    }

    public override string ToString()
    {
      return Title;
    }

    #endregion

	#region IComparable<Category> Members
	
	public int CompareTo(Category other)
	{
		return this.Title.CompareTo(other.Title);
	}

	#endregion
	}
}
