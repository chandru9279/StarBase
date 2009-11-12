using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Caching;

namespace Thon.Starcraft
{
    public partial class FullScreenShotAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string request = Request.QueryString["image"];
            if (request.Contains(".jpg"))
            {
                HtmlImage hi = new HtmlImage();
                hi.Src = "Effect/ScreenShots/" + request;
                hi.Alt = "Full Size ScreenShot of " + request;
                ScreenshotPlaceholder.Controls.Add(hi);
            }
            else
            {
                HtmlAnchor ha = new HtmlAnchor();
                ha.HRef = "javascript:alert(&quot;No fooling around the site, buddy. Zasz Rules!&quot;)";
                ha.InnerText = "Security Warning";
                HtmlGenericControl hgc = new HtmlGenericControl("h1");
                hgc.Controls.Add(ha);
                ScreenshotPlaceholder.Controls.Add(hgc);
            }
        }
        protected void ButtonPrev_Click(object sender, EventArgs e)
        {
            List<string> lin = AddToCache();
            string request = Request.QueryString["image"];
            int index = lin.FindIndex(delegate(string a)
            {
                if(a.Equals(request,StringComparison.InvariantCultureIgnoreCase))
                    return true;
                return false;
            });
            if (index != lin.Count - 1)
                Response.Redirect("FullScreenShot.aspx?image=" + lin[index + 1]);
            else
                Response.Redirect("FullScreenShot.aspx?image=" + lin[0]);
        }

        private List<string> AddToCache()
        {
            if (Cache["imagelist"] == null)
            {
                List<string> lin = new List<string>();
                lin.Add("ss19-hires.jpg");
                lin.Add("ss28-hires.jpg");
                lin.Add("ss31-hires.jpg");
                lin.Add("ss35-hires.jpg");
                lin.Add("ss36-hires.jpg");
                lin.Add("ss37-hires.jpg");
                lin.Add("ss38-hires.jpg");
                lin.Add("ss39-hires.jpg");
                lin.Add("ss40-hires.jpg");
                lin.Add("ss41-hires.jpg");
                lin.Add("ss42-hires.jpg");
                Cache.Insert("imagelist", lin, null, DateTime.Now.AddMonths(1), TimeSpan.Zero);
            }
            return (List<string>)(Cache["imagelist"]);
        }
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            List<string> lin = AddToCache();
            string request = Request.QueryString["image"];
            int index = lin.FindIndex(delegate(string a)
            {
                if (a.Equals(request, StringComparison.InvariantCultureIgnoreCase))
                    return true;
                return false;
            });
            if (index != 0)
                Response.Redirect("FullScreenShot.aspx?image=" + lin[index - 1]);
            else
                Response.Redirect("FullScreenShot.aspx?image=" + lin[lin.Count - 1]);
        }
}
}
