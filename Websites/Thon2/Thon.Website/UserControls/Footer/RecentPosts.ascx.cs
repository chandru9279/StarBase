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
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon
{
    public partial class RecentPostAscx : System.Web.UI.UserControl
    {
        private Post pl = null, psl= null;
        public string title1
        {
            get { if (pl != null) return pl.Title; else return "No posts."; }
        }
        public string title2
        {
            get { if (psl != null) return psl.Title; else return "Only one post."; }
        }

        public string desc1
        {
            get { if (pl != null) return pl.Title; else return "No posts."; }
        }
        public string desc2
        {
            get { if (psl != null) return psl.Title; else return "Only one post."; }
        }

        public string perma1
        {
            get { if (pl != null) return pl.PermaLink.ToString(); else return "javascript:alert(&quot;Sorry! Nothing more to read about.&quot;)"; }
        }
        public string perma2
        {
            get { if (psl != null) return psl.PermaLink.ToString(); else return "javascript:alert(&quot;Sorry! Nothing more to read about.&quot;)"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            pl = Post.Posts[r.Next(Post.Posts.Count)];
            psl = Post.Posts[r.Next(Post.Posts.Count)];
        }
    }
}
