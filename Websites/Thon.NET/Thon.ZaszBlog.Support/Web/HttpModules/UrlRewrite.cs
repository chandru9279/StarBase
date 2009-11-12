using System;

using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Web.HttpModules
{
  
  // Handles pretty URL's and redirects them to the permalinks.
  // We have given absolute links to inquisitive search engines and mention them while ping&trackbacks.
  // These absolute links with month,year etc are only for the users and security.
  // This module analyzes the RawUrl of the request. It affects only the Url property of the request,by using Rewrite
  public class UrlRewrite : IHttpModule
  {
    #region IHttpModule Members
    public void Dispose()
    {
    }
    public void Init(HttpApplication context)
    {
      context.BeginRequest += new EventHandler(context_BeginRequest);
    }
    #endregion
  
    private void context_BeginRequest(object sender, EventArgs e)
    {
      HttpContext context = ((HttpApplication)sender).Context;
	  string url = context.Request.RawUrl.ToUpperInvariant();

      if (url.Contains("ZaszBlog".ToUpperInvariant()) && url.Contains(BlogSettings.Instance.FileExtension.ToUpperInvariant()) && !url.Contains("ERROR404.ASPX"))
      {
        // First condition eliminates .ashx handler extensions which must be left untouched.
        // Second condition : if Error404:FileNotFound page is requested(redirected) then leaves the url untouched.



          /* Mapping :    Incoming + QueryString      RWebRoot : "/ThonZNet/ZaszBlog/"  +
           * 
           * QueryString may be “&delete=true”
           * 
           * ZaszBlog/blog.aspx                  ---> Default.aspx?blog=true
           * ZaszBlog/POST/2008/06/mypost.aspx   ---> post.aspx?id=<post_guid>          + QueryString
           * ZaszBlog/POST/mypost.aspx           ---> post.aspx?id=<post_guid>          + QueryString
           * ZaszBlog/CATEGORY/mycat.aspx        ---> Default.aspx?id=<cat_guid>        + QueryString
           * ZaszBlog/PAGE/mypage.aspx           ---> page.aspx?id=<page_guid>          + QueryString
           * ZaszBlog/CALENDAR/anything.aspx     ---> Default.aspx?calendar=show
           * 
           * ZaszBlog/2008/12/28/DEFAULT.aspx    ---> Default.aspx?date=2008-12-28
           * ZaszBlog/2008/12/DEFAULT.aspx       ---> Default.aspx?year=2008&month=12
           * ZaszBlog/2008/DEFAULT.aspx          ---> Default.aspx?year=2008
           * 
           * if there is &page=mypage after DEFAULT.aspx like :
           *            ZaszBlog/2008/12/DEFAULT.aspx&page=mypage
           * then result is like :
           *            Default.aspx?year=2008&month=12&page=mypage
           *            
           * ZaszBlog/AUTHOR/myname.aspx         ---> Default.aspx?name=myname          + QueryString
           *
           * As you can see Default.aspx always lists a collection of IShowed
           * Particular Post & Page goes to post.aspx & page.aspx.
           * Particular Category goes to default.aspx with &id= to show a list of posts of that category.
           * Particular Date goes to default.aspx with &date= to show a collection of IShowed too for that date
           * Particular Name goes to default.aspx with &name= to show a collection of IShowed too for that person
           * 
           * Overall permutations: You can see 1. A post in Post.aspx 
           *                                   2. A Page in Page.aspx
           *                                   3. A collection of IShowed in Default.aspx
           */

        if (context.Request.Path.Contains("/Blog.aspx") || context.Request.Path.Contains("/blog.aspx"))
        {
          context.RewritePath(SupportUtilities.RelativeWebRoot + "Default.aspx?blog=true");
        }

        // These three occur when Absolute Links are displayed/search-engine-index-request occurs
        else if (url.Contains("/POST/"))
        {
          RewritePost(context);
        }
        else if (url.Contains("/CATEGORY/"))
        {
          RewriteCategory(context);
        }
        else if (url.Contains("/PAGE/"))
        {
          RewritePage(context);
        }
		else if (url.Contains("/CALENDAR/"))
		{
			context.RewritePath(SupportUtilities.RelativeWebRoot + "Default.aspx?calendar=show", false);
		}
		else if (url.Contains("/DEFAULT" + BlogSettings.Instance.FileExtension.ToUpperInvariant()))
		{
			RewriteDefault(context);
		}
		else if (url.Contains("/AUTHOR/"))
		{
			string author = ExtractTitle(context);
			context.RewritePath(SupportUtilities.RelativeWebRoot + "default" + BlogSettings.Instance.FileExtension + "?name=" + author + GetQueryString(context), false);
		}
      }
    }

    private static void RewritePost(HttpContext context)
    {
      DateTime stamp = ExtractDate(context);
      string slug = ExtractTitle(context);
      Post post = Post.GetPostBySlug(slug, stamp);

      if (post != null)
      {
        context.RewritePath(SupportUtilities.RelativeWebRoot + "post.aspx?id=" + post.Id.ToString() + GetQueryString(context), false);
      }
    }    

    private static void RewriteCategory(HttpContext context)
    {
      string title = ExtractTitle(context);
      foreach (Category cat in Category.Categories)
      {
        string legalTitle = SupportUtilities.RemoveIllegalCharacters(cat.Title).ToLowerInvariant();
        if (title.Equals(legalTitle, StringComparison.OrdinalIgnoreCase))
        {
          context.RewritePath(SupportUtilities.RelativeWebRoot + "Default.aspx?id=" + cat.Id.ToString() + GetQueryString(context), false);
          break;
        }
      }
    }

    private static void RewritePage(HttpContext context)
    {
      string title = ExtractTitle(context);
      foreach (Page page in Page.Pages)
      {
        string legalTitle = SupportUtilities.RemoveIllegalCharacters(page.Title).ToLowerInvariant();
        if (title.Equals(legalTitle, StringComparison.OrdinalIgnoreCase))
        {
          context.RewritePath(SupportUtilities.RelativeWebRoot + "page.aspx?id=" + page.Id + GetQueryString(context), false);
          break;
        }
      }
    }

		private static readonly Regex YEAR = new Regex("/([0-9][0-9][0-9][0-9])/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex YEAR_MONTH = new Regex("/([0-9][0-9][0-9][0-9])/([0-1][0-9])/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex YEAR_MONTH_DAY = new Regex("/([0-9][0-9][0-9][0-9])/([0-1][0-9])/([0-3][0-9])/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private static void RewriteDefault(HttpContext context)
		{
			string url = context.Request.RawUrl;
			string page = "&page=" + context.Request.QueryString["page"];
			if (string.IsNullOrEmpty(context.Request.QueryString["page"]))
				page = null;

			if (YEAR_MONTH_DAY.IsMatch(url))
			{
				Match match = YEAR_MONTH_DAY.Match(url);
				string year = match.Groups[1].Value;
				string month = match.Groups[2].Value;
				string day = match.Groups[3].Value;
				string date = year + "-" + month + "-" + day;
				context.RewritePath(SupportUtilities.RelativeWebRoot + "Default.aspx?date=" + date + page, false);
			}
			else if (YEAR_MONTH.IsMatch(url))
			{
				Match match = YEAR_MONTH.Match(url);
				string year = match.Groups[1].Value;
				string month = match.Groups[2].Value;
				string path = string.Format("Default.aspx?year={0}&month={1}", year, month);
				context.RewritePath(SupportUtilities.RelativeWebRoot + path + page, false);
			}
			else if (YEAR.IsMatch(url))
			{
				Match match = YEAR.Match(url);
				string year = match.Groups[1].Value;
				string path = string.Format("Default.aspx?year={0}", year);
				context.RewritePath(SupportUtilities.RelativeWebRoot + path + page, false);
			}
		}

    // Extracts the title from the requested URL.
    private static string ExtractTitle(HttpContext context)
    {
			string url = context.Request.RawUrl.ToLowerInvariant();
            if (url.Contains(BlogSettings.Instance.FileExtension) && url.EndsWith("/"))//url=....ZaszBlog/category/<cat_title>.aspx/ (or) ....ZaszBlog/post/2008/06/<post_title>.aspx/
			{
                //chandru:remember second arg of substring function indicates the length & not the end index
                url = url.Substring(0, url.Length - 1);//url=....ZaszBlog/category/<cat_title>.aspx  (or) ....ZaszBlog/post/2008/06/<post_title>.aspx
				context.Response.AppendHeader("location", url);
				context.Response.StatusCode = 301;//301 - Requested resource has been permanantly moved
			}

            url = url.Substring(0, url.IndexOf(BlogSettings.Instance.FileExtension));//url=....ZaszBlog/category/<cat_title>  (or) ....ZaszBlog/post/2008/06/<post_title>
            int index = url.LastIndexOf("/") + 1;//url=....ZaszBlog/category"/"<cat_title>  (or) ....ZaszBlog/post/2008/06"/"<post_title>
            string title = url.Substring(index); //<cat_title>  (or) <post_title>
      return context.Server.UrlEncode(title);
    }

    private static readonly Regex _Regex = new Regex("/([0-9][0-9][0-9][0-9])/([01][0-9])/", RegexOptions.Compiled);
   
    // Extracts the year and month from the requested URL and returns that as a DateTime.    
    private static DateTime ExtractDate(HttpContext context)
    {
      Match match = _Regex.Match(context.Request.RawUrl);
      if (match.Success)
      {
          // seems like the Groups is a 1 based index
        int year = int.Parse(match.Groups[1].Value);
        int month = int.Parse(match.Groups[2].Value);
        return new DateTime(year, month, 1);
      }
      return DateTime.MinValue;
    }

    // Gets the query string from the requested URL.    
    private static string GetQueryString(HttpContext context)
    {
      string query = context.Request.QueryString.ToString();
      if (!string.IsNullOrEmpty(query))
        return "&" + query;

      return string.Empty;
    }
  }
}