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
using Thon.Support.Web.Controls;
using Thon.Support;

public partial class Starcraft_Default : ThonBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["theme"]))
        { 
            if(Request.QueryString["theme"].Equals("green",StringComparison.InvariantCultureIgnoreCase))
                ThonSettings.Instance.StarcraftTheme = "green";
            else if(Request.QueryString["theme"].Equals("blue",StringComparison.InvariantCultureIgnoreCase))
                ThonSettings.Instance.StarcraftTheme = "blue";
            else if(Request.QueryString["theme"].Equals("yellow",StringComparison.InvariantCultureIgnoreCase))
                ThonSettings.Instance.StarcraftTheme = "yellow";
            else if(Request.QueryString["theme"].Equals("red",StringComparison.InvariantCultureIgnoreCase))
                ThonSettings.Instance.StarcraftTheme = "red";
            else if (Request.QueryString["theme"].Equals("BlueTextured", StringComparison.InvariantCultureIgnoreCase))
                ThonSettings.Instance.StarcraftTheme = "BlueTextured";
            else
                ThonSettings.Instance.StarcraftTheme = "BlueTextured";
        } 
    }
}
