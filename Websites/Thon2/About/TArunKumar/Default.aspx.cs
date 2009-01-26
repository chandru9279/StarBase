using System;
using Thon.Support.Web.Controls;

namespace Thon.About
{
    public partial class TAKDefaultAspx : ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AddMetaTag("keywords", "T Arun Kumar");
        }
    }
}
