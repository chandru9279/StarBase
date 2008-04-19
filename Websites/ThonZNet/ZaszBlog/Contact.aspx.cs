using System;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Thon.Support;
using Thon.ZaszBlog.Support.Web.Controls;

public partial class ContactAspx : BlogBasePage
{

	private static Regex _Regex = new Regex("<[^>]*>", RegexOptions.Compiled);

	protected void Page_Load(object sender, EventArgs e)
	{
		btnSend.Click += new EventHandler(btnSend_Click);
		if (!Page.IsPostBack)
		{
			txtSubject.Text = Request.QueryString["subject"];
			txtName.Text = Request.QueryString["name"];
			txtEmail.Text = Request.QueryString["email"];

			GetCookie();
			phAttachment.Visible = ThonSettings.Instance.EnableContactAttachments;
			InititializeCaptcha();
			SetFocus();
		}

		base.AddMetaTag("description", _Regex.Replace(ThonSettings.Instance.ContactFormMessage, string.Empty));
	}

	/// <summary>
	/// Sets the focus on the first empty textbox.
	/// </summary>
	private void SetFocus()
	{
		if (string.IsNullOrEmpty(Request.QueryString["name"]) && txtName.Text == string.Empty)
		{
			txtName.Focus();
		}
		else if (string.IsNullOrEmpty(Request.QueryString["email"]) && txtEmail.Text == string.Empty)
		{
			txtEmail.Focus();
		}
		else if (string.IsNullOrEmpty(Request.QueryString["subject"]))
		{
			txtSubject.Focus();
		}
		else
		{
			txtMessage.Focus();
		}
	}

	private void btnSend_Click(object sender, EventArgs e)
	{
		bool success = SendEmail();
		divForm.Visible = !success;
		lblStatus.Visible = !success;
		divThank.Visible = success;
		SetCookie();
	}

	private bool SendEmail()
	{

		try
		{
			using (MailMessage mail = new MailMessage())
			{
				mail.From = new MailAddress(txtEmail.Text, txtName.Text);
				mail.To.Add(ThonSettings.Instance.Email);
				mail.Subject = ThonSettings.Instance.EmailSubjectPrefix + " e-mail - " + txtSubject.Text;

				mail.Body = "<div style=\"font: 11px verdana, arial\">";
				mail.Body += Server.HtmlEncode(txtMessage.Text).Replace(Environment.NewLine, "<br />") + "<br /><br />";
				mail.Body += "_______________________________________________________________________________<br />";
				mail.Body += "<h3>Author information</h3>";
				mail.Body += "<div style=\"font-size:10px;line-height:16px\">";
				mail.Body += "<strong>Name:</strong> " + Server.HtmlEncode(txtName.Text) + "<br />";
				mail.Body += "<strong>E-mail:</strong> " + Server.HtmlEncode(txtEmail.Text) + "<br />";

				if (ViewState["url"] != null)
					mail.Body += string.Format("<strong>Website:</strong> <a href=\"{0}\">{0}</a><br />", ViewState["url"]);

				if (HttpContext.Current != null)
				{
					mail.Body += "<strong>IP address:</strong> " + HttpContext.Current.Request.UserHostAddress + "<br />";
					mail.Body += "<strong>User-agent:</strong> " + HttpContext.Current.Request.UserAgent;
				}


				if (txtAttachment.HasFile)
				{
					Attachment attachment = new Attachment(txtAttachment.PostedFile.InputStream, txtAttachment.FileName);
					mail.Attachments.Add(attachment);
				}

				HelperUtilities.SendMailMessage(mail);
			}

			return true;
		}
		catch (Exception ex)
		{
			if (User.Identity.IsAuthenticated)
			{
				if (ex.InnerException != null)
					lblStatus.Text = ex.InnerException.Message;
				else
					lblStatus.Text = ex.Message;
			}

			return false;
		}
	}

	#region Cookies

	/// <summary>
	/// Gets the cookie with visitor information if any is set.
	/// Then fills the contact information fields in the form.
	/// </summary>
	private void GetCookie()
	{
		HttpCookie cookie = Request.Cookies["comment"];
		if (cookie != null)
		{
			txtName.Text = Server.UrlDecode(cookie.Values["name"]);
			txtEmail.Text = cookie.Values["email"];
			ViewState["url"] = cookie.Values["url"];
		}
	}

	/// <summary>
	/// Sets a cookie with the entered visitor information
	/// so it can be prefilled on next visit.
	/// </summary>
	private void SetCookie()
	{
		HttpCookie cookie = new HttpCookie("comment");
		cookie.Expires = DateTime.Now.AddMonths(24);
		cookie.Values.Add("name", Server.UrlEncode(txtName.Text));
		cookie.Values.Add("email", txtEmail.Text);
		cookie.Values.Add("url", string.Empty);
		cookie.Values.Add("country", string.Empty);
		Response.Cookies.Add(cookie);
	}

	#endregion

	#region CAPTCHA

	/// <summary> 
	/// Initializes the captcha and registers the JavaScript 
	/// </summary> 
	private void InititializeCaptcha()
	{
		if (ViewState["captchavalue"] == null)
		{
			ViewState["captchavalue"] = Guid.NewGuid().ToString();
		}

		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.AppendLine("function SetCaptcha(){");
		sb.AppendLine("var form = document.getElementById('" + Page.Form.ClientID + "');");
		sb.AppendLine("var el = document.createElement('input');");
		sb.AppendLine("el.type = 'hidden';");
		sb.AppendLine("el.name = 'captcha';");
		sb.AppendLine("el.value = '" + ViewState["captchavalue"] + "';");
		sb.AppendLine("form.appendChild(el);}");

		Page.ClientScript.RegisterClientScriptBlock(GetType(), "captchascript", sb.ToString(), true);
		Page.ClientScript.RegisterOnSubmitStatement(GetType(), "captchayo", "SetCaptcha()");
	}

	/// <summary> 
	/// Gets whether or not the user is human 
	/// </summary> 
	private bool IsCaptchaValid
	{
		get
		{
			if (ViewState["captchavalue"] != null)
			{
				return Request.Form["captcha"] == ViewState["captchavalue"].ToString();
			}

			return false;
		}
	}

	#endregion

}
