using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thon.Support.Web.Controls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Thon
{
    public partial class AdHocGallery : ThonBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string which = Request.QueryString["folder"];

            if (which == null) Response.Redirect("Errors/404.aspx");
            if (which == string.Empty) Response.Redirect("Errors/404.aspx");
            string[] imges = { "" };

            try
            {
                imges = Directory.GetFiles(Server.MapPath("~/Images/AdHocGallery/" + which));
            }
            catch
            {
                Response.Redirect("Errors/404.aspx");
            }

            if ((imges.Length == 1) && imges[0] == "") Response.Redirect("Errors/404.aspx");

            foreach (string item in imges)
            {
                string actname = item.Substring(item.LastIndexOf('\\') + 1);
                actname = "Images/AdHocGallery/" + which + "/" + actname;
                TableCell tc = new TableCell();
                TableRow tr = new TableRow();
                HtmlGenericControl c = new HtmlGenericControl("c");
                HtmlGenericControl img = new HtmlGenericControl("img");
                img.Attributes.Add("src", actname);
                img.Attributes.Add("height", "450px");
                img.Attributes.Add("width", "450px");
                c.Controls.Add(img);
                tc.Controls.Add(c);
                tr.Cells.Add(tc);
                Table1.Rows.Add(tr);
            }
        }
    }
}
