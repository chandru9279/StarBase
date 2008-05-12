using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace Thon.Support.Web.Controls
{	
    ///<summary>
    ///Does deletepost if it appears in the query string, & if user is an authenticated author
    ///Adds MetaContent-Type, RSDLink, SyndicationLink, JavaScriptKeys, 
    ///Includes CommonScripts.js using a <link> element 
    ///To ALL the aspx'es that extend this class.    
    ///</summary>
	public abstract class ThonBasePage : System.Web.UI.Page
	{	
	    ///<summary>
		/// Adds links and javascript to the HTML header tag.
        /// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsCallback && !Page.IsPostBack)
			{
                AddMetaContentType();

                AddGenericLink("start", ThonSettings.Instance.Name, HelperUtilities.RelativeAppRoot);

                if (ThonSettings.Instance.EnableOpenSearch)
					AddOpenSearchLinkInHeader();
								
				if (!string.IsNullOrEmpty(ThonSettings.Instance.HtmlHeader))
					AddCustomCodeToHead();

				if (!string.IsNullOrEmpty(ThonSettings.Instance.TrackingScript))
					AddTrackingScript();
			}

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
            //Encoding is usuall present in the httpheader, we're just adding it as a meta tag
        }

        public virtual void AddGenericLink(string relation, string title, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = href;
            Page.Header.Controls.Add(link);
        }

        protected virtual void AddOpenSearchLinkInHeader()
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = "search";
            link.Attributes["href"] = HelperUtilities.InternetAppRoot + "ThonHttpHandlers/OpenSearch.ashx";
            link.Attributes["type"] = "application/opensearchdescription+xml";
            link.Attributes["title"] = ThonSettings.Instance.Name;
            Page.Header.Controls.Add(link);
            //<link rel="search" href="http://www.thon.net/ThonZNet/ThonHttpHandlers/OpenSearch.ashx"
            // type="application/opensearchdescription+xml" title="Chandru's Website">
        }

        protected virtual void AddCustomCodeToHead()
        {
            // Adds code in ThonSettings.Instance.HtmlHeader to the head of the page
            string code = string.Format("{0}<!-- Start custom code -->{0}{1}{0}<!-- End custom code -->{0}", Environment.NewLine, ThonSettings.Instance.HtmlHeader);
            LiteralControl control = new LiteralControl(code);
            Page.Header.Controls.Add(control);
        }

        /// <summary>
        /// Adds a JavaScript to the bottom of the page at runtime.    		
        /// You must add the script tags to the ThonSettings.Instance.TrackingScript.		
        /// </summary>
        protected virtual void AddTrackingScript()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "tracking", "\n" + ThonSettings.Instance.TrackingScript, false);
        }

        protected virtual void CompressCss()
        {
            foreach (Control control in Page.Header.Controls)
            {
                HtmlControl c = control as HtmlControl;
                if (c != null && c.Attributes["type"] != null && c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase))
                {
                    if (!c.Attributes["href"].StartsWith("http://"))
                    {
                        c.Attributes["href"] = HelperUtilities.RelativeAppRoot + "StyleSheets/Css.ashx?name=~/" + c.Attributes["href"];
                        //eg href = StyleSheets/MasterPageStyleSheet.css will be converted into :
                        // /ThonZNet/ThonHttpHandlers/Css.ashx?name=~/StyleSheets/MasterPageStyleSheet.css
                        c.EnableViewState = false;
                    }
                }
            }
        }
		
        /// <summary>
        /// Not used here actually, just present for any future use:-)
        /// </summary>
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
			}

			base.OnError(e);
		}
		
	}
}