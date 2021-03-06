﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Thon.ThonDefaultAspx" MasterPageFile="~/ThonMasterPage.master" Title="Welcome to ChandruOn.NET" %>
<%@ Reference Control="~/ZaszBlog/UserControls/PostView.ascx" %>


<%@ Register 
    Assembly="AjaxControlToolkit" 
    Namespace="AjaxControlToolkit" 
    TagPrefix="ajaxToolkit" %>
    
<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="cphmain" EnableViewState="false" runat="server">        

    <table>
    <tr><td>
        <div class="inner_banner">
        <table>
            <tr><td>
            <p class="worksheet">Check Out</p>
            </td></tr>
            <tr><td>
            <p> 
                <a href="Linkz.aspx"class="innerlink">Linkz & Fun </a>
                <a href="Credits.aspx" class="innerlink">Credits </a>
                <a href="http://www.intrepidkarthi.com/" class="innerlink">N. G. Karthikeyan's Portal</a>
                <a href="ZaszBlog/Archive.aspx" class="innerlink">Archive </a> 
                <a href="http://www.google.com/search?q=ZaszBlog&ie=utf-8&oe=utf-8" class="innerlink">Proving SEO</a>
		        <a href="http://www.imdb.com/title/tt0496806/" class="innerlink">Ocean's 11, 12, 13 fan</a>
		    </p>
		    </td></tr>
        </table>
        </div>
     </td></tr>
     <tr><td>   
        <div runat="server" id="posts" /> 
     </td></tr>
     </table>
       
             
            
     <script runat="Server" type="text/C#">
         [System.Web.Services.WebMethod]
         [System.Web.Script.Services.ScriptMethod]
         public static AjaxControlToolkit.Slide[] GetSlides()
         {
            return new AjaxControlToolkit.Slide[] { 
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/15.jpg", "Zasz", "XBOX 360"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/1.jpg", "Me & Pop", "Srinagar Fall"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/2.jpg", "Jaya & Thiagu", "Rotang Pass"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/3.jpg", "All four", "Skiing"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/4.jpg", "Vasanth & Mahesh", "Skiing"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/5.jpg", "Pop & Mom", "Ski"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/6.jpg", "Bones & Family", "1st Floor"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/7.jpg", "Arun", "Forbidden City"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/8.jpg", "Mahesh", "Great Wall"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/9.jpg", "Mahesh", "E.T."),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/10.jpg", "Arun", "Tongli Temple"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/11.jpg", "Mini-Me", "China"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/12.jpg", "Thiagu", "Manaali"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/14.jpg", "Mom", "Rotang Pass"),            
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/13.jpg", "Vasanth", "PSP & Comp"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/16.jpg", "Taj Mahal", "Delhi"),
            new AjaxControlToolkit.Slide("ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/17.jpg", "Mahesh", "Shanghai"),
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
                ImageUrl="ThonHttpHandlers/Image.ashx?picturepath=~/Images/Files/15.jpg"
                AlternateText="ThonSlideShow" /><br />                
            <asp:Label runat="server" ID="imageDescription"></asp:Label><br /><br />
            <asp:Button runat="Server" BorderStyle="Double" BackColor="Cornsilk" ForeColor="Maroon" Font-Bold="true" BorderColor="Maroon" ID="prevButton" Text="Previous" Font-Size="Larger" />
            <asp:Button runat="Server" BorderStyle="Double" BackColor="Cornsilk" ForeColor="Maroon" Font-Bold="true" BorderColor="Maroon" ID="playButton" Text="Play" Font-Size="Larger" />
            <asp:Button runat="Server" BorderStyle="Double" BackColor="Cornsilk" ForeColor="Maroon" Font-Bold="true" BorderColor="Maroon" ID="nextButton" Text="Next" Font-Size="Larger" />
            <ajaxToolkit:SlideShowExtender ID="slideshowextend1" runat="server"                         
                TargetControlID="Image1"
                SlideShowServiceMethod="GetSlides" 
                AutoPlay="false"
                ImageTitleLabelID="imageTitle"
                ImageDescriptionLabelID="imageDescription"
                NextButtonID="nextButton" 
                PlayButtonText="Start SlideShow" 
                StopButtonText="Stop SlideShow"
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
                <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;"> 
                <%=DateTime.Now.AddDays(-1).ToLongDateString() %><br />Put up the new Diablo Fansite kit, and
                the software warez subdomain. Some bugfixes, and content doodads.</p><hr />
                <p style="margin:15px 15px 15px 15px;color:Maroon;font-style:italic;line-height:18px;"> 
                <%=DateTime.Now.AddDays(-6).ToLongDateString() %><br />Working on a HelpDesk solution in Java EE atop the struts 2 framework. And will shortly provide a download link
                to my new ruby app. (Simple app to improve UI using AJAX and javascript)</p><hr />
				<p style="margin:15px 15px 15px 15px;color:#FFAABA;font-style:italic;line-height:18px;">
				<%=DateTime.Now.AddDays(-12).ToLongDateString() %><br />You are looking at it. This website features a Blog and a Gallery presently. Its 
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
                licensed under MS-PL (Microsoft Permissive License) and Creative Commons 
                Attribution 2.5 India . Be happy if you can provide feedback on the working and 
                performance, and anything else i should know.        
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

<center>
<br/>
<!-- Facebook Badge START --><a href="http://en-gb.facebook.com/Chandirasekar.Zasz" title="Chandirasekar Zasz" target="_TOP" style="font-family: &quot;lucida grande&quot;,tahoma,verdana,arial,sans-serif; font-size: 11px; font-variant: normal; font-style: normal; font-weight: normal; color: #3B5998; text-decoration: none;">Chandirasekar Zasz</a><span style="font-family: &quot;lucida grande&quot;,tahoma,verdana,arial,sans-serif; font-size: 11px; line-height: 16px; font-variant: normal; font-style: normal; font-weight: normal; color: #555555; text-decoration: none;">&nbsp;|&nbsp;</span><a href="http://en-gb.facebook.com/facebook-widgets/" title="Make your own badge!" target="_TOP" style="font-family: &quot;lucida grande&quot;,tahoma,verdana,arial,sans-serif; font-size: 11px; font-variant: normal; font-style: normal; font-weight: normal; color: #3B5998; text-decoration: none;">Create your badge</a><br/><a href="http://en-gb.facebook.com/Chandirasekar.Zasz" title="Chandirasekar Zasz" target="_TOP"><img src="http://badge.facebook.com/badge/815596121.2578.703699520.png" width="360" height="265" style="border: 0px;" /></a><!-- Facebook Badge END -->
<br/>
</center>


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
                <a href="http://freshmeat.net/" class="morelink">Freshmeat <span class="links_text"> source</span></a> <br />
            </div><br /><br />
            <div class="freeregistration">
                <div align="center"><span class="free">Free</span> registration</div>
            </div><br /><br />
</asp:Content>

<asp:Content ID="DefaultHeadContent" ContentPlaceHolderID="cphhead" EnableViewState="false" runat="server">
</asp:Content>