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
using System.Collections.Generic;


namespace Thon.Gallery
{
    public partial class GalleryPopupAspx : System.Web.UI.Page
    {
        GalleryContext GDC = new GalleryContext();
        List<Category> cats = new List<Category>();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            bool AdminMode = false;

            if (Context.Request.IsAuthenticated)
                AdminMode = true;

            if (AdminMode)
            {
                DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this picture?')";
                if (!this.IsPostBack)
                {
                    AdminView.Visible = true;
                    AdminImageContent.ImageUrl = "ThonHttpHandlers/GalleryDynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Width=" + Request.QueryString["PhotoWidth"] + "&Height=" + Request.QueryString["PhotoHeight"];
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
                ImageContent.ImageUrl = "ThonHttpHandlers/GalleryDynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Width=" + Request.QueryString["PhotoWidth"] + "&Height=" + Request.QueryString["PhotoHeight"];
                ImageContent.AlternateText = Server.UrlDecode(Request.QueryString["PhotoDescription"]);
                ImageContent.Width = int.Parse(Request.QueryString["PhotoWidth"]);
                ImageContent.Height = int.Parse(Request.QueryString["PhotoHeight"]);
                DownloadLinkHref.NavigateUrl = "ThonHttpHandlers/GalleryDynamicPicture.ashx?Target=" + Request.QueryString["Target"] + "&Download=True";
                DescriptionLiteral.Text = "<p>" + Server.UrlDecode(Request.QueryString["PhotoDate"]) + " - " + Server.UrlDecode(Request.QueryString["PhotoDescription"]) + "</p>";
            }

        }

        protected void UpdateButton_Click(object sender, System.EventArgs e)
        {
            //    SQLString = "UPDATE Photos SET PhotoDate = #" + DateTime.Parse(PhotoDateTextBox.Text).ToString() + "#, PhotoDescription = '" + PhotoDescriptionTextBox.Text.Replace("'", "''") + 
            //    "', PhotoWidth = '" + PhotoWidthTextBox.Text + "', PhotoHeight = '" + PhotoHeightTextBox.Text + "', PhotoResolution = '" + PhotoResolutionTextBox.Text + "', PhotoVisible = " + PhotoVisibleCheckBox.Checked + 
            //    " WHERE PhotoID = " + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";
            string ownr = (string)Session["GalleryOwner"];
            Photo toupdate = GDC.Photos.First(phot => phot.PhotoOwner.Equals(ownr) && phot.PhotoID == int.Parse(PhotoIDLabel.Text));

            toupdate.PhotoDate = DateTime.Parse(PhotoDateTextBox.Text);
            toupdate.PhotoDescription = PhotoDescriptionTextBox.Text.Replace("'", string.Empty);
            toupdate.PhotoWidth = short.Parse(PhotoWidthTextBox.Text);
            toupdate.PhotoHeight = short.Parse(PhotoHeightTextBox.Text);
            toupdate.PhotoResolution = short.Parse(PhotoResolutionTextBox.Text);
            toupdate.PhotoVisible = PhotoVisibleCheckBox.Checked;
                       
            GDC.SubmitChanges();

            //Clear and reassign the categories
            //SQLString = "DELETE FROM PhotosJoinCategories WHERE PhotoNumber=" + int.Parse(PhotoIDLabel.Text);
            IEnumerable<PhotoCategoryJoin> todelete =
                from pcj in GDC.PhotoCategoryJoins
                where pcj.PhotoNumber == int.Parse(PhotoIDLabel.Text)
                select pcj;
            if (todelete != null)
            {
                GDC.PhotoCategoryJoins.DeleteAllOnSubmit(todelete);
                GDC.SubmitChanges();
            }
            //Stub statement, needs more OR's to get anything
            //SQLString = "INSERT INTO PhotosJoinCategories ( CategoryNumber, PhotoNumber ) SELECT CategoryID, " + int.Parse(PhotoIDLabel.Text) + " FROM Categories WHERE CategoryID = 0";
            List<PhotoCategoryJoin> pcjlist = new List<PhotoCategoryJoin>();
            PhotoCategoryJoin pcjoin;
            for (int i = 0; i <= AllCategoriesList.Items.Count - 1; i++)
            {
                if (AllCategoriesList.Items[i].Selected)
                {
                    //SQLString = SQLString + " OR CategoryID = " + AllCategoriesList.Items[i].Value;
                    pcjoin = new PhotoCategoryJoin();
                    pcjoin.PhotoNumber = int.Parse(PhotoIDLabel.Text);
                    pcjoin.CategoryNumber = int.Parse(AllCategoriesList.Items[i].Value);
                    pcjlist.Add(pcjoin);
                }
            }
            GDC.PhotoCategoryJoins.InsertAllOnSubmit(pcjlist);
            GDC.SubmitChanges();

            //If it's already Flickr'ed, mark it for an update on the next sweep
            //SQLString = "INSERT INTO FlickrCommands ( FlickrCommandType, FlickrCommandPhoto ) 
            //SELECT 'UpdatePhoto', Photos.PhotoID FROM Photos WHERE PhotoFlickrID Is Not Null AND PhotoID=" + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";
            IEnumerable<int> flickrphotos =
                from photo in GDC.Photos
                where (photo.PhotoFlickrID != null) && photo.PhotoID == int.Parse(PhotoIDLabel.Text) && photo.PhotoOwner.Equals((string)Session["GalleryOwner"])
                select photo.PhotoID;
            if (flickrphotos.Count() != 0)
            {
                int flickrphoto = flickrphotos.First();
                FlickrCommand fc = new FlickrCommand();
                fc.FlickrCommandType = "UpdatePhoto";
                fc.FlickrCommandPhoto = flickrphoto;
                GDC.FlickrCommands.InsertOnSubmit(fc);
                GDC.SubmitChanges();
            }
            LoadPictureData();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "<script type=\"text/javascript\">window.opener.location.href = window.opener.location.href;window.close();</script>");
        }

        private void LoadPictureData()
        {
            string own = (string)Session["GalleryOwner"];
            Photo thephoto =
                (
                from photo in GDC.Photos
                where photo.PhotoID == int.Parse(Request.QueryString["PhotoID"]) && photo.PhotoOwner.Equals(own)
                select photo
                ).First();


            PhotoIDLabel.Text = thephoto.PhotoID.ToString();
            PhotoDateTextBox.Text = thephoto.PhotoDate.ToString();
            PhotoDescriptionTextBox.Text = thephoto.PhotoDescription;
            PhotoWidthTextBox.Text = thephoto.PhotoWidth.ToString();
            PhotoHeightTextBox.Text = thephoto.PhotoHeight.ToString();
            PhotoResolutionTextBox.Text = thephoto.PhotoResolution.ToString();
            PhotoVisibleCheckBox.Checked = thephoto.PhotoVisible;

            AllCategoriesList.Items.Clear();

            //SQLString = "SELECT * FROM Categories WHERE CategoryOwner='" + Session["GalleryOwner"] + "' ORDER BY CategoryName";
            IEnumerable<Category> IEcats =
                from cat in GDC.Categories
                where cat.CategoryOwner.Equals((string)Session["GalleryOwner"])
                orderby cat.CategoryName
                select cat;            
            cats = IEcats.ToList();
            AllCategoriesList.DataTextField = "CategoryName";
            AllCategoriesList.DataValueField = "CategoryID";
            AllCategoriesList.DataSource = cats;
            AllCategoriesList.DataBind();

            //SQLString = "SELECT CategoryNumber FROM PhotosJoinCategories WHERE PhotoNumber=" + int.Parse(Request.QueryString["PhotoID"].ToString());
            IEnumerable<int> catnos =
                from pcj in GDC.PhotoCategoryJoins
                where pcj.PhotoNumber == int.Parse(Request.QueryString["PhotoID"])
                select pcj.CategoryNumber;

            string CategoriesString = ";";
            foreach(int i in catnos)
                CategoriesString = CategoriesString + i.ToString() + ";";

            for (int i = 0; i < AllCategoriesList.Items.Count; i++)
                if (CategoriesString.Contains(";" + AllCategoriesList.Items[i].Value + ";"))
                    AllCategoriesList.Items[i].Selected = true;
        }

        protected void DeleteButton_Click(object sender, System.EventArgs e)
        {
            AdminImageContent.ImageUrl = "";

            int LastSlashIndex = Request.QueryString["Target"].LastIndexOf("/");
            string ThumbRelativePath = Request.QueryString["Target"].Substring(0, LastSlashIndex + 1) + "Thumbs/" + Request.QueryString["Target"].Substring(LastSlashIndex + 1);

            System.IO.File.Delete(Server.MapPath(Request.QueryString["Target"]));
            System.IO.File.Delete(Server.MapPath(ThumbRelativePath));

            //SQLString = "INSERT INTO FlickrCommands ( FlickrCommandType, FlickrCommandParameter ) 
            //SELECT 'DeletePhoto', Photos.PhotoFlickrID FROM Photos 
            //WHERE PhotoFlickrID Is Not Null AND PhotoID=" + int.Parse(PhotoIDLabel.Text) + " AND PhotoOwner = '" + Session["GalleryOwner"] + "'";

            IEnumerable<string> PhotoFlickrID =
                from photo in GDC.Photos
                where (photo.PhotoFlickrID != null) && photo.PhotoID == int.Parse(PhotoIDLabel.Text) && photo.PhotoOwner.Equals((string)Session["GalleryOwner"])
                select photo.PhotoFlickrID;
            if (PhotoFlickrID.Count() > 0)
            {
                FlickrCommand fc = new FlickrCommand();
                fc.FlickrCommandType = "DeletePhoto";
                fc.FlickrCommandParameter = PhotoFlickrID.First();
                GDC.FlickrCommands.InsertOnSubmit(fc);
                GDC.SubmitChanges();
            }

            //SQLString = "DELETE FROM Photos 
            //WHERE PhotoID=" + int.Parse(Request.QueryString["PhotoID"]) + " AND PhotoOwner='" + Session["GalleryOwner"] + "'";
            Photo p =
                (
                from photo in GDC.Photos
                where photo.PhotoID == int.Parse(Request.QueryString["PhotoID"]) && photo.PhotoOwner.Equals((string)Session["GalleryOwner"])
                select photo
                ).First();
            GDC.Photos.DeleteOnSubmit(p);
            GDC.SubmitChanges();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "<script type=\"text/javascript\">window.opener.location.href = window.opener.location.href;window.close();</script>");
        }
    }
}
