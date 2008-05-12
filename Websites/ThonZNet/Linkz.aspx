<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="Linkz.aspx.cs" Inherits="Thon.LinkzAspx" Title="Cool Linkz" %>

<%@ Register src="UserControls/News.ascx" tagname="News" tagprefix="Google" %>
<%@ Register src="UserControls/WebCrawlerSpidey.ascx" tagname="WebCrawlerSpidey" tagprefix="Google" %>
<%@ Register src="UserControls/MusicSearch.ascx" tagname="MusicSearch" tagprefix="Google" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">    
    <Google:WebCrawlerSpidey ID="WebCrawlerSpidey1" runat="server" />
    <h2>Some Cool Links, folks!</h2>
    <ul>
    <li><a href="http://www.asp.net/">The Official ASP.NET website</a> - Great for downloading many useful utils.</li>
    <li><a href="http://www.codeplex.com/">The Open Source Microsoft.NET Hub</a> - Contribute here.</li>
    <li><a href="http://www.divxcrawler.com/">DivX Crawler</a> - Direct Download movies.</li>
    <li><a href="http://www.thepiratebay.org/">The Pirate Bay</a> - Statuatory Warning : Don't go here.</li>
    <li><a href="http://www.gamespot.com/">GameSpot</a> - Best Reviews & News.</li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
    <br /><br />
    <Google:MusicSearch ID="MusicSearch1" runat="server" />
    <br /><br />
    <Google:News ID="News1" runat="server" />
</asp:Content>

