using System;

namespace Shelf.Support
{
    /// <summary>
    /// Retrieve data from web.config (or app.config)
    /// </summary>
    public static class Preferences
    {
        #region Private Fields

        private static bool _InMediumTrust;

        #endregion

        /// <summary>
        /// Load preferences from *.config (web.config for ASPX, app.config for the console program)
        /// and apply defaults where the values are not found.
        /// </summary>
        static Preferences()
        { 
            _InMediumTrust = IfNullDefault("SearchSetting_InMediumTrust", true);
        }

        /// <summary>
        /// Whether to use stemming (English only), and if so, what mode [ Off | StemOnly | StemAndOriginal ]
        /// Default: Off
        /// </summary>
        public static int StemmingMode
        {
            get
            {
                return IfNullDefault("SearchSetting_StemmingType", 0);
            }
        }

        /// <summary>
        /// Whether to use stemming (English only), and if so, what mode [ Off | Short | List ]
        /// Default: Off
        /// </summary>
        public static int StoppingMode
        {
            get
            {
                return IfNullDefault("SearchSetting_StoppingType", 0);
            }
        }

        /// <summary>
        /// Whether to use go words (English only), and if so, what mode [ Off | On ]
        /// Default: Off
        /// </summary>
        public static int GoWordMode
        {
            get
            {
                return IfNullDefault("SearchSetting_GoType", 0);
            }
        }

        /// <summary>
        /// Number of characters to include in 'file summary'
        /// Default: 350
        /// </summary>
        public static int SummaryCharacters
        {
            get
            {
                return IfNullDefault("SearchSetting_SummaryChars", 350);
            }
        }

        /// <summary>
        /// Application[] cache key where the Catalog is stored, in case you need to alter it
        /// Default: SearchSetting_Catalog
        /// </summary>
        public static string CatalogCacheKey
        {
            get
            {
                return IfNullDefault("SearchSetting_CacheKey", "TCNGKKP");
            }
        }

        /// <summary>
        /// Name of file where the Catalog object is serialized (.dat and .xml)
        /// </summary>
        public static string CatalogFileName
        {
            get
            {   //TODO: remove HttpContext dependency!
                string location = IfNullDefault("SearchSetting_CatalogFilepath", "");
                if (location == "")
                {
                    location = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
                }
                location = location + IfNullDefault("SearchSetting_CatalogFilename", "Shelf_Tc_Ngk_Kp.dat");
                return location;
            }
        }

        /// <summary>
        /// Number of result links to include per page
        /// Default: 10
        /// </summary>
        public static int ResultsPerPage
        {
            get
            {
                return IfNullDefault("SearchSetting_DefaultResultsPerPage", 10);
            }
        }

        /// <summary>
        /// The variable that is shown in the query string to contain the search terms
        /// Zasz: For Google it is "q"
        /// </summary>
        public static string QuerystringParameterName
        {
            get
            {
                return IfNullDefault("SearchSetting_QuerystringParameter", "q");
            }
        }

        /// <summary>
        /// Tagname to extract from html documents before parsing (to 'ignore' menus, for example)
        /// </summary>
        public static string IgnoreRegionTagNoIndex
        {
            get
            {
                return IfNullDefault("SearchSetting_IgnoreRegionTagNoIndex", "");
            }
        }

        /// <summary>
        /// Whether to ignore sections of HTML wrapped in a special comment tag
        /// </summary>
        public static bool IgnoreRegions
        {
            get { return IgnoreRegionTagNoIndex.Length > 0;  }
        }

        /// <summary>
        /// Whether the application is running in medium-trust (and requires Xml rather than Binary serialization)
        /// Default: true
        /// </summary>
        public static bool InMediumTrust
        {
            get
            {
                return _InMediumTrust;
            }
        }

        /// <summary>
        /// Verbosity of the Indexer
        /// </summary>
        public static int IndexerDefaultVerbosity
        {
            get
            {
                return IfNullDefault("SearchSetting_IndexerDefaultVerbosity", 1);
            }
        }


        #region Private Methods: IfNullDefault (Overloaded)

        private static int IfNullDefault(string appSetting, int defaultValue)
        {
            return System.Configuration.ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[appSetting]);
        }
        private static string IfNullDefault(string appSetting, string defaultValue)
        {
            return System.Configuration.ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : System.Configuration.ConfigurationManager.AppSettings[appSetting];
        }
        private static bool IfNullDefault(string appSetting, bool defaultValue)
        {
            return System.Configuration.ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings[appSetting]);
        }

        #endregion

    }  // Preferences class
}
