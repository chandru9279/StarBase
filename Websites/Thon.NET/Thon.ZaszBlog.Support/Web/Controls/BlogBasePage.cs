using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.Support;

namespace Thon.ZaszBlog.Support.Web.Controls
{	
    ///<summary>
    ///Does deletepost if it appears in the query string, & if user is an authenticated author
    ///Adds MetaContent-Type, RSDLink, SyndicationLink, JavaScriptKeys, 
    ///Includes CommonScripts.js using a <![CDATA[<link>]]>element 
    ///To ALL the aspx'es that extend this class.    
    ///</summary>
	public abstract class BlogBasePage : System.Web.UI.Page
	{		
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

            //A deletepost request is handled by simply giving a querystring with deletepost along 
            //with any other resource request... Only the authenticated authors can do this...
			if (!Page.IsPostBack && !string.IsNullOrEmpty(Request.QueryString["deletepost"]))
			{
				if (Page.User.Identity.IsAuthenticated)
				{
					Post post = Post.GetPost(new Guid(Request.QueryString["deletepost"]));
					if (Page.User.IsInRole("Family") || Page.User.Identity.Name == post.Author)
					{
						post.Delete();
						post.Save();
						Response.Redirect(SupportUtilities.RelativeWebRoot);
					}
				}
			}
		}
        
        /// <summary>
        /// Adds links and javascript to the HTML header tag.
        /// </summary>
        /// <param name="e">PageLoad EventArgs</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsCallback && !Page.IsPostBack)
			{
				AddMetaContentType();				
				AddSyndicationLink();
                
				AddGenericLink("contents", "Archive", SupportUtilities.RelativeWebRoot + "archive.aspx");
				AddGenericLink("start", BlogSettings.Instance.Name, SupportUtilities.RelativeWebRoot);
                //<link rel="contents" title="Archive" href="/ThonZNet/ZaszBlog/archive.aspx">
                //<link rel="start" title="ZaszBlog" href="/ThonZNet/ZaszBlog/">
				AddJavaScriptKeys();

    			if (ThonSettings.Instance.EnableOpenSearch)
					AddOpenSearchLinkInHeader();
								
				if (!string.IsNullOrEmpty(ThonSettings.Instance.HtmlHeader))
					AddCustomCodeToHead();

				if (!string.IsNullOrEmpty(ThonSettings.Instance.TrackingScript))
					AddTrackingScript();
			}

			AddJavaScriptInclude(SupportUtilities.RelativeWebRoot + "CommonScripts.js");
            if (ThonSettings.Instance.RemoveWhitespaceInStyleSheets)
				CompressCss();
		}
                
        protected virtual void AddMetaContentType()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "content-type";
            // HTTP-EQUIV elements are used to specify system variables and are treated as part of the HTTP response header
            meta.Content = Response.ContentType + "; charset=" + Response.ContentEncoding.HeaderName;
            Page.Header.Controls.Add(meta);
            //eg:<meta http-equiv = "content-type" content = "text/css|etc; charset=utf-8|ascii|etc">
            //default content encoding can be specified in the <globalization> element in the Web.config
        }

        protected virtual void AddSyndicationLink()
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = "alternate";
            link.Attributes["type"] = "application/rss+xml";
            link.Attributes["title"] = BlogSettings.Instance.Name;
            link.Attributes["href"] = SupportUtilities.FeedUrl;
            //eg:<link rel="alternate" type="application/rss+xml" title="ZaszBlog" href=http://www.thon.net/ThonZNet/ZaszBlogHttpHandlers/Syndication.ashx
            // | http://feeds.feedburner.com/Zasz > where probably http://www.thon.net/ThonZNet/ZaszBlog/ZaszBlogHttpHandlers/Syndication.ashx is registered. 
            Page.Header.Controls.Add(link);
        }

        public virtual void AddGenericLink(string relation, string title, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = href;
            Page.Header.Controls.Add(link);
        }

        /// <summary>
        /// This function creates a jscript stmt that creates jscript variables and assigns them 
        /// values from serverside C# functions..
        /// </summary>
        protected virtual void AddJavaScriptKeys()
		{            
			StringBuilder sb = new StringBuilder();			
			sb.AppendFormat("KEYwebRoot='{0}';", SupportUtilities.RelativeWebRoot);
			HtmlGenericControl script = new HtmlGenericControl("script");
			script.Attributes.Add("type", "text/javascript");
			script.InnerText = sb.ToString();
			Page.Header.Controls.Add(script);
		}

        protected virtual void AddOpenSearchLinkInHeader()
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = "search";
            link.Attributes["href"] = SupportUtilities.InternetAppRoot + "ThonHttpHandlers/OpenSearch.ashx";
            link.Attributes["type"] = "application/opensearchdescription+xml";
            link.Attributes["title"] = ThonSettings.Instance.Name;
            Page.Header.Controls.Add(link);
            //<link rel="search" href="http://www.thon.net/ThonZNet/ThonHttpHandlers/OpenSearch.ashx"
            // type="application/opensearchdescription+xml" title="ZaszBlog">
        }

        /// <summary>
        /// Adds code in BlogSettings.Instance.HtmlHeader to the head of the page
        /// </summary>
        protected virtual void AddCustomCodeToHead()
        {            
            string code = string.Format("{0}<!-- Start custom code -->{0}{1}{0}<!-- End custom code -->{0}", Environment.NewLine, ThonSettings.Instance.HtmlHeader);
            LiteralControl control = new LiteralControl(code);
            Page.Header.Controls.Add(control);
        }

        /// <summary>
        /// Adds a JavaScript to the bottom of the page at runtime, from the browser.		
        /// You must add the script tags to the BlogSettings.Instance.TrackingScript.
        /// </summary>
        protected virtual void AddTrackingScript()
        {
           ClientScript.RegisterStartupScript(this.GetType(), "tracking", "\n" + ThonSettings.Instance.TrackingScript, false);
        }

        public virtual void AddJavaScriptInclude(string url)
        {
            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            //Server.UrlEncode replaces %20 for space, and other replacements for symbols like '<', '>'
            script.Attributes["src"] = HelperUtilities.RelativeAppRoot + "ThonHttpHandlers/JScript.ashx?path=" + Server.UrlEncode(url);
            Page.Header.Controls.Add(script);
        }

        protected virtual void CompressCss()
        {
            foreach (Control control in Page.Header.Controls)
            {
                HtmlControl c = control as HtmlControl;
                if (c != null && c.Attributes["type"] != null && c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase))
                {
                    string href = c.Attributes["href"];
                    if (!href.StartsWith("http://"))
                    {
                        c.Attributes["href"] = href.Insert(href.LastIndexOf('/') + 1, "Css.ashx?name=");
                        c.EnableViewState = false;
                    }
                }
            }
        }
		        
		protected virtual void AddMetaTag(string name, string value)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
				return;

			HtmlMeta meta = new HtmlMeta();
			meta.Name = name;
			meta.Content = value;
			Page.Header.Controls.Add(meta);
		}
        
		protected override void OnError(EventArgs e)
		{
			HttpContext ctx = HttpContext.Current;
			Exception exception = ctx.Server.GetLastError();

			if (exception != null && exception.Message.Contains("callback"))
			{
				// This is a robot spam attack so we send it a 404 status to make it go away.
				ctx.Response.StatusCode = 404;	//404 - File Not Found			
				ctx.Server.ClearError();
				Comment.OnSpamAttack();
			}
			base.OnError(e);
		}
		
	}
}