using System;
using System.IO;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// Return a Document subclass capable of downloading and parsing the
    /// given Uri/ContentType header information
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        /// Construct a Document instance. 
        /// </summary>
        public static Document New (string pathAndName)
        {
            Document newDoc;
            FileInfo i = new FileInfo(pathAndName);
            switch (i.Extension.ToLowerInvariant())
            {
                                                                                                  
                case ".docx": 
                    newDoc = new DocxDocument(pathAndName);
                    break;
                                                                                  
                case ".pptx":
                    newDoc = new PptxDocument(pathAndName);
                    break;
                                                                                   
                case ".xlsx":
                    newDoc = new XlsxDocument(pathAndName);
                    break;

                case ".ppt":
                case ".xls":
                case ".doc":
                case ".pdf":
                    newDoc = new FilterDocument(pathAndName);
                    newDoc.Extension = i.Extension.ToLowerInvariant();
                    break;

                case ".txt":
                    newDoc = new TextDocument(pathAndName);
                    break;

                case ".xml":
                case ".htm":
                case ".html":
                case ".xhtml":    
                    newDoc = new HtmlDocument(pathAndName); 
                    break;

                case ".jpeg":
                case ".jpg":
                    newDoc = new JpegDocument(pathAndName); 
                    break;

                default:
                    //could be rtf or other format
                    newDoc = new IgnoreDocument(pathAndName);
                    // Console.WriteLine("Unrecognized format encountered :" + i.Extension);
                    // Zasz: TODO: Raise Progress Event
                    break;
            }
            newDoc.Length = i.Length;
            return newDoc;
        }
    }
}