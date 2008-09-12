using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Thon.ZaszBlog.Support.Web.Controls;
using Thon.ZaszBlog.Support;
using Thon.ZaszBlog.Support.CodedRepresentations;

public partial class PageAspx : BlogBasePage
{
    /// <summary>
    /// The Page instance to render on the page.
    /// </summary>
    public Thon.ZaszBlog.Support.CodedRepresentations.Page CRPage;

    /// <summary>
    /// Serves the coresponding to the supplied id in QueryString, and does deleting if need be,
    /// and adds meta tags
    /// </summary>
	protected void Page_Load(object sender, EventArgs e)
	{
        //GUID is 32 Hex characters seperated by 4 hyphens : 32+4=36
        if (Request.QueryString["deletepage"] != null && Request.QueryString["deletepage"].Length == 36)
		{
			DeletePage(new Guid(Request.QueryString["deletepage"]));
		}        
		if (Request.QueryString["id"] != null && Request.QueryString["id"].Length == 36)
		{
			ServePage(new Guid(Request.QueryString["id"]));
			AddMetaTags();
		}
		else
		{
			Response.Redirect(SupportUtilities.RelativeWebRoot);
		}        
	}

    /// <summary>
    /// Serves the page by realizing the usercontrols and using the divText DIV tag.
    /// </summary>
    /// <param name="id">The GUID of the Page to be rendered.</param>
	private void ServePage(Guid id)
	{
        this.CRPage = Thon.ZaszBlog.Support.CodedRepresentations.Page.GetPage(id);

		if (this.CRPage == null || (!this.CRPage.IsVisible && !this.User.Identity.IsAuthenticated))
			Response.Redirect(SupportUtilities.RelativeWebRoot + "Error404.aspx", true);

        // Set title
		h1Title.InnerHtml = this.CRPage.Title;

		ServingEventArgs arg = new ServingEventArgs(this.CRPage.Content, ServingLocation.SinglePage);
        Thon.ZaszBlog.Support.CodedRepresentations.Page.OnServing(this.CRPage, arg);

		if (arg.Cancel)
			Response.Redirect(SupportUtilities.RelativeWebRoot + "Error404.aspx", true);

		if (arg.Body.ToLowerInvariant().Contains("[usercontrol"))
		{
			InjectUserControls(arg.Body);
		}
		else
		{
			divText.InnerHtml = arg.Body;
		}
	}

	/// <summary>
	/// Adds the meta tags and title to the HTML header.
	/// </summary>
	private void AddMetaTags()
	{
		if (this.CRPage != null)
		{
			base.Title = Server.HtmlEncode(this.CRPage.Title);
			base.AddMetaTag("keywords", Server.HtmlEncode(this.CRPage.Keywords));
			base.AddMetaTag("description", Server.HtmlEncode(this.CRPage.Description));
		}
	}

	/// <summary>
	/// Deletes the page.
	/// </summary>
	private void DeletePage(Guid id)
	{
		if (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
		{
			Thon.ZaszBlog.Support.CodedRepresentations.Page page = Thon.ZaszBlog.Support.CodedRepresentations.Page.GetPage(id);
			page.Delete();
			page.Save();
            Response.Redirect("~/ZaszBlog/Blog.aspx", true);
		}
	}

	private static readonly Regex _BodyRegex = new Regex(@"\[UserControl:(.*?)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	/// <summary>
	/// Injects any user controls if one is referenced in the text, by using the expression
    /// [UserControl:~/path/usercontrol.ascx;paramname=paramvalue;]
	/// </summary>
	private void InjectUserControls(string content)
	{
		int currentPosition = 0;
		MatchCollection myMatches = _BodyRegex.Matches(content);

		foreach (Match myMatch in myMatches)
		{
			if (myMatch.Index > currentPosition)
			{
				divText.Controls.Add(new LiteralControl(content.Substring(currentPosition, myMatch.Index - currentPosition)));
			}

			try
			{
				string all = myMatch.Groups[1].Value.Trim();
				UserControl usercontrol = null;
				
				if (!all.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
				{
					int index = all.IndexOf(".ascx", StringComparison.OrdinalIgnoreCase) +5;
					usercontrol = (UserControl)LoadControl(all.Substring(0, index));
				
					string parameters = Server.HtmlDecode(all.Substring(index));
					Type type = usercontrol.GetType();
					string[] paramCollection = parameters.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
					
					foreach (string param in paramCollection)
					{
						string name = param.Split('=')[0].Trim();
						string value = param.Split('=')[1].Trim();
						System.Reflection.PropertyInfo property = type.GetProperty(name);
						property.SetValue(usercontrol, Convert.ChangeType(value, property.PropertyType), null);
					}
				}
				else
				{
					usercontrol = (UserControl)LoadControl(all);
				}

				divText.Controls.Add(usercontrol);
			}
			catch (Exception)
			{
				divText.Controls.Add(new LiteralControl("ERROR - UNABLE TO LOAD CONTROL : " + myMatch.Groups[1].Value));
			}

			currentPosition = myMatch.Index + myMatch.Groups[0].Length;
		}

		// Finally we add any trailing static text.
		divText.Controls.Add(new LiteralControl(content.Substring(currentPosition, content.Length - currentPosition)));
	}

    /// <summary>
	/// Gets the admin links to edit and delete a page.
	/// </summary>
	/// <value>The admin links.</value>
	public string AdminLinks
	{
		get
		{
			if (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<div id=\"admin\">");
				sb.AppendFormat("<a href=\"{0}Admin/Pages/Pages.aspx?id={1}\">Edit</a> | ", SupportUtilities.RelativeWebRoot, this.CRPage.Id.ToString());
				sb.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"if (confirm('Are you sure you want to delete the page?')) location.href='?deletepage={0}'\">Delete</a>", this.CRPage.Id.ToString());
				sb.AppendLine("</div>");
				return sb.ToString();
			}
			return string.Empty;
		}
	}
}