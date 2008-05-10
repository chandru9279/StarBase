<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" 
AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Thon.Gallery.GalleryAspx" 
Title="Gallery" Debug="true" %>

<asp:Content ID="Header" ContentPlaceHolderID="cphhead" Runat="Server">
<link href="StyleSheets/Gallery.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="MainGalleryContent" ContentPlaceHolderID="cphmain" Runat="Server">    
    
    <div id="GalleryStream" class="GalleryStream">
        <asp:Repeater ID="PhotosRepeater" runat="server" 
            onitemdatabound="PhotosRepeater_ItemDataBound">
            <ItemTemplate>
                <asp:Literal runat="server" ID="PhotoItem"></asp:Literal>
            </ItemTemplate>
        </asp:Repeater>    
        <h2 runat="server" id="NoEntriesH2" visible="false"><asp:Literal ID="SorryLiteral" runat="server" Text="Sorry, no matching entries could be located." meta:resourcekey="SorryLiteral"></asp:Literal></h2>
    </div> 
       
    <div runat="server" id="GalleryNavigation" class="GalleryNavigation" visible="false">
        <asp:Hyperlink runat="server" ID="PreviousButton" Text="Previous" meta:resourcekey="PreviousButton"></asp:Hyperlink>
        <asp:Hyperlink runat="server" ID="NextButton" Text="Next" meta:resourcekey="NextButton"></asp:Hyperlink>
    </div>
        
    <div class="GalleryJavascript"> 
        <script type="text/javascript">   
            //Only show it if they have javascript
                        
            function PopWindow(PopupURL, PhotoID, PhotoPath, PhotoWidth, PhotoHeight, PhotoDate, PhotoDescription) {
            
                var MaxWidth = screen.width * 0.9;
                var MaxHeight = screen.height * 0.8;
                var ScaleFactor = 1;

                ScaleFactor = MaxWidth / PhotoWidth
                if (MaxHeight / PhotoHeight < ScaleFactor) {
                    ScaleFactor = MaxHeight / PhotoHeight;
                }
                if (ScaleFactor > 1) {
                    ScaleFactor = 1;
                }

                var ImageWidth = parseInt(PhotoWidth * ScaleFactor);
                var ImageHeight = parseInt(PhotoHeight * ScaleFactor);
                var WindowWidth = ImageWidth;
                var WindowHeight = ImageHeight + 60;
                window.open(PopupURL + '?Target=' + PhotoPath + unescape('%26PhotoWidth=') + ImageWidth + unescape('%26PhotoHeight=') + ImageHeight + unescape('%26PhotoDate=') + PhotoDate + unescape('%26PhotoDescription=') + escape(PhotoDescription), PhotoID, 'status=no,width=' + WindowWidth + ',height=' + WindowHeight + ',scrollbars=no,resizable=no');
                
            }
            
            function PopWindowAdmin(PopupURL, PhotoID, PhotoPath, PhotoWidth, PhotoHeight) {
            
                var EditingWidth = 300
                var ScrollBarWidth = 20
            
                var MaxWidth = (screen.width * 0.9) - EditingWidth;
                var MaxHeight = (screen.height * 0.8) - ScrollBarWidth;
                var ScaleFactor = 1;

                ScaleFactor = MaxWidth / PhotoWidth
                if (MaxHeight / PhotoHeight < ScaleFactor) {
                    ScaleFactor = MaxHeight / PhotoHeight;
                }
                if (ScaleFactor > 1) {
                    ScaleFactor = 1;
                }

                var ImageWidth = parseInt(PhotoWidth * ScaleFactor);
                var ImageHeight = parseInt(PhotoHeight * ScaleFactor);
                var WindowWidth = ImageWidth + EditingWidth;
                var WindowHeight = ImageHeight + ScrollBarWidth;
                window.open(PopupURL + '?Target=' + PhotoPath + unescape('%26PhotoID=') + PhotoID + unescape('%26PhotoWidth=') + ImageWidth + unescape('%26PhotoHeight=') + ImageHeight, PhotoID, 'status=no,width=' + WindowWidth + ',height=' + WindowHeight + ',scrollbars=yes,resizable=no');
                
            }
            </script>
        <input type="hidden" name="KeepFeaturesOpen" value="false" />
    </div>
    
</asp:Content>


<asp:Content ID="Sidebar" ContentPlaceHolderID="cphsidebar" Runat="Server">

     <div id="Features">
    
        <div runat="server" id="GalleryFeatures" visible="false">
            <asp:LoginStatus ID="LoginStatus1" runat="server" />
            <div runat="server" id="GalleryAdminUpload" visible="false">
                <h1><asp:Literal ID="UploadLiteral" runat="server" Text="Upload Photos" meta:resourcekey="UploadLiteral"></asp:Literal></h1>
                <asp:FileUpload ID="UncatalogedFileUpload" runat="server" />
                <asp:Button ID="UncatalogedUploadButton" runat="server" Text="Upload Image" 
                    meta:resourcekey="UncatalogedUploadButton" 
                    onclick="UncatalogedUploadButton_Click" />
            </div>
            
            <div runat="server" id="GalleryAdminCatalog" visible="false">
                <h1><asp:Literal ID="CatelogLiteral" runat="server" Text="Catalog Uploads" meta:resourcekey="CatalogLiteral"></asp:Literal></h1>
                <asp:Label runat="server" ID="UncatalogedCountLabel"></asp:Label>
                <asp:Button ID="UncatalogedCatalogButton" runat="server" Text="Catalog Uploads" meta:resourcekey="UncatalogedCatalogButton" OnClick="UncatalogedCatalogButton_Click"/>
            </div>
            
            <div runat="server" id="GalleryCategories" visible="false">
                <h1><asp:Literal ID="CategoriesLiteral" runat="server" Text="Categories" meta:resourcekey="CategoriesLiteral"></asp:Literal></h1>
                <asp:BulletedList runat="server" ID="CategoriesList" DisplayMode="HyperLink"></asp:BulletedList>        
                <asp:TextBox runat="server" ID="CategoriesAddDeleteBox" Visible="false"></asp:TextBox>
                <asp:Button runat="server" ID="CategoriesAddButton" Text="Add" Visible="false" 
                    meta:resourcekey="CategoriesAddButton" onclick="CategoriesAddButton_Click"></asp:Button>
                <asp:Button runat="server" ID="CategoriesDeleteButton" Text="Delete" 
                    Visible="false" meta:resourcekey="CategoriesDeleteButton" 
                    onclick="CategoriesDeleteButton_Click"></asp:Button>
                <span style="clear: both; display: block;"></span>
            </div>
            
            <div runat="server" id="GallerySearch" visible="false">
                <h1><asp:Literal ID="Literal1" runat="server" Text="Search" meta:resourcekey="SearchLiteral"></asp:Literal></h1>
                <asp:TextBox runat="server" ID="GallerySearchBox" EnableViewState="true" 
                    ontextchanged="GallerySearchBox_TextChanged"></asp:TextBox>
            </div>
            
            <div runat="server" id="GallerySort" visible="false">
                <h1><asp:Literal ID="SortLiteral" runat="server" Text="Sort" meta:resourcekey="SortLiteral"></asp:Literal></h1>
                <asp:BulletedList runat="server" ID="SortList" DisplayMode="HyperLink"></asp:BulletedList>
            </div>
            
            <div runat="server" id="GalleryPageSize" visible="false">
                <h1><asp:Literal ID="PhotosPerPageLiteral" runat="server" Text="PhotosPerPage" meta:resourcekey="PhotosPerPageLiteral"></asp:Literal></h1>
                <asp:BulletedList runat="server" ID="PageSizeList" DisplayMode="HyperLink"></asp:BulletedList>
            </div>
            
        </div>
    
    </div>
    
</asp:Content>

