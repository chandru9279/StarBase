<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="Linkz.aspx.cs" Inherits="Thon.LinkzAspx" Title="Cool Linkz" %>
<%@ Register src="~/UserControls/Widgets/News.ascx" tagname="News" tagprefix="Google" %>
<%@ Register src="~/UserControls/Widgets/WebCrawlerSpidey.ascx" tagname="WebCrawlerSpidey" tagprefix="Google" %>
<%@ Register src="~/UserControls/Widgets/MusicSearch.ascx" tagname="MusicSearch" tagprefix="Google" %>
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
    <table cellspacing="5px" cellpadding="5px" border="2px">
    <tr><td>The Thon.Support & Thon.ZaszBlog.Support dll (<%=DateTime.Now.Date.ToString("d")%>)</td><td><a href="Downloads/Site.zip"> <img style="border:none;border-width:0px;" src="Images/Download.gif" alt="Download" /></a></td></tr>
    <tr><td>The VWD Website svn working copy (<%=DateTime.Now.Date.ToString("d")%>)</td><td><a href="Downloads/Assembly.zip"> <img style="border:none;border-width:0px;" src="Images/Download.gif" alt="Download" /></a></td></tr>
    </table>
    <table cellspacing="10px">
    <tr><td><Google:MusicSearch ID="MusicSearch1" runat="server" /></td></tr>
    <tr><td><Google:News ID="News1" runat="server" /></td></tr></table>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
    <center><Google:TowerAd runat="server" ID="googletowerad" /></center>
</asp:Content>

