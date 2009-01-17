using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shelf.Support.Indexing
{
    class ResourceServer
    {
        private List<string> _paths = null;

        public ResourceServer()
        {
            _paths= new List<string>();
            GetResourcePaths();
        }

        private void GetResourcePaths()
        {
            // Consult Database or XML File and populate Paths
            
            #region Zasz : TestingCode

            string loc = "C:/Users/Agate/Documents/Visual Studio 2008/Projects/Shelf/Shelf.Web/";
            List<string> samples = new List<string>();
            samples.Add(loc + "Library/ASMX Client with a WCF Service.docx");
            samples.Add(loc + "Library/C#- Console Applications.pdf");
            samples.Add(loc + "Library/Desert Landscape.jpg");
            samples.Add(loc + "Library/Forest.jpg");
            samples.Add(loc + "Library/PARALLEL PORT INTERFACING TUTORIAL.HTML");
            samples.Add(loc + "Library/powerpoint.ppt");
            samples.Add(loc + "Library/Read Me.txt");
            samples.Add(loc + "Library/Zasz.xlsx");
            PathCollection = samples;

            #endregion

        }

        public List<string> PathCollection 
        { 
            get
            {
                if (_paths == null)
                    GetResourcePaths();
                return _paths;
            }
            private set
            { _paths = value; }
        }
    }
}
