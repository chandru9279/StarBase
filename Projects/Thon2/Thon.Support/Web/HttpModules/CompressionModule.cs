using System;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.IO;
using System.IO.Compression;

namespace Thon.Support.Web.HttpModules
{
    ///<summary>
    /// Compresses the output using standard gzip/deflate.
    /// </summary>
    public sealed class CompressionModule : IHttpModule
    {

    #region IHttpModule Members

        void IHttpModule.Dispose()
        {
        }

        /// <summary>
        /// In this module we do compression for ordinary aspx page generated response..
        /// Why this is done in the prerequest handler execute is that ---> the response.filter object may be set 
        /// anywhere .. ASP runtime uses it to supply the generated stream from the aspx page hanlers
        /// and get the filtered stream back to send to client...(So we can set it before response is 
        /// written like here or after request is written like in css handler)
        /// </summary>
        void IHttpModule.Init(HttpApplication context)
        {
            if (ThonSettings.Instance.EnableHttpCompression)
            {                
                context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
            }
        }

    #endregion

    private const string GZIP = "gzip";
    private const string DEFLATE = "deflate";

    #region Compress page

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Context.CurrentHandler is System.Web.UI.Page && app.Request["HTTP_X_MICROSOFTAJAX"] == null)
            {
                if (IsEncodingAccepted(DEFLATE))
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(DEFLATE);
                }
                else if (IsEncodingAccepted(GZIP))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(GZIP);
                }
            }
            else if (app.Context.Request.Path.Contains("WebResource.axd"))
            {
                app.Context.Response.Cache.SetExpires(DateTime.Now.AddDays(30));
            }
        }

        ///<summary>
        /// Checks the request headers to see if the specified encoding is accepted by the client.
        /// These are already present in the CssHandler.cs
        /// </summary>
        private static bool IsEncodingAccepted(string encoding)
        {
            HttpContext context = HttpContext.Current;
            return context.Request.Headers["Accept-encoding"] != null && context.Request.Headers["Accept-encoding"].Contains(encoding);
        }    
        private static void SetEncoding(string encoding)
        {
            HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
        }
    #endregion

    }
}