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
    public partial class FullWallpaperAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string request = Request.QueryString["image"];
            if (request.Contains(".jpg"))
            {
                HtmlImage hi = new HtmlImage();
                hi.Src = "Effect/WallPapers/" + request;
                hi.Alt = "Full Size WallPaper of " + request;
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
                Response.Redirect("FullWallpaper.aspx?image=" + lin[index + 1]);
            else
                Response.Redirect("FullWallpaper.aspx?image=" + lin[0]);
        }

        private List<string> AddToCache()
        {
            if (Cache["imagelist"] == null)
            {
                List<string> lin = new List<string>();
                lin.Add("Bel_Shir_environment.jpg");
                lin.Add("HallWay_environment.jpg");
                lin.Add("Kel-Mor_concept.jpg");
                lin.Add("MarSara_environment.jpg");
                lin.Add("Protoss_Cinematic.jpg");
                lin.Add("protoss_concept.jpg");
                lin.Add("Protoss_Immortal_concept.jpg");
                lin.Add("protoss_Orthos_concept.jpg");
                lin.Add("Protoss_Zealot_cinematic.jpg");
                lin.Add("Relic_Crater_cinematic.jpg");
                lin.Add("Terran_Marine_cinematic.jpg");
                lin.Add("Terran_Viking_concept.jpg");
                lin.Add("wallpaper-marine.jpg");
                lin.Add("wallpaper-nuke.jpg");
                lin.Add("wallpaper-zeratul2.jpg");                
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
                Response.Redirect("FullWallpaper.aspx?image=" + lin[index - 1]);
            else
                Response.Redirect("FullWallpaper.aspx?image=" + lin[lin.Count - 1]);
        }
    }
}
