using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thon.Support;

namespace Thon
{
    public partial class ThonConfigurationAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindSettings();

            Page.MaintainScrollPositionOnPostBack = true;
            Page.Title = "Thon Settings";

            btnSave.Click += new EventHandler(btnSave_Click);
            btnSaveTop.Click += new EventHandler(btnSave_Click);
            btnTestSmtp.Click += new EventHandler(btnTestSmtp_Click);
        }

        private void btnTestSmtp_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(txtEmail.Text, txtName.Text);
                mail.To.Add(mail.From);
                mail.Subject = "Test mail from " + txtName.Text;
                mail.Body = "Success";
                SmtpClient smtp = new SmtpClient(txtSmtpServer.Text);
                smtp.Credentials = new System.Net.NetworkCredential(txtSmtpUsername.Text, txtSmtpPassword.Text);
                smtp.EnableSsl = cbEnableSsl.Checked;
                smtp.Port = int.Parse(txtSmtpServerPort.Text);
                smtp.Send(mail);
                lbSmtpStatus.Text = "Test successfull";
                lbSmtpStatus.Style.Add(HtmlTextWriterStyle.Color, "green");
            }
            catch
            {
                lbSmtpStatus.Text = "Could not connect";
                lbSmtpStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------
            // Set Basic settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.Name = txtName.Text;
            ThonSettings.Instance.Description = txtDescription.Text;

            //-----------------------------------------------------------------------
            // Set Email settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.Email = txtEmail.Text;
            ThonSettings.Instance.SmtpServer = txtSmtpServer.Text;
            ThonSettings.Instance.SmtpServerPort = int.Parse(txtSmtpServerPort.Text);
            ThonSettings.Instance.SmtpUserName = txtSmtpUsername.Text;
            ThonSettings.Instance.SmtpPassword = txtSmtpPassword.Text;
            ThonSettings.Instance.EnableSsl = cbEnableSsl.Checked;
            ThonSettings.Instance.EmailSubjectPrefix = txtEmailSubjectPrefix.Text;

            //-----------------------------------------------------------------------
            // Set Advanced settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.EnableHttpCompression = cbEnableCompression.Checked;
            ThonSettings.Instance.RemoveWhitespaceInStyleSheets = cbRemoveWhitespaceInStyleSheets.Checked;
            ThonSettings.Instance.EnableOpenSearch = cbEnableOpenSearch.Checked;
            ThonSettings.Instance.HandleWwwSubdomain = rblWwwSubdomain.SelectedItem.Value;

            //-----------------------------------------------------------------------
            // Contact Form settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.ContactFormMessage = txtFormMessage.Text;
            ThonSettings.Instance.ContactThankMessage = txtThankMessage.Text;
            ThonSettings.Instance.EnableContactAttachments = cbEnableAttachments.Checked;

            //-----------------------------------------------------------------------
            // HTML header section
            //-----------------------------------------------------------------------
            ThonSettings.Instance.HtmlHeader = txtHtmlHeader.Text;

            //-----------------------------------------------------------------------
            // Visitor tracking settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.TrackingScript = txtTrackingScript.Text;

            //-----------------------------------------------------------------------
            //  Persist settings
            //-----------------------------------------------------------------------
            ThonSettings.Instance.Save();
            Response.Redirect(Request.RawUrl, true);
        }

        private void BindSettings()
        {
            //-----------------------------------------------------------------------
            // Bind Basic settings
            //-----------------------------------------------------------------------
            txtName.Text = ThonSettings.Instance.Name;
            txtDescription.Text = ThonSettings.Instance.Description;

            //-----------------------------------------------------------------------
            // Bind Email settings
            //-----------------------------------------------------------------------
            txtEmail.Text = ThonSettings.Instance.Email;
            txtSmtpServer.Text = ThonSettings.Instance.SmtpServer;
            txtSmtpServerPort.Text = ThonSettings.Instance.SmtpServerPort.ToString();
            txtSmtpUsername.Text = ThonSettings.Instance.SmtpUserName;
            txtSmtpPassword.Text = ThonSettings.Instance.SmtpPassword;
            cbEnableSsl.Checked = ThonSettings.Instance.EnableSsl;
            txtEmailSubjectPrefix.Text = ThonSettings.Instance.EmailSubjectPrefix;

            //-----------------------------------------------------------------------
            // Bind Advanced settings
            //-----------------------------------------------------------------------
            cbEnableCompression.Checked = ThonSettings.Instance.EnableHttpCompression;
            cbRemoveWhitespaceInStyleSheets.Checked = ThonSettings.Instance.RemoveWhitespaceInStyleSheets;
            cbEnableOpenSearch.Checked = ThonSettings.Instance.EnableOpenSearch;
            rblWwwSubdomain.SelectedValue = ThonSettings.Instance.HandleWwwSubdomain;

            //-----------------------------------------------------------------------
            // Bind Contact Form settings
            //-----------------------------------------------------------------------
            txtThankMessage.Text = ThonSettings.Instance.ContactThankMessage;
            txtFormMessage.Text = ThonSettings.Instance.ContactFormMessage;
            cbEnableAttachments.Checked = ThonSettings.Instance.EnableContactAttachments;

            //-----------------------------------------------------------------------
            // HTML header section
            //-----------------------------------------------------------------------
            txtHtmlHeader.Text = ThonSettings.Instance.HtmlHeader;

            //-----------------------------------------------------------------------
            // Visitor tracking settings
            //-----------------------------------------------------------------------
            txtTrackingScript.Text = ThonSettings.Instance.TrackingScript;
        }

    }
}