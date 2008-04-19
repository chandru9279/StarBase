using System;
using System.Xml;
using System.Web;
using Thon.ZaszBlog.Support;

namespace Thon.Website.HttpHandlers
{
    ///<summary>
    /// The search engine looks at the robots.txt and sees that to get an xml sitemap,
    /// it has to request sitemap.axd, which requests are handled by this Http Handler.
    /// This provides the actual links google has to query&index to get blog data, in a 
    /// schema which google has specified. Zasz : Think this same schema is supported by
    /// other major search engines also : Yahoo,MSN,Live etc.
    /// </summary>
    public class SiteMap : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (XmlWriter writer = XmlWriter.Create(context.Response.OutputStream))
            {
                writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.84");

                ZaszBlogSitemap.ReturnUrlSet(writer);
                
                writer.WriteEndElement();
                context.Response.ContentType = "text/xml";

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
} 