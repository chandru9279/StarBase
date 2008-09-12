using System;
using System.IO;
using System.Web;
using Thon.Support;

namespace Thon.Support.Web.HttpHandlers
{
    /// <summary>
    /// The FileHandler serves all files that is uploaded to the site except for images. 
    /// By using a HttpHandler to serve files, it is very easy to add the capability to stop
    /// bandwidth leeching or to create a statistics analysis feature upon it.  
    /// </summary>
    public class FileHandler : IHttpHandler
    {

    #region IHttpHandler Members
    public bool IsReusable
    {
      get { return false; }
    }
    public void ProcessRequest(HttpContext context)
    {
      if (!string.IsNullOrEmpty(context.Request.QueryString["filepath"]))
      {
			    string filepath = context.Request.QueryString["filepath"];
			    OnServing(filepath);

			    try
			    {
                    FileInfo info = new FileInfo(context.Server.MapPath(filepath));
    				
				    if (info.Exists && info.Directory.FullName.ToUpperInvariant().Contains("\\FILES"))
				    {
					    context.Response.AppendHeader("Content-Disposition", "inline; filename=" + filepath);
					    SetContentType(context, filepath);
					    context.Response.TransmitFile(info.FullName);
					    OnServed(filepath);
				    }
				    else
				    {
					    OnBadRequest(filepath);
					    context.Response.Status = "404 Bad Request";
				    }
			    }
			    catch (Exception)
			    {
				    OnBadRequest(filepath);
				    context.Response.Status = "404 Bad Request";
			    }
      }
    }

    private static void SetContentType(HttpContext context, string filepath)
    {
      if (filepath.EndsWith(".pdf"))
      {
        context.Response.AddHeader("Content-Type", "application/pdf");
      }
      else 
      {
	    context.Response.AddHeader("Content-Type", "application/octet-stream");
      }
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the requested file does not exist;
    /// </summary>
    public static event EventHandler<EventArgs> Serving;

    private static void OnServing(string file)
    {
      if (Serving != null)
      {
        Serving(file, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Occurs when a file is served;
    /// </summary>
    public static event EventHandler<EventArgs> Served;

    private static void OnServed(string file)
    {
      if (Served != null)
      {
        Served(file, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Occurs when the requested file does not exist;
    /// </summary>
    public static event EventHandler<EventArgs> BadRequest;

    private static void OnBadRequest(string file)
    {
      if (BadRequest != null)
      {
        BadRequest(file, EventArgs.Empty);
      }
    } 

    #endregion

    }

}