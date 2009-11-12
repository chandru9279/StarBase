<%@ Page Language="C#" MasterPageFile="~/Starcraft/StarMaster.master" AutoEventWireup="true" CodeFile="Artwork.aspx.cs" Inherits="Thon.Starcraft.ArtworkAspx" Title="Artwork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
center
{
    color:Silver;
}
center a
{
    color:Silver;
}
center a:visited
{
    color:White;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StarCPH" Runat="Server">
<br />
<center><a href="Artwork.aspx?faction=terran">Terran</a> | <a href="Artwork.aspx">Protoss</a></center>
<% if (string.IsNullOrEmpty(Request.QueryString["faction"]))
   { %>
    <img src="Effect/Protoss/Colossus.png" alt="Colossus" />
    <center>Colossus</center>
    <img src="Effect/Protoss/Zealot.png" style="width:600px" alt="Zealot" />
    <center>Zealot</center>
    <img src="Effect/Protoss/DarkTemplar.png"  alt="DarkTemplar" />
    <center>DarkTemplar</center>
    <img src="Effect/Protoss/Mothership.png" alt="Mothership"/>
    <center>Mothership</center>
    <img src="Effect/Protoss/Phoenix.png" alt="Phoenix"/>
    <center>Phoenix</center>
    <img src="Effect/Protoss/Reaver.png" alt="Reaver"/>
    <center>Reaver</center>
    <img src="Effect/Protoss/Stalker.png" alt="Stalker"/>
    <center>Stalker</center>
    <%}
   else
   {%>
    <img src="Effect/Terran/Banshee.png" alt="Banshee"/>
    <center>Stalker</center>
    <img src="Effect/Terran/Thor.png" alt="Thor"/>
    <center>Thor</center>
    <img src="Effect/Terran/Ghost.png" alt="Ghost"/>
    <center>Ghost</center>
    <img src="Effect/Terran/Marine.png" alt="Marine"/>
    <center>Marine</center>
    <img src="Effect/Terran/Reaper.png" alt="Reaper"/>
    <center>Reaper</center>
    <img src="Effect/Terran/SCV.png" alt="SCV"/>
    <center>SCV</center>
    <%} %>
</asp:Content>

