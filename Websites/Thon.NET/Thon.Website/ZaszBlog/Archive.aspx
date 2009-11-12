<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Archive.aspx.cs" Inherits="ArchiveAspx" EnableViewState="false" EnableTheming="true" Title="Archive" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master"%>

<asp:Content ID="ArchiveAspxContent" ContentPlaceHolderID="cphBody" Runat="Server">
  <div id="archive">
    <h1>Archive</h1>
    <ul runat="server" id="ulMenu" />
    <asp:placeholder runat="server" id="phArchive" />
    <br />
    
    <h2>Total</h2>
    <span><asp:literal runat="server" id="ltPosts" /></span><br />
    <span><asp:literal runat="server" id="ltComments" /></span><br />
    <span><asp:literal runat="server" id="ltRaters" /></span>
  </div>
</asp:Content>