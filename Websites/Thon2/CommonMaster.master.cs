using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Thon.Support;

public partial class CommonMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack || Page.IsCallback))
            FooterPlaceHolder.Controls.Add(LoadControl("~/UserControls/Footer/Futures.ascx"));
    }

    protected void FLOne_Click(object sender, EventArgs e)
    {
        FooterPlaceHolder.Controls.Add(LoadControl("~/UserControls/Footer/Futures.ascx"));
        FLBStyles(1);
    }
    protected void FLTwo_Click(object sender, EventArgs e)
    {       
        FooterPlaceHolder.Controls.Add(LoadControl("~/UserControls/Footer/HinduNews.ascx"));
        FLBStyles(2);
    }
    protected void FLThree_Click(object sender, EventArgs e)
    {
        FooterPlaceHolder.Controls.Add(LoadControl("~/UserControls/Footer/WaitFor.ascx"));
        FLBStyles(3);
    }
    protected void FLFour_Click(object sender, EventArgs e)
    {
        FooterPlaceHolder.Controls.Add(LoadControl("~/UserControls/Footer/RecentPosts.ascx"));
        FLBStyles(4);
    }

    private void FLBStyles(int i)
    {
        FLOne.CssClass = "innermenu";
        FLTwo.CssClass = "innermenu";
        FLThree.CssClass = "innermenu";
        FLFour.CssClass = "innermenu";
        switch (i)
        {
            case 1:
                FLOne.CssClass = "innermenu_hover"; break;
            case 2:
                FLTwo.CssClass = "innermenu_hover"; break;
            case 3:
                FLThree.CssClass = "innermenu_hover"; break;
            case 4:
                FLFour.CssClass = "innermenu_hover"; break;
        }
    }

    protected void MasterSearchTextBox_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ZaszBlog/Search.aspx?q=" + MasterSearchTextBox.Text + "&comment=true");
    }

    protected void SendFeedback_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(footerfeedback.Text))
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress("Feedback@Thon.net");
            msg.Body = footerfeedback.Text;
            msg.To.Add(new System.Net.Mail.MailAddress("chandru9279@gmail.com"));
            msg.Subject = "Feedback from Thon.net";
            Thon.ZaszBlog.Support.SupportUtilities.SendMailMessageAsync(msg);
            footerfeedback.Text = "";
        }
    }

    protected string accessurl()
    {
        return HelperUtilities.InternetAppRoot.ToString();
    }
}
