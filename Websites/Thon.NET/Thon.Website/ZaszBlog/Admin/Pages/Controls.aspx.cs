using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Thon.ZaszBlog.Support;


public partial class AdminControls : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
      BindSettings();

    btnSave.Click += new EventHandler(btnSave_Click);
    Page.Title = "Controls";
  }

  void btnSave_Click(object sender, EventArgs e)
  {
    BlogSettings.Instance.NumberOfRecentPosts = int.Parse(txtNumberOfPosts.Text, CultureInfo.InvariantCulture);
    BlogSettings.Instance.DisplayCommentsOnRecentPosts = cbDisplayComments.Checked;
    BlogSettings.Instance.DisplayRatingsOnRecentPosts = cbDisplayRating.Checked;

    BlogSettings.Instance.NumberOfRecentComments = int.Parse(txtNumberOfComments.Text, CultureInfo.InvariantCulture);

    BlogSettings.Instance.SearchButtonText = txtSearchButtonText.Text;
    BlogSettings.Instance.SearchCommentLabelText = txtCommentLabelText.Text;
    BlogSettings.Instance.SearchDefaultText = txtDefaultSearchText.Text;
    BlogSettings.Instance.EnableCommentSearch = cbEnableCommentSearch.Checked;

    BlogSettings.Instance.Save();
  }

  private void BindSettings()
  {
    txtNumberOfPosts.Text = BlogSettings.Instance.NumberOfRecentPosts.ToString();
    cbDisplayComments.Checked = BlogSettings.Instance.DisplayCommentsOnRecentPosts;
    cbDisplayRating.Checked = BlogSettings.Instance.DisplayRatingsOnRecentPosts;

    txtNumberOfComments.Text = BlogSettings.Instance.NumberOfRecentComments.ToString();

    txtSearchButtonText.Text = BlogSettings.Instance.SearchButtonText;
    txtCommentLabelText.Text = BlogSettings.Instance.SearchCommentLabelText;
    txtDefaultSearchText.Text = BlogSettings.Instance.SearchDefaultText;
    cbEnableCommentSearch.Checked = BlogSettings.Instance.EnableCommentSearch;
  }
}
