using System;
using System.Xml;
using System.Web;
using Thon.Support;
using Thon.ZaszBlog.Support;
// Thon.Website coresponds to AppCode directory
namespace Thon.Website.HttpHandlers
{
    ///<summary>
    /// Displays the open search XML provider as described at http://opensearch.a9.com/
    /// This is not exactly NECESSARY. But many sites make use of this feature to get links for search & syndication for community support.
    /// The OpenSearch document needs to be linked to from the HTML head tag. This link will be added automatically from BlogBasePage.cs.
    /// </summary>
    public class OpenSearchHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (XmlWriter writer = XmlWriter.Create(context.Response.OutputStream))
            {
                writer.WriteStartElement("OpenSearchDescription", "http://a9.com/-/spec/opensearch/1.1/");

                writer.WriteElementString("ShortName", ThonSettings.Instance.Name);
                writer.WriteElementString("Description", ThonSettings.Instance.Description);

                writer.WriteRaw("<Image height=\"96\" width=\"96\" type=\"image/png\">" + HelperUtilities.InternetAppRoot.ToString() + "Images/Logo/Thon/logo_96.png</Image>");

                writer.WriteStartElement("Url");
                writer.WriteAttributeString("type", "text/html");
                writer.WriteAttributeString("template", HelperUtilities.InternetAppRoot.ToString() + "ZaszBlog/Search.aspx?q={searchTerms}");

                writer.WriteStartElement("Url");
                writer.WriteAttributeString("type", "application/rss+xml");
                writer.WriteAttributeString("template", SupportUtilities.AbsoluteWebRoot.ToString() + "ZaszBlogHttpHandlers/Syndication.ashx?q={searchTerms}");

                writer.WriteEndElement();
            }

        context.Response.ContentType = "text/xml";
        context.Response.Cache.SetValidUntilExpires(true);
        context.Response.Cache.SetExpires(DateTime.Now.AddDays(30));
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.Cache.SetLastModified(DateTime.Now);
        context.Response.Cache.SetETag(Guid.NewGuid().ToString());
        ThonSettings.Changed += delegate { context.Response.Cache.SetExpires(DateTime.Now.AddDays(30)); };
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}

/*
Sample rendered XML :
  
<OpenSearchDescription>
<ShortName>ZaszBlog</ShortName>
<Description>Beta</Description>
<Image height="16" width="16" type="image/png">
http://localhost:49174/ThonZnet/Images/Logo/Thon/logo_96.png
</Image>
<Url type="text/html" template="http://localhost:49174/ThonZnet/ZaszBlog/Search.aspx?q={searchTerms}">
<Url type="application/rss+xml" template="http://localhost:49174/ThonZnet/ZaszBlog/ZaszBlogHttpHandlers/Syndication.ashx?q={searchTerms}"/>
</Url>
</OpenSearchDescription>
*/