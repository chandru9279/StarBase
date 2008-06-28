using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;


namespace Thon.Support.Web.HttpModules
{
  
  // Rewrites Urls for SEO purposes - To make Dynamic Pages seem static.
  public class RewriteUrl : IHttpModule
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
        string aspx = url.Substring(url.LastIndexOf("/") + 1);
        List<string> GhostAspxes;
        if (context.Cache["GhostAspxes"] == null)
        {
            GhostAspxes = new List<string>();            
            GhostAspxes.Add("CCA25.ASPX");
            GhostAspxes.Add("MSPL.ASPX");
            GhostAspxes.Add("MSRL.ASPX");
            GhostAspxes.Add("CODEHIGHLIGHT.ASPX");
            GhostAspxes.Add("GNULGPL.ASPX");
            GhostAspxes.Add("GNUGPL.ASPX");
            context.Cache["GhostAspxes"] = GhostAspxes;
        }
        else
            GhostAspxes = (List<string>)context.Cache["GhostAspxes"];

        if (GhostAspxes.Contains(aspx))
        {
            string path = aspx.Remove(aspx.IndexOf('.')).ToLowerInvariant();
            context.RewritePath(HelperUtilities.RelativeAppRoot + "License.aspx?license=" + path, false);
        }
    }
  }
}
