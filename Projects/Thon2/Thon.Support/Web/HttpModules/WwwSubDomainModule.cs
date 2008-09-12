using System;
using System.Web;
using Thon.Support;
using System.Text.RegularExpressions;

namespace Thon.Support.Web.HttpModules
{
  // Removes or adds the www subdomain from all requests and makes a permanent redirection to the new location.
  // Works on GET requests only. POST request packets need not be redirected at all coz they can occur only after GET.
  public class WwwSubDomainModule : IHttpModule
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
      if (string.IsNullOrEmpty(ThonSettings.Instance.HandleWwwSubdomain) || ThonSettings.Instance.HandleWwwSubdomain == "ignore")
        return;
      HttpContext context = (sender as HttpApplication).Context;
      if (context.Request.HttpMethod != "GET" || context.Request.RawUrl.Contains("/admin/") || context.Request.IsLocal)
        return;

      if (context.Request.PhysicalPath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
      {
        string url = context.Request.Url.ToString();

        if (url.Contains("://www.") && ThonSettings.Instance.HandleWwwSubdomain == "remove")
          RemoveWww(context);
        
        if (!url.Contains("://www.") && ThonSettings.Instance.HandleWwwSubdomain == "add")
          AddWww(context);        
      }
    }

    // Adds the www subdomain to the request and redirects.
    private static void AddWww(HttpContext context)
    {
      string url = context.Request.Url.ToString().Replace("://", "://www.");
      PermanentRedirect(url, context);
    }

    // Removes the www subdomain from the request and redirects.
    // DictionaryEntry object will contain
    private static Regex _Regex = new Regex("(http|)://www\\.", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static void RemoveWww(HttpContext context)
    {
      string url = context.Request.Url.ToString();
      if (_Regex.IsMatch(url))
      {
        //The value of the $1...$9 properties is modified whenever a successful parenthesized match is made.
        //Any number of parenthesized substrings may be specified in a regular expression pattern,
        //but only the nine most recent can be stored.
        //Here $1 is replaced by http or https, the paranthesized regex being (http|https).
        //http://www. becomes http://
        url = _Regex.Replace(url, "$1://");        
        PermanentRedirect(url, context);
      }
    }

    // After adding or removing www, this function is called: this removes default.aspx and tells the client
    // also to skip it.
    // Sends permanent redirection headers (301)
    private static void PermanentRedirect(string url, HttpContext context)
    {
			if (url.EndsWith("Default.aspx", StringComparison.OrdinalIgnoreCase))
				url = url.Replace("Default.aspx", string.Empty);

      context.Response.Clear();
      context.Response.StatusCode = 301; //permanantly moved to another location
      context.Response.AppendHeader("location", url);
      context.Response.End();
    }
  }
}