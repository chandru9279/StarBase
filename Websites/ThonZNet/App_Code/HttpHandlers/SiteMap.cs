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

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

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

    

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\Artwork.aspx'");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\BlackList.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\Default.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();





                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\Gamers.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\Screenshots.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

    
    
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Starcraft\Wallpapers.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();






                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "chandirasekar\Default.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();





                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "CreateUser.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Credits.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InterneAppRoot + "Default.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();





                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Gallery.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();






                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Index.html");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();



                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "CCA25.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "GNULGPL.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();




                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "GNUGPL.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "MSPL.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "MSRL.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "CodeHighlight.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Linkz.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Locate.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                writer.WriteStartElement("url");
                writer.WriteElementString("loc", HelperUtilities.InternetAppRoot + "Login.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

     
     
     
     
