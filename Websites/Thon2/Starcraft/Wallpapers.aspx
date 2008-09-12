<%@ Page Language="C#" MasterPageFile="~/Starcraft/StarMaster.master" AutoEventWireup="true" CodeFile="Wallpapers.aspx.cs" Inherits="Thon.Starcraft.WallpapersAspx" Title="Wallpapers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
.walltable tr td
{    
    border-color:Silver;
    border-style:dashed;
    border-spacing:2px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StarCPH" Runat="Server">
<table class="walltable" cellspacing="5px">
<thead>
<tr><th colspan="2">Wallpapers</th></tr>
</thead>
<tbody>
<tr>
<td><a href="FullWallpaper.aspx?image=Bel_Shir_environment.jpg">    <img src="Effect/Thumbnails/Walls/Bel_Shir_environment.jpg" alt="Bel_Shir_environment"/></a></a></td>
<td><a href="FullWallpaper.aspx?image=HallWay_environment.jpg">    <img src="Effect/Thumbnails/Walls/HallWay_environment.jpg" alt="HallWay_environment"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=Protoss_Immortal_concept.jpg">    <img src="Effect/Thumbnails/Walls/Protoss_Immortal_concept.jpg" alt="Protoss_Immortal_concept"/></a></td>
<td><a href="FullWallpaper.aspx?image=Kel-Mor_concept.jpg">    <img src="Effect/Thumbnails/Walls/Kel-Mor_concept.jpg" alt="Mor_concept"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=MarSara_environment.jpg">    <img src="Effect/Thumbnails/Walls/MarSara_environment.jpg" alt="MarSara_environment"/></a></td>
<td><a href="FullWallpaper.aspx?image=Protoss_Cinematic.jpg">    <img src="Effect/Thumbnails/Walls/Protoss_Cinematic.jpg" alt="Protoss_Cinematic"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=Terran_Marine_cinematic.jpg">    <img src="Effect/Thumbnails/Walls/Terran_Marine_cinematic.jpg" alt="Terran_Marine_cinematic"/></a></td>
<td><a href="FullWallpaper.aspx?image=Protoss_Zealot_cinematic.jpg">    <img src="Effect/Thumbnails/Walls/Protoss_Zealot_cinematic.jpg" alt="Protoss_Zealot_cinematic"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=Relic_Crater_cinematic.jpg">    <img src="Effect/Thumbnails/Walls/Relic_Crater_cinematic.jpg" alt="Relic_Crater_cinematic"/></a></td>
<td><a href="FullWallpaper.aspx?image=protoss_Orthos_concept.jpg">    <img src="Effect/Thumbnails/Walls/protoss_Orthos_concept.jpg" alt="protoss_Orthos_concept"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=wallpaper-nuke.jpg">    <img src="Effect/Thumbnails/Walls/wallpaper-nuke.jpg" alt="nuke"/></a></td>
<td><a href="FullWallpaper.aspx?image=wallpaper-marine.jpg">    <img src="Effect/Thumbnails/Walls/wallpaper-marine.jpg" alt="marine"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=protoss_concept.jpg">    <img src="Effect/Thumbnails/Walls/protoss_concept.jpg" alt="protoss_concept"/></a></td>
<td><a href="FullWallpaper.aspx?image=wallpaper-zeratul2.jpg">    <img src="Effect/Thumbnails/Walls/wallpaper-zeratul2.jpg" alt="zeratul2"/></a></td>
</tr>
<tr>
<td><a href="FullWallpaper.aspx?image=Terran_Viking_concept.jpg">    <img src="Effect/Thumbnails/Walls/Terran_Viking_concept.jpg" alt="Terran_Viking_concept"/></a></td>
<td style="border-style:none;">    </td>
</tr>
</tbody>
</table>
</asp:Content>

