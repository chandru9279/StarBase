<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="False" CodeFile="Post.aspx.cs" Inherits="PostAspx" MasterPageFile="~/ZaszBlog/SecureZaszBlogMaster.master" %>
<%@ Register Src="UserControls/CommentViewContainer.ascx" TagName="CommentViewer" TagPrefix="CVC" %>
<%@ Register Namespace="Thon.ZaszBlog.Controls" TagPrefix="ZaszBlog" %>

<asp:content id="PostContent" contentplaceholderid="cphBody" runat="Server">
    <asp:placeholder runat="server" id="phPostNavigation" visible="false">
        <div id="postnavigation">
        <asp:hyperlink runat="server" id="hlPrev" /> | 
        <asp:hyperlink runat="server" id="hlNext" />
        </div>
    </asp:placeholder>

    <asp:placeholder runat="server" id="pwPost" />

    <asp:placeholder runat="server" id="phRDF">        
    <rdf:RDF xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:trackback="http://madskills.com/public/xml/rss/module/trackback/">
    <rdf:Description rdf:about="<%=Post.AbsoluteLink %>" dc:identifier="<%=Post.AbsoluteLink %>" dc:title="<%=Post.Title %>" trackback:ping="<%=Post.TrackbackLink %>" />
    </rdf:RDF>
    </asp:placeholder>
    <ZaszBlog:RelatedPosts runat="server" ID="related" MaxResults="3" ShowDescription="true" DescriptionMaxLength="100" />
    <CVC:CommentViewer ID="CommentView1" runat="server" />
</asp:content>