using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Shelf.Support.Searching;
using Shelf.Web.UserControls;
using Shelf.Support;
using Shelf.Support.Logging;
using System.Text;

namespace Shelf.Web
{
    public partial class SearchAspx : System.Web.UI.Page
    {
        #region Protected Fields: Protected(not private) coz the subclass Aspx uses all of these
        /// <summary>Displayed in HTML - count of words IN CATALOG (not results)</summary>
        protected int _WordCount;
        /// <summary>Displayed in HTML - error message IF an error occurred</summary>
        protected string _ErrorMessage = String.Empty;
        /// <summary>Get from Cache</summary>
        protected Catalog _Catalog = null;
        /// <summary>The Search term entered</summary>
        protected string _SearchTerm = String.Empty;
        /// <summary>Datasource to bind the results collection to, for paged display</summary>
        protected PagedDataSource _PagedResults = new PagedDataSource();
        /// <summary>Display string: time the search too</summary>
        protected string _DisplayTime;
        /// <summary>Display string: matches (links and number of)</summary>
        protected string _Matches = "";
        /// <summary>Display string: Number of pages that match the query</summary>
        protected string _NumberOfMatches;
        #endregion

        int MaxResultsPerPage
        {
            get
            {
                return Preferences.ResultsPerPage;
            }
        }

        string SearchQuery
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString[Preferences.QuerystringParameterName]))
                {
                    return string.Empty;
                }
                else
                {
                    return Request.QueryString[Preferences.QuerystringParameterName].ToString().Trim(' ');
                }
            }
        }

        /// <summary>
        /// Method that invokes the search engine to get resuts
        /// </summary>
        SortedList GetSearchResults(SearchEngine se)
        {
            return se.GetResults(this.SearchQuery, _Catalog);
        }

        protected void Page_Load()
        {
            bool getCatalog = false;
            try
            {   // see if there is a catalog object in the cache
                _Catalog = (Catalog)Cache[Preferences.CatalogCacheKey];
                _WordCount = _Catalog.Length; // if so, get the _WordCount
            }
            catch (Exception ex)
            {
                // If not, we'll need to load_from_file or build the catalog again.
                Logger.PerformanceLog(this, "Catalog object unavailable : Loadind from file!" + ex.ToString());
                _Catalog = null; // in case
            }

            if (null == _Catalog)
                getCatalog = true;
            else if (_Catalog.Length == 0)
                getCatalog = true;

            if (getCatalog)
            {
                // No catalog 'in memory', so let's look for one
                // First, for a serialized version on disk	
                _Catalog = Catalog.Load();	// returns null if not found

                // Still no Catalog, so we have to start building a new one
                if (null == _Catalog)
                {
                    Logger.PerformanceLog(this, "Catalog object unavailable & serialized file missing : Building new Catalog!");
                    Response.Redirect("Crawling.aspx", true);
                }
                else
                {	// Yep, there was a serialized catalog file
                    // Don't forget to add to cache for next time (the Spider does this too)
                    Cache[Preferences.CatalogCacheKey] = _Catalog;
                    _WordCount = _Catalog.Length; // if so, get the _WordCount
                    Logger.PerformanceLog(this, "Deserialized catalog and put in Cache");
                }
            }


            ucSearchPanelHeader.WordCount = _WordCount;
            ucSearchPanelFooter.WordCount = _WordCount;


            if (this.SearchQuery == "")
            {
                ucSearchPanelFooter.Visible = false;
                ucSearchPanelFooter.IsFooter = true;
                ucSearchPanelHeader.IsSearchResultsPage = false;
            }
            else
            {
                SearchEngine se = new SearchEngine();
                SortedList output = GetSearchResults(se); // which'll do se.GetResults(this.SearchQuery, _Catalog);

                _NumberOfMatches = output.Count.ToString();
                if (output.Count > 0)
                {
                    _PagedResults.DataSource = output.GetValueList();
                    _PagedResults.AllowPaging = true;
                    _PagedResults.PageSize = MaxResultsPerPage; //;Preferences.ResultsPerPage; //10;
                    _PagedResults.CurrentPageIndex = Request.QueryString["page"] == null ? 0 : Convert.ToInt32(Request.QueryString["page"]) - 1;

                    _Matches = se.SearchQueryMatchHtml;
                    _DisplayTime = se.DisplayTime;

                    SearchResults.DataSource = _PagedResults;
                    SearchResults.DataBind();
                }
                else
                {
                    lblNoSearchResults.Visible = true;
                }
                // Set the display info in the top & bottom user controls
                ucSearchPanelHeader.Word = ucSearchPanelFooter.Word = SearchQuery;
                ucSearchPanelFooter.Visible = true;
                ucSearchPanelFooter.IsFooter = true;
                ucSearchPanelHeader.IsSearchResultsPage = true;
            }

        } // Page_Load

        protected string CreatePageUrl(string searchFor, int pageNumber)
        {
            return "Search.aspx?" + Preferences.QuerystringParameterName + "=" + this.SearchQuery + "&page=" + pageNumber;
        }

        /// <summary>
        /// This method implements a 'rolling window' page-number index
        /// for the underlying PagedDataSource
        /// </summary>
        /// <remarks>
        /// http://www.sitepoint.com/article/asp-nets-pageddatasource
        /// http://www.uberasp.net/ArticlePrint.aspx?id=29
        /// 
        /// http://www.codeproject.com/KB/aspnet/Mastering_DataBinding.aspx
        /// </remarks>
        public string CreatePagerLinks(PagedDataSource objPds, string BaseUrl)
        {
            StringBuilder sbPager = new StringBuilder();
            StringBuilder sbPager1 = new StringBuilder();

            sbPager1.Append("<td><font color='black'>Yi</font><font color='green'>pp</font></td>");

            if (objPds.IsFirstPage)
            {	// lower link is blank
                sbPager.Append("<td></td>");
            }
            else
            {	// first+prev link
                sbPager.Append("<td align=\"right\">");
                // first page link
                sbPager.Append("<a href=\"");
                sbPager.Append(CreatePageUrl(BaseUrl, 1));
                sbPager.Append("\" alt=\"First Page\" title=\"First Page\">|&lt;</a>&nbsp;");
                if (objPds.CurrentPageIndex != 1)
                {
                    // previous page link CurrentPageIndex is always one less than whats on the query string,
                    // coz index is zero-based, while page numbers start with 1
                    sbPager.Append("<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, objPds.CurrentPageIndex));
                    sbPager.Append("\" alt=\"Previous Page\" title=\"Previous Page\">&laquo;</a>&nbsp;");
                }
                sbPager.Append("</td>");
            }
            // calc low and high limits for numeric links
            int intLow = objPds.CurrentPageIndex - 1;
            int intHigh = objPds.CurrentPageIndex + 3;
            if (intLow < 1) intLow = 1;
            if (intHigh > objPds.PageCount) intHigh = objPds.PageCount;
            if (intHigh - intLow < 5) while ((intHigh < intLow + 4) && intHigh < objPds.PageCount) intHigh++;
            if (intHigh - intLow < 5) while ((intLow > intHigh - 4) && intLow > 1) intLow--;
            for (int x = intLow; x < intHigh + 1; x++)
            {
                // numeric links
                if (x == objPds.CurrentPageIndex + 1)
                {
                    sbPager1.Append("<td width='10' align='center'><font color='red'><b>e</b></td>");
                    sbPager.Append("<td>" + x.ToString() + "</td>");
                }
                else
                {
                    sbPager1.Append("<td width='10' align='center'><font color='red'><b>e</b></td>");
                    sbPager.Append("<td>");
                    sbPager.Append("<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, x));
                    sbPager.Append("\" alt=\"Go to page\" title=\"Go to page\">");
                    sbPager.Append(x.ToString());
                    sbPager.Append("</a> ");
                    sbPager.Append("</td>");
                }
            }
            if (!objPds.IsLastPage)
            {
                sbPager.Append("<td>");
                if ((objPds.CurrentPageIndex + 2) != objPds.PageCount)
                {
                    // next page link
                    sbPager.Append("&nbsp;<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, objPds.CurrentPageIndex + 2));
                    sbPager.Append("\" alt=\"Next Page\" title=\"Next Page\">&raquo;</a> ");
                }
                // last page link
                sbPager.Append("&nbsp;<a href=\"");
                sbPager.Append(CreatePageUrl(BaseUrl, objPds.PageCount));
                sbPager.Append("\" alt=\"Last Page\" title=\"Last Page\">&gt;|</a>");
                sbPager.Append("</td>");
            }
            else
            {
                if (objPds.PageCount == 1) sbPager.Append("<td> of 1</td>");
            }
            // convert the final links to a string and assign to labels
            return "<table cellpadding='0' cellspacing='1' border='0'><tr>" + sbPager1.ToString() + "</tr><tr>" + sbPager.ToString() + "</tr></table>";
        }

    }
}
