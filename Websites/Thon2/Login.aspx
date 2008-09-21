<%@ Page Language="C#" MasterPageFile="~/SecureMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Thon.LoginAspx" Title="Sign In, Sign Out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
 <script type="text/javascript">
 // ctl00_cphmain_ThonLogin_Password
      function PressLogin()
      {        
        var loginbtn = document.getElementById('ctl00_cphmain_ThonLogin_LoginButton');
        if(loginbtn == null) 
        {
            alert("Null");
            return;
        }
        else
        {
            alert("Press");
            try{loginbtn.click();}
            catch(ex){alert("Err");}
        }
     }      
 </script>
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
        TextLayout="TextOnTop" Width="221px" DestinationPageUrl="Default.aspx" RememberMeSet="True">        
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="White" BorderColor="#CC9966" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" />
        <LayoutTemplate>
            <table border="0" cellpadding="4" cellspacing="0" 
                style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" style="width:221px;">
                            <tr>
                                <td align="center" 
                                    style="color:White;background-color:#990000;font-size:0.9em;font-weight:bold;">
                                    Log In</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                    Name:</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="ThonLogin">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>                                    
                                    <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password" onkeypress="if(event.keyCode==13) { PressLogin(); }"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                        ControlToValidate="Password" ErrorMessage="Password is required." 
                                        ToolTip="Password is required." ValidationGroup="ThonLogin">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="RememberMe" runat="server" Checked="True" 
                                        Text="Remember me next time." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="LoginButton" runat="server" BackColor="White" 
                                        BorderColor="#CC9966" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" Text="Log In" 
                                        ValidationGroup="ThonLogin" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="PasswordRecoveryLink" runat="server" 
                                        NavigateUrl="~/Login.aspx?forgot=true">Forgot your password?</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
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
