<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Thon.ZaszBlog.ZaszBlogDefaultAspx" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master" %>
<%@ Register Namespace="Thon.ZaszBlog.Controls" TagPrefix="ZaszBlog" %>
<%@ Register Src="~/ZaszBlog/UserControls/PostList.ascx" TagName="PostList" TagPrefix="PL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <PL:PostList ID="PostList1" runat="server" />
  <ZaszBlog:PostCalendar runat="server" ID="calendar" 
    EnableViewState="false"
    ShowPostTitles="true" 
    BorderWidth="0"
    NextPrevStyle-CssClass="header"
    CssClass="calendar" 
    WeekendDayStyle-CssClass="weekend" 
    OtherMonthDayStyle-CssClass="other" 
    UseAccessibleHeader="true" 
    Visible="false" 
    Width="100%" />
</asp:Content>