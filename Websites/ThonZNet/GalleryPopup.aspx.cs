using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Thon.Gallery
{
    public partial class GalleryPopupAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            bool AdminMode = false;

            if (Session["GalleryConnectionString"] != null)
            {
                if (Context.Request.IsAuthenticated == true && (Session["GalleryOwner"].ToString() == "Any") | Session["GalleryOwner"].ToString().ToLower().Contains(Context.User.Identity.Name.ToLower()))
                {
                    AdminMode = true;
                }
            }

            if (AdminMode == true)
            {
                //Admin mode

                DeleteButton.OnClientClick = "return confirm('" + GetLocalResourceObject("CodeDeleteConfirm").ToString() + "')";

                if (!this.IsPostBack)
                {

                    AdminView.Visible = true;
                    AdminImageContent.ImageUrl = "DynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Width=" + Request.QueryString["PhotoWidth"] + "&Height=" + Request.QueryString["PhotoHeight"];
                    AdminImageContent.AlternateText = "Preview Photo";
                    ImageContent.Width = int.Parse(Request.QueryString["PhotoWidth"]);
                    ImageContent.Height = int.Parse(Request.QueryString["PhotoHeight"]);
                    LoadPictureData();

                }
            }

            else
            {
                UserView.Visible = true;
                string temp = Request.QueryString["PhotoWidth"];
                ImageContent.ImageUrl = "DynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Width=" + Request.QueryString["PhotoWidth"] + "&Height=" + Request.QueryString["PhotoHeight"];
                ImageContent.AlternateText = Server.UrlDecode(Request.QueryString["PhotoDescription"]);
                ImageContent.Width = int.Parse(Request.QueryString["PhotoWidth"]);
                ImageContent.Height = int.Parse(Request.QueryString["PhotoHeight"]);
                DownloadLinkHref.NavigateUrl = "DynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Download=True";
                DescriptionLiteral.Text = "<p>" + Server.UrlDecode(Request.QueryString["PhotoDate"]) + " - " + Server.UrlDecode(Request.QueryString["PhotoDescription"]) + "</p>";
            }

        }

        protected void UpdateButton_Click(object sender, System.EventArgs e)
        {

            //Save the changes
            System.Data.OleDb.OleDbConnection myOleDbConnection = new System.Data.OleDb.OleDbConnection(Session["GalleryConnectionString"].ToString());
            myOleDbConnection.Open();

            string SQLString;
            SQLString = "UPDATE Photos SET PhotoDate = #" + DateTime.Parse(PhotoDateTextBox.Text).ToString() + "#, PhotoDescription = '" + PhotoDescriptionTextBox.Text.Replace("'", "''") + "', PhotoWidth = '" + PhotoWidthTextBox.Text + "', PhotoHeight = '" + PhotoHeightTextBox.Text + "', PhotoResolution = '" + PhotoResolutionTextBox.Text + "', PhotoVisible = " + PhotoVisibleCheckBox.Checked + " WHERE PhotoID = " + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";
            System.Data.OleDb.OleDbCommand myOleDbCommand;
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();

            //Clear and reassign the categories
            SQLString = "DELETE FROM PhotosJoinCategories WHERE PhotoNumber=" + int.Parse(PhotoIDLabel.Text);
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();

            //Stub statement, needs more OR's to get anything
            SQLString = "INSERT INTO PhotosJoinCategories ( CategoryNumber, PhotoNumber ) SELECT CategoryID, " + int.Parse(PhotoIDLabel.Text) + " FROM Categories WHERE CategoryID = 0";
            int i;
            for (i = 0; i <= AllCategoriesList.Items.Count - 1; i++)
            {
                if (AllCategoriesList.Items[i].Selected == true)
                {
                    SQLString = SQLString + " OR CategoryID = " + AllCategoriesList.Items[i].Value;
                }
            }
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();

            //If it's already Flickr'ed, mark it for an update on the next sweep
            SQLString = "INSERT INTO FlickrCommands ( FlickrCommandType, FlickrCommandPhoto ) SELECT 'UpdatePhoto', Photos.PhotoID FROM Photos WHERE PhotoFlickrID Is Not Null AND PhotoID=" + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();

            myOleDbConnection.Close();

            LoadPictureData();

            //Close the window
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "<script type=\"text/javascript\">window.opener.location.href = window.opener.location.href;window.close();</script>");

        }


        private void LoadPictureData()
        {

            //Read the database for the rest
            System.Data.OleDb.OleDbConnection myOleDbConnection = new System.Data.OleDb.OleDbConnection(Session["GalleryConnectionString"].ToString());
            myOleDbConnection.Open();

            string SQLString;

            SQLString = "SELECT * FROM Photos WHERE PhotoID=" + int.Parse(Request.QueryString["PhotoID"].ToString()) + " AND PhotoOwner='" + Session["GalleryOwner"] + "'";
            System.Data.OleDb.OleDbCommand myOleDbCommand;
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            System.Data.OleDb.OleDbDataReader myOleDbDataReader;
            myOleDbDataReader = myOleDbCommand.ExecuteReader();

            if (myOleDbDataReader.Read())
            {
                PhotoIDLabel.Text = myOleDbDataReader["PhotoID"].ToString();
                PhotoDateTextBox.Text = DateTime.Parse(myOleDbDataReader["PhotoDate"].ToString()).ToString();
                PhotoDescriptionTextBox.Text = FixNull(myOleDbDataReader["PhotoDescription"].ToString());
                PhotoWidthTextBox.Text = myOleDbDataReader["PhotoWidth"].ToString();
                PhotoHeightTextBox.Text = myOleDbDataReader["PhotoHeight"].ToString();
                PhotoResolutionTextBox.Text = myOleDbDataReader["PhotoResolution"].ToString();
                PhotoVisibleCheckBox.Checked = bool.Parse(myOleDbDataReader["PhotoVisible"].ToString());
            }

            myOleDbDataReader.Close();
            AllCategoriesList.Items.Clear();

            SQLString = "SELECT * FROM Categories WHERE CategoryOwner='" + Session["GalleryOwner"] + "' ORDER BY CategoryName";
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbDataReader = myOleDbCommand.ExecuteReader();

            AllCategoriesList.DataTextField = "CategoryName";
            AllCategoriesList.DataValueField = "CategoryID";
            AllCategoriesList.DataSource = myOleDbDataReader;
            AllCategoriesList.DataBind();
            myOleDbDataReader.Close();


            SQLString = "SELECT CategoryNumber FROM PhotosJoinCategories WHERE PhotoNumber=" + int.Parse(Request.QueryString["PhotoID"].ToString());
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbDataReader = myOleDbCommand.ExecuteReader();

            //Build up a list of them
            string CategoriesString = ";";
            while (myOleDbDataReader.Read())
            {
                CategoriesString = CategoriesString + myOleDbDataReader["CategoryNumber"].ToString() + ";";
            }

            int i;
            for (i = 0; i <= AllCategoriesList.Items.Count - 1; i++)
            {
                if (CategoriesString.Contains(";" + AllCategoriesList.Items[i].Value + ";"))
                {
                    AllCategoriesList.Items[i].Selected = true;
                }
            }

            myOleDbDataReader.Close();
            myOleDbConnection.Close();

        }

        protected void DeleteButton_Click(object sender, System.EventArgs e)
        {

            //Delete it
            AdminImageContent.ImageUrl = "";

            int LastSlashIndex = Request.QueryString["Target"].LastIndexOf("/");
            string ThumbRelativePath = Request.QueryString["Target"].Substring(0,

            LastSlashIndex + 1) + "Thumbs/" + Request.QueryString["Target"].Substring

            (LastSlashIndex + 1);

            System.IO.File.Delete(Server.MapPath(Request.QueryString["Target"]));
            System.IO.File.Delete(Server.MapPath(ThumbRelativePath));

            System.Data.OleDb.OleDbConnection myOleDbConnection = new System.Data.OleDb.OleDbConnection(Session["GalleryConnectionString"].ToString());
            myOleDbConnection.Open();

            string SQLString;
            System.Data.OleDb.OleDbCommand myOleDbCommand;

            //If it's already Flickr'ed, mark it for an update on the next sweep
            SQLString = "INSERT INTO FlickrCommands ( FlickrCommandType, FlickrCommandParameter ) SELECT 'DeletePhoto', Photos.PhotoFlickrID FROM Photos WHERE PhotoFlickrID Is Not Null AND PhotoID=" + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();

            SQLString = "DELETE FROM Photos WHERE PhotoID=" + int.Parse(Request.QueryString["PhotoID"]) + " AND PhotoOwner='" + Session["GalleryOwner"] + "'";
            myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
            myOleDbCommand.ExecuteNonQuery();
            myOleDbConnection.Close();

            //Close the window
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "<script type=\"text/javascript\">window.opener.location.href = window.opener.location.href;window.close();</script>");
        }

        private string FixNull(object dbvalue, string ReplacementString)
        {
            if (object.ReferenceEquals(dbvalue, DBNull.Value))
                return ReplacementString;
            else
                return dbvalue.ToString();
        }
        private string FixNull(object dbvalue)
        {
            if (object.ReferenceEquals(dbvalue, DBNull.Value))
                return "";
            else
                return dbvalue.ToString();
        }

    }
}
