using System;
using System.Web;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Web.HttpHandlers
{
  // Sends out the blogroll.xml file
  public class OpmlHandler : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/xml";
      context.Response.AppendHeader("Content-Disposition", "attachment; filename=opml.xml");
      context.Response.TransmitFile(  context.Request.PhysicalApplicationPath + BlogSettings.Instance.StorageLocation.Replace("~/",string.Empty).Replace("/","\\") + "blogroll.xml");
    }
    public bool IsReusable
    {
        get { return false; }
    }

  }
}