using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Xml;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Thon.ZaszBlog
{
    public partial class ZaszBlogMasterPageMaster : System.Web.UI.MasterPage
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
            System.Net.WebClient wc = new System.Net.WebClient();
            string eksemmelle = "";
            wc.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            try
            {
                eksemmelle = wc.DownloadString("http://www.hindu.com/rss/01hdline.xml");
            }
            catch(Exception e1)
            {
                e1=null;

            }
            if(!string.IsNullOrEmpty(eksemmelle))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(eksemmelle);
                string title, content, link, date;
                XmlNodeList list = doc.SelectNodes("rss/channel/item");
                HtmlGenericControl hgc;
                HtmlAnchor anc;

                HtmlGenericControl maindiv = new HtmlGenericControl("div");
                maindiv.Attributes.Add("style", "overflow:scroll; height:190px; width:400px; border-style:single; border-width:1px; border-color:silver; padding:3px 3px 3px 3px;");
                foreach (XmlNode node in list)
                {
                    title = node.SelectSingleNode("title").InnerText;
                    link = node.SelectSingleNode("link").InnerText;
                    content = node.SelectSingleNode("description").InnerText;
                    date = node.SelectSingleNode("pubDate").InnerText;

                    title = "<b>" + title + "</b>";
                    anc = new HtmlAnchor();
                    anc.Attributes.Add("style", "color:#cdb753; text-decoration:none; font-size:small;");                    
                    anc.HRef = link;
                    anc.InnerHtml = title;
                    maindiv.Controls.Add(anc);
                    
                    hgc = new HtmlGenericControl("br");
                    maindiv.Controls.Add(hgc);

                    hgc = new HtmlGenericControl("p");
                    hgc.Attributes.Add("style", "color:silver;font-size:small;");
                    hgc.InnerText = content;
                    maindiv.Controls.Add(hgc);

                    hgc = new HtmlGenericControl("br");
                    maindiv.Controls.Add(hgc);

                    hgc = new HtmlGenericControl("i");
                    hgc.Attributes.Add("style", "color:silver;");
                    hgc.InnerText = "Published Date : " + date + "\r\n\r\n";
                    maindiv.Controls.Add(hgc);
                    hgc = new HtmlGenericControl("br");
                    maindiv.Controls.Add(hgc);
                    
                }

                FooterPlaceHolder.Controls.Add(maindiv);
            }
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
    }
}
