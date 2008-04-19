<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Change Password" %>

<asp:Content ID="LoginBodyContent" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:Login ID="ThonLogin" runat="server" class="loginbox" PasswordRecoveryText="Forgot your password?" 
  PasswordRecoveryUrl="~/ZaszBlog/Login.aspx?forgot=true" 
    onloggedin="ThonLogin_LoggedIn" BackColor="#F7F7DE" BorderColor="#CCCC99" 
    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
    Font-Size="10pt" MembershipProvider="ThonMembershipProvider"  >
      <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
</asp:Login>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" 
    Visible="False" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" 
    BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" 
    MembershipProvider="ThonMembershipProvider">
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:PasswordRecovery>
  <asp:ChangePassword runat="server" ID="ThonChangePassword" visible="False" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" 
    BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" 
    Font-Names="Verdana" Font-Size="10pt" 
    MembershipProvider="ThonMembershipProvider" >
      <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
</asp:ChangePassword>
</asp:Content>
