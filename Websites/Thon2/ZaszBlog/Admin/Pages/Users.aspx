<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="AdminNewUser" Title="Create new user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">
    <br />
    <div class="settings">
        <h1>
            Create new user
        </h1>
        <br />
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" LoginCreatedUser="false">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
        <asp:Label runat="server" ID="lblError" Text="Username is already taken" style="color:Red" visible="false" />
    </div>
    <br />
    <div class="settings">
        <asp:GridView runat="server" ID="gridUsers" AutoGenerateColumns="false" UseAccessibleHeader="true" CellPadding="4"
             HeaderStyle-HorizontalAlign="left" DataKeyNames="username" OnLoad="gridUsers_Load" AlternatingRowStyle-CssClass="alt" 
             AutoGenerateEditButton="true" AutoGenerateDeleteButton="true">
            <Columns>
                
                <asp:TemplateField HeaderText="Username">
                  <ItemTemplate>
                    <%# Server.HtmlEncode(Eval("username").ToString()) %>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="txtUsername" Text='<%# Eval("username") %>' />
                  </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Password">
                  <ItemTemplate>
                    **********
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="txtPassword" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="dynamic" ErrorMessage="Password required" />
                  </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="E-Mail">
                  <ItemTemplate>
                    <%# Server.HtmlEncode(Eval("email").ToString()) %>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="txtEmail" Text='<%# Eval("email") %>' />
                  </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Roles" ItemStyle-Wrap="false" ItemStyle-Width="100%" />
            </Columns>
            <HeaderStyle HorizontalAlign="Left" />
        </asp:GridView>
    </div>
</asp:Content>