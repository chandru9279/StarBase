<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page.aspx.cs" Inherits="PageAspx" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master"%>
<asp:content id="PageMain" contentplaceholderid="cphBody" runat="Server">
    <div id="page">
    <h1 runat="server" id="h1Title" />
    <div runat="server" id="divText" />
    <%=AdminLinks %>
  </div>
</asp:content>