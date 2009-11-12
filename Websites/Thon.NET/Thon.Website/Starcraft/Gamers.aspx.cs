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
using System.IO;
using System.Collections.Generic;

namespace Thon.Starcraft
{
    public partial class GamersAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string [] images = Directory.GetFiles(Server.MapPath("~/Starcraft/Effect/Gamers"));
            for (int i = 0; i < images.Length; i++)
                images[i] = images[i].Substring(images[i].LastIndexOf(@"\") + 1);
            List<string> imns = new List<string>(images);
            imns.Remove("Me.jpg");
            imns.Remove("KonvictEffect.gif");
            KonvictControlAscx kca;
            kca = (KonvictControlAscx)LoadControl("~/Starcraft/UserControls/KonvictControl.ascx");
            kca.KNumber = 1;
            kca.ImageName = "Me.jpg";
            kca.Name = "Me";
            gamerimages.Controls.Add(kca);
            for (int i = 0; i < imns.Count; i++)
            {
                kca = (KonvictControlAscx)LoadControl("~/Starcraft/UserControls/KonvictControl.ascx");
                kca.KNumber = i + 2;
                kca.ImageName = imns[i];
                kca.Name = imns[i].Substring(0, imns[i].Length - 4);
                gamerimages.Controls.Add(kca);
            }
        }
    }
}
