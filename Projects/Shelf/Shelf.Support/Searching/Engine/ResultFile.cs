using System;
using System.Collections.Generic;
using System.Text;

namespace Shelf.Support.Searching
{
    /// <summary>
    /// An Enhancement of the File class for use only during display of Search results
    /// </summary>
    public class ResultFile : File
    {
        private int _Rank;
        public ResultFile(File sourceFile)
        {
            this.PathAndName = sourceFile.PathAndName;
            this.Title = sourceFile.Title;
            this.Description = sourceFile.Description;
            this.CrawledDate = sourceFile.CrawledDate;
            this.Size = sourceFile.Size;
            this.Rank = -1;
            this.KeywordString = sourceFile.KeywordString;
            this.Extension = sourceFile.Extension;
        }
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        public string TitleText
        {
            get 
            {
                return this.Title.Replace("&", "");
            }
        }

        public string DescriptionText
        {
            get
            {
                return this.Description.Replace("&", "");
            }
        }
    }
}
