<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" AutoEventWireup="true" ValidateRequest="False" CodeFile="Categories.aspx.cs" Inherits="AdminCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">

  <asp:Label ID="lblNewCategory" runat ="server" AssociatedControlID="txtNewCategory" Text="Title" /><br /><asp:TextBox runat="Server" ID="txtNewCategory" Width="200" /><br />
    <asp:Label ID="lblNewNewDescription" runat ="server" AssociatedControlID="txtNewNewDescription" Text="Description" /><br /><asp:TextBox runat="Server" ID="txtNewNewDescription" Width="400" TextMode="MultiLine" Rows="4" /><br /><br />
  <asp:Button runat="server" ID="btnAdd" ValidationGroup="new" 
        onclick="btnAdd_Click" Text="Add Category"/>
  <asp:CustomValidator runat="Server" ID="valExist" ValidationGroup="new" ControlToValidate="txtNewCategory" ErrorMessage="The category already exist" Display="dynamic" />
  <asp:RequiredFieldValidator runat="Server" ValidationGroup="new" ControlToValidate="txtNewCategory" ErrorMessage="Please enter a valid name" /><br /><hr />


  <asp:GridView runat="server" ID="grid" CssClass="category" 
    GridLines="None"
    AutoGenerateColumns="False" 
    AlternatingRowStyle-CssClass="alt" 
    AutoGenerateDeleteButton="True" 
    AutoGenerateEditButton="True">
    <Columns>      
      <asp:TemplateField HeaderText="Name">
        <ItemTemplate>
          <%# Server.HtmlEncode(Eval("title").ToString()) %>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="txtTitle" Text='<%# Eval("title") %>' />
        </EditItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Description">
       <ItemTemplate>
          <%# Server.HtmlEncode(Eval("description").ToString())%>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="txtDescription" Text='<%# Eval("description") %>'  />
        </EditItemTemplate>
      </asp:TemplateField>
    </Columns>
      <AlternatingRowStyle CssClass="alt" />
  </asp:GridView>
  
</asp:Content>