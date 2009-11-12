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
using Thon.Support;

namespace Thon.Starcraft
{
    public partial class StarMaster : System.Web.UI.MasterPage
    {
        protected bool theme = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Thon.Support.ThonSettings.Instance.StarcraftTheme))
            {
                Page.Header.FindControl("themelink").Visible = false;
                theme = true;
            }
            if (theme)
            { 
               if(Thon.Support.ThonSettings.Instance.StarcraftTheme.Equals("green",StringComparison.InvariantCultureIgnoreCase))
                   AddStyleSheetLink("StyleSheets/Green.css");
               else if(Thon.Support.ThonSettings.Instance.StarcraftTheme.Equals("blue",StringComparison.InvariantCultureIgnoreCase))
                   AddStyleSheetLink("StyleSheets/Blue.css");
               else if(Thon.Support.ThonSettings.Instance.StarcraftTheme.Equals("yellow",StringComparison.InvariantCultureIgnoreCase))
                   AddStyleSheetLink("StyleSheets/Yellow.css");
               else if(Thon.Support.ThonSettings.Instance.StarcraftTheme.Equals("red",StringComparison.InvariantCultureIgnoreCase))
                   AddStyleSheetLink("StyleSheets/Red.css");
               else if(Thon.Support.ThonSettings.Instance.StarcraftTheme.Equals("BlueTextured",StringComparison.InvariantCultureIgnoreCase))
                   AddStyleSheetLink("StyleSheets/BlueTexture.css");
               else
                   AddStyleSheetLink("StyleSheets/BlueTexture.css");
            }
        }
        protected void AddStyleSheetLink(string href)
        {
            AddLinkInHeader("stylesheet", href, "text/css", ThonSettings.Instance.Name);
        }

        protected void AddLinkInHeader(string relation, string href, string type, string title)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = relation;
            link.Attributes["href"] = href;
            link.Attributes["type"] = type;
            link.Attributes["title"] = title;
            Page.Header.Controls.Add(link);
        }
    }
}