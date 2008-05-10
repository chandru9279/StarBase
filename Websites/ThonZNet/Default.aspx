<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Thon.ThonDefaultAspx" MasterPageFile="~/ThonMasterPage.master" %>

<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="cphmain" EnableViewState="false" runat="server">    
    <div class="page" id="home">
    		<div id="WhatsNew">	
			<h3>What's New</h3>
			<p>Just wired up the site with Monster http handlers and Google Analytics, thinking 
            about placing ads, is that a good idea?</p>
            </div>
		
		    <div id="Welcome">
			<h3>Welcome	to the Site</h3>
			<p>This	is the Thiagarajan family personal site. In	it you will find weekly (in 
                future daily) updates on the variety of activities happening. Since I&#39;m looking 
                to bring up a full featured Web Content Management System as part of this site, 
                along with a lot of other Tid-Bits you won&#39;t find this coming out of beta for a 
                while now.</p>
            </div>
                
			<h3>Yeah, Thats us!</h3>
            <br />
            
            <table>
            
            <tr>
            <td style="width:150px"><p style="width:150px">You can see the human componant of the house on the first photo.
            All of them are scanned poor quality photos, soon they'll be replaced with
            hi-res images(my brother just bought a costly DigiCam).. Down below is the pet dog and cat, and all our induvidual images.
            Most of them were taken at home or during the all-india tour. Check out the Gallery
            for more images. Site is best viewed using 1280x1024 resolution, and in Internet Explorer.
            Needs more flash/silver-light content, and some better stylesheets, which will be here
            soon. Meanwhile , you can subscribe to RSS/ATOM feeds for any post or author, in the blog
            You'll probably need a feed reader, such as the Vista Sidebar gadget or you can get one for
            free <a href="http://www.feedreader.com/">here</a>. You'll be updated as posts and comments 
            get published. The blog is not confined to family, if you find an interested topic 
            and would like to post yourself, give <a href="mailto:chandru9279@gmail.com">me</a> an e-mail.
            Two roles exist in this site for users, one Administrative role for doing the maintainance and
            the developing and other is the Blogger role, this allows publishing of posts. Anonymous users,
            practically any one can comments, view those images in Gallery marked as Visible by 
            authenticated users. There's a Credits page for thanking and acknowledging the various sources 
            & OpenSource projects from which i stole, and customized into features in this site... be sure to 
            look through it, the hard-working guys deserve the credit. The entire site is SEO(Search Engine
             Optimized) in every possible way, robots.txt, search-engine-sitemap, meta-tags, ping-facility, 
             the entire works.... so if you have unique enough material on blog posts, you can get hit by 
             Google, Yahoo, Live searches.
            </p></td>
            <td rowspan="2">
			<div><table>
                <tr><td colspan="3"><img src="Mods/Famille.jpg" alt="Full Familley"/></td></tr>                
                <tr><td rowspan="2"><img src="Mods/Jayalakshmi.jpg" alt="T. Jayalakshmi"/></td> <td><img src="Mods/ArunKumar.jpg" alt="T. ArunKumar"/></td> <td><img src="Mods/Chandirasekar.jpg" alt="T. Chandirasekar"/></td> </tr>
                <tr><td colspan="2"><img src="Mods/Thiagarajan.jpg" alt="R. Thiagarajan"/></td></tr>
                <tr><td colspan="3"><img src="Mods/Kutty.jpg" alt="Kutty"/></td></tr>
                <tr><td colspan="3"><img src="Mods/Bones.jpg" alt="Bones & Familley"/></td></tr>                               
            </table></div>
            </td>
            </tr>
            
            <tr>
            <td>
            </td>
            </tr>
            
            </table>
            			
			<div id="LatestWork">
				<h3>My Latest Piece of Work</h3>
				<p>You are looking at it. This website features a Blog and a Gallery presently. Its 
                in Beta, and hopefully many more features will be added. Please suggest any 
                features to chandru9279@gmail.com</p>
			</div>
			
			<div id="OpenSource">
				<h3>OpenSource</h3>
			    <p>Will provide a download link in short order .. best way is to run a subversion 
                server on the host, which must be discussed with the hosting provider, soon 
                after which everyone will be able to obtain a working copy of this site, 
                licensed under MS-PL (Microsoft Permissive License) . Be happy if you can 
                provide feedback on the working and performance, and anything else i should 
                know.</p>
		    </div>
	</div>
</asp:Content>

<asp:Content ID="DefaultSideBarContent" ContentPlaceHolderID="cphsidebar" EnableViewState="false" runat="server">
</asp:Content>

<asp:Content ID="DefaultHeadContent" ContentPlaceHolderID="cphhead" EnableViewState="false" runat="server">
</asp:Content>