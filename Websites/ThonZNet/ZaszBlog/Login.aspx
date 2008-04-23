<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Change Password" %>

<asp:Content ID="LoginBodyContent" ContentPlaceHolderID="cphBody" Runat="Server">

<asp:Login ID="ThonLogin" runat="Server" PasswordRecoveryText="Forgot your password?" 
            PasswordRecoveryUrl="~/ZaszBlog/Login.aspx?forgot=true" 
            onloggedin="ThonLogin_LoggedIn" MembershipProvider="ThonMembershipProvider" 
            FailureAction="RedirectToLoginPage" Height="143px" Width="240px"  >
</asp:Login>


<asp:PasswordRecovery ID="ThonPasswordRecovery" runat="Server" 
            Visible="False" 
            MembershipProvider="ThonMembershipProvider">
</asp:PasswordRecovery>


<asp:ChangePassword runat="Server" ID="ThonChangePassword" visible="False" 
            oncontinuebuttonclick="ChangePassword_ContinueButtonClick" 
            MembershipProvider="ThonMembershipProvider" >
</asp:ChangePassword>
</asp:Content>
