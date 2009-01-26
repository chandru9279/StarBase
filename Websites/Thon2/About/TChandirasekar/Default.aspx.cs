using System;
using Thon.Support.Web.Controls;

namespace Thon.About
{
    public partial class TCDefaultAspx : ThonBasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Server.Transfer("~/About/TChandirasekar/WebResume.htm");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
