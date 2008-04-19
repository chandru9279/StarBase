<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" ValidateRequest="False" AutoEventWireup="true" CodeFile="Controls.aspx.cs" Inherits="AdminControls" Title="Control settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
<br />
<div class="settings">

  <h1>Recent posts</h1>
  <label for="<%=txtNumberOfPosts.ClientID %>">Number of posts</label>
  <asp:TextBox runat="server" ID="txtNumberOfPosts" Width="30" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumberOfPosts" ErrorMessage="Required" />
  <asp:CompareValidator runat="Server" ControlToValidate="txtNumberOfPosts" Operator="dataTypeCheck" Type="integer" ErrorMessage="Please enter a valid number" /><br />
  
  <label for="<%=cbDisplayComments.ClientID %>">Display comments</label>
  <asp:CheckBox runat="Server" ID="cbDisplayComments" /><br />
  
  <label for="<%=cbDisplayRating.ClientID %>">Display rating</label>
  <asp:CheckBox runat="Server" ID="cbDisplayRating" />

</div>

<div class="settings">

  <h1>Recent comments</h1>
  <label for="<%=txtNumberOfPosts.ClientID %>">Number of comments</label>
  <asp:TextBox runat="server" ID="txtNumberOfComments" Width="30" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumberOfComments" ErrorMessage="Required" />
  <asp:CompareValidator runat="Server" ControlToValidate="txtNumberOfComments" Operator="dataTypeCheck" Type="integer" ErrorMessage="Please enter a valid number" /><br />

</div>

<div class="settings">

  <h1>Search field</h1>
  <label for="<%=txtSearchButtonText.ClientID %>">Button text</label>
  <asp:TextBox runat="server" ID="txtSearchButtonText" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSearchButtonText" ErrorMessage="Required" /><br />
  
  <label for="<%=txtDefaultSearchText.ClientID %>">Search field text</label>
  <asp:TextBox runat="server" ID="txtDefaultSearchText" /> The default text shown in the search textbox
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDefaultSearchText" ErrorMessage="Required" /><br />
  
  <label for="<%=cbEnableCommentSearch.ClientID %>">Enable comment search</label>
  <asp:CheckBox runat="Server" ID="cbEnableCommentSearch" /><br />
  
  <label for="<%=txtCommentLabelText.ClientID %>">Comment label text</label>
  <asp:TextBox runat="server" ID="txtCommentLabelText" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCommentLabelText" ErrorMessage="Required" /><br />

</div>

<div style="text-align: right">
  <asp:Button runat="server" ID="btnSave" Text="Save Settings"/>
</div><br />
</asp:Content>

