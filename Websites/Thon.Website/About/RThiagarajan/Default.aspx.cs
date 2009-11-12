using System;
using Thon.Support.Web.Controls;

namespace Thon.About
{
    public partial class RTDefaultAspx : ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AddMetaTag("keywords", "R Thiagarajan");
        }
    }
}
