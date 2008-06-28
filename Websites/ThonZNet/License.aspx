<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="License.aspx.cs" Inherits="License" Title="Untitled Page" %>

<%@ Register Src="~/UserControls/License/CCAttribution25.ascx" TagPrefix="License" TagName="CCA25" %>
<%@ Register Src="~/UserControls/License/MicrosoftReciprocal.ascx" TagPrefix="License" TagName="MSRL" %>
<%@ Register Src="~/UserControls/License/CodeHighlight.ascx" TagPrefix="License" TagName="CodeHighlight" %>
<%@ Register Src="~/UserControls/License/GNULesser.ascx" TagPrefix="License" TagName="GNULGPL" %>
<%@ Register Src="~/UserControls/License/GNULibrary.ascx" TagPrefix="License" TagName="GNUGPL" %>
<%@ Register Src="~/UserControls/License/MicrosoftPublic.ascx" TagPrefix="License" TagName="MSPL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
<div style="height:790px;overflow:scroll;border-style:ridge;">
<%
    switch (Request.QueryString["license"])
    {
       case "cca25":
            %><License:CCA25 runat="server" ID="l0" /><%
           break;
       case "mspl":
            %><License:MSPL runat="server" ID="l1" /><%
           break;
       case "msrl":
            %><License:MSRL runat="server" ID="l2" /><%
           break;
       case "codehighlight":
            %><License:CodeHighlight runat="server" ID="l3" /><%
           break;
       case "gnulgpl":
            %><License:GNULGPL runat="server" ID="l4" /><%
           break;
       case "gnugpl":
            %><License:GNUGPL runat="server" ID="l5" /><%
           break;
       default :
            %><License:CCA25 runat="server" ID="l6" /><%
           break;
    }
           
%>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

