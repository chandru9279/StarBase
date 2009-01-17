using System;
using System.IO;
using System.Xml;
using ionic.utils.zip;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// Load a Microsoft Excel 2007 Xml file format
    /// </summary>
    /// <remarks>
    /// <see cref="DocxDocument" />
    /// Xlsx...
    /// http://www.gemboxsoftware.com/Excel2007/DemoApp.htm
    /// </remarks>
    public class XlsxDocument : Document
    {
        private string _WordsOnly;

        public XlsxDocument(string pathAndName)
            : base(pathAndName)
        {
            Extension = "xlsx";
        }

        public override void Parse()
        {
            // no parsing (for now).
        }

        public override string WordsOnly
        {
            get { return _WordsOnly; }
        }

        /// <remarks>
        /// .NET System.IO.Compression and zip files
        /// http://blogs.msdn.com/dotnetinterop/archive/2006/04/05/.NET-System.IO.Compression-and-zip-files.aspx
        /// </remarks>
        public override bool Populate()
        {
            string filename = PathAndName;
            this.Title = System.IO.Path.GetFileNameWithoutExtension(filename);
            // Will be accessing this data in the xlsx file
            //  xl/workbook.xml              sheet
            //  xl/worksheets/sheet{0}.xml   v
            try
            {
                using (ZipFile zip = ZipFile.Read(filename))
                {
                    int slideCount = 0;
                    using (MemoryStream streamroot = new MemoryStream())
                    {   // open the presentation 'root' file to see how many slides there are
                        zip.Extract("xl/workbook.xml", streamroot);
                        streamroot.Seek(0, SeekOrigin.Begin);
                        XmlDocument xmldocroot = new XmlDocument();
                        xmldocroot.Load(streamroot);
                        XmlNodeList objXML = xmldocroot.GetElementsByTagName("sheet");
                        slideCount = objXML.Count;
                    }
                    XmlDocument xmlSheet;
                    string entryToExtractPattern = @"xl/worksheets/sheet{0}.xml";
                    for (int slideId = 1; slideId <= slideCount; slideId++)
                    {   // now open each slide file to extract text
                        using (MemoryStream stream = new MemoryStream())
                        {
                            string entryToExtract = String.Format(entryToExtractPattern, slideId);
                            zip.Extract(entryToExtract, stream);
                            stream.Seek(0, SeekOrigin.Begin);
                            xmlSheet = new XmlDocument();
                            xmlSheet.Load(stream);
                        }
                        string slideWords = "";
                        foreach (XmlElement x in xmlSheet.GetElementsByTagName("v"))
                        {
                            slideWords = slideWords + " " + x.InnerText;
                        }
                        _WordsOnly = _WordsOnly + " " + slideWords + Environment.NewLine + Environment.NewLine;
                        this.All = _WordsOnly;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Xlsx population failed. Exception : " + ex.Message, ex);
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

