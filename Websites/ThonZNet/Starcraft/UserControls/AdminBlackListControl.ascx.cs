using System;
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

namespace Thon.Starcraft
{
    public partial class AdminBlackListControlAscx : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {
                ThonStarcraftDataContext sdc = new ThonStarcraftDataContext();
                BlackList bl = new BlackList();
                bl.Name = Name.Text;
                bl.Genre = Genre.SelectedValue;
                bl.Rating = Int32.Parse(Rating.SelectedValue);
                bl.ID = Guid.NewGuid();
                sdc.BlackLists.InsertOnSubmit(bl);
                sdc.SubmitChanges();
            }
            catch (Exception e11)
            { ErrorLbl.Visible = true; }
        }
}
}
