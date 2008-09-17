using System;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace Aveo.Daybook.TrayClient
{
    /// <summary>
    /// A configuration section for web.config.
    /// </summary>
    /// <remarks>
    /// In the config section you can get or persist the settings that the user has selected to the App.config file
    /// </remarks>
    public partial class SettingsAdapter : System.Configuration.AppSettingsReader
    {
        #region Instance
        private static SettingsAdapter instance = null;        
        /// <summary>
        /// Holds or creates the singleton instance of the <see cref="Aveo.Daybook.TrayClient.SettingsAdapter"/>
        /// </summary>
        public static SettingsAdapter Instance
        {
            get
            {
                return instance == null ? new SettingsAdapter() : instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Private constructor that initializes the private static variable instance.
        /// This is a design pattern that ensures SettingsAdapter is a singleton 
        /// object.
        /// </summary>        
        private SettingsAdapter()
        {
            instance = this;
        }
        #endregion
    }
}

