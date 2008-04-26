<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/ThonMasterPage.master" EnableViewState="False" ValidateRequest="false" %>
<%@ Register Src="~/UserControls/GalleryControl.ascx" TagName="GalleryControl" TagPrefix="Gallery" %>

<asp:Content ID="ChildContent" ContentPlaceHolderID="cphmain" Runat="Server">
    <Gallery:GalleryControl runat="server" ID="GalleryControl" PageSize="25" Owner="Admin" FilePath="~/App_Data/Gallery" PopupPage="~/GalleryPopup.aspx" ItemCSSClasses="PhotoItemHidden,PhotoItem" IncludeCategories="true" IncludeSort="true" IncludeSearch="true" IncludePageSize="true" />
</asp:Content>