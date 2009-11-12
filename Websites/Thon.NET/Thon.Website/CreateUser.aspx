<%@ Page Language="C#" MasterPageFile="~/SecureMaster.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Thon.CreateUserAspx" Title="Add New User" %>

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
    <br />
    <asp:LoginView ID="LoginView1" runat="server">
        <LoggedInTemplate>
            <p style="font-size:small;">You can create new users to this website! Fill in the form below, to create
            new users..</p>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <p style="font-size:small;">You do not have privileges to create users. Please contact the WebMaster
            if you need a new user created.</p>
        </AnonymousTemplate>
    </asp:LoginView>
    <br /><br />
    <div style="font-size:larger;">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#FFFBD6" 
        BorderColor="#FFDFAD" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="0.8em" Visible="False">
        <SideBarStyle BackColor="#990000" Font-Size="0.9em" VerticalAlign="Top" />
        <SideBarButtonStyle 
            ForeColor="White" Font-Size="Medium" />
        <ContinueButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#990000"/>
        <NavigationButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#990000"  />
        <HeaderStyle BackColor="#FFCC66" BorderColor="#FFFBD6" BorderStyle="Solid" 
            BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="#333333" 
            HorizontalAlign="Center" />
        <CreateUserButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#990000"/>
        <TitleTextStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0" style="font-family:Verdana;font-size:100%;">
                        <tr>
                            <td align="center" colspan="2" 
                                style="color:White;background-color:#507CD1;font-weight:bold;font-size:Larger;">
                                Complete</td>
                        </tr>
                        <tr>
                            <td style="font-size:medium;">
                                Your account has been successfully created.<br />
                                A new user to this&nbsp; website has been added,<br />
                                and can access this website, immediately.</td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="ContinueButton" runat="server" BackColor="White" 
                                    BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
                                    CausesValidation="False" CommandName="Continue" Font-Names="Verdana" 
                                    ForeColor="#284E98" Text="Continue" ValidationGroup="CreateUserWizard1" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    </div>
    <br />
    <br />
    <p style="font-size:large;">Upcoming features in this site:<br /></p>
    <ul class="normal" style="margin:15px 0px 15px 15px;">
    <li>A customized integration of the LiveChat</li>
    <li>Online WebDev Utilities like HTML encoder/decoder & URL encoder/decoder</li>
    <li>File share/storage portal</li>    
    <li>SSL transport-layer security for https connections</li>
    <li>Subdomains</li>
    <li>Forum-Wiki mixup app</li>
    </ul>
    <div style="height:600px" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

