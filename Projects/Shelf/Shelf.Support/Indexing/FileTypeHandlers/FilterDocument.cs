using System;
using System.Collections.Generic;
using System.Text;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    public class FilterDocument : Document
    {
        private string _All;
        private string _WordsOnly;

        public FilterDocument(string pathAndName)
            : base(pathAndName)
        { }

        /// <summary>
        /// Set 'all' and 'words only' to the same value (no parsing)
        /// </summary>
        public override string All
        {
            get { return _All; }
            set { 
                _All = value;
                _WordsOnly = _All;
            }
        }
        
        public override string WordsOnly
        {
            get { return _WordsOnly; }
        }

        public override void Parse()
        {
            // no parsing (for now).
        }

        public override bool Populate()
        {
            string filename = PathAndName;
            this.Title = System.IO.Path.GetFileNameWithoutExtension(PathAndName);

            try
            {
                EPocalipse.IFilter.FilterReader ifil = new EPocalipse.IFilter.FilterReader(filename);
                this.All = ifil.ReadToEnd();
                ifil.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("IFilter population failed. Exception : " + ex.Message, ex);
            }
            if (this.All != string.Empty)
            {
                this.Description = base.GetDescriptionFromWordsOnly(WordsOnly);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}