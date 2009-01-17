using System;
using System.Collections.Generic;
using System.Text;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// Document instance when the location is to be ignored (ie not indexable).
    /// </summary>
    /// <remarks>
    /// Created this in case we still want to use the Document class (the superclass), say 
    /// to get the filesize for reporting, logging or something...
    /// </remarks>
    public class IgnoreDocument : Document
    {
        #region Constructor

        public IgnoreDocument(string pathAndName)
            : base(pathAndName)
        {        }

        #endregion

        public override string WordsOnly
        {
            get { return string.Empty; }
        }
        public override void Parse ()
        {
            // no parsing, content is not used for indexing at all.. 
        }
        public override bool Populate ()
        {
            return false;
        }
    }
}
