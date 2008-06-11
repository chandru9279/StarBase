<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Thon.ThonDefaultAspx" MasterPageFile="~/ThonMasterPage.master" %>
<%@ Register 
    Assembly="AjaxControlToolkit" 
    Namespace="AjaxControlToolkit" 
    TagPrefix="ajaxToolkit" %>
    
<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="cphmain" EnableViewState="false" runat="server">        
        <div class="inner_banner">
        <table>
            <tr><td>
            <p class="worksheet">Check Out</p>
            </td></tr>
            <tr><td>
            <p> 
                <a href="Linkz.aspx"class="innerlink">Linkz & Fun </a>
                <a href="Credits.aspx" class="innerlink">Credits </a>
                <a href="http://vcan.12gbfree.com/" class="innerlink">N. G. Karthikeyan's Portal</a>
                <a href="ZaszBlog/Archive.aspx" class="innerlink">Archive </a> 
                <a href="http://www.google.com/search?q=ZaszBlog&ie=utf-8&oe=utf-8" class="innerlink">Proving SEO</a>
		        <a href="http://www.oceansthirteen.com" class="innerlink">Ocean's 11, 12, 13 fan</a>
		    </p>
		    </td></tr>
        </table>
        </div>
       
       
             
            
     <script runat="Server" type="text/C#">
         [System.Web.Services.WebMethod]
         [System.Web.Script.Services.ScriptMethod]
         public static AjaxControlToolkit.Slide[] GetSlides()
         {
             return new AjaxControlToolkit.Slide[] { 
            new AjaxControlToolkit.Slide("Images/Mods/1.jpg", "Me & Pop", "Srinagar Fall"),
            new AjaxControlToolkit.Slide("Images/Mods/2.jpg", "Jaya & Thiagu", "Rotang Pass"),
            new AjaxControlToolkit.Slide("Images/Mods/3.jpg", "All four", "Skiing"),
            new AjaxControlToolkit.Slide("Images/Mods/4.jpg", "Vasanth & Mahesh", "Skiing"),
            new AjaxControlToolkit.Slide("Images/Mods/5.jpg", "Pop & Mom", "Ski"),
            new AjaxControlToolkit.Slide("Images/Mods/6.jpg", "Bones & Family", "1st Floor"),
            new AjaxControlToolkit.Slide("Images/Mods/7.jpg", "Arun", "Forbidden City"),
            new AjaxControlToolkit.Slide("Images/Mods/8.jpg", "Mahesh", "Great Wall"),
            new AjaxControlToolkit.Slide("Images/Mods/9.jpg", "Mahesh", "E.T."),
            new AjaxControlToolkit.Slide("Images/Mods/10.jpg", "Arun", "Tongli Temple"),
            new AjaxControlToolkit.Slide("Images/Mods/11.jpg", "Mini-Me", "China"),
            new AjaxControlToolkit.Slide("Images/Mods/12.jpg", "Thiagu", "Manaali"),
            new AjaxControlToolkit.Slide("Images/Mods/13.jpg", "Vasanth", "PSP & Comp"),
            new AjaxControlToolkit.Slide("Images/Mods/14.jpg", "Mom", "Rotang Pass"),
            new AjaxControlToolkit.Slide("Images/Mods/15.jpg", "Cool", "XBOX 360"),
            new AjaxControlToolkit.Slide("Images/Mods/16.jpg", "Taj Mahal", "Delhi"),
            new AjaxControlToolkit.Slide("Images/Mods/17.jpg", "Mahesh", "Shanghai"),
            };
         }
    </script> 
			<h3></h3>
			
            
    <asp:Panel ID="WelPanel1" runat="server" Style="cursor: pointer;color:Maroon;font-size:larger;font-style:normal;font-weight:bold;">
        <div class="heading">
            <asp:ImageButton ID="WelImageButton" runat="server" ImageUrl="~/ZaszBlog/Images/MasterPageUserControls/Collapse.jpg" AlternateText="collapse" Enabled="False" />
            &nbsp;&nbsp;Welcome!
        </div>
    </asp:Panel>
    <asp:Panel ID="WelPanel2" runat="server" Style="overflow: hidden;">
        <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;">&nbsp;&nbsp; You have reached the Online Residence of T.Arun Kumar, R. Thiagarajan,  T. Jayalakshmi, T. Chandirasekar.
			And Kuttys(Dog) and Bones(Cat). You can see us in the slideshow. Most of them were taken at home or during 
			the All-India tour. Check out the Gallery for more images. Soon they'll be replaced with hi-res images(my 
			brother just bought a costly DigiCam) Meanwhile , you can subscribe to RSS/ATOM feeds for any post or author,
			in the blog You'll probably need a feed reader, such as the Vista Sidebar gadget or you can get one for
            free <a href="http://www.feedreader.com/">here</a>The blog is not confined to family, if you find an interested topic 
            and would like to post yourself, give <a href="mailto:chandru9279@gmail.com">me</a> an e-mail.
            The entire site is SEO &#39;d(Search Engine Optimized) in every possible way, robots.txt, search-engine-sitemap,
            meta-tags, ping-facility, the entire works.... so if you have unique enough material on blog posts, you can get 
            hit by Google, Yahoo, Live searches.
            </p>
    </asp:Panel>
        <ajaxToolkit:CollapsiblePanelExtender ID="WelCollapsiblePanelExtender" 
            runat="server" 
            Enabled="True" 
            TargetControlID="WelPanel2"
            ExpandControlID="WelPanel1" 
            CollapseControlID="WelPanel1"
            Collapsed="True" 
            ImageControlID="WelImageButton" 
            CollapsedImage="~/ZaszBlog/Images/MasterPageUserControls/Expand.jpg"
            ExpandedImage="~/ZaszBlog/Images/MasterPageUserControls/Collapse.jpg"
            /> 
            <div style="text-align:center">
            <asp:Label runat="Server" ID="imageTitle" /><br />
            <asp:Image ID="Image1" runat="server" 
                Height="342"
                Style="border: 2px solid black;width:auto" 
                ImageUrl="Images/Mods/Bones.jpg"
                AlternateText="ThonSlideShow" /><br />
            <asp:Label runat="server" ID="imageDescription"></asp:Label><br /><br />
            <asp:Button runat="Server" ID="prevButton" Text="Prev" Font-Size="Larger" />
            <asp:Button runat="Server" ID="playButton" Text="Play" Font-Size="Larger" />
            <asp:Button runat="Server" ID="nextButton" Text="Next" Font-Size="Larger" />
            <ajaxToolkit:SlideShowExtender ID="slideshowextend1" runat="server"                         
                TargetControlID="Image1"
                SlideShowServiceMethod="GetSlides" 
                AutoPlay="true"                 
                ImageTitleLabelID="imageTitle"
                ImageDescriptionLabelID="imageDescription"
                NextButtonID="nextButton" 
                PlayButtonText="Play" 
                StopButtonText="Stop"
                PreviousButtonID="prevButton" 
                PlayButtonID="playButton" 
                Loop="true" />
        </div>
        
        
    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;color:Maroon;font-size:larger;font-style:normal;font-weight:bold;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/ZaszBlog/Images/MasterPageUserControls/Collapse.jpg" AlternateText="collapse" Enabled="False" />
            &nbsp;&nbsp;What's New
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;">
           <h3>My Latest Piece of Work</h3><br />
				<p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;">You are looking at it. This website features a Blog and a Gallery presently. Its 
                in Beta, and hopefully many more features will be added. Please suggest any 
                features to chandru9279@gmail.com</p><hr />
           <h3>What's New</h3><br />
			    <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;">Just wired up the site with Monster http handlers and Google Analytics, thinking 
                about placing ads, is that a good idea?</p>         
        </p>
    </asp:Panel>
        <ajaxToolkit:CollapsiblePanelExtender ID="Description_ContentPanel_CollapsiblePanelExtender" 
            runat="server" 
            Enabled="True" 
            TargetControlID="Description_ContentPanel"
            ExpandControlID="Description_HeaderPanel" 
            CollapseControlID="Description_HeaderPanel"
            Collapsed="True" 
            ImageControlID="Description_ToggleImage" 
            CollapsedImage="~/ZaszBlog/Images/MasterPageUserControls/Expand.jpg"
            ExpandedImage="~/ZaszBlog/Images/MasterPageUserControls/Collapse.jpg"
            /> 
    <br />  
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;color:Maroon;font-size:larger;font-style:normal;font-weight:bold;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/ZaszBlog/Images/MasterPageUserControls/Expand.jpg" AlternateText="expand" Enabled="false" />
            &nbsp;&nbsp;Open Source
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;">
                &nbsp;&nbsp;Will provide a download link in short order .. best way is to run a subversion 
                server on the host, which must be discussed with the hosting provider, soon 
                after which everyone will be able to obtain a working copy of this site, 
                licensed under MS-PL (Microsoft Permissive License) . Be happy if you can 
                provide feedback on the working and performance, and anything else i should 
                know.        
        </p>
    </asp:Panel>    
        <ajaxToolkit:CollapsiblePanelExtender ID="Properties_ContentPanel_CollapsiblePanelExtender" runat="server"
         Enabled="True"
         TargetControlID="Properties_ContentPanel"
         ExpandControlID="Properties_HeaderPanel"
         CollapseControlID="Properties_HeaderPanel"
         Collapsed="True" 
         ImageControlID="Properties_ToggleImage" 
         CollapsedImage="~/ZaszBlog/Images/MasterPageUserControls/Expand.jpg"
         ExpandedImage="~/ZaszBlog/Images/MasterPageUserControls/Collapse.jpg"
         />
</asp:Content>

<asp:Content ID="DefaultSideBarContent" ContentPlaceHolderID="cphsidebar" EnableViewState="false" runat="server">
            <div class="right_head">
                <div class="morelinks_head">Some Links </div>
            </div>
            <div class="links_morearea">
                <a href="http://www.sourceforge.net/" class="morelink">SourceForge <span class="links_text"> dev</span></a>
                <a href="http://www.asp.net/" class="morelink">ASP <span class="links_text"> .net</span></a> 
                <a href="http://www.scopeinternational.com/" class="morelink">Scope <span class="links_text"> international</span></a> 
                <a href="http://www.pnbindia.com/" class="morelink">Punjab National <span class="links_text"> bank</span></a> <br />
                <a href="http://www.google.com/" class="morelink">Google <span class="links_text"> search</span></a> <br />
            </div><br /><br />
            <div class="freeregistration">
                <div align="center"><span class="free">Free</span> registration</div>
            </div><br /><br />
</asp:Content>

<asp:Content ID="DefaultHeadContent" ContentPlaceHolderID="cphhead" EnableViewState="false" runat="server">
</asp:Content>