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

public partial class chandirasekar_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Server.Transfer("~/chandirasekar/WebResume.htm");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
