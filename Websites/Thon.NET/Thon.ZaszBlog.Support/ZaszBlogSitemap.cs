using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support
{
    public class ZaszBlogSitemap
    {
        public static void ReturnUrlSet(XmlWriter writer)
        {
            foreach (Post post in Post.Posts)
            {
                if (post.IsVisible)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", post.AbsoluteLink.ToString());
                    writer.WriteElementString("lastmod", post.DateModified.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("changefreq", "monthly");
                    writer.WriteEndElement();
                }
            }

            foreach (Page page in Page.Pages)
            {
                if (page.IsVisible)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", page.AbsoluteLink.ToString());
                    writer.WriteElementString("lastmod", page.DateModified.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("changefreq", "monthly");
                    writer.WriteEndElement();
                }
            }

            // Also adding the main data-containable aspx pages
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", SupportUtilities.AbsoluteWebRoot.ToString() + "Archive.aspx");
            writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteElementString("changefreq", "daily");
            writer.WriteEndElement();

            writer.WriteStartElement("url");
            writer.WriteElementString("loc", SupportUtilities.AbsoluteWebRoot.ToString() + "Contact.aspx");
            writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteElementString("changefreq", "monthly");
            writer.WriteEndElement();

            if (Page.GetFrontPage() != null)
            {
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", SupportUtilities.AbsoluteWebRoot.ToString() + "Blog.aspx");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "daily");
                writer.WriteEndElement();
            }
        }
    }
}
