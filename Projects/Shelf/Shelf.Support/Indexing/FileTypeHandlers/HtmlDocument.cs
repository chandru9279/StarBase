using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Shelf.Support.Indexing;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// Storage for parsed HTML data returned by ParsedHtmlData();
    /// </summary>
    /// <remarks>
    /// Arbitrary class to encapsulate just the properties we need 
    /// to index Html pages (Title, Meta tags, Keywords, etc).
    /// </remarks>
    public class HtmlDocument : Document
    {
        private string _WordsOnly = string.Empty;
        private string _All;
        private string _Description;

        #region Constructor

        public HtmlDocument(string pathAndName)
            : base(pathAndName)
        {
            Extension = "html";
        }

        #endregion

        /// <summary>
        /// Raw content of page.
        /// Html stripped to make up the 'wordsonly'.
        /// </summary>
        public override string All
        {
            get { return _All; }
            set { 
                _All = value;
                _WordsOnly = StripHtml(_All);
            }
        }

        public override string WordsOnly
        {
            get { return this.KeywordString + this._Description + this._WordsOnly; }
        }

        public override string Description
        {
            get {
                // ### If no META DESC, grab start of file text ###
                if (String.Empty == this._Description)
                {
                    if (_WordsOnly.Length > Preferences.SummaryCharacters)
                    {
                        _Description = _WordsOnly.Substring(0, Preferences.SummaryCharacters);
                    }
                    else
                    {
                        _Description = WordsOnly;
                    }
                    _Description = Regex.Replace(_Description, @"\s+", " ").Trim();
                }
                return _Description; 
            }
            set 
            {
                _Description = Regex.Replace(value, @"\s+", " ").Trim();
            }
        }

        public override void Parse()
        {
            string htmlData = this.All;	// contains HTML tags

            // The RegEx works with line breaks, it also works with more variations of improperly formatted tags.   Further, it will not incorrectly catch tags that begin with "title" such as: <titlepage>
            this.Title = Regex.Match(
                  htmlData
                , @"(?<=<s*title(?:\s[^>]*)?\>)[\s\S]*?(?=\</\s*title(?:\s[^>]*)?\>)"
                , RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Value;
            this.Title = this.Title.Trim();

            if (string.IsNullOrEmpty(this.Title))
                this.Title = System.IO.Path.GetFileNameWithoutExtension(PathAndName);

            string metaKey = String.Empty, metaValue = String.Empty;
            foreach (Match metamatch in Regex.Matches(htmlData
                , @"<meta\s*(?:(?:\b(\w|-)+\b\s*(?:=\s*(?:""[^""]*""|'[^']*'|[^""'<> ]+)\s*)?)*)/?\s*>"
                , RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture))
            {
                metaKey = String.Empty;
                metaValue = String.Empty;
                // Loop through the attribute/value pairs inside the tag
                foreach (Match submetamatch in Regex.Matches(metamatch.Value.ToString()
                    , @"(?<name>\b(\w|-)+\b)\s*=\s*(""(?<value>[^""]*)""|'(?<value>[^']*)'|(?<value>[^""'<> ]+)\s*)+"
                    , RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture))
                {

                    if ("http-equiv" == submetamatch.Groups[1].ToString().ToLower())
                    {
                        metaKey = submetamatch.Groups[2].ToString();
                    }
                    if (("name" == submetamatch.Groups[1].ToString().ToLower())
                        && (metaKey == String.Empty))
                    { // if it's already set, HTTP-EQUIV takes precedence
                        metaKey = submetamatch.Groups[2].ToString();
                    }
                    if ("content" == submetamatch.Groups[1].ToString().ToLower())
                    {
                        metaValue = submetamatch.Groups[2].ToString();
                    }
                }
                switch (metaKey.ToLower())
                {
                    case "description":
                        this.Description = metaValue;
                        break;
                    case "keywords":
                    case "keyword":
                        base.SetKeywords(metaValue);// Keywords = metaValue;
                        break;
                }
                // Zasz: TODO: RaiseProgressEvent ProgressEvent(this, new ProgressEventArgs(4, metaKey + " = " + metaValue));
            }
        } // Parse

        public override bool Populate()
        {
            FileStream f = System.IO.File.OpenRead(PathAndName);
            StreamReader sr = new StreamReader(f);            
            this.All = sr.ReadToEnd();
            sr.Close();
            f.Close();
            return true; //success
        }

        /// <summary>
        /// Stripping HTML
        /// http://www.4guysfromrolla.com/webtech/042501-1.shtml
        /// </summary>
        /// <remarks>
        /// Using regex to find tags without a trailing slash
        /// http://concepts.waetech.com/unclosed_tags/index.cfm
        ///
        /// http://msdn.microsoft.com/library/en-us/script56/html/js56jsgrpregexpsyntax.asp
        ///
        /// Replace html comment tags
        /// http://www.faqts.com/knowledge_base/view.phtml/aid/21761/fid/53
        /// </remarks>
        protected string StripHtml(string Html)
        {
            //Strips the <script> tags from the Html
            string scriptregex = @"<scr" + @"ipt[^>.]*>[\s\S]*?</sc" + @"ript>";
            System.Text.RegularExpressions.Regex scripts = new System.Text.RegularExpressions.Regex(scriptregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string scriptless = scripts.Replace(Html, " ");

            //Strips the <style> tags from the Html
            string styleregex = @"<style[^>.]*>[\s\S]*?</style>";
            System.Text.RegularExpressions.Regex styles = new System.Text.RegularExpressions.Regex(styleregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string styleless = styles.Replace(scriptless, " ");

            //Strips the <NOSEARCH> tags from the Html (where NOSEARCH is set in the web.config/Preferences class)
            //TODO: NOTE: this only applies to INDEXING the text - links are parsed before now, so they aren't "excluded" by the region!! (yet)
            string ignoreless = string.Empty;
            if (Preferences.IgnoreRegions)
            {
                string noSearchStartTag = "<!--" + Preferences.IgnoreRegionTagNoIndex + "-->";
                string noSearchEndTag = "<!--/" + Preferences.IgnoreRegionTagNoIndex + "-->";
                string ignoreregex = noSearchStartTag + @"[\s\S]*?" + noSearchEndTag;
                System.Text.RegularExpressions.Regex ignores = new System.Text.RegularExpressions.Regex(ignoreregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
                ignoreless = ignores.Replace(styleless, " ");
            }
            else
            {
                ignoreless = styleless;
            }

            //Strips the <!--comment--> tags from the Html	
            //string commentregex = @"<!\-\-.*?\-\->";		// alternate suggestion from antonello franzil 
            string commentregex = @"<!(?:--[\s\S]*?--\s*)?>";
            System.Text.RegularExpressions.Regex comments = new System.Text.RegularExpressions.Regex(commentregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string commentless = comments.Replace(ignoreless, " ");

            //Strips the HTML tags from the Html
            System.Text.RegularExpressions.Regex objRegExp = new System.Text.RegularExpressions.Regex("<(.|\n)+?>", RegexOptions.IgnoreCase);

            //Replace all HTML tag matches with the empty string
            string output = objRegExp.Replace(commentless, " ");

            //Replace all _remaining_ < and > with &lt; and &gt;
            output = output.Replace("<", "&lt;");
            output = output.Replace(">", "&gt;");

            objRegExp = null;
            return output;
        }
    }
}
