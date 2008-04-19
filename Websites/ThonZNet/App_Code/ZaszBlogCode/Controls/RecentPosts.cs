using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Collections.Generic;
using Thon.ZaszBlog.Support;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Controls
{
  /// <summary>
  /// Shows a chronological list of recent posts.
  /// </summary>
  public class RecentPosts : Control
  {

    static RecentPosts()
    {
      BuildPostList();
      Post.Saved += new EventHandler<SavedEventArgs>(Post_Saved);
      Post.CommentAdded += delegate { BuildPostList(); };
      Post.CommentRemoved += delegate { BuildPostList(); };
      Post.Rated += delegate { BuildPostList(); };
      BlogSettings.Changed += delegate { BuildPostList(); };
    }

    static void Post_Saved(object sender, SavedEventArgs e)
    {
      if (e.Action != SaveAction.Update)
        BuildPostList();
    }

    private static object _SyncRoot = new object();
    private static List<Post> _Posts = new List<Post>();

    private static void BuildPostList()
    {
      lock (_SyncRoot)
      {
        int number = BlogSettings.Instance.NumberOfRecentPosts;
        if (number > Post.Posts.Count)
          number = Post.Posts.Count;

        int counter = 1;
        _Posts.Clear();
        foreach (Post post in Post.Posts)
        {
          if (counter <= number && post.IsPublished)
          {
            _Posts.Add(post);
            counter++;
          }
        }
      }
    }

    private static string RenderPosts()
    {

			if (_Posts.Count == 0)
			{
				return "<p>None</p>";
			}

      StringBuilder sb = new StringBuilder();
      sb.Append("<ul class=\"recentPosts\" id=\"recentPosts\">");

      foreach (Post post in _Posts)
      {
        if (!post.IsVisible)
          continue;

        string rating = Math.Round(post.Rating, 1).ToString(System.Globalization.CultureInfo.InvariantCulture);

        string link = "<li><a href=\"{0}\">{1}</a>{2}{3}</li>";
        string comments = string.Format("<span>Comments : {0}</span>", post.ApprovedComments.Count);
        string rate = string.Format("<span>Rating: {0} / {1}</span>", rating, post.Raters);

        if (!BlogSettings.Instance.DisplayCommentsOnRecentPosts || !BlogSettings.Instance.IsCommentsEnabled)
          comments = null;

        if (!BlogSettings.Instance.DisplayRatingsOnRecentPosts || !BlogSettings.Instance.EnableRating)
          rate = null;

        if (post.Raters == 0)
          rating = "Not rated yet";

        sb.AppendFormat(link, post.RelativeLink, HttpUtility.HtmlEncode(post.Title), comments, rate);
      }

      sb.Append("</ul>");
      return sb.ToString();
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
      if (Page.IsCallback)
        return;
      
      string html = RenderPosts();
      writer.Write(html);
    }
  }
}