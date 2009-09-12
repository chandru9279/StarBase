using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support;
using Thon.ZaszBlog.Support.Web.Controls;

namespace Thon
{
    public partial class ThonDefaultAspx : Thon.Support.Web.Controls.ThonBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try { string welcome = Request.QueryString[0]; }
            catch (ArgumentOutOfRangeException loading) { Server.Transfer("Index.html"); loading = null; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = SupportUtilities.RelativeWebRoot + "/UserControls/PostView.ascx";
            int counter = 0, count = Post.Posts.Count;


            for (int i = 0; i < count; i++ )
            {
                Post post = Post.Posts[i];
                if (counter == 2)
                    break;

                if (post.IsVisible || Page.User.Identity.IsAuthenticated)
                {
                    PostViewBase postView = (PostViewBase)LoadControl(path);
                    postView.ShowExcerpt = true;
                    postView.Post = post;
                    postView.Location = ServingLocation.PostList;
                    posts.Controls.Add(postView);
                    counter++;
                }
            }
        }
    }
}
