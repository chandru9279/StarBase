<%@ Page Language="C#" MasterPageFile="~/Starcraft/StarMaster.master" AutoEventWireup="true" CodeFile="Screenshots.aspx.cs" Inherits="Thon.Starcraft.ScreenshotsAspx" Title="ScreenShots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.screens a img
{
    
    margin:5px 2px 5px 2px;
    border-color:Silver;
    border-style:double;    
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StarCPH" Runat="Server">
    <div class="screens">
        <a href="FullScreenShot.aspx?image=ss19-hires.jpg"><img src="Effect/Thumbnails/ss19-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss42-hires.jpg"><img src="Effect/Thumbnails/ss42-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss28-hires.jpg"><img src="Effect/Thumbnails/ss28-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss31-hires.jpg"><img src="Effect/Thumbnails/ss31-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss35-hires.jpg"><img src="Effect/Thumbnails/ss35-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss36-hires.jpg"><img src="Effect/Thumbnails/ss36-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss37-hires.jpg"><img src="Effect/Thumbnails/ss37-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss38-hires.jpg"><img src="Effect/Thumbnails/ss38-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss39-hires.jpg"><img src="Effect/Thumbnails/ss39-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss40-hires.jpg"><img src="Effect/Thumbnails/ss40-hires.jpg" alt="Screenshot"/></a>
        <a href="FullScreenShot.aspx?image=ss41-hires.jpg"><img src="Effect/Thumbnails/ss41-hires.jpg" alt="Screenshot"/></a>
</div>
</asp:Content>

