using System;
using System.IO;
using System.Web;

namespace Thon.Support.Web.HttpHandlers
{
    /// <summary>
    /// </remarks>
    public class CssImage : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["image"]))
            {
                string filename = context.Request.QueryString["image"];
                string filepath = context.Request.Path.Replace("CssImage.ashx",filename);

                OnServing(filepath);
                try
                {
                    FileInfo fi = new FileInfo(context.Server.MapPath(filepath));

                    if (fi.Exists)
                    {
                        int index = filepath.LastIndexOf(".") + 1;
                        string extension = filepath.Substring(index).ToUpperInvariant();

                        // Written in the forum that IE is not handling jpg image types. This is the fix.
                        if (string.Compare(extension, "JPG") == 0)
                            context.Response.ContentType = "image/jpeg";
                        else
                            context.Response.ContentType = "image/" + extension;

                        string etag = "\"" + fi.CreationTimeUtc.GetHashCode() + "\"";
                        string incomingEtag = context.Request.Headers["If-None-Match"];

                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
                        context.Response.Cache.SetLastModified(fi.CreationTimeUtc);
                        context.Response.Cache.SetETag(etag);

                        if (String.Compare(incomingEtag, etag) == 0)
                        {
                            context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        }
                        else
                        {
                            context.Response.TransmitFile(fi.FullName);
                        }

                        OnServed(filepath);
                    }
                    else
                    {
                        OnBadRequest(filepath);
                        context.Response.Status = "404 Bad Request";
                    }
                }
                catch (Exception ex)
                {
                    OnBadRequest(ex.Message);
                    context.Response.Status = "404 Bad Request";
                }
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