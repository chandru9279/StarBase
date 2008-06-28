using System;

public partial class SEODefault : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Server.Transfer("Default.aspx?You=Welcome");
    }
}
