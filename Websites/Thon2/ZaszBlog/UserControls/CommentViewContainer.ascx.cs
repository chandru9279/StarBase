using System;
using System.IO;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.Security;
using Thon.ZaszBlog.Support;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support.Web.Controls;

public partial class UserControlsCommentView : UserControl, ICallbackEventHandler
{
  private string _Callback;

  #region ICallbackEventHandler Members

  public string GetCallbackResult()
  {
    return _Callback;
  }

  public void RaiseCallbackEvent(string eventArgument)
  {
    string[] args = eventArgument.Split(new string[] { "-|-" }, StringSplitOptions.None);
    string author = args[0];
    string email = args[1];
    string website = args[2];
    string content = args[3];
    bool notify = bool.Parse(args[4]);

    Comment comment = new Comment();
    comment.Id = Guid.NewGuid();
    comment.Author = Server.HtmlEncode(author);
    comment.Email = email;
    comment.Content = Server.HtmlEncode(content);
    comment.IP = Request.UserHostAddress;
    comment.DateCreated = DateTime.Now;
    comment.Parent = Post;
    comment.IsApproved = !BlogSettings.Instance.EnableCommentsModeration;

    if (Page.User.Identity.IsAuthenticated)
      comment.IsApproved = true;
      
    if (website.Trim().Length > 0)
    {
      if (!website.ToLowerInvariant().Contains("://"))
        website = "http://" + website;

      Uri url;
      if (Uri.TryCreate(website, UriKind.Absolute, out url))
        comment.Website = url;
    }

    if (notify && !Post.NotificationEmails.Contains(email))
      Post.NotificationEmails.Add(email);

    Post.AddComment(comment);
    SetCookie(author, email, website);

    string path = "~/ZaszBlog/UserControls/CommentView.ascx";
      
    CommentViewBase control = (CommentViewBase)LoadControl(path);
    control.Comment = comment;
    control.Post = Post;

    using (StringWriter sw = new StringWriter())
    {
      control.RenderControl(new HtmlTextWriter(sw));
      _Callback = sw.ToString();
    }
  }

  #endregion

  protected void Page_Load(object sender, EventArgs e)
  {
    if (Post == null)
      Response.Redirect(SupportUtilities.RelativeWebRoot);

    if (!Page.IsPostBack && !Page.IsCallback)
    {
      if (Request.QueryString["deletecomment"] != null)
        DeleteComment();

      if (!string.IsNullOrEmpty(Request.QueryString["approvecomment"]))
        ApproveComment();

      if (!string.IsNullOrEmpty(Request.QueryString["approveallcomments"]))
        ApproveAllComments();

      string path = SupportUtilities.RelativeWebRoot + "UserControls/CommentView.ascx";

      //Add approved Comments
      foreach (Comment comment in Post.Comments)
      {
        CommentViewBase control = (CommentViewBase)LoadControl(path);
        if (comment.IsApproved || !BlogSettings.Instance.EnableCommentsModeration)
        {
          control.Comment = comment;
          control.Post = Post;
          phComments.Controls.Add(control);
        }
      }

      //Add unapproved comments for authenticated users
      foreach (Comment comment in Post.Comments)
      {
        CommentViewBase control = (CommentViewBase)LoadControl(path);

        if (!comment.IsApproved && Page.User.Identity.IsAuthenticated)
        {
          control.Comment = comment;
          control.Post = Post;
          phComments.Controls.Add(control);
        }
      }

      if (BlogSettings.Instance.IsCommentsEnabled)
      {
        if (!Post.IsCommentsEnabled || (BlogSettings.Instance.DaysCommentsAreEnabled > 0 &&
           Post.DateCreated.AddDays(BlogSettings.Instance.DaysCommentsAreEnabled) < DateTime.Now.Date))
        {
          phAddComment.Visible = false;
          lbCommentsDisabled.Visible = true;
        }

        GetCookie();
        BindLivePreview();
      }
      else
      {
        phAddComment.Visible = false;
      }
      InititializeCaptcha();
    }
    
   
    btnSave.Click += new EventHandler(btnSave_Click);
  }

  private void ApproveComment()
  {
    foreach (Comment comment in Post.NotApprovedComments)
    {
      if (comment.Id == new Guid(Request.QueryString["approvecomment"]))
      {
        Post.ApproveComment(comment);

        int index = Request.RawUrl.IndexOf("?");
          // Remove the query string
        string url = Request.RawUrl.Substring(0, index);
        Response.Redirect(url, true);
      }
    }
  }

  private void ApproveAllComments()
  {

    Post.ApproveAllComments();

    int index = Request.RawUrl.IndexOf("?");
    string url = Request.RawUrl.Substring(0, index);
    Response.Redirect(url, true);
  }

  private void DeleteComment()
  {
    foreach (Comment comment in Post.Comments)
    {
      if (comment.Id == new Guid(Request.QueryString["deletecomment"]))
      {
        Response.Write(Request.Url);
        Post.RemoveComment(comment);

        int index = Request.RawUrl.IndexOf("?");
				string url = Request.RawUrl.Substring(0, index) + "#comment";
        Response.Redirect(url, true);
      }
    }
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (!IsCaptchaValid || !Page.IsValid)
      Response.Redirect(Post.RelativeLink.ToString(), true);

    SaveComment();
    SetCookie(txtName.Text, txtEmail.Text, txtWebsite.Text);
    Response.Redirect(Post.RelativeLink.ToString() + "#addcomment", true);
  }

  /// <summary>
  /// Saves a comment made from a postback. This only runs when 
  /// the browser doesn't support AJAX.
  /// </summary>
  private void SaveComment()
  {
    Comment comment = new Comment();
    comment.Id = Guid.NewGuid();
    comment.Author = txtName.Text;
    comment.Email = txtEmail.Text;
    comment.IP = Request.UserHostAddress;
    comment.Content = Server.HtmlEncode(txtContent.Text);
    comment.DateCreated = DateTime.Now;
    comment.Parent = Post;
    if (txtWebsite.Text.Trim().Length > 0)
    {
      if (!txtWebsite.Text.ToLowerInvariant().Contains("://"))
        txtWebsite.Text = "http://" + txtWebsite.Text;

      Uri website;
      if (Uri.TryCreate(txtWebsite.Text, UriKind.Absolute, out website))
        comment.Website = website;
    }
    comment.IsApproved = !BlogSettings.Instance.EnableCommentsModeration;

    if (Page.User.Identity.IsAuthenticated)
      comment.IsApproved = true;

    Post.AddComment(comment);
  }

  private void BindLivePreview()
  {
    string path = SupportUtilities.RelativeWebRoot + "UserControls/CommentView.ascx";
    CommentViewBase control = (CommentViewBase)LoadControl(path);
    Comment comment = new Comment();
    comment.Content = string.Empty;
    comment.DateCreated = DateTime.Now;
    comment.IP = Request.UserHostAddress;

    if (!string.IsNullOrEmpty(txtName.Text))
      comment.Author = txtName.Text;

    if (!string.IsNullOrEmpty(txtEmail.Text))
      comment.Email = txtEmail.Text;

    if (txtWebsite.Text.Trim().Length > 0)
    {
      if (!txtWebsite.Text.ToLowerInvariant().Contains("://"))
        txtWebsite.Text = "http://" + txtWebsite.Text;

      Uri website;
      if (Uri.TryCreate(txtWebsite.Text, UriKind.Absolute, out website))
        comment.Website = website;
    }

    control.Comment = comment;
    control.Post = Post;
    phLivePreview.Controls.Add(control);
  }

  /// <summary>
  /// Validates that the name of the person writing posting a comment
  /// cannot be the same as the author of the post.
  /// </summary>
  protected void CheckAuthorName(object sender, ServerValidateEventArgs e)
  {
    e.IsValid = true;
    if (!Page.User.Identity.IsAuthenticated)
    {
      if (txtName.Text.ToLowerInvariant() == Post.Author.ToLowerInvariant())
        e.IsValid = false;
    }
  }

  #region Cookies

  /// <summary>
  /// Sets a cookie with the entered visitor information
  /// so it can be prefilled on next visit.
  /// </summary>
  private void SetCookie(string name, string email, string website)
  {
    HttpCookie cookie = new HttpCookie("comment");
    cookie.Expires = DateTime.Now.AddMonths(24);
    cookie.Values.Add("name", Server.UrlEncode(name));
    cookie.Values.Add("email", email);
    cookie.Values.Add("url", website);
    Response.Cookies.Add(cookie);
  }

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
      txtWebsite.Text = cookie.Values["url"];
    }
    else if (Page.User.Identity.IsAuthenticated)
    {
      MembershipUser user = Membership.GetUser();
      txtName.Text = user.UserName;
      txtEmail.Text = user.Email;
      txtWebsite.Text = Request.Url.Host;
    }
  }

  #endregion

  #region CAPTCHA

  /// <summary>
  /// Open source code: http://www.sourceforge.net/ Project:CAPTCHA
  /// Gets whether or not the user is human
  /// </summary>
  private bool IsCaptchaValid
  {
    get
    {
      if (ViewState["captchavalue"] != null)
      {
				return Request.Form[ViewState["captchafield"].ToString()] == ViewState["captchavalue"].ToString();
      }

      return false;
    }
  }

  /// <summary>
  /// Initializes the captcha and registers the JavaScript
  /// </summary>
	private void InititializeCaptcha()
  {
    if (ViewState["captchavalue"] == null)
    {
      ViewState["captchavalue"] = Guid.NewGuid().ToString();
			ViewState["captchafield"] = Guid.NewGuid().ToString();
    }

    StringBuilder sb = new StringBuilder();
    sb.Append("function SetCaptcha(){");
    sb.Append("var form = $('" + Page.Form.ClientID + "');");
    sb.Append("var el = document.createElement('input');");
    sb.Append("el.type = 'hidden';");
		sb.Append("el.name = '" + ViewState["captchafield"] + "';");
    sb.Append("el.value = '" + ViewState["captchavalue"] + "';");
    sb.Append("form.appendChild(el);}");
        
    Page.ClientScript.RegisterClientScriptBlock(GetType(), "captchascript", sb.ToString(), true);
    Page.ClientScript.RegisterOnSubmitStatement(GetType(), "captchayo", "SetCaptcha()");
  }

  #endregion

  #region Protected methods and properties

  /// <summary>
  /// The regular expression used to parse links.
  /// </summary>
  private static Regex regex =
      new Regex("((http://|www\\.)([A-Z0-9.-]{1,})\\.[0-9A-Z?&=-_\\./]{2,})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

  private Post _Post;

  /// <summary>
  /// Gets or sets the post from which the comments are parsed.
  /// </summary>
  public Post Post
  {
    get { return _Post; }
    set { _Post = value; }
  }

  /// <summary>
  /// Examines the comment body for any links and turns them
  /// automatically into ones that can be clicked.
  /// <remarks>
  /// All links added to comments will have the rel attribute set
  /// to nofollow to prevent negative pagerank.
  /// </remarks>
  /// </summary>
  protected string ResolveLinks(string body)
  {
    foreach (Match match in regex.Matches(body))
    {
      if (!match.Value.Contains("://"))
        body = body.Replace(match.Value, regex.Replace(body, "<a href=\"http://$1\" rel=\"nofollow\">$1</a>"));
      else
        body = body.Replace(match.Value, regex.Replace(body, "<a href=\"$1\" rel=\"nofollow\">$1</a>"));
    }

    return body.Replace(Environment.NewLine, "<br />");
  }

  /// <summary>
  /// Displays a delete link to visitors that is authenticated
  /// using the default membership provider.
  /// </summary>
  /// <param name="id">The id of the comment.</param>
  protected string AdminLink(string id)
  {
    if (Page.User.Identity.IsAuthenticated)
    {
      StringBuilder sb = new StringBuilder();
      foreach (Comment comment in Post.Comments)
      {
        if (comment.Id.ToString() == id)
          sb.AppendFormat(" | <a href=\"mailto:{0}\">{0}</a>", comment.Email);
      }

      string confirmDelete = "Are you sure you want to delete the comment?";
      sb.AppendFormat(" | <a href=\"?deletecomment={0}\" onclick=\"return confirm('{1}?')\">{2}</a>",
                      id.ToString(), confirmDelete, "Delete");
      return sb.ToString();
    }

    return string.Empty;
  }

  /// <summary>
  /// Displays the Gravatar image that matches the specified email.
  /// </summary>
  protected string Gravatar(string email, string name, int size)
  {
    if (email.Contains("://"))
      return
          string.Format(
              "<img class=\"thumb\" src=\"http://images.websnapr.com/?url={0}&amp;size=t\" alt=\"{1}\" />", name,
              email);
    //http://www.artviper.net/screenshots/screener.php?&url={0}&h={1}&w={1}
    MD5 md5 = new MD5CryptoServiceProvider();
    byte[] result = md5.ComputeHash(Encoding.ASCII.GetBytes(email));

    StringBuilder hash = new StringBuilder();
    for (int i = 0; i < result.Length; i++)
      hash.Append(result[i].ToString("x2"));

    StringBuilder image = new StringBuilder();
    image.Append("<img src=\"");
    image.Append("http://www.gravatar.com/avatar.php?");
    image.Append("gravatar_id=" + hash.ToString());
    image.Append("&amp;rating=G");
    image.Append("&amp;size=" + size);
    image.Append("&amp;default=");
    image.Append(Server.UrlEncode(SupportUtilities.AbsoluteWebRoot + "Images/NoAvatar.jpg"));
    image.Append("\" alt=\"\" />");
    return image.ToString();
  }

  #endregion

}