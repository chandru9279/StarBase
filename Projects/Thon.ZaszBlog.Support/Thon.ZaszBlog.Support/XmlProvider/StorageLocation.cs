using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.XmlProvider
{
    public partial class XmlStorageProvider : StorageProvider
    {
        // BlogSettings.Instance.StorageLocation will have say "~/App_Data/"
        //internal static string _Folder = System.Web.HttpContext.Current.Server.MapPath(BlogSettings.Instance.StorageLocation);
        // _Folder will have C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\ZaszBlog\

        // We need to have the exact path of the settings file so that 1st time BlogSettings is initiated,
        // it can load its settings from this file.
        //internal static string _SettingsFolder = System.Web.HttpContext.Current.Server.MapPath(Thon.Support.ThonSettings.Instance.ZaszBlogSettingsLocation);
        // _SettingsFolder will have C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\ZaszBlog\
        // which is same as _Folder becoz in BlogSettings.Instance.StorageLocation we will store "~/App_Data/ZaszBlog/"       

        //These are used when Winforms application is used to modify the settings
        internal static string _Folder = @"C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\App_Data\ZaszBlog\";
        internal static string _SettingsFolder = _Folder;

    }
}
