<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Thon.Default" MasterPageFile="~/ThonMasterPage.master" %>

<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="cphmain" EnableViewState="false" runat="server">    
    <div class="page" id="home">			
			<h4>My Latest Piece of Work</h4>
			<p>You are looking at it. This website features a Blog and a Gallery presently. Its 
                in Beta, and hopefully many more features will be added. Please suggest any 
                features to chandru9279@gmail.com</p>
		
		
			<h3>Welcome	to My Site</h3>
			<p>This	is the Thiagarajan family personal site. In	it you will find weekly (in 
                future daily) updates on the variety of activities happening. Since I&#39;m looking 
                to bring up a full featured Web Content Management System as part of this site, 
                along with a lot of other Tid-Bits you won&#39;t find this coming out of beta for a 
                while now.</p>
                
			<h4>Yeah, Thats us!</h4>
            <p>&nbsp;</p>
			<div style="align:center">
			<table>
                <tr><td colspan="3"><img src="Mods/Famille.jpg" /></td></tr>
                
                <tr><td rowspan="2"><img src="Mods/Jayalakshmi.jpg" /></td> <td><img src="Mods/ArunKumar.jpg" /></td> <td><img src="Mods/Chandirasekar.jpg" /></td> </tr>
                <tr><td colspan="2"><img src="Mods/Thiagarajan.jpg" /></td></tr>
                <tr><td colspan="3"><img src="Mods/Kutty.jpg" /></td></tr>
                <tr><td colspan="3"><img src="Mods/Bones.jpg" /></td></tr>                
            </table>
			</div>
			
			<div id="whatsnew">
			
				<h4>What's New</h4>
				<p>Just wired up the site with monster http handlers and Google Analytics, thinking 
                    about placing ads, is that a good idea?</p>
			</div>
			
			<div id="coollinks">
				<h4>Cool Links</h4>
				<ul	class="link">
					<li><a href="#">Lorem ipsum dolositionr</a></li>
					<li><a href="#">Lorem ipsum dolositionr</a></li>
					<li><a href="#">Lorem ipsum dolositionr</a></li>
					<li><a href="#">Lorem ipsum dolositionr</a></li>
					<li><a href="#">Lorem ipsum dolositionr</a></li>
				</ul>
			</div>
			
			<hr	/>
			<p>Lorem<a href="#"> ipsum dolor sit amet</a>, consectetuer	adipiscing elit, sed diam nonummy nibh euismod tincidunt ut	laoreet	dolore magna aliquam erat volutpat.	Ut wisi	enim ad	minim veniam, quis nostrud exercitation	consequat. Duis	autem veleum iriure	dolor in hendrerit in vulputate	velit esse molestie	consequat, vel willum.</p>
		</div>
	</div>
</asp:Content>

<asp:Content ID="DefaultSideBarContent" ContentPlaceHolderID="cphsidebar" EnableViewState="false" runat="server">
</asp:Content>

<asp:Content ID="DefaultHeadContent" ContentPlaceHolderID="cphhead" EnableViewState="false" runat="server">
</asp:Content>