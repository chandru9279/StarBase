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
    public partial class KonvictControlAscx : System.Web.UI.UserControl
    {
        private string name;
        public string Name
        {
            get
            { return name; }

            set
            { name = value; }
        }

        private string imagename;
        public string ImageName
        {
            get
            { return imagename; }

            set
            { imagename = value; }
        }

        private int number;
        public int KNumber
        {
            get
            { return number; }

            set
            { number = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}