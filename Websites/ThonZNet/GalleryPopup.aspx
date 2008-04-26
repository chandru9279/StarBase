<%@ Page Language="C#" AutoEventWireup="True" CodeFile="GalleryPopup.aspx.cs" Inherits="GalleryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Photo Gallery</title>
    <link type="text/css" rel="stylesheet" href="Skins/Normal/~Normal.css" />
</head>
<body>
    <form runat="server">
        <div id="GalleryPopup">
            <div runat="server" id="UserView" visible="false">
                <div class="PhotoContent"><asp:Image ID="ImageContent" runat="server" /></div>
                <table class="PhotoTextItems">
                    <tr>
                        <td class="PhotoDescription"><asp:Literal ID="DescriptionLiteral" runat="server"></asp:Literal></td>
                        <td class="DownloadLink"><asp:HyperLink ID="DownloadLinkHref" runat="server" meta:resourcekey="DownloadLinkHref" Text="Download (Full Size)"></asp:HyperLink></td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="AdminView" visible="false">
                <div class="AdminPhoto">
                    <asp:Image ID="AdminImageContent" runat="server" />
                </div>
                <div class="AdminForm">

                    <div class="FormRow">
                        
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoIDLiteral" Text="Photo ID" meta:resourcekey="PhotoIDLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:Label ID="PhotoIDLabel" runat="server"></asp:Label></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoDateLiteral" Text="Date / Time" meta:resourcekey="PhotoDateLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:TextBox ID="PhotoDateTextBox" runat="server" CssClass="FixedWidth"></asp:TextBox></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoDescriptionLiteral" Text="Description" meta:resourcekey="PhotoDescriptionLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:TextBox ID="PhotoDescriptionTextBox" runat="server" Rows="4" TextMode="MultiLine" CssClass="FixedWidth"></asp:TextBox></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoWidthLiteral" Text="Width" meta:resourcekey="PhotoWidthLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:TextBox ID="PhotoWidthTextBox" runat="server" CssClass="FixedWidth"></asp:TextBox></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoHeightLiteral" Text="Height" meta:resourcekey="PhotoHeightLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:TextBox ID="PhotoHeightTextBox" runat="server" CssClass="FixedWidth"></asp:TextBox></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoResolutionLiteral" Text="Resolution" meta:resourcekey="PhotoResolutionLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:TextBox ID="PhotoResolutionTextBox" runat="server" CssClass="FixedWidth"></asp:TextBox></div>
                    </div>

                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoVisibleLiteral" Text="Visible" meta:resourcekey="PhotoVisibleLiteral"></asp:Literal>:</div>
                        <div class="FormItem"><asp:CheckBox ID="PhotoVisibleCheckBox" runat="server" CssClass="FixedWidth"></asp:CheckBox></div>
                    </div>
                   
                    <div class="FormRow">
                        <div class="FormLabel"><asp:Literal runat="server" ID="PhotoCategoriesLiteral" Text="Categories" meta:resourcekey="PhotoCategoriesLiteral"></asp:Literal>:</div>
                        <div class="FormItem"></div>
                    </div>
                    
                    <div class="FormRow">
                        <div class="FormItem"><asp:CheckBoxList runat="server" ID="AllCategoriesList" RepeatLayout="Flow" RepeatDirection="Vertical"></asp:CheckBoxList></div>
                    </div>
                    
                    <div class="FormRow">
                        <asp:LinkButton ID="DeleteButton" runat="server" CssClass="DeleteButton" Text="Delete" meta:resourcekey="DeleteButton"></asp:LinkButton> <asp:LinkButton ID="UpdateButton" runat="server" CssClass="SaveButton" Text="Save & Close" meta:resourcekey="UpdateButton"></asp:LinkButton>
                    </div>

                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
