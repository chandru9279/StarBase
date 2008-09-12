﻿<%@ Page Language="C#" MasterPageFile="~/SecureMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Thon.LoginAspx" Title="Sign In, Sign Out" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
<div style="font-size:larger;">
    <table width="100%"><tr>
    <td align="left"><asp:LoginName ID="LoginName1" runat="server" FormatString="Have a good day, {0}" /></td>    
    <td align="right"><asp:LoginStatus ID="LoginStatus1" 
            runat="server" Height="20pt" 
            LoginText="Sign In" LogoutText="Sign Out" /></td>
    </tr></table>
    <br />
    <asp:Login ID="ThonLogin" runat="server" onloggedin="ThonLogin_LoggedIn" 
        PasswordRecoveryUrl="~/Login.aspx?forgot=true" 
        PasswordRecoveryText="Forgot your password?" BackColor="#FFFBD6" 
        BorderColor="#FFDFAD" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" 
        TextLayout="TextOnTop" Width="221px" >
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="White" BorderColor="#CC9966" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TitleTextStyle BackColor="#990000" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
    </asp:Login>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" Visible="False" 
        BackColor="#FFFBD6" BorderColor="#FFDFAD" BorderPadding="4" BorderStyle="Solid" 
        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" >
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <SuccessTextStyle Font-Bold="True" ForeColor="#990000" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#990000" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
        <SubmitButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
    </asp:PasswordRecovery>
    <asp:ChangePassword runat="server" ID="ChangePassword" visible="False" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" 
        BackColor="#FFFBD6" BorderColor="#FFDFAD" BorderPadding="4" BorderStyle="Solid" 
        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" >
        <CancelButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
        <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
        <ContinueButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
        <ChangePasswordButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
        <TitleTextStyle BackColor="#990000" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
        <TextBoxStyle Font-Size="0.8em" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    </asp:ChangePassword>
    <br />
    <br />
    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>
