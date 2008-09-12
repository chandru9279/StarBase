﻿using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls.WebParts;
using Thon.ZaszBlog.Support;
using Thon.ZaszBlog.Support.Web.Controls;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon
{
    public partial class FSSearchAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        private const int PAGE_SIZE = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                bool includeComments = Request.QueryString["comment"] == "true";
                BindSearchResult(Request.QueryString["q"], includeComments);
                Page.Title = "Search Results For '" + Request.QueryString["q"] + "'";
                h1Headline.InnerText = Page.Title;
            }
            else
            {
                Page.Title = "Search";
                h1Headline.InnerHtml = "Search";
            }

            base.AddMetaTag("description", "Search for Thon.NET site and blog." + BlogSettings.Instance.Description);
        }

        private void BindSearchResult(string searchTerm, bool includeComments)
        {
            int page = 1;
            if (Request.QueryString["page"] != null)
            {
                page = int.Parse(Request.QueryString["page"]);
            }

            int start = PAGE_SIZE * (page - 1);
            List<IShowed> list = StaticSearch.Hits(searchTerm, includeComments);

            /* Add the search results from the rest of the site to this list here */
            if (list.Count == 0) lblNoResults.InnerText = "Sorry. No results matching your search string was found.";
            if (start <= list.Count - 1)
            {
                int size = PAGE_SIZE;
                if (start + size > list.Count)
                    size = list.Count - start;

                rep.DataSource = list.GetRange(start, size);
                rep.DataBind();

                BindPaging(list.Count, page - 1);
            }
        }

        protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlGenericControl control = (HtmlGenericControl)e.Item.FindControl("type");
            string type = "<strong>Type</strong>: {0}";
            string categories = "<strong>Categories</strong> : {0}";
            string tags = "<strong>Tags</strong> : {0}";
            string keywords = "<strong>Keywords</strong> : {0}    ";
            if (e.Item.DataItem is Comment)
            {
                control.InnerHtml = string.Format(type, "Comment");
            }
            else if (e.Item.DataItem is Post)
            {
                Post post = (Post)e.Item.DataItem;
                string text = string.Format(type, "Post");
                if (post.Categories.Count > 0)
                {
                    string cat = string.Empty;
                    foreach (Category category in post.Categories)
                    {
                        cat += category.Title + ", ";
                    }

                    text += "<br />" + string.Format(categories, cat.Substring(0, cat.Length - 2));
                }

                if (post.Tags.Count > 0)
                {
                    string t = string.Empty;
                    foreach (string tag in post.Tags)
                    {
                        t += tag + ", ";
                    }

                    text += "<br />" + string.Format(tags, t.Substring(0, t.Length - 2));
                }

                control.InnerHtml = text;
            }
            else if (e.Item.DataItem is Thon.ZaszBlog.Support.CodedRepresentations.Page)
            {
                Thon.ZaszBlog.Support.CodedRepresentations.Page page = (Thon.ZaszBlog.Support.CodedRepresentations.Page)e.Item.DataItem;
                string text = string.Format(type, "Page");

                if (!string.IsNullOrEmpty(page.Keywords))
                {
                    text += "<br />" + string.Format(keywords, page.Keywords);
                }

                control.InnerHtml = text;
            }
        }

        private void BindPaging(int results, int page)
        {
            if (results <= PAGE_SIZE)
                return;

            decimal pages = Math.Ceiling((decimal)results / (decimal)PAGE_SIZE);

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            for (int i = 0; i < pages; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                if (i == page)
                {
                    li.Attributes.Add("class", "active");
                }

                HtmlAnchor a = new HtmlAnchor();
                a.InnerHtml = (i + 1).ToString();

                string comment = string.Empty;
                if (Request.QueryString["comment"] != null)
                {
                    comment = "&amp;comment=true";
                }

                a.HRef = "?q=" + Request.QueryString["q"] + comment + "&amp;page=" + (i + 1);

                li.Controls.Add(a);
                ul.Controls.Add(li);
            }
            Paging.Controls.Add(ul);
        }

        #region Data manipulation

        /// <summary>
        /// Removes the comment anchor from the URL
        /// </summary>
        protected string ShortenUrl(string uri)
        {
            string url = SupportUtilities.ConvertToAbsolute(uri).ToString();
            if (!url.Contains("#"))
                return url;

            int index = url.IndexOf("#");
            return url.Substring(0, index);
        }

        /// <summary>
        /// Shortens the content to fit to a search result
        /// </summary>
        protected string GetContent(string description, string content)
        {
            string text = string.Empty;
            if (!string.IsNullOrEmpty(description))
            {
                text = description;
            }
            else
            {
                text = StripHtml(content);
                if (text.Length > 200)
                    text = text.Substring(0, 200) + " …";
                text = "“" + text.Trim() + "”";
            }

            return text;
        }

        private static Regex _Regex = new Regex("<[^>]*>", RegexOptions.Compiled);
        /// <summary>
        /// Removes all HTML tags from a given string
        /// </summary>
        private static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return _Regex.Replace(html, string.Empty);
        }

        #endregion
}
}