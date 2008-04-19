using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Thon.ZaszBlog.Support;
using System.Collections.Generic;
using Thon.ZaszBlog.Support.Web.Controls;
using Thon.ZaszBlog.Support.CodedRepresentations;

public partial class ZaszBlogError404Aspx : BlogBasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Request.QueryString["aspxerrorpath"] != null && Request.QueryString["aspxerrorpath"].Contains("/post/"))
    {
      DirectHitSearch();
      divDirectHit.Visible = true;
    }
    else if (Request.UrlReferrer == null)
    {
      divDirectHit.Visible = true;
    }
    else if (Request.UrlReferrer.Host == Request.Url.Host)
    {
      divInternalReferrer.Visible = true;
    }
    else if (GetSearchKey() != string.Empty)
    {
      SearchTerm = GetSearchTerm(GetSearchKey());
      BindSearchResult();
      divSearchEngine.Visible = true;
    }
    else if (Request.UrlReferrer != null)
    {
      divExternalReferrer.Visible = true;    
    }
  }

  //checks in QueryString "aspxerrorpath" and puts the results in divDirectHit\phSearchResult
  private void DirectHitSearch()
  {
    string from = Request.QueryString["aspxerrorpath"];
    int index = from.LastIndexOf("/") + 1;
    string title = from.Substring(index).Replace(".aspx", string.Empty).Replace("-", " ");

    List<IShowed> items = StaticSearch.Hits(title, false);
    if (items.Count > 0)
    {
      LiteralControl result = new LiteralControl(string.Format("<li><a href=\"{0}\">{1}</a></li>", items[0].RelativeLink.ToString(), items[0].Title));
      phSearchResult.Controls.Add(result);
    }
  }

  //searches and fills divSearchEngine/divSearchResult
  private void BindSearchResult()
  {
      List<IShowed> items = StaticSearch.Hits(SearchTerm, false);
    int max = 1;
    foreach (IShowed item in items)
    {
      HtmlAnchor link = new HtmlAnchor();
      link.InnerHtml = item.Title;
      link.HRef = item.RelativeLink.ToString();
      divSearchResult.Controls.Add(link);

      if (!string.IsNullOrEmpty(item.Description))
      {
        HtmlGenericControl desc = new HtmlGenericControl("span");
        desc.InnerHtml = item.Description;

        divSearchResult.Controls.Add(new LiteralControl("<br />"));
        divSearchResult.Controls.Add(desc);
      }

      divSearchResult.Controls.Add(new LiteralControl("<br />"));
      max++;
      if (max == 3)
        break;
    }
  }

  protected string SearchTerm = string.Empty;//protected becoz this is used in the inheriting page
  
  // returns the string which is the start of the search term in famous search sites
  private string GetSearchKey()
  {
    string referrer = Request.UrlReferrer.Host.ToLowerInvariant();
    if (referrer.Contains("google.") && referrer.Contains("q="))
      return "q=";

    if (referrer.Contains("yahoo.") && referrer.Contains("p="))
      return "p=";

    if (referrer.Contains("q="))
      return "q=";

    return string.Empty;
  }

  // referrer string will be like " www.google.com?q=search+term&etc... "
  private string GetSearchTerm(string key)
  {
    string referrer = Request.UrlReferrer.ToString();
    int start = referrer.IndexOf(key) + key.Length;
    int stop = referrer.IndexOf("&", start);
    if (stop == -1)
      stop = referrer.Length;

    string term = referrer.Substring(start, stop - start);
    return term.Replace("+", " ");
  }
  // return string will be like "search term"
}
