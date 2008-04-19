using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support;

public partial class AdminConfiguration : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e) 
    {
        if (!IsPostBack)
            BindSettings();

        Page.MaintainScrollPositionOnPostBack = true;
        btnSave.Click += new EventHandler(btnSave_Click);
        btnSaveTop.Click += new EventHandler(btnSave_Click);
    }

    private void btnSave_Click(object sender, EventArgs e) {
        //-----------------------------------------------------------------------
        // Set Basic settings
        //-----------------------------------------------------------------------
        BlogSettings.Instance.Name = txtName.Text;
        BlogSettings.Instance.Description = txtDescription.Text;
        BlogSettings.Instance.PostsPerPage = int.Parse(txtPostsPerPage.Text);
        BlogSettings.Instance.EnableRelatedPosts = cbShowRelatedPosts.Checked;
        BlogSettings.Instance.EnableRating = cbEnableRating.Checked;
        BlogSettings.Instance.ShowDescriptionInPostList = cbShowDescriptionInPostList.Checked;
        BlogSettings.Instance.ShowPostNavigation = cbShowPostNavigation.Checked;

        //-----------------------------------------------------------------------
        // Set Email settings
        //-----------------------------------------------------------------------
        BlogSettings.Instance.SendMailOnComment = cbComments.Checked;

        //-----------------------------------------------------------------------
        // Set Comments settings
        //-----------------------------------------------------------------------
        BlogSettings.Instance.IsCommentsEnabled = cbEnableComments.Checked;
        BlogSettings.Instance.IsCoCommentEnabled = cbEnableCoComment.Checked;
        BlogSettings.Instance.ShowLivePreview = cbShowLivePreview.Checked;
        BlogSettings.Instance.DaysCommentsAreEnabled = int.Parse(ddlCloseComments.SelectedValue);
        BlogSettings.Instance.EnableCommentsModeration = cbEnableCommentsModeration.Checked;
        BlogSettings.Instance.Avatar = rblAvatar.SelectedValue;

        //-----------------------------------------------------------------------
        // Set Advanced settings
        //-----------------------------------------------------------------------
		BlogSettings.Instance.EnableTrackBackSend = cbEnableTrackBackSend.Checked;
		BlogSettings.Instance.EnableTrackBackReceive = cbEnableTrackBackReceive.Checked;
		BlogSettings.Instance.EnablePingBackSend = cbEnablePingBackSend.Checked;
		BlogSettings.Instance.EnablePingBackReceive = cbEnablePingBackReceive.Checked;

        //-----------------------------------------------------------------------
        // Set Syndication settings
        //-----------------------------------------------------------------------
        BlogSettings.Instance.SyndicationFormat = ddlSyndicationFormat.SelectedValue;
        BlogSettings.Instance.PostsPerFeed = int.Parse(txtPostsPerFeed.Text);
      
        if (txtAlternateFeedUrl.Text.Trim().Length > 0 && !txtAlternateFeedUrl.Text.Contains("://"))
            txtAlternateFeedUrl.Text = "http://" + txtAlternateFeedUrl.Text;

        BlogSettings.Instance.AlternateFeedUrl = txtAlternateFeedUrl.Text;

        //-----------------------------------------------------------------------
        //  Persist settings
        //-----------------------------------------------------------------------
        BlogSettings.Instance.Save();
        Response.Redirect(Request.RawUrl, true);
    }

    private void BindSettings() {
        //-----------------------------------------------------------------------
        // Bind Basic settings
        //-----------------------------------------------------------------------
        txtName.Text = BlogSettings.Instance.Name;
        txtDescription.Text = BlogSettings.Instance.Description;
        txtPostsPerPage.Text = BlogSettings.Instance.PostsPerPage.ToString();
        cbShowRelatedPosts.Checked = BlogSettings.Instance.EnableRelatedPosts;
        cbEnableRating.Checked = BlogSettings.Instance.EnableRating;
        cbShowDescriptionInPostList.Checked = BlogSettings.Instance.ShowDescriptionInPostList;

        //-----------------------------------------------------------------------
        // Bind Comments settings
        //-----------------------------------------------------------------------
        cbEnableComments.Checked = BlogSettings.Instance.IsCommentsEnabled;
        cbEnableCoComment.Checked = BlogSettings.Instance.IsCoCommentEnabled;
        cbShowLivePreview.Checked = BlogSettings.Instance.ShowLivePreview;
		cbShowPostNavigation.Checked = BlogSettings.Instance.ShowPostNavigation;
        ddlCloseComments.SelectedValue = BlogSettings.Instance.DaysCommentsAreEnabled.ToString();
        cbEnableCommentsModeration.Checked = BlogSettings.Instance.EnableCommentsModeration;
        rblAvatar.SelectedValue = BlogSettings.Instance.Avatar;

        //-----------------------------------------------------------------------
        // Bind Email settings
        //-----------------------------------------------------------------------
        cbComments.Checked = BlogSettings.Instance.SendMailOnComment;

        //-----------------------------------------------------------------------
        // Bind Advanced settings
        //-----------------------------------------------------------------------
		cbEnablePingBackSend.Checked = BlogSettings.Instance.EnablePingBackSend;
		cbEnablePingBackReceive.Checked = BlogSettings.Instance.EnablePingBackReceive;
		cbEnableTrackBackSend.Checked = BlogSettings.Instance.EnableTrackBackSend;
		cbEnableTrackBackReceive.Checked = BlogSettings.Instance.EnableTrackBackReceive;

        //-----------------------------------------------------------------------
        // Bind Syndication settings
        //-----------------------------------------------------------------------
        ddlSyndicationFormat.SelectedValue = BlogSettings.Instance.SyndicationFormat;
        txtPostsPerFeed.Text = BlogSettings.Instance.PostsPerFeed.ToString();
        txtAlternateFeedUrl.Text = BlogSettings.Instance.AlternateFeedUrl;
    }

}