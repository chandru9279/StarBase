<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Change Password" %>

<asp:Content ID="LoginHeadContent" ContentPlaceHolderID="cphhead" Runat="Server">
    <div>
    
</div>
</asp:Content>

<asp:Content ID="LoginMainContent" ContentPlaceHolderID="cphmain" Runat="Server">
    <asp:Login ID="ThonLogin" runat="server" onloggedin="ThonLogin_LoggedIn" PasswordRecoveryUrl="~/Login.aspx?forgot=true" PasswordRecoveryText="Forgot your password?"/>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" Visible="false">
    </asp:PasswordRecovery>
    <asp:ChangePassword runat="server" ID="ChangePassword" visible="false" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" />
</asp:Content>

<asp:Content ID="LoginSideBarContent" ContentPlaceHolderID="cphsidebar" Runat="Server">
    <div>    
</div>
</asp:Content>

