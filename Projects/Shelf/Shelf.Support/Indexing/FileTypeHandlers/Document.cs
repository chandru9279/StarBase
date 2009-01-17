using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    public abstract class Document
    {
        #region Private fields

        private string _PathAndName;
        private string _All;
        private string _Extension = String.Empty;
        private string _Title;
        private long _Length;
        private string _Description = String.Empty;
        private List<string> _Keywords = new List<string>();
        
        #endregion

        // Populates the object with data in the file and about the file
        public abstract bool Populate(); 
        // Infers the values of other properties of this class by reading through the file
        public abstract void Parse();

        #region Properties

        public virtual string All
        {
            get { return _All; }
            set { _All = value; }
        }

        public virtual string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        public abstract string WordsOnly { get; }
        
        public virtual string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public virtual long Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        /// <summary>
        /// Keywords (tags)
        /// </summary>
        public virtual List<string> Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }

        /// <summary>
        /// Comma-seperated list of keywords
        /// </summary>
        public string KeywordString 
        {
            get
            {
                string s = string.Empty;
                int i = 0;
                foreach (string word in _Keywords)
                {
                    if (i > 0) s += ", ";
                    s += word;
                    i++;
                }
                return s;
            }
            set 
            {
                SetKeywords(value);
            }
        }

        public virtual string PathAndName
        {
            get { return _PathAndName; }
            set { _PathAndName = value; }
        }

        public virtual string[] WordsArray
        {
            get { return this.WordsStringToArray(WordsOnly); }
        }

        #endregion
        
        /// <summary>
        /// Constructor for any document requires the Uri be specified
        /// </summary>
        public Document(string pathandname)
        {
            _PathAndName = pathandname;
        }


        #region Methods

        protected string[] WordsStringToArray(string words)
        {
            // COMPRESS ALL WHITESPACE into a single space, seperating words
            // Called by accessing the WordsArray property
            if (words.Length > 0)
            {
                Regex r = new Regex(@"\s+");            //remove all whitespace
                string compressed = r.Replace(words, " ");
                return compressed.Split(' ');
            }
            else
            {
                return new string[0];
            }
        }

        protected string GetDescriptionFromWordsOnly(string wordsonly)
        {
            string description = string.Empty;
            if (wordsonly.Length > Preferences.SummaryCharacters)
            {
                description = wordsonly.Substring(0, Preferences.SummaryCharacters);
            }
            else
            {
                description = WordsOnly;
            }
            description = System.Text.RegularExpressions.Regex.Replace(description, @"\s+", " ").Trim();
            return description;
        }

        /// <summary>
        /// Converts a single long string of comma or ; seperated keywords into a list
        /// and stores them internally in the Keywords property
        /// </summary>
        /// <param name="keywords">comma or ; seperated keywords string</param>
        protected void SetKeywords(string keywords)
        {
            string[] words = keywords.Split(new char[] { ',', ';'});
            foreach (string word in words)
            {
                if (word.Trim() != "")
                {
                    this.Keywords.Add(word.Trim());
                }
            }
        }

        #endregion
    }
}