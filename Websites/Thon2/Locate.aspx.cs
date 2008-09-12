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
    public partial class LocateAspx : ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AddMetaTag("keywords", "crib,location,address,Plot No 66-A,Door 10,Second street,Venkatesa Nagar,Virugambakkam,Chennai,PIN 600-092,Tamil Nadu,India");            
        }
    }
}
