using System;
using System.Xml;
using System.Web;
using Thon.ZaszBlog.Support;
using Thon.Support;

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

                withRelativeUrl("Starcraft/Artwork.aspx", writer);
                withRelativeUrl("Starcraft/BlackList.aspx", writer);
                withRelativeUrl("Starcraft/Default.aspx", writer);
                withRelativeUrl("Starcraft/Gamers.aspx", writer);
                withRelativeUrl("Starcraft/Screenshots.aspx", writer);
                withRelativeUrl("Starcraft/Wallpapers.aspx", writer);
                withRelativeUrl("Chandirasekar/Default.aspx", writer);
                withRelativeUrl("CreateUser.aspx", writer);
                withRelativeUrl("Credits.aspx", writer);
                withRelativeUrl("Default.aspx", writer);
                withRelativeUrl("Gallery.aspx", writer);
                //withRelativeUrl("Index.html", writer); No need default.aspx serves index.html
                withRelativeUrl("SEODefault.aspx", writer);
                withRelativeUrl("CCA25.aspx", writer);
                withRelativeUrl("GNULGPL.aspx", writer);
                withRelativeUrl("GNUGPL.aspx", writer);
                withRelativeUrl("MSPL.aspx", writer);
                withRelativeUrl("MSRL.aspx", writer);
                withRelativeUrl("CodeHighlight.aspx", writer);
                withRelativeUrl("Linkz.aspx", writer);
                withRelativeUrl("Locate.aspx", writer);
                withRelativeUrl("Login.aspx", writer);



                writer.WriteEndElement();
                context.Response.ContentType = "text/xml";
            }
        }

        /// <summary>
        /// Google schema, Change frequency is assumed monthly, last modified is always one month before
        /// so that search engines update this every time they crawl it.
        /// </summary>
        /// <param name="relativePath">
        /// The path that must be appended to HelperUtilities.InternetAppRoot = http://www.chandruon.net (or) http://www.localhost:port/
        /// </param>
        private void withRelativeUrl(string relativePath, XmlWriter writer)
        {

            writer.WriteStartElement("url");
            writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + relativePath);
            writer.WriteElementString("lastmod", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteElementString("changefreq", "monthly");
            writer.WriteEndElement();
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