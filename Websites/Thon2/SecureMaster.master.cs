using System;
using Thon.Support;

namespace Thon
{
    public partial class SecureMaster : System.Web.UI.MasterPage
    {
        public partial class ThonMasterPage : System.Web.UI.MasterPage
        {
            protected string accessurl()
            {
                return HelperUtilities.InternetAppRoot.ToString();
            }
        }
    }
}
