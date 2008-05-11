<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Thon.LoginAspx" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
<asp:Login ID="ThonLogin" runat="server" onloggedin="ThonLogin_LoggedIn" PasswordRecoveryUrl="~/Login.aspx?forgot=true" PasswordRecoveryText="Forgot your password?"/>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" Visible="false">
    </asp:PasswordRecovery>
    <asp:ChangePassword runat="server" ID="ChangePassword" visible="false" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

