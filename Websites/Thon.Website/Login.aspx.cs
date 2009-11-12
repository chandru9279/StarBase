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

namespace Thon
{
    public partial class LoginAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                ChangePassword.Visible = true;  
                ThonLogin.Visible = false;
            }
            else
            {
                ThonLogin.FindControl("username").Focus();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["forgot"]) && Request.QueryString["forgot"].Equals("true", StringComparison.InvariantCultureIgnoreCase))
            { ThonPasswordRecovery.Visible = true; }
        }

        /// <summary>
        /// Handles the LoggedIn event of the Login1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ThonLogin_LoggedIn(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole(ThonLogin.UserName, "Family"))
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }

        /// <summary>
        /// Handles the ContinueButtonClick event of the changepassword1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ChangePassword_ContinueButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(HelperUtilities.RelativeAppRoot, true);
        }
    }
}