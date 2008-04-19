<%@ Page AutoEventWireup="true" CodeFile="Pages.aspx.cs" Inherits="AdminPages"
    Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master"
    Title="Add page" ValidateRequest="false" %>
<%@ Register Src="../htmlEditor.ascx" TagPrefix="Blog" TagName="TextEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
  <script type="text/javascript">
  function ToggleVisibility()
  {
    var element = document.getElementById('<%=ulPages.ClientID%>');
    if (element.style.display == "none")
      element.style.display = "block";
    else
      element.style.display = "none";
  }
  </script>
  
  <div id="divPages" runat="server" visible="true" enableviewstate="False" style="margin-bottom: 10px">
    <a id="aPages" runat="server" href="javascript:void(ToggleVisibility());" />
    <ul id="ulPages" runat="server" style="display:none;list-style-type:circle;" />
  </div>
  
  <label for="<%=txtTitle.ClientID %>">Title</label>
  <asp:TextBox runat="server" ID="txtTitle" Width="400px" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="Please enter a title" />&nbsp;&nbsp;&nbsp;
  
  <label for="<%=ddlParent.ClientID %>">Select parent</label>
  <asp:DropDownList runat="server" id="ddlParent" />&nbsp;&nbsp;&nbsp;
  
  <asp:CheckBox runat="Server" ID="cbIsFrontPage" Text="Is front page" />
  
  <asp:CheckBox runat="Server" ID="cbShowInList" Text="Show in list" Checked="true" />
  <br /><br />

  <blog:TextEditor runat="server" id="txtContent" TabIndex="4" />
  
  <table id="entrySettings">
    <tr>
      <td class="label">Upload Image</td>
      <td>
        <asp:FileUpload runat="server" ID="txtUploadImage" Width="400" />
        <asp:Button runat="server" ID="btnUploadImage" Text="Upload" CausesValidation="False" />
      </td>
    </tr>
    <tr>
      <td class="label">Upload File</td>
      <td>
        <asp:FileUpload runat="server" ID="txtUploadFile" Width="400" />        
        <asp:Button runat="server" ID="btnUploadFile" Text="Upload" CausesValidation="False" ValidationGroup="fileUpload" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUploadFile" ErrorMessage="Specify a file name" ValidationGroup="fileUpload" />
      </td>
    </tr>    
    <tr>
      <td class="label">Description</td>
      <td><asp:TextBox runat="server" ID="txtDescription" TextMode="multiLine" Columns="50" Rows="5" /></td>
    </tr>
    <tr>
      <td class="label">Keywords</td>
      <td><asp:TextBox runat="server" ID="txtKeyword" Width="400" /></td>
    </tr>
    <tr>
      <td class="label">Settings</td>
      <td><asp:CheckBox runat="Server" ID="cbIsPublished" Checked="true" Text="Publish" /></td>
    </tr>
  </table>  
  
  <div style="text-align:right">
    <asp:Button runat="server" ID="btnSave" Text="Save Page" />
  </div>
  <br />
</asp:Content>