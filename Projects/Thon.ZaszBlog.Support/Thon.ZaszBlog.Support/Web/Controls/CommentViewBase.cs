using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Web.Controls
{
    // One commentview.ascx for each comment is created.. Got this one right in my first try ..(Zasz WEBLOG)
    // Inherit from this class when building the commentview.ascx user control 
	// The class exposes a lot of functionality to the custom comment control 
	public class CommentViewBase : UserControl
	{
		#region Properties

		private Post _Post;
		public Post Post
		{
			get { return _Post; }
			set { _Post = value; }
		}

		private Comment _Comment;
		public Comment Comment
		{
			get { return _Comment; }
			set { _Comment = value; }
		}

		#endregion

		#region Methods
		// The regular expression used to parse links. For future ZaszBlog use .....
		// private static readonly Regex regex = new Regex("((http://|www\\.)([A-Z0-9.-]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		// private const string link = "<a href=\"{0}{1}\" rel=\"nofollow\">{2}</a>";
        		
		// Must examine the comment body for any links and turns them
		// automatically into one that can be clicked. That is append <a href> tag
		public string Text
		{
			get
			{   
                ServingEventArgs arg = new ServingEventArgs(Comment.Content, ServingLocation.SinglePost);
                Comment.OnServing(Comment, arg);
                return arg.Body.Replace("\n", "<br />"); ;
			}
		}


        // Displays a delete link to visitors that are authenticated using the default membership provider.
		protected string AdminLinks
		{
			get
			{
                if (Page.User.IsInRole("Family") || Page.User.Identity.Name.Equals(Post.Author))
				{
					BlogBasePage page = (BlogBasePage)Page;
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					sb.AppendFormat(" | <a href=\"mailto:{0}\">{0}</a>", Comment.Email);
					sb.AppendFormat(" | <a href=\"http://www.domaintools.com/go/?service=whois&amp;q={0}/\">{0}</a>", Comment.IP);
                    sb.AppendFormat(" | <a href=\"javascript:void(0);\" onclick=\"if (confirm('Are you sure you want to  delete the comment?')) location.href='?deletecomment={0}'\">Delete</a>", Comment.Id);

					if (!Comment.IsApproved)
					{
						sb.AppendFormat(" | <a href=\"?approvecomment={0}\">{1}</a>", Comment.Id, "Approve");

					}
					return sb.ToString();
				}
				return string.Empty;
			}
		}

        /// <summary>
		/// Displays the Gravatar image that matches the specified email.
		/// </summary>
		protected string Gravatar(int size)
		{
			if (BlogSettings.Instance.Avatar == "none")
				return null;

			if (String.IsNullOrEmpty(Comment.Email) || !Comment.Email.Contains("@"))
			{
				if (Comment.Website != null && Comment.Website.ToString().Length > 0 && Comment.Website.ToString().Contains("http://"))
				{
					return string.Format("<img class=\"thumb\" src=\"http://images.websnapr.com/?url={0}&amp;size=t\" alt=\"{1}\" />", Server.UrlEncode(Comment.Website.ToString()), Comment.Email);
				}

				return "<img src=\"" + SupportUtilities.RelativeWebRoot + "Images/noavatar.jpg\" alt=\"" + Comment.Author + "\" />";
			}

			string img = "<img src=\"{0}\" alt=\"{1}\" />";
			string hash = CreateMd5Hash();
            string noavatar = SupportUtilities.AbsoluteWebRoot + "Images/noavatar.jpg";
			string monster = SupportUtilities.AbsoluteWebRoot + "Monster.ashx?seed=" + hash.Substring(0, 10) + "&size=" + size;
			string gravatar = "http://www.gravatar.com/avatar.php?gravatar_id=" + hash + "&amp;size=" + size + "&amp;default=";

			string link = string.Empty;
			switch (BlogSettings.Instance.Avatar)
			{
				case "monster":
                    link = Server.UrlEncode(monster);
					break;

				case "gravatar":
					link = gravatar + Server.UrlEncode(noavatar);
					break;

				case "combine":
					link = gravatar + Server.UrlEncode(monster);
					break;
			}

			return string.Format(img, link, Comment.Author);
		}

		private string CreateMd5Hash()
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(Encoding.ASCII.GetBytes(Comment.Email));

			System.Text.StringBuilder hash = new System.Text.StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				hash.Append(result[i].ToString("x2"));
			}

			return hash.ToString();
		}
		#endregion

	}
}
