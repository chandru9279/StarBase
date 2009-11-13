<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" 
AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Thon.Gallery.GalleryAspx" 
Title="Gallery" Debug="true" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Header" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>


<asp:Content ID="MainGalleryContent" ContentPlaceHolderID="cphmain" Runat="Server">    
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div id="GalleryStream" class="GalleryStream">
        <asp:Repeater ID="PhotosRepeater" runat="server" 
            onitemdatabound="PhotosRepeater_ItemDataBound">
            <ItemTemplate>
                <asp:Literal runat="server" ID="PhotoItem"></asp:Literal>
            </ItemTemplate>
        </asp:Repeater>    
        <h2 runat="server" id="NoEntriesH2" visible="false"><asp:Literal ID="SorryLiteral" runat="server" Text="Sorry, no matching entries could be located." ></asp:Literal></h2>
    </div> 
       
    <div runat="server" id="GalleryNavigation" class="GalleryNavigation" visible="false">
        <asp:Hyperlink runat="server" ID="PreviousButton" Text="Previous" ></asp:Hyperlink>
        <asp:Hyperlink runat="server" ID="NextButton" Text="Next" ></asp:Hyperlink>
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
            <div runat="server" id="GalleryAdminUpload" visible="false" style="width:180px;">
                <h1><asp:Literal ID="UploadLiteral" runat="server" Text="Upload Photos" ></asp:Literal></h1><br /><br /><br />
                <asp:FileUpload ID="UncatalogedFileUpload" runat="server" BackColor="#fcffff"/><br /><br />
                <asp:Button ID="UncatalogedUploadButton" runat="server" Text="Upload Image" 
                    onclick="UncatalogedUploadButton_Click" /><br /><br />
            </div>
            
            <div runat="server" id="GalleryAdminCatalog" visible="false" style="width:180px;">
                <h1><asp:Literal ID="CatelogLiteral" runat="server" Text="Catalog Uploads" ></asp:Literal></h1><br /><br /><br />
                <center style="color:Maroon"><asp:Label runat="server" ID="UncatalogedCountLabel"></asp:Label></center><br /><br />
                <center style="margin-bottom:5px;">
                <asp:Button ID="UncatalogedCatalogButton" runat="server" Text="Catalog Uploads"  OnClick="UncatalogedCatalogButton_Click"/>
                <span style="clear: both; display: block;" />
                </center>
            </div>
            
            <div runat="server" id="GalleryCategories" visible="false" style="width:180px;">
                <h1><asp:Literal ID="CategoriesLiteral" runat="server" Text="Categories" ></asp:Literal></h1>
                <br /><br /><br /><br />
                <asp:BulletedList runat="server" ID="CategoriesList" DisplayMode="HyperLink"></asp:BulletedList> <br /><br />
                <center><table cellpadding="7px">
                <tr><td colspan="2"><asp:TextBox runat="server" ID="CategoriesAddDeleteBox" Visible="false"></asp:TextBox></td></tr>
                <tr><td><asp:Button runat="server" ID="CategoriesAddButton" Text="Add" Visible="false" onclick="CategoriesAddButton_Click"></asp:Button></td><td><asp:Button runat="server" ID="CategoriesDeleteButton" Text="Delete" Visible="false" onclick="CategoriesDeleteButton_Click"></asp:Button></td></tr>
                <%-- <tr><td><asp:Label runat="server" ID="Consol"></asp:Label></td></tr> --%>
                </table></center>       
                
                
                
                <span style="clear: both; display: block;" />
            </div>
            
            <div runat="server" id="GallerySearch" visible="false" style="width:180px;">
                <h1><asp:Literal ID="Literal1" runat="server" Text="Search" ></asp:Literal></h1><br /><br /><br />
                <asp:TextBox runat="server" ID="GallerySearchBox" EnableViewState="true" 
                    ontextchanged="GallerySearchBox_TextChanged"></asp:TextBox>                
                <cc1:TextBoxWatermarkExtender ID="GallerySearchBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="GallerySearchBox" WatermarkText="Type. Hit Enter" WatermarkCssClass="redwatermark">
                </cc1:TextBoxWatermarkExtender>
                
                <br /><br />
                    
                    <span style="clear: both; display: block;"></span>
            </div>
            
            <div runat="server" id="GallerySort" visible="false" style="width:180px;">
                <h1><asp:Literal ID="SortLiteral" runat="server" Text="Sort" ></asp:Literal></h1><br /><br /><br />
                <asp:BulletedList runat="server" ID="SortList" DisplayMode="HyperLink"></asp:BulletedList><br />
                <span style="clear: both; display: block;"></span>
            </div>
            
            <div runat="server" id="GalleryPageSize" visible="false" style="width:180px;">
                <h1><asp:Literal ID="PhotosPerPageLiteral" runat="server" Text="PhotosPerPage" ></asp:Literal></h1><br /><br /><br />
                <asp:BulletedList runat="server" ID="PageSizeList" DisplayMode="HyperLink" ></asp:BulletedList>
                <span style="clear: both; display: block;"></span>
            </div>
            
        </div>
    
    </div>
    
</asp:Content>

