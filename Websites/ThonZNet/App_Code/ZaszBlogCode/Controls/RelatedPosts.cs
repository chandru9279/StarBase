using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.IO;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support;

namespace Thon.ZaszBlog.Controls
{

  public class RelatedPosts : Control
  {

		public RelatedPosts()
		{
			Post.Saved += new EventHandler<SavedEventArgs>(Post_Saved);
		}

		void Post_Saved(object sender, SavedEventArgs e)
		{
			if (e.Action == SaveAction.Update)
			{
				Post post = (Post)sender;
				if (_Cache.ContainsKey(post.Id))
					_Cache.Remove(post.Id);
			}
		}

    #region Properties

    private IShowed _Item;

    public IShowed Item
    {
			get { return _Item; }
			set { _Item = value; }
    }

    private int _MaxResults = 3;

    public int MaxResults
    {
      get { return _MaxResults; }
      set { _MaxResults = value; }
    }

    private bool _ShowDescription;

    public bool ShowDescription
    {
      get { return _ShowDescription; }
      set { _ShowDescription = value; }
    }

    private int _DescriptionMaxLength = 100;

    public int DescriptionMaxLength
    {
      get { return _DescriptionMaxLength; }
      set { _DescriptionMaxLength = value; }
    }

    private string _Headline = "RelatedPosts";

    public string Headline
    {
      get { return _Headline; }
      set { _Headline = value; }
    }

    #endregion

    #region Private fiels

    private static Dictionary<Guid, string> _Cache = new Dictionary<Guid,string>();
    private static object _SyncRoot = new object();

    #endregion    

    public override void RenderControl(HtmlTextWriter writer)
    {
      if (!BlogSettings.Instance.EnableRelatedPosts || Item == null)
        return;
      
      if (!_Cache.ContainsKey(Item.Id))
      {
        lock (_SyncRoot)
        {
          if (!_Cache.ContainsKey(Item.Id))
          {            
            List<IShowed> relatedPosts = SearchForPosts();
            if (relatedPosts.Count <= 1)
              return;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string link = "<a href=\"{0}\">{1}</a>";
            string desc = "<span>{0}</span>";
            sb.Append("<h1>+++</h1>");
            sb.Append("<div id=\"relatedPosts\">");

            int count = 0;
            foreach (IShowed post in relatedPosts)
            {
              if (post != this.Item)
              {
                sb.Append(string.Format(link, post.RelativeLink, post.Title));
                if (ShowDescription)
                {
                  string description = post.Description;
                  if (description != null && description.Length > DescriptionMaxLength)
                    description = description.Substring(0, DescriptionMaxLength) + "...";

                  sb.Append(string.Format(desc, description));
                }
                count++;
              }

              if (count == MaxResults)
                break;
            }

            sb.Append("</div>");
            _Cache.Add(Item.Id, sb.ToString());
          }
        }
      }

      writer.Write(_Cache[Item.Id].Replace("+++", this.Headline));
    }

    private List<IShowed> SearchForPosts()
    {
      List<IShowed> list = StaticSearch.FindRelatedItems(this.Item);
      List<IShowed> posts = new List<IShowed>();
      foreach (IShowed item in list)
      {
        if (item is Post || item is Thon.ZaszBlog.Support.CodedRepresentations.Page)
          posts.Add(item);
      }

      return posts;
    }
  }
}