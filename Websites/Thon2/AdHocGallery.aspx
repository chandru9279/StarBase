<%@ Page Title="" Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="AdHocGallery.aspx.cs" Inherits="Thon.AdHocGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
    <div style="margin-left:20px;">
    Actual size images matching ' <%=Request.QueryString["folder"] %> '<br />    
    </div>
    <asp:Table ID="Table1" CellPadding="30" CellSpacing="10" runat="server">
    </asp:Table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

