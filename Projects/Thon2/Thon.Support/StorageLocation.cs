using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Thon.Support
{
    public static class StorageLocation
    {
        // We need to have the exact path of the settings file so that 1st time ThonSettings is initiated,
        // it can load its settings from a file in this folder. This has to be commented when using the WinForms
        // client to set the settings.
        internal static string _SettingsFolder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
        // For Winforms client:
        // internal static string _SettingsFolder = @"C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\";
        // _SettingsFolder will have C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\
    }
}
