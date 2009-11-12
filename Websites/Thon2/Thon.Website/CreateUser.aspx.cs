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

namespace Thon
{
    public partial class CreateUserAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.IsInRole("Family"))
            {
                CreateUserWizard1.Enabled = true;
                CreateUserWizard1.Visible = true;
            }
            else
            {
                CreateUserWizard1.Enabled = false;
                CreateUserWizard1.Visible = false;
            }
        }
    }
}