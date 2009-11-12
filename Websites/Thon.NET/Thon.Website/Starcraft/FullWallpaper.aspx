<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FullWallpaper.aspx.cs" Inherits="Thon.Starcraft.FullWallpaperAspx" Title="Full-Size-Wallpaper" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheets/Common.css" rel="stylesheet" type="text/css" />
    <link runat="server" id="themelink" href="StyleSheets/BlueTexture.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div id="everything">
    <div id="header">
      <h1>StarCraft II Fansite</h1>
    </div>
    <div id="middle">
    <table>
    <tbody>
    <tr>
    <td>
        <center><h1>Starcraft II</h1></center>
        <center>To Download : Do Right-Click and Save As ..</center>
        <div style="width:900px; overflow:scroll;">
        <asp:PlaceHolder runat="server" ID="ScreenshotPlaceholder"></asp:PlaceHolder>
        </div>
        <br />
        <hr />
        <center>
        <asp:Button ID="ButtonPrev" runat="server" BorderStyle="None" 
                BackColor="Transparent" ForeColor="Silver" Font-Underline="true" 
                Text="Previous" onclick="ButtonPrev_Click" />
        <asp:Button ID="ButtonNext" runat="server" BorderStyle="None" 
                BackColor="Transparent" ForeColor="Silver" Font-Underline="true" Text="Next" 
                onclick="ButtonNext_Click" />
        </center>
        <hr />
        <center><a href="Wallpapers.aspx">Back</a> to Wallpapers listing<br /></center>
        <center><a href="Default.aspx">Back</a> to home</center>
        <hr />    
    </td>
    <td>
    <div id="right-image-bar" style="vertical-align:text-top top;">
        <img src="Effect/Avatar/armorer_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/hydralisk_80.gif" alt="Avatar" />
        <img src="Effect/Avatar/zerg3_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/kerrigan_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/mengsk_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/raynor_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/raynor2_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/tattoo1_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/tattoo2_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/tychus_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/valerian_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/zeratul_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/zerg1_80.gif" alt="Avatar"/>
        <img src="Effect/Avatar/zerg2_80.gif" alt="Avatar"/>
    </div>
    </td>
    </tr>
    <tr><td colspan="2">
    <div style="width:893px;background-color:#000000;background-image:url(Effect/Back.jpg);background-repeat:no-repeat;background-position:bottom left;background-attachment:fixed; overflow:hidden;">
    <div style="width:893px;height:274px;background-image:url(Effect/Mask.gif)" />    
    </div>
    </td></tr>
    </tbody>
    </table>
    
    </div>  
    
    
	<div id="footer">
    <div id="subnav"><a href="Default.aspx?theme=red">Red</a> | <a href="Default.aspx?theme=blue">
        Blue</a> | <a href="Default.aspx?theme=green">Green</a> | <a href="Default.aspx?theme=yellow">
        Yellow</a> | <a href="Default.aspx?theme=BlueTextured">Blue Textured</a></div>
    <span class="copyright">StarCraft® II and Blizzard Entertainment® are all trademarks 
        or registered trademarks of Blizzard Entertainment in the United States and/or 
        other countries. <br /> These terms and all related materials, logos, and images 
        are copyright © Blizzard Entertainment. This site is in no way associated with 
        or endorsed by Blizzard Entertainment®.</span></div>
    
</div>
</form></body>
</html>




