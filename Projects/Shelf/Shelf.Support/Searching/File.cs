using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Shelf.Support.Searching
{
    /// <summary>
    /// File attributes
    /// </summary>
    /// <remarks>
    /// *Beware* ambiguity with System.IO.File - always fully qualify File object references
    /// </remarks>
    [Serializable]
    public class File
    {
        #region Fields
        private string _PathAndName;
        private string _Title;
        private string _Extension; 
        private string _Description;
        private DateTime _CrawledDate;
        private long _Size;
        private string _KeywordString; 
        #endregion

        [XmlAttribute("u")]
        public string PathAndName
        {
            get { return _PathAndName; }
            set { _PathAndName = value; }
        }

        [XmlAttribute("t")]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        [XmlAttribute("e")]
        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        [XmlElement("d")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [XmlAttribute("d")]
        public DateTime CrawledDate
        {
            get { return _CrawledDate; }
            set { _CrawledDate = value; }
        }

        [XmlAttribute("s")]
        public long Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        
        /// <summary>
        /// Keyword string (comma seperated)
        /// </summary>
        [XmlElement("kw")]
        public string KeywordString
        {
            get { return _KeywordString; }
            set { _KeywordString = value; }
        }

        /// <summary>
        /// Required for serialization
        /// </summary>
        public File() { }
       
        /// <summary>
        /// Constructor requires all File attributes
        /// </summary>
        public File(string path, string title, string description, DateTime datecrawl
            , long length
            , string extension
            , string keywords)
        {
            _Title = title;
            _Description = description;
            _CrawledDate = datecrawl;
            _PathAndName = path;
            _Size = length;
            _Extension = extension;
            _KeywordString = keywords;
        }

        /// <summary>
        /// Debug string
        /// </summary>
        public override string ToString()
        {
            return "\tFILE :: " + PathAndName + " -- " + Title + " - " + Size + " bytes + \n\t" + Description + "\n";
        }
    }
}
