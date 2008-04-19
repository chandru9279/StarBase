using System;
using System.IO;
using System.Text;
using System.Web;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support
{  
  //Searches the post collection and returns a result based on a search term.
  //It is used for related posts and the in-site search feature.  
  public static class StaticSearch
  {

    static StaticSearch()
    {
      BuildCatalog();
      Post.Saved += new EventHandler<SavedEventArgs>(Post_Saved);
      Page.Saved += new EventHandler<SavedEventArgs>(Page_Saved);
      BlogSettings.Changed += delegate { BuildCatalog(); };
      Post.CommentAdded += new EventHandler<EventArgs>(Post_CommentAdded);
      Post.CommentRemoved += delegate { BuildCatalog(); };
      Comment.Approved += new EventHandler<EventArgs>(Post_CommentAdded);
    }
    
    #region Event handlers

    private static void Post_Saved(object sender, SavedEventArgs e)
    {
      lock (_SyncRoot)
      {
        if (e.Action == SaveAction.Insert)
        {
          AddItem(sender as Post);
        }
        else
        {
          BuildCatalog();
        }
      }
    }

    private static void Page_Saved(object sender, SavedEventArgs e)
    {
      lock (_SyncRoot)
      {
        if (e.Action == SaveAction.Insert)
        {
          AddItem(sender as Page);
        }
        else
        {
          BuildCatalog();
        }
      }
    }

    //only visible comments (i.e. approved ones will be added to catalog)
	private static void Post_CommentAdded(object sender, EventArgs e)
	{
		if (BlogSettings.Instance.EnableCommentSearch)
		{
			Comment comment = (Comment)sender;
			if (comment.IsVisible)
				AddItem(comment);
		}
	}

    #endregion

    #region Entry and Result structs

    //a search optimized post object cleansed from HTML and stop words.  
    internal struct Entry
    {
        internal IShowed Item;
        internal string Title;
        internal string Content;
    }

    //contains an IShowed object and its ranking
    internal struct Result : IComparable<Result>
    {
        internal int Rank;
        internal IShowed Item;
        public int CompareTo(Result other)
        { return other.Rank.CompareTo(Rank); }
    }

    #endregion

    #region BuildCatalog

    // clears _Catalog & AddItem s posts comments and pages.
    private static void BuildCatalog()
    {
        OnIndexBuilding();

        lock (_SyncRoot)
        {
            _Catalog.Clear();
            foreach (Post post in Post.Posts)
            {
                if (!post.IsVisible)
                    continue;

                AddItem(post);
                if (BlogSettings.Instance.EnableCommentSearch)
                {
                    foreach (Comment comment in post.Comments)
                    {
                        if (comment.IsVisible)
                            AddItem(comment);
                    }
                }
            }

            foreach (Page page in Page.Pages)
            {
                if (page.IsVisible)
                    AddItem(page);
            }
        }

        OnIndexBuild();
    }

    // Adds an IShowed item to the search catalog,making it searchable..
    public static void AddItem(IShowed item)
    {
        Entry entry = new Entry();
        entry.Item = item;
        entry.Title = CleanContent(item.Title, false);
        entry.Content = HttpUtility.HtmlDecode(CleanContent(item.Content, true));
        if (item is Comment)
        {
            entry.Content += HttpUtility.HtmlDecode(CleanContent(item.Author, false));
        }
        _Catalog.Add(entry);
    }

    // Removes stop words and HTML from the specified string.    
    private static string CleanContent(string content, bool removeHtml)
    {
        if (removeHtml)
            content = _StripHtml.Replace(content, string.Empty);

        content = content
                                        .Replace("\\", string.Empty)
                                        .Replace("|", string.Empty)
                                        .Replace("(", string.Empty)
                                        .Replace(")", string.Empty)
                                        .Replace("[", string.Empty)
                                        .Replace("]", string.Empty)
                                        .Replace("+", string.Empty);

        string[] words = content.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i].ToLowerInvariant().Trim();
            if (word.Length > 1 && !_StopWords.Contains(word))
                sb.Append(word + " ");
        }

        return sb.ToString();
    }

    // Retrieves the stop words from the stopwords.txt file located in App_Data.    
    private static StringCollection GetStopWords()
    {
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(BlogSettings.Instance.StorageLocation) + "StopWords.txt"))
        {
            string file = reader.ReadToEnd();
            string[] words = file.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            StringCollection col = new StringCollection();
            col.AddRange(words);
            return col;
        }
    }

    #endregion

    #region Search
    
    // Searches all the posts and returns a ranked result set.
    public static List<IShowed> Hits(string searchTerm, bool includeComments)
    {
      lock (_SyncRoot)
      {
        List<Result> results = BuildResultSet(searchTerm, includeComments);
        List<IShowed> items = results.ConvertAll<IShowed>(new Converter<Result, IShowed>(ResultToPost));
				results.Clear();
        OnSearcing(searchTerm);
        return items;
      }
    }
  
    public static List<IShowed> FindRelatedItems(IShowed post)
    {
      // the post's title along wit the title's of all categories the post belongs to ..
      string term = post.Title;
      foreach (Category cat in post.Categories)
      {
        term += " " + cat.Title;
      }
      term = CleanContent(term, false);
      return Hits(term, false);
    }

    private static List<Result> BuildResultSet(string searchTerm, bool includeComments)
    {
      List<Result> results = new List<Result>();
      string term = CleanContent(searchTerm.ToLowerInvariant().Trim(), false);
      string[] terms = term.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      string regex = string.Format("({0})", string.Join("|", terms));
      //regex now has for example "(search|term|regular|expression)"
      foreach (Entry entry in _Catalog)
      {
        Result result = new Result();
        if (!(entry.Item is Comment))
        {
          int titleMatches = Regex.Matches(entry.Title, regex).Count;
          result.Rank = titleMatches * 20;

          int postMatches = Regex.Matches(entry.Content, regex).Count;
          result.Rank += postMatches;
        }
        else if (includeComments)
        {
          int commentMatches = Regex.Matches(entry.Content + entry.Title, regex).Count;
          result.Rank += commentMatches;
        }

        if (result.Rank > 0)
        {
          result.Item = entry.Item;
          results.Add(result);
        }
      }

      results.Sort();//uses CompareTo() which is implemented below so that sorting is done by rank
      return results;
    }

    private static IShowed ResultToPost(Result result)
    {
      return result.Item;
    }

    #endregion

    #region Properties and private fields

    private readonly static object _SyncRoot = new object();
    private readonly static Regex _StripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);
    private readonly static StringCollection _StopWords = GetStopWords();
    private static Collection<Entry> _Catalog = new Collection<Entry>();

    #endregion

    #region Events

    public static event EventHandler<EventArgs> Searching;
    private static void OnSearcing(string searchTerm)
    {
      if (Searching != null)
      {
        Searching(searchTerm, EventArgs.Empty);
      }
    }

    public static event EventHandler<EventArgs> IndexBuilding;
    private static void OnIndexBuilding()
    {
      if (IndexBuilding != null)
      {
        IndexBuilding(null, EventArgs.Empty);
      }
    }

    public static event EventHandler<EventArgs> IndexBuild;
    private static void OnIndexBuild()
    {
      if (IndexBuild != null)
      {
        IndexBuild(null, EventArgs.Empty);
      }
    }

    #endregion

  }

  

}
