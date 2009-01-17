using System;
using System.Collections.Generic;
using System.Text;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// ASCII Text Document (plaintext)
    /// </summary>
    public class TextDocument : Document
    {
        private string _WordsOnly;
        private string _All;

        public override string WordsOnly
        {
            get { return _WordsOnly; }
        }

        public override string[] WordsArray
        {
            get { return this.WordsStringToArray(WordsOnly); }
        }

        /// <summary>
        /// Set 'all' and 'words only' to the same value (no parsing)
        /// </summary>
        public override string All {
            get { return _All; }
            set { 
                _All = value;
                _WordsOnly = value;
            }
        }

        

        #region Constructor

        public TextDocument(string pathAndName)
            : base(pathAndName)
        {
            Extension = "txt";
        }

        #endregion

        public override void Parse()
        {
            // no parsing, by default the content is used "as is"
        }
        public override bool Populate()
        {
            System.IO.StreamReader stream = new System.IO.StreamReader
                (System.IO.File.OpenRead(PathAndName), System.Text.Encoding.ASCII);

            this.All = stream.ReadToEnd();
            this.Title = System.IO.Path.GetFileName(this.PathAndName);
            this.Description = base.GetDescriptionFromWordsOnly(WordsOnly);
            stream.Close();
            return true; 
        }
    }
}
