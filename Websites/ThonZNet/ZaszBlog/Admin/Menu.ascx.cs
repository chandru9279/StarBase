using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;
using Thon.ZaszBlog.Support;
using Thon.Support;

public partial class AdminUserControlMenu : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsCallback)
			BindMenu();
	}

	private void BindMenu()
	{
		foreach (SiteMapNode adminNode in SiteMap.Providers["SecuritySiteMap"].RootNode.ChildNodes[0].ChildNodes[0].ChildNodes)
		{
			if (adminNode.IsAccessibleToUser(HttpContext.Current))
			{
				if (!Request.RawUrl.ToUpperInvariant().Contains("/ADMIN/") && (adminNode.Url.Contains("xmanager") || adminNode.Url.Contains("PingServices")))
					continue;

				HtmlAnchor a = new HtmlAnchor();
				a.HRef = adminNode.Url;

				a.InnerHtml = "<span>" + adminNode.Title + "</span>";
                //"<span>" + Translate(info.Name.Replace(".aspx", string.Empty)) + "</span>";
				if (Request.RawUrl.EndsWith(adminNode.Url, StringComparison.OrdinalIgnoreCase))
					a.Attributes["class"] = "current";
				HtmlGenericControl li = new HtmlGenericControl("li");
				li.Controls.Add(a);
				ulMenu.Controls.Add(li);
			}
		}

		if (!Request.RawUrl.ToUpperInvariant().Contains("/ADMIN/"))
            AddItem("Change password", SupportUtilities.RelativeWebRoot + "Login.aspx");
	}

    public void AddItem(string text, string url)
    {
        HtmlAnchor a = new HtmlAnchor();
        a.InnerHtml = "<span>" + text + "</span>";
        a.HRef = url;
        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Controls.Add(a);
        ulMenu.Controls.Add(li);
    }
}
