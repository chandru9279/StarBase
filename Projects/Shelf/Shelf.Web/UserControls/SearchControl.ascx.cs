using System;
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
using Shelf.Support;

namespace Shelf.Web.UserControls
{
    public partial class SearchControlAscx : System.Web.UI.UserControl
    {

        protected Panel pnlResultsSearch;
        protected Panel pnlHomeSearch;
        protected HtmlGenericControl pHeading;
        protected HtmlTableRow rowSummary, rowFooter1, rowFooter2;

        /// <summary>Size of the searchable catalog (number of unique words)</summary>
        public int WordCount = -1;

        /// <summary>Word(s) displayed in search input box</summary>
        public string Word = "";

        /// <summary>
        /// Whether the enable semantics check box is checked(selected)
        /// </summary>
        public bool SemanticsEnabled
        {
            get
            {
                return SearchPage_Semantics.Checked || ResultsPage_Semantics.Checked;
            }
        }

        /// <summary>Whether the standalone home page version, or the on Search Results page</summary>
        private bool _IsSearchResultsPage;

        /// <summary>
        /// Value is either
        ///   false: being displayed on the 'home page' - only thing on the page
        ///   true:  on the Results page (at the top _and_ bottom)
        /// <summary>
        public bool IsSearchResultsPage
        {
            get { return _IsSearchResultsPage; }
            set
            {
                _IsSearchResultsPage = value;
                if (_IsSearchResultsPage)
                {
                    pnlHomeSearch.Visible = false;
                    pnlResultsSearch.Visible = true;
                }
                else
                {
                    pnlHomeSearch.Visible = true;
                    pnlResultsSearch.Visible = false;
                }
            }
        }

        /// <summary>Whether the control is placed at the Header or Footer</summary>
        protected bool _IsFooter;

        /// <summary>
        /// Footer control has more 'display items' than the one shown
        /// in the Header - setting this property shows/hides them
        /// </summary>
        public bool IsFooter
        {
            set
            {
                _IsFooter = value;
                pHeading.Visible = !_IsFooter;
                rowFooter1.Visible = _IsFooter;
                rowFooter2.Visible = _IsFooter;
                rowSummary.Visible = !_IsFooter;
            }
        }

        /// <summary>
        /// Error message - on Home Page version ONLY
        /// ie. ONLY when IsSearchResultsPage = true
        /// </summary>
        public string _ErrorMessage;

        /// <summary>
        /// Error message to be displayed if search input box is empty
        /// </summary>
        public string ErrorMessage
        {
            set
            {
                _ErrorMessage = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void SearchPage_Search_Click(object sender, EventArgs e)
        {
            string result = "Search.aspx?" + Preferences.QuerystringParameterName + "=" + SearchPage_TextBox.Text;

            if (SemanticsEnabled)
                result += "&semantics=true";

            Response.Redirect(result);
        }

        protected void ResultsPage_SearchButton_Click(object sender, EventArgs e)
        {
            string result = "Search.aspx?" + Preferences.QuerystringParameterName + "=" + ResultsPage_TextBox.Text;

            if (SemanticsEnabled)
                result += "&semantics=true";

            Response.Redirect(result);
        }
    }
}