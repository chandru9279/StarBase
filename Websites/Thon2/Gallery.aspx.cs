using System;
using System.Collections;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Thon.Support;

namespace Thon.Gallery
{
    public partial class GalleryAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        private GalleryContext GDC;
        bool AdminMode = false;
        int _CurrentPage = 1;

        #region SortOrder
        public enum SortOrderType : byte
        {
            AscendingDate,
            DescendingDate
        }
        SortOrderType _SortOrder = SortOrderType.DescendingDate;
        public SortOrderType SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        #endregion

        #region PageSize
        private int _PageSize = 25;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        #endregion

        #region FilePath
        private string _FilePath = "~/App_Data/Gallery/";
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        #endregion

        #region Owner
        private string _Owner = "Zasz";
        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        #endregion

        #region IncludeCategories
        private bool _IncludeCategories = true;
        public bool IncludeCategories
        {
            get { return _IncludeCategories; }
            set { _IncludeCategories = value; }
        }
        #endregion

        #region IncludeSearch
        private bool _IncludeSearch = true;
        public bool IncludeSearch
        {
            get { return _IncludeSearch; }
            set { _IncludeSearch = value; }
        }
        #endregion

        #region IncludeSort
        private bool _IncludeSort = true;
        public bool IncludeSort
        {
            get { return _IncludeSort; }
            set { _IncludeSort = value; }
        }
        #endregion

        #region IncludePageSize
        private bool _IncludePageSize = true;
        public bool IncludePageSize
        {
            get { return _IncludePageSize; }
            set { _IncludePageSize = value; }
        }
        #endregion

        #region ItemCSSClasses
        private string _ItemCSSClasses = "PhotoItemHidden,PhotoItem";
        public string ItemCSSClasses
        {
            get { return _ItemCSSClasses; }
            set { _ItemCSSClasses = value; }
        }
        #endregion

        #region PopupPage
        private string _PopupPage = "~/GalleryPopup.aspx";
        public string PopupPage
        {
            get { return _PopupPage; }
            set { _PopupPage = value; }
        }
        #endregion

        #region ThumbnailSize
        private int _ThumbnailSize = 120;
        public int ThumbnailSize
        {
            get { return _ThumbnailSize; }
            set { _ThumbnailSize = value; }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            GDC = new GalleryContext();
            if (Context.Request.IsAuthenticated == true && (_Owner == "Any" || _Owner.ToLower().Contains(Context.User.Identity.Name.ToLower())))
            {
                AdminMode = true;
                Session["GalleryOwner"] = _Owner;
                GalleryFeatures.Visible = true;
               // GalleryAdvanced.Visible = true;
                CategoriesDeleteButton.OnClientClick = "return alert('Are you sure you want to delete the category?')";
            }

            if (Request.QueryString["Page"] != null)
                _CurrentPage = int.Parse(Request.QueryString["Page"].ToString());

            if (Request.QueryString["PageSize"] != null)
                _PageSize = int.Parse(Request.QueryString["PageSize"].ToString());

            if (Request.QueryString["SortOrder"] != null)
            {
                if (Request.QueryString["SortOrder"] == "Oldest")
                    _SortOrder = SortOrderType.AscendingDate;
                else
                    _SortOrder = SortOrderType.DescendingDate;
            }

            RenderGalleryUploads();

            if (_IncludeCategories == true)
                RenderCategories();

            if (_IncludeSearch == true)
                GallerySearch.Visible = true;

            if (_IncludeSort == true)
                RenderSort();

            if (_IncludePageSize == true)
                RenderPageSize();

            if (!Page.IsPostBack)
                ViewState["CurrentPage"] = _CurrentPage;

            BindRepeater();

        }

        private void RenderCategories()
        {
            SortedList CategoriesSortedList = null;
            CategoriesSortedList = (SortedList)Cache["GalleryControl" + _Owner + "Categories"];
            if (CategoriesSortedList == null)
            {
                CategoriesSortedList = new SortedList();

                var curr_owner_categories =
                    from cat in GDC.Categories
                    where cat.CategoryOwner.Equals(_Owner)
                    orderby cat.CategoryName
                    select cat.CategoryName;

                foreach (string catname in curr_owner_categories)
                {
                    string LinkString = Request.Path + "?Categories=" + Server.UrlEncode(catname);                    
                    string AdditionalQueryString = "";
                    if (Request.QueryString["PageSize"] != null)
                    {
                        AdditionalQueryString = AdditionalQueryString + "&PageSize=" + Request.QueryString["PageSize"];
                    }
                    if (Request.QueryString["SortOrder"] != null)
                    {
                        AdditionalQueryString = AdditionalQueryString + "&SortOrder=" + Request.QueryString["SortOrder"];
                    }
                    if (PreviousButton.Visible == true)
                    {
                        PreviousButton.NavigateUrl = Request.Path + "?Page=" + (_CurrentPage - 1).ToString() + AdditionalQueryString;
                    }
                    if (NextButton.Visible == true)
                    {
                        NextButton.NavigateUrl = Request.Path + "?Page=" + (_CurrentPage + 1).ToString() + AdditionalQueryString;
                    }
                    CategoriesSortedList.Add(catname, LinkString + AdditionalQueryString);
                    Cache.Insert("GalleryControl" + _Owner + "Categories", CategoriesSortedList, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                }
            }

            GalleryFeatures.Visible = true;
            GalleryCategories.Visible = true;
           // GalleryAdvanced.Visible = true;
            CategoriesList.Visible = true;

            CategoriesList.DataSource = CategoriesSortedList;
            CategoriesList.DataTextField = "Key";
            CategoriesList.DataValueField = "Value";
            CategoriesList.DataBind();

            CategoriesList.Items.Insert(0, new ListItem("Everything", Request.Path));

            if (AdminMode == true)
                CategoriesList.Items.Insert(1, new ListItem("None Assigned", Request.Path + "?Categories=NoneAssigned"));

            CategoriesAddDeleteBox.Visible = AdminMode;
            CategoriesAddButton.Visible = AdminMode;
            CategoriesDeleteButton.Visible = AdminMode;
        }

        private void RenderSort()
        {
            GalleryFeatures.Visible = true;
            GallerySort.Visible = true;
           // GalleryAdvanced.Visible = true;
            string AdditionalQueryString = "";
            if (Request.QueryString["Categories"] != null)
            {
                AdditionalQueryString = "&Categories=" + Server.UrlEncode(Request.QueryString["Categories"]);
            }
            if (Request.QueryString["Search"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&Search=" + Server.UrlEncode(Request.QueryString["Search"]);
            }
            if (Request.QueryString["PageSize"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&PageSize=" + Server.UrlEncode(Request.QueryString["PageSize"]);
            }
            if (_IncludeSort == true)
            {
                SortList.Items.Add(new ListItem("Newest First", Request.Path + "?Page=" + _CurrentPage + AdditionalQueryString + "&SortOrder=Newest"));
                SortList.Items.Add(new ListItem("Oldest First", Request.Path + "?Page=" + _CurrentPage + AdditionalQueryString + "&SortOrder=Oldest"));
            }
        }

        private void RenderPageSize()
        {
            GalleryFeatures.Visible = true;
            GalleryPageSize.Visible = true;
           // GalleryAdvanced.Visible = true;
            string AdditionalQueryString = "";
            if (Request.QueryString["Categories"] != null)
            {
                AdditionalQueryString = "&Categories=" + Server.UrlEncode(Request.QueryString["Categories"]);
            }
            if (Request.QueryString["Search"] != null)
            {
                AdditionalQueryString = "&Search=" + Server.UrlEncode(Request.QueryString["Search"]);
            }
            if (Request.QueryString["Search"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&Search=" + Server.UrlEncode(Request.QueryString["Search"]);
            }
            if (Request.QueryString["SortOrder"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&SortOrder=" + Server.UrlEncode(Request.QueryString["SortOrder"]);
            }
            PageSizeList.Items.Clear();
            PageSizeList.Items.Add(new ListItem("10 Per Page", Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=10"));
            PageSizeList.Items.Add(new ListItem("25 Per Page", Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=25"));
            PageSizeList.Items.Add(new ListItem("50 Per Page", Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=50"));
            PageSizeList.Items.Add(new ListItem("100 Per Page", Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=100"));
            PageSizeList.Items.Add(new ListItem("500 Per Page", Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=500"));
        }

        private void RenderGalleryUploads()
        {
            if (AdminMode)
            {
                //Show the admin menu
                GalleryAdminUpload.Visible = true;
                //Force it small, CSS won't work.
                UncatalogedFileUpload.Attributes.Add("size", "6");
                int FileCount = System.IO.Directory.GetFiles(Server.MapPath(_FilePath + "/Uploads")).Length;
                if (FileCount > 0)
                {
                    GalleryAdminCatalog.Visible = true;
                    if (FileCount == 1)
                    {
                        UncatalogedCountLabel.Text = "There is " + FileCount + " file waiting to be cataloged.";
                    }
                    else
                    {
                        UncatalogedCountLabel.Text = "There are " + FileCount + " files waiting to be cataloged.";
                    }
                }
                else
                {
                    GalleryAdminCatalog.Visible = false;
                }
            }
        }

        private void BindRepeater()
        {
            IEnumerable<Photo> results;
            List<Photo> ioqlist;
            if (Request.QueryString["PhotoID"] != null)
            {

                //Specific Search
                if (AdminMode == true)
                {
                    results =
                        from photo in GDC.Photos
                        where photo.PhotoID == int.Parse(Request.QueryString["PhotoID"].Replace("'", string.Empty)) && photo.PhotoOwner.Equals(_Owner)
                        select photo;
                    //SQLString = "SELECT Photos.* FROM Photos WHERE PhotoID = " + Request.QueryString["PhotoID"].Replace("'", "''") + "" + " AND PhotoOwner='" + _Owner + "'";
                }
                else
                {
                    results =
                         from photo in GDC.Photos
                         where photo.PhotoID == int.Parse(Request.QueryString["PhotoID"].Replace("'", string.Empty)) && photo.PhotoOwner.Equals(_Owner) && photo.PhotoVisible
                         select photo;
                    //SQLString = "SELECT Photos.* FROM Photos WHERE PhotoID = " + Request.QueryString["PhotoID"].Replace("'", "''") + " AND PhotoOwner='" + _Owner + "' AND PhotoVisible=True";
                }

                GallerySearchBox.Text = Request.QueryString["Search"];
            }

            else if (Request.QueryString["Search"] != null)
            {

                //Specific Search
                if (AdminMode == true)
                {
                    string searchrequest = Request.QueryString["Search"].Replace("'", string.Empty);
                    results =
                        (
                        from photo in GDC.Photos
                        join pcj in GDC.PhotoCategoryJoins on photo.PhotoID equals pcj.PhotoNumber
                        into joined
                        from j in joined.DefaultIfEmpty()
                        join cat in GDC.Categories on j.CategoryNumber equals cat.CategoryID
                        into catsjoined
                        from cj in catsjoined.DefaultIfEmpty()
                        where photo.PhotoOwner == _Owner && (photo.PhotoDescription.Contains(searchrequest) || cj.CategoryName.Contains(searchrequest))
                        select photo
                        ).Distinct();
                    //SQLString = "SELECT DISTINCT Photos.* FROM (Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) LEFT JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID 
                    //WHERE PhotoOwner='" + _Owner + "' AND PhotoDescription Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%' OR CategoryName Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%'";
                }
                else
                {
                    string searchrequest = Request.QueryString["Search"].Replace("'", string.Empty);
                    results =
                        (
                        from photo in GDC.Photos
                        join pcj in GDC.PhotoCategoryJoins on photo.PhotoID equals pcj.PhotoNumber
                        into joined
                        from j in joined.DefaultIfEmpty()
                        join cat in GDC.Categories on j.CategoryNumber equals cat.CategoryID
                        into catsjoined
                        from cj in catsjoined.DefaultIfEmpty()
                        where photo.PhotoVisible && photo.PhotoOwner == _Owner && (photo.PhotoDescription.Contains(searchrequest) || cj.CategoryName.Contains(searchrequest))
                        select photo
                        ).Distinct();
                    //SQLString = "SELECT DISTINCT Photos.* FROM (Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) LEFT JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID 
                    //WHERE PhotoOwner='" + _Owner + "' AND PhotoVisible = True AND (PhotoDescription Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%' OR CategoryName Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%') AND PhotoOwner='" + _Owner + "'";
                }

                GallerySearchBox.Text = Request.QueryString["Search"];
            }

            else if (Request.QueryString["Categories"] != null)
            {
                //Special 'No Categories' Search
                if (Request.QueryString["Categories"] == "NoneAssigned")
                {
                    results =
                         from photo in GDC.Photos
                         join pcj in GDC.PhotoCategoryJoins on photo.PhotoID equals pcj.PhotoNumber
                         into joined
                         from j in joined.DefaultIfEmpty()
                         where photo.PhotoOwner == _Owner && j.PhotoNumber == null
                         select photo;
                    //SQLString = "SELECT Photos.* FROM Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber WHERE PhotoOwner='" + _Owner + "' AND PhotoNumber Is Null";
                }
                else
                {
                    //Specific Search
                    string req = Request.QueryString["Categories"].Replace("'", string.Empty);
                    if (AdminMode == true)
                    {
                        results =
                            from photo in GDC.Photos
                            join pcj in GDC.PhotoCategoryJoins on photo.PhotoID equals pcj.PhotoNumber
                            into joined
                            from j in joined.DefaultIfEmpty()
                            join cat in GDC.Categories on j.CategoryNumber equals cat.CategoryID
                            into catsjoined
                            from cj in catsjoined.DefaultIfEmpty()
                            where photo.PhotoOwner == _Owner && cj.CategoryName.Contains(req)
                            select photo;
                        //SQLString = "SELECT Photos.* FROM (Photos INNER JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) INNER JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID 
                        //WHERE CategoryName='" + Request.QueryString["Categories"].Replace("'", "''") + "' AND PhotoOwner='" + _Owner + "'";                        
                    }
                    else
                    {
                        results =
                             from photo in GDC.Photos
                             join pcj in GDC.PhotoCategoryJoins on photo.PhotoID equals pcj.PhotoNumber
                             into joined
                             from j in joined.DefaultIfEmpty()
                             join cat in GDC.Categories on j.CategoryNumber equals cat.CategoryID
                             into catsjoined
                             from cj in catsjoined.DefaultIfEmpty()
                             where photo.PhotoOwner == _Owner && cj.CategoryName.Contains(req) && photo.PhotoVisible
                             select photo;
                        //SQLString = "SELECT Photos.* FROM (Photos INNER JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) INNER JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID 
                        //WHERE PhotoVisible = True AND (CategoryName='" + Request.QueryString["Categories"].Replace("'", "''") + "') AND PhotoOwner='" + _Owner + "'";
                    }

                }
            }

            else
            {

                if (AdminMode == true)
                {
                    //Normal
                    results =
                        from photo in GDC.Photos
                        where photo.PhotoOwner == _Owner
                        select photo;
                    //SQLString = "SELECT * FROM Photos WHERE PhotoOwner='" + _Owner + "'";
                }
                else
                {
                    results =
                        from photo in GDC.Photos
                        where photo.PhotoOwner == _Owner && photo.PhotoVisible
                        select photo;
                    //SQLString = "SELECT * FROM Photos WHERE PhotoVisible = True AND PhotoOwner='" + _Owner + "'";
                }

            }
            IOrderedEnumerable<Photo> ioq;
            if (_SortOrder == SortOrderType.AscendingDate)
            {
                IQueryable<Photo> iqresults = (IQueryable<Photo>)results;
                ioq =
                    iqresults.OrderBy
                    (
                        delegate(Photo p)
                        {
                            if (p.PhotoDate.HasValue)
                                return p.PhotoDate.Value.CompareTo(DateTime.MinValue);
                            else
                                return 0;
                        }
                    );
                //SQLString = SQLString + " ORDER BY PhotoDate";
            }
            else
            {
                IQueryable<Photo> iqresults = (IQueryable<Photo>)results;
                ioq =
                    iqresults.OrderBy
                    (
                        delegate(Photo p)
                        {
                            if (p.PhotoDate.HasValue)
                                return p.PhotoDate.Value.CompareTo(DateTime.MaxValue);
                            else
                                return 0;
                        }
                    );
                //SQLString = SQLString + " ORDER BY PhotoDate DESC";
            }
            ioqlist = ioq.ToList();
            ioq = null;

            try
            {
                _CurrentPage = int.Parse(ViewState["CurrentPage"].ToString());
                if (_CurrentPage == 0)
                    _CurrentPage = 1;
            }
            catch (Exception w)
            {
                _CurrentPage = 1;
                w = null;
            }

            int StartOffset = _PageSize * (_CurrentPage - 1);
            //Get it one row longer than necessary, so we can peek (and make the next button work right)

            bool ShowPrevious = _CurrentPage > 1;
            bool ShowNext = ioqlist.Count() > _PageSize;

            PreviousButton.Text = "Previous " + _PageSize + " Photos";
            NextButton.Text = "Next " + _PageSize + " Photos";

            GalleryNavigation.Visible = ShowPrevious | ShowNext;

            PreviousButton.Visible = ShowPrevious;
            NextButton.Visible = ShowNext;


            string AdditionalQueryString = "";

            if (Request.QueryString["Categories"] != null)
            {
                AdditionalQueryString = "&Categories=" + Server.UrlEncode(Request.QueryString["Categories"]);
            }

            if (Request.QueryString["Search"] != null)
            {
                AdditionalQueryString = "&Search=" + Server.UrlEncode(Request.QueryString["Search"]);
            }

            if (Request.QueryString["PageSize"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&PageSize=" + Server.UrlEncode(Request.QueryString["PageSize"]);
            }

            if (Request.QueryString["SortOrder"] != null)
            {
                AdditionalQueryString = AdditionalQueryString + "&SortOrder=" + Server.UrlEncode(Request.QueryString["SortOrder"]);
            }

            if (PreviousButton.Visible == true)
            {
                PreviousButton.NavigateUrl = Request.Path + "?Page=" + (_CurrentPage - 1).ToString() + AdditionalQueryString;
            }

            if (NextButton.Visible == true)
            {
                NextButton.NavigateUrl = Request.Path + "?Page=" + (_CurrentPage + 1).ToString() + AdditionalQueryString;
            }

            if (ioqlist.Count() > _PageSize)
            {
                //Trim off that last telltale record
                ioqlist.RemoveAt(_PageSize);
            }

            PhotosRepeater.Visible = true;
            PhotosRepeater.DataSource = ioqlist;
            PhotosRepeater.DataBind();
            if (PhotosRepeater.Items.Count == 0)
            {
                NoEntriesH2.Visible = true;
            }

        }

        protected void PhotosRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {

            string JavascriptStatement;
            string LinkTarget;

            if (AdminMode)
            {
                //Default values for people without javascript
                LinkTarget = this.ResolveUrl(_PopupPage) + "?Target=%PhotoPath%&amp;PhotoID=%PhotoID%&amp;PhotoWidth=640&amp;PhotoHeight=480";
                //Dymanic screen sizing
                JavascriptStatement = "PopWindowAdmin('%PopupPage%', %PhotoID%, '%PhotoPath%', %PhotoWidth%, %PhotoHeight%);return false;";
            }
            else
            {
                //Default values for people without javascript
                LinkTarget = this.ResolveUrl(_PopupPage) + "?Target=%PhotoPath%&amp;PhotoWidth=640&amp;PhotoHeight=480&amp;PhotoDate=%PhotoDate%&amp;PhotoDescription=%PhotoDescription%";
                //Dymanic screen sizing
                JavascriptStatement = "PopWindow('%PopupPage%', %PhotoID%, '%PhotoPath%', %PhotoWidth%, %PhotoHeight%, '%PhotoDate%', '%PhotoDescription%');return false;";
            }

            //string RelativePath = this.ResolveUrl(_FilePath);
            string RelativePath = HelperUtilities.RelativeAppRoot + "ThonHttpHandlers/Image.ashx?picturepath=" + _FilePath;
            Photo photoitem = (Photo)e.Item.DataItem;
            string PhotoPath = this.ResolveUrl(_FilePath) + photoitem.PhotoID.ToString() + ".jpg";
            string PhotoThumbPath = RelativePath + "Thumbs/" + photoitem.PhotoID.ToString() + ".jpg";

            JavascriptStatement = JavascriptStatement.Replace("%PopupPage%", this.ResolveUrl(_PopupPage));
            JavascriptStatement = JavascriptStatement.Replace("%PhotoID%", photoitem.PhotoID.ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoPath%", PhotoPath);
            JavascriptStatement = JavascriptStatement.Replace("%PhotoWidth%", photoitem.PhotoWidth.Value.ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoHeight%", photoitem.PhotoHeight.Value.ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoDate%", JScriptEncode(Server.UrlEncode(photoitem.PhotoDate.Value.ToShortDateString())));
            JavascriptStatement = JavascriptStatement.Replace("%PhotoDescription%", JScriptEncode(Server.UrlEncode(photoitem.PhotoDescription)));

            LinkTarget = LinkTarget.Replace("%PhotoID%", photoitem.PhotoID.ToString());
            LinkTarget = LinkTarget.Replace("%PhotoPath%", PhotoPath);
            LinkTarget = LinkTarget.Replace("%PhotoDate%", Server.UrlEncode(photoitem.PhotoDate.Value.ToShortDateString()));
            LinkTarget = LinkTarget.Replace("%PhotoDescription%", Server.UrlEncode(photoitem.PhotoDescription));

            //Assign the CSS class, maybe random
            string[] ClassArray = _ItemCSSClasses.Split(',');

            System.Random MyRandom = new System.Random(DateTime.Now.Millisecond + int.Parse(photoitem.PhotoDate.Value.ToString("hhmmss")) + photoitem.PhotoID);
            int ArrayOffset = MyRandom.Next(1, ClassArray.Length-1);

            string PhotoItemClass = ClassArray[ArrayOffset].Trim();

            if (!photoitem.PhotoVisible)
            {
                //Admin mode, add the hidden class to the other class. The first is the hidden one.
                PhotoItemClass = PhotoItemClass + " " + ClassArray[0].Trim();
            }

            Literal MyLiteral = (Literal)e.Item.FindControl("PhotoItem");

            //The newlines are important; they make IE7 not break...
            MyLiteral.Text = "<div class=\"" + PhotoItemClass + "\">" + Environment.NewLine + "<a onclick=\"" + JavascriptStatement + "\" href=\"" + LinkTarget + "\"><img src=\"" + PhotoThumbPath + "\" alt=\"" + Server.UrlEncode(photoitem.PhotoDescription) + "\" />" + "</a>" + Environment.NewLine + "</div>";

        }

        protected void GallerySearchBox_TextChanged(object sender, System.EventArgs e)
        {
            Response.Redirect(Request.Path + "?Search=" + Server.UrlEncode(GallerySearchBox.Text));
        }

        protected void CategoriesAddButton_Click(object sender, System.EventArgs e)
        {
            //Add new category
            if (CategoriesAddDeleteBox.Text.Length > 0)
            {
                Category cat = new Category();
                cat.CategoryName = CategoriesAddDeleteBox.Text.Replace("'", string.Empty);
                cat.CategoryOwner = _Owner;                
                GDC.Categories.InsertOnSubmit(cat);
                GDC.SubmitChanges();
                //string SQLString = "INSERT INTO Categories ( CategoryName, CategoryOwner ) SELECT '" + CategoriesAddDeleteBox.Text.Replace("'", "''") + "','" + _Owner + "'";
                CategoriesAddDeleteBox.Text = "";
                Cache.Remove("GalleryControl" + _Owner + "Categories");
                RenderCategories();
            }

        }

        protected void CategoriesDeleteButton_Click(object sender, System.EventArgs e)
        {
            //Delete typed category
            if (CategoriesAddDeleteBox.Text.Length > 0)
            {
                string SQLString = "DELETE FROM Categories WHERE CategoryOwner='" + _Owner + "' AND CategoryName='" + CategoriesAddDeleteBox.Text.Replace("'", "''") + "'";
                Category todelete = (
                                    from cat in GDC.Categories
                                    where cat.CategoryOwner.Equals(_Owner) && cat.CategoryName.Equals(CategoriesAddDeleteBox.Text.Replace("'", string.Empty))
                                    select cat
                                    ).First();
                GDC.Categories.DeleteOnSubmit(todelete);
                GDC.SubmitChanges();
                CategoriesAddDeleteBox.Text = "";
                Cache.Remove("GalleryControl" + _Owner + "Categories");
                RenderCategories();
            }

        }

        protected void UncatalogedCatalogButton_Click(object sender, System.EventArgs e)
        {
            //Move and catalog anything in the staging area
            int PhotoID;
            DateTime PhotoDate;
            int PhotoWidth;
            int PhotoHeight;
            int PhotoRes;

            foreach (string OldFileName in System.IO.Directory.GetFiles(Server.MapPath(_FilePath + "/Uploads")))
            {

                System.Drawing.Image OriginalImg;
                OriginalImg = System.Drawing.Image.FromFile(OldFileName);
                try
                {
                    //Get the date time from the EXIF
                    string DateString = System.Text.ASCIIEncoding.ASCII.GetString(OriginalImg.GetPropertyItem(36867).Value);
                    string SecondHalf = DateString.Substring(DateString.IndexOf(" "), (DateString.Length - DateString.IndexOf(" ")));
                    string FirstHalf = DateString.Substring(0, 10);
                    FirstHalf = FirstHalf.Replace(":", "-");
                    PhotoDate = DateTime.Parse(FirstHalf + SecondHalf);
                }
                catch
                {
                    //Damn, We couldn't retreive it
                    PhotoDate = DateTime.Now;
                }

                //ExifDTOrig
                PhotoHeight = OriginalImg.Height;
                PhotoWidth = OriginalImg.Width;
                PhotoRes = (int)OriginalImg.HorizontalResolution;

                //We need to figure out the dimensions for the thumbnail, with a defined max

                int NewWidth;
                int NewHeight;
                //Preseves the aspect ratio
                if (PhotoWidth > PhotoHeight)
                {
                    NewWidth = _ThumbnailSize;
                    NewHeight = (int)Math.Floor((double)(((float)PhotoHeight / (float)PhotoWidth) * _ThumbnailSize));
                }

                else
                {
                    NewHeight = _ThumbnailSize;
                    NewWidth = (int)Math.Floor((double)(((float)PhotoWidth / (float)PhotoHeight) * _ThumbnailSize));
                }

                System.Drawing.Image NewImg;
                NewImg = GenerateThumbnail(OriginalImg, NewWidth, NewHeight);
                OriginalImg.Dispose();
                //SQLString = "INSERT INTO Photos ( PhotoDate, PhotoWidth, PhotoHeight, PhotoResolution, PhotoOwner ) SELECT #" + AmericanizeDateForQuery(PhotoDate) + "#, " + PhotoWidth + ", " + PhotoHeight + ", " + PhotoRes + ", '" + _Owner + "'";
                Photo newphoto = new Photo();
                newphoto.PhotoDate = PhotoDate;
                newphoto.PhotoWidth = (short)PhotoWidth;
                newphoto.PhotoHeight = (short)PhotoHeight;
                newphoto.PhotoResolution = (short)PhotoRes;
                newphoto.PhotoOwner = _Owner;
                GDC.Photos.InsertOnSubmit(newphoto);
                GDC.SubmitChanges();

                PhotoID =
                    (
                    from photo in GDC.Photos
                    where photo.PhotoOwner.Equals(_Owner)
                    select photo.PhotoID
                    ).Max();

                //SQLString = "SELECT Max(PhotoID) AS MaxOfPhotoID FROM Photos WHERE PhotoOwner='" + _Owner + "'";
                string NewFileName = Server.MapPath(_FilePath + "/") + PhotoID + ".jpg";
                string ThumbFileName = Server.MapPath(_FilePath + "/Thumbs/") + PhotoID + ".jpg";
                NewImg.Save(ThumbFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                NewImg.Dispose();
                System.IO.File.Move(OldFileName, NewFileName);
            }
            BindRepeater();
            GalleryAdminCatalog.Visible = true;
        }

        private System.Drawing.Image GenerateThumbnail(System.Drawing.Image Original, int Width, int Height)
        {
            System.Drawing.Bitmap tn = new System.Drawing.Bitmap(Width, Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tn);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(Original, new System.Drawing.Rectangle(0, 0, tn.Width, tn.Height), 0, 0, Original.Width, Original.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            return (System.Drawing.Image)tn;
        }

        protected void UncatalogedUploadButton_Click(object sender, System.EventArgs e)
        {
            if (UncatalogedFileUpload.HasFile)
            {
                string NewFileName = UncatalogedFileUpload.PostedFile.FileName.Substring(UncatalogedFileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                while (System.IO.File.Exists(Server.MapPath(_FilePath + "/Uploads/") + NewFileName))
                {
                    NewFileName = "Another " + NewFileName;
                }
                UncatalogedFileUpload.SaveAs(Server.MapPath(_FilePath + "/Uploads/") + NewFileName);
            }
            RenderGalleryUploads();
        }

        private string JScriptEncode(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            return s.Replace("'", "\\'").Replace("\"", "\\\"").Replace("/", "\\/").Replace("-", "\\-");
        }
    }
}
