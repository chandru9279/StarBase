<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="Linkz.aspx.cs" Inherits="Thon.LinkzAspx" Title="Cool Linkz" %>

<%@ Register src="UserControls/Widgets/News.ascx" tagname="News" tagprefix="Google" %>
<%@ Register src="UserControls/Widgets/WebCrawlerSpidey.ascx" tagname="WebCrawlerSpidey" tagprefix="Google" %>
<%@ Register src="UserControls/Widgets/MusicSearch.ascx" tagname="MusicSearch" tagprefix="Google" %>
<%@ Register src="~/UserControls/AdSense/TowerAd.ascx"tagname="TowerAd" tagprefix="Google" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">    
    <Google:WebCrawlerSpidey ID="WebCrawlerSpidey1" runat="server" />
    <br /><h2>Some Cool Links, folks!</h2><br />
    <ul class="normal">
    <li><a href="http://www.asp.net/">The Official ASP.NET website</a> - Great for downloading many useful utils.</li>
    <li><a href="http://www.codeplex.com/">The Open Source Microsoft.NET Hub</a> - Contribute here.</li>
    <li><a href="http://www.divxcrawler.com/">DivX Crawler</a> - Direct Download movies.</li>
    <li><a href="http://www.thepiratebay.org/">The Pirate Bay</a> - Statuatory Warning : Don't go here.</li>
    <li><a href="http://www.gamespot.com/">GameSpot</a> - Best Reviews & News.</li>
    <li><a href="http://chandruon.net/chandirasekar">My subdomain (http://chandirasekar.thiagarajan.net/)</a> 
        - Currently has my resumé.</li>
    </ul> 
    <br /><h2>Download this site source </h2><br />
    <ul>
    <li><a href="Thon.Support.zip"><img src="Images/Download.gif" alt="Download" /></a><%=DateTime.Now.Date.ToString("d")%> The Thon.Support & Thon.ZaszBlog.Support dll  </li>
    <li><a href="Thon.zip"><img src="Images/Download.gif" alt="Download" /></a><%=DateTime.Now.Date.ToString("d")%> The VWD Website svn working copy </li>
    </ul>   
    <table><tr><td><Google:MusicSearch ID="MusicSearch1" runat="server" /></td><td><Google:News ID="News1" runat="server" /></td></tr></table>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
    <Google:TowerAd runat="server" ID="googletowerad" />
</asp:Content>

