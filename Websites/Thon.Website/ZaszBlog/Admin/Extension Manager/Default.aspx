<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ExtensionsDefaultAspx" Title="Extensions" %>
<%@ Reference Control = "Extensions.ascx" %>
<%@ Reference Control = "Editor.ascx" %>
<%@ Reference Control = "Settings.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
<br />
<div class="settings">
    <asp:HiddenField ID="args" runat="server" />
    <div>
        <asp:PlaceHolder ID="ucPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
</div>
</asp:Content>
