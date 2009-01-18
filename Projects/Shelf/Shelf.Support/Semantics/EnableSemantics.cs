using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shelf.Support.Semantics.WordNet.Common;
using Shelf.Support.Logging;

namespace Shelf.Support.Semantics
{
    public class EnableSemantics
    {
        private const string PartOfSpechFormat = "<div><strong>{0}</strong><div>{1}</div></div>";
        private const string LinkedWordFormat = "<a href=\"Search.aspx?{1}=" + "{0}\">{0}</a>";

        private string _SemanticsHtml = "";
        /// <summary>
        /// Note the += symbol : New info for each word in GivenQueryString is appended.
        /// </summary>
        public string SemanticsHtml
        {
            get { return _SemanticsHtml; }
            private set { _SemanticsHtml += value; }
        }

        private List<string> _synonyms;
        /// <summary>
        /// Maybe null if a word has no synonyms
        /// </summary>
        private string NewQueryString
        {
            get 
            {
                string newqs = "";
                foreach (string syn in _synonyms)
                {
                    newqs += (syn + " ");
                }
                return newqs;
            }
        }

        private string _GivenQueryString;
        private string GivenQueryString
        {
            get { return _GivenQueryString; }
            set { _GivenQueryString = value; }
        }

        public string GetSemanticSearchString(string querystring)
        {
            GivenQueryString = querystring;
            string newquery = "";
            foreach (string word in querystring.Split(" ,.+;:/\\|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    DefineWord(word.Trim(' ', '?', '\"', ',', '\'', ';', ':', '.', '(', ')'));
                    newquery += NewQueryString.Replace(word, string.Empty);
                }
                catch (Exception ex)
                {
                    Logger.PerformanceLog(this, "Exception while DefineWord(" + word + "); Exception : " + ex.ToString());
                    newquery = GivenQueryString;
                }
            }
            return newquery;
        }

        private void DefineWord(string word)
        {
            Dictionary<string, List<Definition>> definitions = DictionaryHelper.GetDefinition(word);
            PopulateResults(word, definitions);
        }

        private string FormatDefinition(string text)
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                int exStart = text.IndexOf('"');
                if (exStart > -1)
                {
                    retVal += "<strong>";
                    retVal += text.Insert(exStart, "</strong><br /><i>");
                    retVal += "</i>";
                }
                else
                {
                    retVal = string.Format("<strong>{0}</strong>", text);
                }
            }
            return retVal;
        }

        private void PopulateResults(string orgWord, Dictionary<string, List<Definition>> results)
        {
            StringBuilder sb = new StringBuilder();

            if (results.Count > 0)
            {
                #region Prep for sorting, build and rendering
                List<string> words = new List<string>();
                _synonyms = new List<string>();
                Dictionary<string, List<Definition>> defSets = new Dictionary<string, List<Definition>>();
                #endregion

                #region Sort results by part of speech
                foreach (string key in results.Keys)
                {
                    foreach (Definition def in results[key])
                    {
                        string pos = def.DisplayPartOfSpeech;
                        if (!defSets.ContainsKey(pos))
                            defSets.Add(pos, new List<Definition>());

                        defSets[pos].Add(def);
                        foreach (string word in def.Words)
                        {
                            if (!_synonyms.Contains(word))
                            {
                                _synonyms.Add(word);
                                string linkedWord = string.Format(LinkedWordFormat, word.ToLower(), Preferences.QuerystringParameterName);
                                words.Add(linkedWord);
                            }
                        }
                    }
                }
                #endregion

                #region Build markup for browser control
                foreach (string key in defSets.Keys)
                {
                    StringBuilder defText = new StringBuilder("<ul>");
                    foreach (Definition def in defSets[key])
                    {
                        string formattedDefinition = FormatDefinition(def.DefinitionText);
                        if (!string.IsNullOrEmpty(formattedDefinition))
                        {
                            defText.AppendLine(string.Format("<li>{0}</li>", formattedDefinition));
                        }
                    }
                    defText.AppendLine("</ul>");
                    sb.AppendFormat(PartOfSpechFormat, string.Format("{0} <sup>({1})</sup>", orgWord, key), defText.ToString());
                }

                sb.Append(string.Join(", ", words.ToArray()));
                #endregion
            }
            else
            {
                #region Build no results markup
                sb.AppendFormat("<h1>No match was found for \"{0}\".</h1><br />Try your search on <a href=\"http://wordnet.princeton.edu/perl/webwn?s={0}\" target=\"_blank\">WordNet Online</a>", orgWord);
                #endregion
            }

            #region StoreBuiltMarkup
            sb.Append("<br /><br />");
            SemanticsHtml = sb.ToString();
            #endregion
        }
    }
}
