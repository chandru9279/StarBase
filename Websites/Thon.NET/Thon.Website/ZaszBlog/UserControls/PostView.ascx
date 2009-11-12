<%@ Control Language="C#" EnableTheming="true" AutoEventWireup="true" EnableViewState="false" Inherits="Thon.ZaszBlog.Support.Web.Controls.PostViewBase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ACT" %>
<%@ Register Src="~/UserControls/AdSense/BannerAd.ascx" TagName="BannerAd" TagPrefix="Google" %>

<asp:Panel BackColor="White" runat="server" ID="PostPanel">
    <div class="post">        
        <h1><a class="postheader taggedlink" href="<%=Post.RelativeLink %>"><%=Post.Title %></a></h1>
        <div class="descr"><img id="Img1" src="~/ZaszBlog/Images/MasterPageUserControls/TimeIcon.gif" runat="server" alt="clock" /> <%=Post.DateCreated.ToString("MMMM d, yyyy HH:mm")%> by <img id="Img2" src="~/ZaszBlog/Images/MasterPageUserControls/Author.gif" runat="server" alt="author" /> <a href="<%=VirtualPathUtility.ToAbsolute("~/ZaszBlog/") + "author/" + Post.Author %>.aspx"><%=Post.Author %></a></div>
        <div class="entry"><asp:PlaceHolder ID="BodyContent" runat="server" /></div>
        <%=Rating %>
        <br />
        <div class="postfooter">
            Tags: <%=TagLinks(", ") %><br />
            Categories: <%=CategoryLinks(" | ") %><br />
            Actions: <%=AdminLinks %>
            <a rel="nofollow" href="mailto:?subject=<%=Post.Title %>&amp;body=Thought you might like this: <%=Post.AbsoluteLink.ToString() %>">E-mail</a> | 
            <a rel="nofollow" href="http://www.dotnetkicks.com/submit?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>">Kick it!</a> |
            <a href="<%=Post.PermaLink %>" rel="bookmark">Permalink</a> |
            <a rel="nofollow" href="<%=Post.RelativeLink %>#comment">
                 <img id="Img4" runat="server" alt="comment" src="~/ZaszBlog/Images/MasterPageUserControls/Comments.gif" />Comments (<%=Post.ApprovedComments.Count %>)</a>
             |   
            <a rel="nofollow" href="<%=CommentFeed %>"><asp:Image ID="Image1" runat="Server" ImageUrl="~/ZaszBlog/Images/RSSButton.gif" AlternateText="RSS comment feed" style="margin-right:3px" />Comment RSS</a> | <a href="javascript:bookmarksite()" rel="Bookmark">Bookmark/Favorites</a> 
        </div>
    </div>
</asp:Panel>
<ACT:RoundedCornersExtender 
runat="server" 
TargetControlID="PostPanel" 
ID="PostPanelRCExtender"
Corners="All"
Radius="5"
Enabled="true"        
Color="Maroon"        
/>
<br />