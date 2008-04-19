<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Change Password" %>

<asp:Content ID="LoginBodyContent" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:Login ID="ThonLogin" runat="server" class="loginbox" PasswordRecoveryText="Forgot your password?" 
  PasswordRecoveryUrl="~/ZaszBlog/Login.aspx?forgot=true" 
    onloggedin="ThonLogin_LoggedIn" BackColor="#EFF3FB" BorderColor="#B5C7DE" 
    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
    Font-Size="0.8em" MembershipProvider="ThonMembershipProvider" 
        FailureAction="RedirectToLoginPage" BorderPadding="4" ForeColor="#333333"  >
      <TitleTextStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#FFFFFF" 
            Font-Size="0.9em" />
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
</asp:Login>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" 
    Visible="False" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderStyle="Solid" 
    BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
    MembershipProvider="ThonMembershipProvider" BorderPadding="4">
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <SuccessTextStyle Font-Bold="True" ForeColor="#507CD1" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        <SubmitButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:PasswordRecovery>
  <asp:ChangePassword runat="server" ID="ThonChangePassword" visible="False" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" 
    BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderStyle="Solid" BorderWidth="1px" 
    Font-Names="Verdana" Font-Size="0.8em" 
    MembershipProvider="ThonMembershipProvider" BorderPadding="4" >
      <CancelButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
          BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
      <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
      <PasswordHintStyle Font-Italic="True" ForeColor="#507CD1" />
      <ContinueButtonStyle BackColor="White" BorderColor="#507CD1" 
          BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
          ForeColor="#284E98" />
      <ChangePasswordButtonStyle BackColor="White" BorderColor="#507CD1" 
          BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
          ForeColor="#284E98" />
      <TextBoxStyle Font-Size="0.8em" />
      <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
</asp:ChangePassword>
</asp:Content>
