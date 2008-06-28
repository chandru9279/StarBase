using System;
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

namespace Thon
{
    public partial class ThonDefaultAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try { string welcome = Request.QueryString[0]; }
            catch (ArgumentOutOfRangeException loading) { Server.Transfer("Index.html"); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
        }
    }
}
