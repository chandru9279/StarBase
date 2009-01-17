using System;
using System.Collections; // DictionaryEntry
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Shelf.Support.Indexing.FileTypeHandlers
{
    /// <summary>
    /// Extract a handful of Exif/Xmp fields for indexing... 
    /// </summary>
    /// <remarks>
    /// 
    /// EXIFextractor by Asim Goheer
    /// http://www.codeproject.com/KB/graphics/exifextractor.aspx
    /// 
    /// XMP Metadata (2.0)
    /// http://www.shahine.com/omar/ReadingXMPMetadataFromAJPEGUsingC.aspx
    /// 
    /// Gallery Server Pro
    /// http://www.codeproject.com/KB/web-image/Gallery_Server_Pro.aspx
    /// 
    /// Another library
    /// http://www.codeproject.com/KB/GDI-plus/ImageInfo.aspx
    /// 
    /// Yet another (VB.NET)
    /// http://www.codeproject.com/KB/vb/exif_reader.aspx
    /// </remarks>
    public class JpegDocument : Document
    {
        private string _All;
        private string _WordsOnly;

        public JpegDocument(string pathAndName)
            : base(pathAndName)
        {
            Extension = "jpg";
        }

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
            // No parsing 
        }

        /// <summary>
        /// Extract Title, Description, Keywords (tags), Make, Model from image
        /// </summary>
        public override bool Populate()
        {
            string filename = PathAndName;
            this.Title = System.IO.Path.GetFileNameWithoutExtension(filename);
            try
            {
                Goheer.EXIF.EXIFextractor er2 = new Goheer.EXIF.EXIFextractor(filename, "","");
                foreach (DictionaryEntry de in er2)
                {
                    switch (de.Key.ToString())
                    {
                        case "Equip Make"://: SONY
                            _WordsOnly += " " + de.Value.ToString().Trim(new char[] { '\0' });
                            break;
                        case "Equip Model": // : DSC-H9
                            _WordsOnly += " " + de.Value.ToString().Trim(new char[] { '\0' });
                            break;
                        default:
                            _WordsOnly = _WordsOnly + de.Key + " : " + de.Value + Environment.NewLine;
                            break;
                    }
                }


                string xmp = GetXmpXmlDocFromImage(filename);
                LoadDoc(xmp);
                _WordsOnly += " " + Title;
                _WordsOnly += " " + Description;
                foreach (string k in Keywords)
                    _WordsOnly += " " + k; // So they're indexed..
                
                _WordsOnly = System.Text.RegularExpressions.Regex.Replace(_WordsOnly, @"[^\w0-9,. ]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                this.All = _WordsOnly;
            }
            catch (Exception ex)
            {
                throw new Exception("Jpeg info extraction failed. Exception : " + ex.Message, ex);
            }
            if (this.All != string.Empty)
            {
                if (this.Description == string.Empty)
                {   
                    // if no description was set, use the rest of the text
                    this.Description = base.GetDescriptionFromWordsOnly(WordsOnly);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #region XMP

        private Int32 Rating;

        // http://www.shahine.com/omar/ReadingXMPMetadataFromAJPEGUsingC.aspx
        public static string GetXmpXmlDocFromImage(string filename)
        {
            string contents;
            string xmlPart;
            string beginCapture = "<rdf:RDF";
            string endCapture = "</rdf:RDF>";
            int beginPos;
            int endPos;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                contents = sr.ReadToEnd();
                System.Diagnostics.Debug.Write(contents.Length + " chars" + Environment.NewLine);
                sr.Close();
            }

            beginPos = contents.IndexOf(beginCapture, 0);
            endPos = contents.IndexOf(endCapture, 0);

            System.Diagnostics.Debug.Write("xml found at pos: " + beginPos.ToString() + " - " + endPos.ToString());

            xmlPart = contents.Substring(beginPos, (endPos - beginPos) + endCapture.Length);

            System.Diagnostics.Debug.Write("Xml len: " + xmlPart.Length.ToString());

            return xmlPart;
        }

        // http://www.shahine.com/omar/ReadingXMPMetadataFromAJPEGUsingC.aspx
        private void LoadDoc(string xmpXmlDoc)
        {
            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager NamespaceManager;
            try
            {
                doc.LoadXml(xmpXmlDoc);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured while loading XML metadata from image. The error was: " + ex.Message);
            }

            try
            {
                doc.LoadXml(xmpXmlDoc);

                NamespaceManager = new XmlNamespaceManager(doc.NameTable);
                NamespaceManager.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                NamespaceManager.AddNamespace("exif", "http://ns.adobe.com/exif/1.0/");
                NamespaceManager.AddNamespace("x", "adobe:ns:meta/");
                NamespaceManager.AddNamespace("xap", "http://ns.adobe.com/xap/1.0/");
                NamespaceManager.AddNamespace("tiff", "http://ns.adobe.com/tiff/1.0/");
                NamespaceManager.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

                // get ratings
                XmlNode xmlNode = doc.SelectSingleNode("/rdf:RDF/rdf:Description/xap:Rating", NamespaceManager);

                // Alternatively, there is a common form of RDF shorthand that writes simple properties as
                // attributes of the rdf:Description element.
                if (xmlNode == null)
                {
                    xmlNode = doc.SelectSingleNode("/rdf:RDF/rdf:Description", NamespaceManager);
                    xmlNode = xmlNode.Attributes["xap:Rating"];
                }

                if (xmlNode != null)
                {
                    this.Rating = Convert.ToInt32(xmlNode.InnerText);
                }

                // get keywords
                xmlNode = doc.SelectSingleNode("/rdf:RDF/rdf:Description/dc:subject/rdf:Bag", NamespaceManager);
                if (xmlNode != null)
                {

                    foreach (XmlNode li in xmlNode)
                    {
                        Keywords.Add(li.InnerText);
                    }
                }

                // get description
                xmlNode = doc.SelectSingleNode("/rdf:RDF/rdf:Description/dc:description/rdf:Alt", NamespaceManager);

                if (xmlNode != null)
                {
                    this.Description = xmlNode.ChildNodes[0].InnerText;
                }

                // get title
                xmlNode = doc.SelectSingleNode("/rdf:RDF/rdf:Description/dc:title/rdf:Alt", NamespaceManager);

                if (xmlNode != null)
                {
                    this.Title = xmlNode.ChildNodes[0].InnerText;
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occured while readning meta-data from image. The error was: " + ex.Message);
            }
            finally
            {
                doc = null;
            }
        }

        #endregion
    }
}



