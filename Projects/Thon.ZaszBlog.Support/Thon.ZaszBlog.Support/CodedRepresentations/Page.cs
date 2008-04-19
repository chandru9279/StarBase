using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
  
  // A page is much like a post, but is not part of the
  // blog chronology and is more static in nature.  
  // Pages can be used for "About" pages or other static  
  public sealed class Page : BusinessBase<Page, Guid>, IShowed
  {
    public Page()
    {
      base.Id = Guid.NewGuid();
      DateCreated = DateTime.Now;
    }

    #region Properties Title;Content;Description;Keywords;Parent;IsPublished;IsVisible;IsFrontPage;ShowInList;RelativeLink;AbsoluteLink;Pages;Author;Categories

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

    private string _Content;    
    public string Content
    {
      get { return _Content; }
      set
      {
        if (_Content != value) MarkChanged("Content");
        _Content = value;
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

    private string _Keywords;
    public string Keywords
    {
      get { return _Keywords; }
      set
      {
        if (_Keywords != value) MarkChanged("Keywords");
        _Keywords = value;
      }
    }

    // A hirarchy of pages is usually created Family -> Mahesh(Arun), Vasanth(Me,TC,Chandru)-> topic1,topic2,etc. , Pop, Mom, Bones, Kuttie
    private Guid _Parent;
    public Guid Parent
    {
      get { return _Parent; }
      set 
      {
        if (_Parent != value) MarkChanged("Parent");
        _Parent = value; 
      }
    }

    private bool _IsPublished;
    public bool IsPublished
    {
      get { return _IsPublished; }
      set
      {
        if (_IsPublished != value) MarkChanged("IsPublished");
        _IsPublished = value;
      }
    }

    public bool IsVisible
    {
      get { return IsPublished; }
    }

    private bool _IsFrontPage;
    public bool IsFrontPage
    {
      get { return _IsFrontPage; }
      set
      {
        if (_IsFrontPage != value) MarkChanged("IsFrontPage");
        _IsFrontPage = value; 
      }
    }

    // Gets or sets whether or not this page should be in the sitemap list.
    private bool _ShowInList;
    public bool ShowInList
    {
      get { return _ShowInList; }
      set
      {
        if (_ShowInList != value) MarkChanged("ShowInList");
        _ShowInList = value; 
      }
    }

    // The relative URI to the page. For in-site use only.
    public string RelativeLink
    {
        get { return SupportUtilities.RelativeWebRoot + "page/" + SupportUtilities.RemoveIllegalCharacters(Title) + BlogSettings.Instance.FileExtension; }
    }
    
    // The absolute URI to the path.
    public Uri AbsoluteLink
    {
        get { return SupportUtilities.ConvertToAbsolute(RelativeLink); }
    }

    private static object _SyncRoot = new object();

    // Gets an unsorted list of all pages.
    private static List<Page> _Pages;
    public static List<Page> Pages
    {
      get
      {
        if (_Pages == null)
        {
					lock (_SyncRoot)
					{
						if (_Pages == null)
						{
							_Pages = StaticDataService.FillPages();
							_Pages.Sort(delegate(Page p1, Page p2) { return String.Compare(p1.Title, p2.Title); });
						}
					}        
        }
        return _Pages;
      }
    }

    public String Author
    {
      get { return Thon.Support.ThonSettings.Instance.MyName; }
    }

    public StateList<Category> Categories
    {
      get { return null; }
    }

    #endregion

    // Returns the front page if any is available.
    public static Page GetFrontPage()
    {
        foreach (Page page in Pages)
        {
            if (page.IsFrontPage)
                return page;
        }
        return null;
    }

    // Returns a page based on the specified id.
    public static Page GetPage(Guid id)
    {
        foreach (Page page in Pages)
        {
            if (page.Id == id)
                return page;
        }
        return null;
    }

    #region Base overrides

    protected override void ValidationRules()
    {
      AddRule("Title", "Title must be set", string.IsNullOrEmpty(Title));
      AddRule("Content", "Content must be set", string.IsNullOrEmpty(Content));
    }

    protected override Page DataSelect(Guid id)
    {
      return StaticDataService.SelectPage(id);
    }
      
    protected override void DataUpdate()
    {
      StaticDataService.UpdatePage(this);
    }

    protected override void DataInsert()
    {
      StaticDataService.InsertPage(this);

      if (IsNew)
        Pages.Add(this);      
    }

    protected override void DataDelete()
    {
      StaticDataService.DeletePage(this);
      if (Pages.Contains(this))
        Pages.Remove(this);      
    }

    public override string ToString()
    {
      return Title;
    }
    
    public new static Page Load(Guid id) 
    {
      Page instance = new Page();
      instance = instance.DataSelect(id);
      instance.Id = id;
      if (instance != null) {
          instance.MarkOld();
          return instance;
      }
      return null;
    }

    #endregion

    #region Events

    public static event EventHandler<ServingEventArgs> Serving;    
    public static void OnServing(Page page, ServingEventArgs arg)
    {
      if (Serving != null)
      {
        Serving(page, arg);
      }
    }
    public void OnServing(ServingEventArgs eventArgs)
    {
      if (Serving != null)
      {
        Serving(this, eventArgs);
      }
    }

    #endregion

  }
}
