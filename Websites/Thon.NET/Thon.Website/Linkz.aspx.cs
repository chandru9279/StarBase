﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Thon.Support.Web.Controls;

namespace Thon
{
    public partial class LinkzAspx : ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AddMetaTag("keywords", "links");
        }
    }
}
