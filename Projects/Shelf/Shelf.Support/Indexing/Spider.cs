using System;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Shelf.Support;
using Shelf.Support.Searching;
using Shelf.Support.Indexing.FileTypeHandlers;
using System.Collections.Generic;

namespace Shelf.Support.Indexing
{
    /// <summary>
    /// The Spider
    /// </summary>
    public class Spider
    {
        #region Private fields

        /// <summary>The Catalog object</summary>
        private Catalog _Catalog;

        /// <summary>Set of visited files while crawling</summary>
        private ArrayList _Visited = new ArrayList();

        /// <summary>Stemmer to use</summary>
        private IStemming _Stemmer;

        /// <summary>Stemmer to use</summary>
        private IStopper _Stopper;

        /// <summary>Go word parser to use</summary>
        private IGoWord _GoChecker;

        #endregion

        #region Public events/handlers: SpiderProgressEvent
        /// <summary>
        /// Event Handler to communicate progress and errors back to the calling code
        /// </summary>
        public event SpiderProgressEventHandler SpiderProgressEvent;

        /// <summary>
        /// Only trigger the event if a Handler has been attached.
        /// </summary>
        private void ProgressEvent(object sender, ProgressEventArgs pea)
        {
            if (this.SpiderProgressEvent != null)
            {
                SpiderProgressEvent(sender, pea);
            }
        }
        #endregion

        /// <summary>
        /// Creates an instance of ResourceServer <see cref="ResourceServer"/> and uses it 
        /// to get a list of Resources and catalogs their contents
        /// </summary>
        /// <remarks>
        ///This is the MAIN method of the indexing system.
        /// </remarks>
        public Catalog BuildCatalog()
        {
            _Catalog = new Catalog();
            ProgressEvent(this, new ProgressEventArgs(1, "Spider.BuildCatalog() starting."));
            // Setup Stop, Go, Stemming
            SetPreferences();
            ResourceServer server = new ResourceServer();
            foreach (string path in server.PathCollection)
            {
                ProcessPath(path);
            }
            // Now we've FINISHED Spidering
            ProgressEvent(this, new ProgressEventArgs(1, "Spider.BuildCatalog() complete."));
            if (_Catalog.Length > 0)
            {
                ProgressEvent(this, new ProgressEventArgs(2, "Serializing to disk location " + Preferences.CatalogFileName));
                // Serialization of the Catalog, so we can load it again if the server Application is restarted
                _Catalog.Save();
                ProgressEvent(this, new ProgressEventArgs(3, "Save to disk " + Preferences.CatalogFileName + " successful"));
            }
            else
            {
                ProgressEvent(this, new ProgressEventArgs(3, "Not serializing/saving: Empty catalog!"));
            }
            return _Catalog;// finished, return to the calling code to 'use'
        }
      
        /// <summary>
        /// Setup Stop, Go, Stemming
        /// </summary>
        private void SetPreferences()
        {
            switch (Preferences.StemmingMode)
            {
                case 1:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stemming enabled."));
                    _Stemmer = new PorterStemmer();	//Stemmer = new SnowStemming();
                    break;
                case 2:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stemming enabled."));
                    _Stemmer = new PorterStemmer();
                    break;
                default:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stemming DISabled."));
                    _Stemmer = new NoStemming();
                    break;
            }
            switch (Preferences.StoppingMode)
            {
                case 1:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stop words shorter than 3 chars."));
                    _Stopper = new ShortStopper();
                    break;
                case 2:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stop words from list."));
                    _Stopper = new ListStopper();
                    break;
                default:
                    ProgressEvent(this, new ProgressEventArgs(1, "Stopping DISabled."));
                    _Stopper = new NoStopping();
                    break;
            }
            switch (Preferences.GoWordMode)
            {
                case 1:
                    ProgressEvent(this, new ProgressEventArgs(1, "Go Words enabled."));
                    _GoChecker = new ListGoWord();
                    break;
                default:
                    ProgressEvent(this, new ProgressEventArgs(1, "Go Words DISabled."));
                    _GoChecker = new NoGoWord();
                    break;
            }
        }

        /// <summary>
        /// Processes a file at the given path, using the DocumentFactory to
        /// receive a Document subclass, then calling the Parse() method to get the words which
        /// are then added to the Catalog.
        /// </summary>
        protected void ProcessPath(string path)
        {
            int wordcount = 0;

            if (_Visited.Contains(path))
            {
                ProgressEvent(this, new ProgressEventArgs(2, path + " already crawled."));
            }
            else
            {
                _Visited.Add(path);
                ProgressEvent(this, new ProgressEventArgs(2, path + " being processed"));
                Document uploadDocument = GetDocumentObject(path);
                
                if (uploadDocument != null)
                {
                    // ### IMPORTANT ### 
                    // Document is actually parsed here!
                    uploadDocument.Parse();
                    wordcount = AddToCatalog(uploadDocument);
                    if (wordcount > 0)
                    {
                        ProgressEvent(this, new ProgressEventArgs(1, uploadDocument.Title + " parsed " + wordcount + " words!"));
                        ProgressEvent(this, new ProgressEventArgs(4, "The description of " + uploadDocument.PathAndName + System.Environment.NewLine
                                                                    + uploadDocument.Description
                                                                    + System.Environment.NewLine));
                    }
                    else
                    {
                        ProgressEvent(this, new ProgressEventArgs(2, path + " parsed but zero words found."));
                    }
                }
                else
                {
                    ProgressEvent(this, new ProgressEventArgs(1, "Document processing failed on " + path));
                }
            } // not visited
        }

        /// <summary>
        /// Attempts to ( using the DocumentFactory )
        /// to get a Document subclass object that is able to parse the document data.
        /// </summary>
        protected Document GetDocumentObject(string path)
        {
            bool success = false;
            // Check for existance
            bool exists = System.IO.File.Exists(path);
            Document currentDocument = null;
            if (exists)
            {
                currentDocument = DocumentFactory.New(path);
               
                try
                { 
                    success = currentDocument.Populate(); 
                }
                catch (Exception ex)
                {
                    success = false;
                    ProgressEvent(this, new ProgressEventArgs(2, "Document Population Failure: " + currentDocument.PathAndName + " : " + ex.ToString())); 
                }

                if (success)
                {
                    ProgressEvent(this, new ProgressEventArgs(2, "Document Population Successful : " + currentDocument.Title + " Type " + currentDocument.Extension));
                }
                else
                {
                    ProgressEvent(this, new ProgressEventArgs(2, "Document Population Failure: " + currentDocument.PathAndName));
                    currentDocument = null; // Have to Nullify it to prevent BuildCatalog() from calling Parse() on it 
                }
            }
            else
            {
                ProgressEvent(this, new ProgressEventArgs(2, "Document does not exist for path : " + path));
            }
            return currentDocument;
        }

        /// <summary>
        /// Add the Document subclass to the catalog, BY FIRST 'copying' the main
        /// properties into a File class. The distinction is a bit arbitrary: Documents
        /// are downloaded and indexed, but their content is modelled in as a File
        /// class in the Catalog.
        /// </summary>
        /// <return>Number of words catalogued in the Document</return>
        protected int AddToCatalog(Document downloadDocument)
        {
            File infile = new File(downloadDocument.PathAndName
                , downloadDocument.Title
                , downloadDocument.Description
                , DateTime.Now
                , downloadDocument.Length
                , downloadDocument.Extension
                , downloadDocument.KeywordString);

            // ### Loop through words in the file ###
            int i = 0;          // count of words
            string key;    // temp variables
            
            foreach (string word in downloadDocument.WordsArray)
            {
                key = word.ToLower();
                if (!_GoChecker.IsGoWord(key))
                {	// not a special case, parse like any other word
                    RemovePunctuation(ref key);

                    if (!IsNumber(ref key))
                    {	// not a number, so get rid of numeric seperators and catalog as a word
                        // TODO: remove inline punctuation, split hyphenated words?
                        // http://blogs.msdn.com/ericgu/archive/2006/01/16/513645.aspx
                        key = System.Text.RegularExpressions.Regex.Replace(key, "[,.]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                        // Apply Stemmer (set by preferences)
                        key = _Stemmer.StemWord(key);

                        // Apply Stopper (set by preferences)
                        key = _Stopper.StopWord(key);
                    }
                }
                else
                {
                    ProgressEvent(this, new ProgressEventArgs(4, "Found GoWord " + key + " in " + downloadDocument.Title));
                }
                if (key != String.Empty)
                {
                    _Catalog.Add(key, infile, i);
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Each word is identified purely by the whitespace around it. It could still include punctuation
        /// attached to either end of the word, or "in" the word (ie a dash, which we will remove for
        /// indexing purposes)
        /// </summary>
        private void RemovePunctuation(ref string word)
        {   
            //if (Preferences.AssumeAllWordsAreEnglish)
            //{   // if all words are english, this strict parse to remove all punctuation ensures
                  // words are reduced to their least unique form before indexing
                  // word = System.Text.RegularExpressions.Regex.Replace(word, @"[^a-z0-9,.]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                word = System.Text.RegularExpressions.Regex.Replace(word, @"[^\w0-9,.]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //}
            //else 
            //{   // by stripping out this specific list of punctuation only, there is potential to leave lots 
                  // of bloat in the word before indexing BUT this will allow any language to be indexed
            //    word = word.Trim(' ','?','\"',',','\'',';',':','.','(',')','[',']','%','*','$','-'); 
            //}
        }


        #region IsNumber
        /// <summary>
        /// TODO: parse numbers here 
        /// ie remove thousands seperator, currency, etc
        /// and also trim decimal part, so number searches are only on the integer value
        /// </summary>
        private bool IsNumber(ref string word)
        {
            int i = word.Length - 1;
            while (i != -1)
            {
                if (!numchars.Contains(word[i])) break;
                i--;
            }
            return i == -1;
        }

        /// <summary>
        /// Characters that can be in a number
        /// </summary>
        private static char [] numcs = {'.',',','1','2','3','4','5','6','7','8','9','0'};

        private List<char> numchars = new List<char>(Spider.numcs);
        
        #endregion

    }
}