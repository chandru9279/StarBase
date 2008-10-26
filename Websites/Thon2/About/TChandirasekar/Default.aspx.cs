using System;
using Thon.Support.Web.Controls;

public partial class TCDefaultAspx : ThonBasePage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Server.Transfer("~/About/TChandirasekar/WebResume.htm");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
