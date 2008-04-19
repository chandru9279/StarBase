<%@ Page AutoEventWireup="true" CodeFile="AddEntry.aspx.cs" Inherits="AdminAddEntry"
    Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" ValidateRequest="False" %>
<%@ Register src="~/ZaszBlog/Admin/HtmlEditor.ascx" TagPrefix="Blog" TagName="TextEditor" %>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
<script type="text/javascript">
function ToggleVisibility()
{
  var element = document.getElementById('<%=ulDrafts.ClientID%>');
  if (element.style.display == "none")
    element.style.display = "block";
  else
    element.style.display = "none";
}

function GetSlug()
{
  var title = document.getElementById('<%=txtTitle.ClientID %>').value;
  WebForm_DoCallback('__Page', title, ApplySlug, 'slug', null, false) 
}

function ApplySlug(arg, context)
{
  var slug = document.getElementById('<%=txtSlug.ClientID %>');
  slug.value = arg;
}

function AutoSave()
{  
  var content = tinyMCE.getContent('mce_editor_0');
  var title = document.getElementById('<%=txtTitle.ClientID %>').value;
  var desc = document.getElementById('<%=txtDescription.ClientID %>').value;
  var slug = document.getElementById('<%=txtSlug.ClientID %>').value;
  var tags = document.getElementById('<%=txtTags.ClientID %>').value;
  var s = ';|;';
  var post = content + s + title + s + desc + s + slug + s + tags;
  
  if (content.length > 10)
  {
    WebForm_DoCallback('__Page', '_autosave' + post, null, 'autosave', null, false);
  }
  
  setTimeout("AutoSave()", 5000);
}
</script>

  <div id="divDrafts" runat="server" visible="False" enableviewstate="False" style="margin-bottom: 10px">
    <a id="aDrafts" runat="server" href="javascript:void(ToggleVisibility());" />
    <ul id="ulDrafts" runat="server" style="display:none;list-style-type:circle" />
  </div>

  <label for="<%=txtTitle.ClientID %>">Title</label>
  <asp:TextBox runat="server" ID="txtTitle" Width="500px" TabIndex="1" />&nbsp;&nbsp;&nbsp;
  
  <label for="<%=ddlAuthor.ClientID %>">Author</label>
  <asp:DropDownList runat="Server" ID="ddlAuthor" TabIndex="2" />&nbsp;&nbsp;&nbsp;
  
  <label for="<%=txtDate.ClientID %>">Date</label>
  <asp:TextBox runat="server" ID="txtDate" Width="110px" TabIndex="3" />
  
  <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDate" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9] [0-9][0-9]:[0-9][0-9]" ErrorMessage="Please enter a valid date (yyyy-mm-dd hh:mm)" Display="dynamic" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ErrorMessage="Please enter a date (yyyy-mm-dd hh:mm)" Display="Dynamic" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" ErrorMessage="Please enter a title" Display="Dynamic" />
  <br /><br />
  
  <Blog:TextEditor runat="server" id="txtContent" TabIndex="4" />

  <br />
  
  <table id="entrySettings">
    <tr>
      <td class="label">Upload Image</td>
      <td>
        <asp:FileUpload runat="server" ID="txtUploadImage" Width="400" TabIndex="5" />
        <asp:Button runat="server" ID="btnUploadImage" Text="Upload" CausesValidation="False" TabIndex="6" />
      </td>
    </tr>
    <tr>
      <td class="label">Upload File</td>
      <td>
        <asp:FileUpload runat="server" ID="txtUploadFile" Width="400" TabIndex="7" />
        <asp:Button runat="server" ID="btnUploadFile" Text="Upload" CausesValidation="False" ValidationGroup="fileUpload" TabIndex="8" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUploadFile" ErrorMessage="Specify a file name" ValidationGroup="fileUpload" />
      </td>
    </tr>    
    <tr>
      <td class="label">Slug (optional)</td>
      <td>
        <asp:TextBox runat="server" ID="txtSlug" TabIndex="9" Width="400" />
        <a href="javascript:void(GetSlug());">Extract from title</a>
      </td>
    </tr>
    <tr>
      <td class="label">Description</td>
      <td><asp:TextBox runat="server" ID="txtDescription" TextMode="multiLine" Columns="50" Rows="3" Width="400" Height="32px" TabIndex="9" /></td>
    </tr>
    <tr>
      <td class="label">Categories</td>
      <td>
        <asp:TextBox runat="server" ID="txtCategory" ValidationGroup="category" TabIndex="10" />
        <asp:Button runat="server" ID="btnCategory" Text="Add" ValidationGroup="category" TabIndex="11" />
        <asp:CustomValidator runat="Server" ID="valExist" ValidationGroup="category" ControlToValidate="txtCategory" ErrorMessage="The category already exist" Display="dynamic" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCategory" ErrorMessage="Required" ValidationGroup="category" /><br />
        
        <asp:CheckBoxList runat="server" Width="400" ID="cblCategories" CssClass="cblCategories" RepeatLayout="flow" RepeatDirection="Horizontal" TabIndex="12" />
      </td>
    </tr>
    <tr>
      <td class="label">Tags</td>
      <td>
        <asp:TextBox runat="server" ID="txtTags" Width="400" TabIndex="13" />
        <span>Separate each tag with a comma</span>
      </td>
    </tr>
    <tr>
      <td class="label">Settings</td>
      <td>
        <asp:CheckBox runat="server" ID="cbEnableComments" Text="Enable comments" Checked="true" TabIndex="14" />
        <asp:CheckBox runat="server" ID="cbPublish" Text="Publish" Checked="true" TabIndex="15" />
      </td>
    </tr>
  </table>  
  
  <div style="text-align:right">  
    <asp:Button runat="server" ID="btnSave" TabIndex="16" Text="Save Post"/>
  </div>
  <br />
<% if (Request.QueryString["id"] == null){ %>  
  <script type="text/javascript">
    setTimeout("AutoSave()", 5000);
  </script>
<%} %>
</asp:Content>

