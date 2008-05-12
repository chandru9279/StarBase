<%@ Page Language="C#" MasterPageFile="~/SecureMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Thon.LoginAspx" Title="Sign In, Sign Out" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
    <table width="100%"><tr>
    <td align="left"><asp:LoginName ID="LoginName1" runat="server" FormatString="Have a good day, {0}" /></td>    
    <td align="right"><p style="font-size:large;"><asp:LoginStatus ID="LoginStatus1" 
            runat="server" Height="20pt" 
            LoginText="Sign In" LogoutText="Sign Out" /></p></td>
    </tr></table>
    <br />
    <asp:Login ID="ThonLogin" runat="server" onloggedin="ThonLogin_LoggedIn" 
        PasswordRecoveryUrl="~/Login.aspx?forgot=true" 
        PasswordRecoveryText="Forgot your password?" BackColor="#F7F6F3" 
        BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="1.5em" ForeColor="#333333" >
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
    </asp:Login>
    <asp:PasswordRecovery ID="ThonPasswordRecovery" runat="server" Visible="False" 
        BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" 
        BorderWidth="1px" Font-Names="Verdana" Font-Size="1.5em" >
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <SuccessTextStyle Font-Bold="True" ForeColor="#5D7B9D" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
        <SubmitButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#284775" />
    </asp:PasswordRecovery>
    <asp:ChangePassword runat="server" ID="ChangePassword" visible="False" 
        oncontinuebuttonclick="ChangePassword_ContinueButtonClick" 
        BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" 
        BorderWidth="1px" Font-Names="Verdana" Font-Size="1.5em" >
        <CancelButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#284775" />
        <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
        <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#284775" />
        <ChangePasswordButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
        <TextBoxStyle Font-Size="0.8em" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    </asp:ChangePassword>
    <br />
    <br />
    <p style="font-size:small;">Upcoming features in this site:<ul>
    <li>A customized integration of the LiveChat</li>
    <li>Online WebDev Utilities like HTML encoder/decoder & URL encoder/decoder</li>
    <li>File share/storage portal</li>  
    <li>SSL transport-layer security for https connections</li>
    <li>Subdomains</li>
    <li>Forum-Wiki mixup app</li>  
    </ul></p>
    <div style="height:550px" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>
