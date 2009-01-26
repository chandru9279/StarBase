<%@ Page Title="" Language="C#" MasterPageFile="~/Software/SoftMaster.master" AutoEventWireup="true" CodeFile="TwoOhApp.aspx.cs" Inherits="Thon.Warez.TwoOhAppAspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphsoftware" Runat="Server">
    
    
    <table cellpadding="5" cellspacing="5" style="font-family: Verdana,Tahoma; font-size: small;">
        <tr>
            <td>
                <center>
                    <h1>Two Oh App</h1>
                </center>
            </td>
        </tr>
        <tr>
            <td>
                <p>This app was also developed while working on Daybook <a href="http://www.daybook.in">http://www.daybook.in</a> The source is 
                proprietery (is that the right spelling?), The source for the functionality is not available.
                However basic connectivity can be seen in this app, a simplified version that all can use, that also only
                for academic purposes.
                </p>
                <ol>
                    <li>Connects to site and does variety of jobs</li>
                    <li>Uses the WCF(Windows Communication Foundation) fx</li>
                    <li>Uses PrincipalPermission Based declarative Security</li>
                    <li>Tray resident</li>
                    <li>Registers as start with windows, like antivirus s/w etc</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td>
                <center>The Two Oh App</center>
                <a href="Images/DaybookApp.jpg" target="_blank"><img alt="2.0 App" src="Images/DaybookApp.jpg" height="560" width="700"/></a>
            </td>
        </tr>
        <tr>
            <td>
                <p>.NET 2.0 The tray resident occassionally contacts the site and notifies updates, such as events, schedules, contact updates.</p>
            </td>
        </tr>
        
        <tr>
            <td>
                <p>The screenshot is actually old. This is a newer one : An MDI (Multiple Document Interface)</p>
                <a href="Images/2ohapp.jpg" target="_blank"><img alt="2.0 App" src="Images/2ohapp.jpg" height="437" width="700"/></a>
            </td>
        </tr>
        
        <tr>
            <td>
                <center>The Tray Resident</center>
                <center><a href="Images/Mini/bubble.jpg" target="_blank"><img src="Images/Mini/bubble.jpg" /></a></center>
            </td>
        </tr>
        <tr>
            <td>
                <center>
                    <a href="javascript:alert('Please wait, download will be available within a few days');">
                        <img alt="download" src="../Images/Download.gif" style="border: none; border-width: 0px;" /></a>
                    <br />
                    Download source
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

