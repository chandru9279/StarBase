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

namespace Thon.Gallery
{
    public partial class GalleryControlAscx : System.Web.UI.UserControl
    {
        private GalleryContext GDC;
        private const string DataTableName = "Gallery";
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
        private string _FilePath;// = "~/App_Data/Gallery"
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        #endregion

        #region Owner
        private string _Owner = "Any";
        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        #endregion

        #region IncludeCategories
        private bool _IncludeCategories = false;
        public bool IncludeCategories
        {
            get { return _IncludeCategories; }
            set { _IncludeCategories = value; }
        }
        #endregion

        #region IncludeSearch
        private bool _IncludeSearch = false;
        public bool IncludeSearch
        {
            get { return _IncludeSearch; }
            set { _IncludeSearch = value; }
        }
        #endregion

        #region IncludeSort
        private bool _IncludeSort = false;
        public bool IncludeSort
        {
            get { return _IncludeSort; }
            set { _IncludeSort = value; }
        }
        #endregion

        #region IncludePageSize
        private bool _IncludePageSize = false;
        public bool IncludePageSize
        {
            get { return _IncludePageSize; }
            set { _IncludePageSize = value; }
        }
        #endregion

        #region ItemCSSClasses
        private string _ItemCSSClasses = "PhotoItem";
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
                GalleryAdvanced.Visible = true;
                CategoriesDeleteButton.OnClientClick = "return alert('Are you sure you want to delete the category?')";
            }

            if (Request.QueryString["Page"] != null)
            {
                _CurrentPage = int.Parse(Request.QueryString["Page"].ToString());
            }
            if (Request.QueryString["PageSize"] != null)
            {
                _PageSize = int.Parse(Request.QueryString["PageSize"].ToString());
            }
            if (Request.QueryString["SortOrder"] != null)
            {
                if (Request.QueryString["SortOrder"] == "Oldest")
                {
                    _SortOrder = SortOrderType.AscendingDate;
                }
                else
                {
                    _SortOrder = SortOrderType.DescendingDate;
                }
            }

            RenderGalleryUploads();

            if (_IncludeCategories == true)
            {
                RenderCategories();
            }

            if (_IncludeSearch == true)
            {
                GallerySearch.Visible = true;
            }

            if (_IncludeSort == true)
            {
                RenderSort();
            }

            if (_IncludePageSize == true)
            {
                RenderPageSize();
            }

            if (!Parent.Page.IsPostBack)
            {
                ViewState["CurrentPage"] = _CurrentPage;
            }

            BindRepeater();

        }

        private string AmericanizeDateForQuery(DateTime TargetDate)
        {
            string ReturnString;
            if (TargetDate.Date == TargetDate)
            {
                ReturnString = TargetDate.ToString("MM\\/dd\\/yyyy");
            }
            else
            {
                ReturnString = TargetDate.ToString("MM\\/dd\\/yyyy hh\\:mm\\:ss tt");
            }
            return ReturnString;
        }

        private void RenderCategories()
        {
            SortedList CategoriesSortedList = null;
            CategoriesSortedList = (SortedList)Cache["GalleryControl" + _Owner + "Categories"];
            if (CategoriesSortedList == null)
            {
                CategoriesSortedList = new SortedList();

                var results = 
                    from cat in GDC.Categories
                    where cat.CategoryOwner.Equals(_Owner)
                    orderby cat.CategoryName 
                    select cat.CategoryName;

                results.ToList();

                Hashtable TagsCountHash = new Hashtable();


                foreach(var re)


                while (myOleDbDataReader.Read())
                {


                    string LinkString = Request.Path + "?Categories=" + Server.UrlEncode(myOleDbDataReader["CategoryName"].ToString());

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

                    CategoriesSortedList.Add(myOleDbDataReader["CategoryName"], LinkString + AdditionalQueryString);

                    Cache.Insert("GalleryControl" + _Owner + "Categories", CategoriesSortedList, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

                }

                myOleDbConnection.Close();
                myOleDbConnection = null;

            }

            GalleryFeatures.Visible = true;
            GalleryCategories.Visible = true;
            GalleryAdvanced.Visible = true;
            CategoriesList.Visible = true;

            CategoriesList.DataSource = CategoriesSortedList;
            CategoriesList.DataTextField = "Key";
            CategoriesList.DataValueField = "Value";
            CategoriesList.DataBind();

            CategoriesList.Items.Insert(0, new ListItem(GetLocalResourceObject("CodeEverything").ToString(), Request.Path));

            if (AdminMode == true)
            {
                CategoriesList.Items.Insert(1, new ListItem(GetLocalResourceObject("CodeNoneAssigned").ToString(), Request.Path + "?Categories=NoneAssigned"));
            }

            CategoriesAddDeleteBox.Visible = AdminMode;
            CategoriesAddButton.Visible = AdminMode;
            CategoriesDeleteButton.Visible = AdminMode;

        }

        private void RenderSort()
        {
            GalleryFeatures.Visible = true;
            GallerySort.Visible = true;
            GalleryAdvanced.Visible = true;
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
                SortList.Items.Add(new ListItem(GetLocalResourceObject("CodeNewestFirst").ToString(), Request.Path + "?Page=" + _CurrentPage + AdditionalQueryString + "&SortOrder=Newest"));
                SortList.Items.Add(new ListItem(GetLocalResourceObject("CodeOldestFirst").ToString(), Request.Path + "?Page=" + _CurrentPage + AdditionalQueryString + "&SortOrder=Oldest"));
            }
        }

        private void RenderPageSize()
        {
            GalleryFeatures.Visible = true;
            GalleryPageSize.Visible = true;
            GalleryAdvanced.Visible = true;
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
            PageSizeList.Items.Add(new ListItem("10 " + GetLocalResourceObject("CodePerPage").ToString(), Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=10"));
            PageSizeList.Items.Add(new ListItem("25 " + GetLocalResourceObject("CodePerPage").ToString(), Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=25"));
            PageSizeList.Items.Add(new ListItem("50 " + GetLocalResourceObject("CodePerPage").ToString(), Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=50"));
            PageSizeList.Items.Add(new ListItem("100 " + GetLocalResourceObject("CodePerPage").ToString(), Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=100"));
            PageSizeList.Items.Add(new ListItem("500 " + GetLocalResourceObject("CodePerPage").ToString(), Request.Path + "?Page=1" + AdditionalQueryString + "&PageSize=500"));
        }

        private void RenderGalleryUploads()
        {
            if (AdminMode == true)
            {
                //Show the admin menu
                GalleryAdminUpload.Visible = true;
                //Force it small, CSS won't work.
                UncatalogedFileUpload.Attributes.Add("size", "1");
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

            System.Data.DataSet PostsDataset = new System.Data.DataSet();

            string SQLString;

            if (Request.QueryString["PhotoID"] != null)
            {

                //Specific Search
                if (AdminMode == true)
                {
                    //All
                    SQLString = "SELECT Photos.* FROM Photos WHERE PhotoID = " + Request.QueryString["PhotoID"].Replace("'", "''") + "" + " AND PhotoOwner='" + _Owner + "'";
                }
                else
                {
                    SQLString = "SELECT Photos.* FROM Photos WHERE PhotoID = " + Request.QueryString["PhotoID"].Replace("'", "''") + " AND PhotoOwner='" + _Owner + "' AND PhotoVisible=True";
                }

                GallerySearchBox.Text = Request.QueryString["Search"];
            }

            else if (Request.QueryString["Search"] != null)
            {

                //Specific Search
                if (AdminMode == true)
                {
                    //All
                    SQLString = "SELECT DISTINCT Photos.* FROM (Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) LEFT JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID WHERE PhotoOwner='" + _Owner + "' AND PhotoDescription Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%' OR CategoryName Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%'";
                }
                else
                {
                    SQLString = "SELECT DISTINCT Photos.* FROM (Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) LEFT JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID WHERE PhotoOwner='" + _Owner + "' AND PhotoVisible = True AND (PhotoDescription Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%' OR CategoryName Like '%" + Request.QueryString["Search"].Replace("'", "''") + "%') AND PhotoOwner='" + _Owner + "'";
                }

                GallerySearchBox.Text = Request.QueryString["Search"];
            }

            else if (Request.QueryString["Categories"] != null)
            {

                //Special 'No Categories' Search
                if (Request.QueryString["Categories"] == "NoneAssigned")
                {
                    SQLString = "SELECT Photos.* FROM Photos LEFT JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber WHERE PhotoOwner='" + _Owner + "' AND PhotoNumber Is Null";
                }
                else
                {
                    //Specific Search
                    if (AdminMode == true)
                    {
                        SQLString = "SELECT Photos.* FROM (Photos INNER JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) INNER JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID WHERE CategoryName='" + Request.QueryString["Categories"].Replace("'", "''") + "' AND PhotoOwner='" + _Owner + "'";
                    }
                    else
                    {
                        SQLString = "SELECT Photos.* FROM (Photos INNER JOIN PhotosJoinCategories ON Photos.PhotoID = PhotosJoinCategories.PhotoNumber) INNER JOIN Categories ON PhotosJoinCategories.CategoryNumber = Categories.CategoryID WHERE PhotoVisible = True AND (CategoryName='" + Request.QueryString["Categories"].Replace("'", "''") + "') AND PhotoOwner='" + _Owner + "'";
                    }

                }
            }

            else
            {

                if (AdminMode == true)
                {
                    //Normal
                    SQLString = "SELECT * FROM Photos WHERE PhotoOwner='" + _Owner + "'";
                }
                else
                {
                    SQLString = "SELECT * FROM Photos WHERE PhotoVisible = True AND PhotoOwner='" + _Owner + "'";
                }

            }

            if (_SortOrder == SortOrderType.AscendingDate)
            {
                SQLString = SQLString + " ORDER BY PhotoDate";
            }
            else
            {
                SQLString = SQLString + " ORDER BY PhotoDate DESC";
            }

            System.Data.OleDb.OleDbConnection MyOleDbConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
            MyOleDbConnection.Open();

            System.Data.OleDb.OleDbDataAdapter myOleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(SQLString, MyOleDbConnection);
            _CurrentPage = int.Parse(ViewState["CurrentPage"].ToString());

            if (_CurrentPage == 0)
            {
                _CurrentPage = 1;
            }

            int StartOffset = _PageSize * (_CurrentPage - 1);
            //Get it one row longer than necessary, so we can peek (and make the next button work right)
            myOleDbDataAdapter.Fill(PostsDataset, StartOffset, _PageSize + 1, DataTableName);

            bool ShowPrevious = _CurrentPage > 1;
            bool ShowNext = PostsDataset.Tables[DataTableName].Rows.Count > _PageSize;

            PreviousButton.Text = GetLocalResourceObject("CodePrevious").ToString() + " " + _PageSize + " " + GetLocalResourceObject("CodePhotos").ToString();
            NextButton.Text = GetLocalResourceObject("CodeNext").ToString() + " " + _PageSize + " " + GetLocalResourceObject("CodePhotos").ToString();

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

            if (PostsDataset.Tables[DataTableName].Rows.Count > _PageSize)
            {
                //Trim off that last telltale record
                PostsDataset.Tables[DataTableName].Rows.RemoveAt(_PageSize);
            }

            PhotosRepeater.Visible = true;
            PhotosRepeater.DataSource = PostsDataset.Tables[DataTableName];
            PhotosRepeater.DataBind();

            myOleDbDataAdapter = null;
            PostsDataset = null;
            MyOleDbConnection.Close();
            MyOleDbConnection = null;

            if (PhotosRepeater.Items.Count == 0)
            {
                NoEntriesH2.Visible = true;
            }

        }

        protected void PhotosRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {

            string JavascriptStatement;
            string LinkTarget;

            if (AdminMode == true)
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

            string RelativePath = this.ResolveUrl(_FilePath);
            if (RelativePath.EndsWith("/") == false)
            {
                RelativePath = RelativePath + "/";
            }


            string PhotoPath = RelativePath + ((DataRow)e.Item.DataItem)["PhotoID"] + ".jpg";
            string PhotoThumbPath = RelativePath + "Thumbs/" + ((DataRow)e.Item.DataItem)["PhotoID"] + ".jpg";

            JavascriptStatement = JavascriptStatement.Replace("%PopupPage%", this.ResolveUrl(_PopupPage));
            JavascriptStatement = JavascriptStatement.Replace("%PhotoID%", ((DataRow)e.Item.DataItem)["PhotoID"].ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoPath%", PhotoPath);
            JavascriptStatement = JavascriptStatement.Replace("%PhotoWidth%", ((DataRow)e.Item.DataItem)["PhotoWidth"].ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoHeight%", ((DataRow)e.Item.DataItem)["PhotoHeight"].ToString());
            JavascriptStatement = JavascriptStatement.Replace("%PhotoDate%", JScriptEncode(Server.UrlEncode((System.DateTime.Parse(((DataRow)e.Item.DataItem)["PhotoDate"].ToString())).ToShortDateString())));
            JavascriptStatement = JavascriptStatement.Replace("%PhotoDescription%", JScriptEncode(Server.UrlEncode((FixNull(((DataRow)e.Item.DataItem)["PhotoDescription"])))));

            LinkTarget = LinkTarget.Replace("%PhotoID%", ((DataRow)e.Item.DataItem)["PhotoID"].ToString());
            LinkTarget = LinkTarget.Replace("%PhotoPath%", PhotoPath);
            LinkTarget = LinkTarget.Replace("%PhotoDate%", Server.UrlEncode(DateTime.Parse(((DataRow)e.Item.DataItem)["PhotoDate"].ToString()).ToShortDateString()));
            LinkTarget = LinkTarget.Replace("%PhotoDescription%", Server.UrlEncode(FixNull(((DataRow)e.Item.DataItem)["PhotoDescription"].ToString())));

            //Assign the CSS class, maybe random
            string[] ClassArray = _ItemCSSClasses.Split(',');

            System.Random MyRandom = new System.Random(int.Parse(DateTime.Now.Millisecond + (System.DateTime.Parse(((DataRow)e.Item.DataItem)["PhotoDate"].ToString())).ToString("hhmmss") + ((DataRow)e.Item.DataItem)["PhotoID"].ToString()));
            int ArrayOffset = MyRandom.Next(1, ClassArray.Length);

            string PhotoItemClass = ClassArray[ArrayOffset].Trim();

            if (bool.Parse(((DataRow)e.Item.DataItem)["PhotoVisible"].ToString()) == false)
            {
                //Admin mode, add the hidden class to the other class. The first is the hidden one.
                PhotoItemClass = PhotoItemClass + " " + ClassArray[0].Trim();
            }

            Literal MyLiteral = (Literal)e.Item.FindControl("PhotoItem");

            //The newlines are important; they make IE7 not break...
            MyLiteral.Text = "<div class=\"" + PhotoItemClass + "\">" + Environment.NewLine + "<a onclick=\"" + JavascriptStatement + "\" href=\"" + LinkTarget + "\"><img src=\"" + PhotoThumbPath + "\" alt=\"" + Server.UrlEncode(FixNull(((DataRow)e.Item.DataItem)["PhotoDescription"])) + "\" />" + "</a>" + Environment.NewLine + "</div>";


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

                System.Data.OleDb.OleDbConnection myOleDbConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                myOleDbConnection.Open();
                string SQLString = "INSERT INTO Categories ( CategoryName, CategoryOwner ) SELECT '" + CategoriesAddDeleteBox.Text.Replace("'", "''") + "','" + _Owner + "'";
                //Create a command object
                System.Data.OleDb.OleDbCommand myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
                myOleDbCommand.ExecuteNonQuery();
                myOleDbConnection.Close();

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

                System.Data.OleDb.OleDbConnection myOleDbConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                myOleDbConnection.Open();
                string SQLString = "DELETE FROM Categories WHERE CategoryOwner='" + _Owner + "' AND CategoryName='" + CategoriesAddDeleteBox.Text.Replace("'", "''") + "'";
                //Create a command object
                System.Data.OleDb.OleDbCommand myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
                myOleDbCommand.ExecuteNonQuery();
                myOleDbConnection.Close();

                CategoriesAddDeleteBox.Text = "";

                Cache.Remove("GalleryControl" + _Owner + "Categories");

                RenderCategories();

            }

        }

        protected void UncatalogedCatalogButton_Click(object sender, System.EventArgs e)
        {

            //Move and catalog anything in the staging area

            int PhotoID;
            System.DateTime PhotoDate;
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

                    //Screw it, we couldn't retreive it
                    PhotoDate = DateTime.Now.AddHours(TimeZoneOffset);

                }

                //ExifDTOrig
                PhotoHeight = OriginalImg.Height;
                PhotoWidth = OriginalImg.Width;
                PhotoRes = (int)OriginalImg.HorizontalResolution;

                //We need to figure out the dimensions for the thumbnail, with a defined max

                int NewWidth;
                int NewHeight;

                if (PhotoWidth > PhotoHeight)
                {
                    NewWidth = _ThumbnailSize;
                    NewHeight = (PhotoHeight / PhotoWidth) * _ThumbnailSize;
                }

                else
                {
                    NewHeight = _ThumbnailSize;
                    NewWidth = (PhotoWidth / PhotoHeight) * _ThumbnailSize;
                }

                System.Drawing.Image NewImg;
                NewImg = GenerateThumbnail(OriginalImg, NewWidth, NewHeight);
                OriginalImg.Dispose();

                System.Data.OleDb.OleDbConnection myOleDbConnection;
                myOleDbConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                myOleDbConnection.Open();

                string SQLString;

                SQLString = "INSERT INTO Photos ( PhotoDate, PhotoWidth, PhotoHeight, PhotoResolution, PhotoOwner ) SELECT #" + AmericanizeDateForQuery(PhotoDate) + "#, " + PhotoWidth + ", " + PhotoHeight + ", " + PhotoRes + ", '" + _Owner + "'";

                System.Data.OleDb.OleDbCommand myOleDbCommand;
                myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);
                myOleDbCommand.ExecuteNonQuery();

                //Specify the SQL string
                SQLString = "SELECT Max(PhotoID) AS MaxOfPhotoID FROM Photos WHERE PhotoOwner='" + _Owner + "'";

                //Create a command object
                myOleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, myOleDbConnection);

                //Get a datareader
                System.Data.OleDb.OleDbDataReader myOleDbDataReader;
                myOleDbDataReader = myOleDbCommand.ExecuteReader();
                myOleDbDataReader.Read();
                PhotoID = int.Parse(myOleDbDataReader["MaxOfPhotoID"].ToString());
                myOleDbDataReader.Close();
                myOleDbConnection.Close();

                if (System.IO.Directory.Exists(Server.MapPath(_FilePath + "/Thumbs")) == false)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(_FilePath + "/Thumbs"));
                }

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

        protected void UncatalogedUploadButton_Click(object sender, System.EventArgs e)
        {

            if (UncatalogedFileUpload.HasFile == true)
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
            return s.Replace("'", "\\'").Replace("\"", "\\\"").Replace("/", "\\/").Replace("-", "\\-");
        }


    }
}
