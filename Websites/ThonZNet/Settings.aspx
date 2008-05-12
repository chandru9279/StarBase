<%@ Page Language="C#" MasterPageFile="~/SecureMaster.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Thon.ThonConfigurationAspx"
    Title="Settings"%>
<%@ Import Namespace="Thon.Support" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="Header" runat="server">
<link href="StyleSheets/SettingsAspxStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="SettingsContent" ContentPlaceHolderID="cphmain" runat="Server">    
    <br />
    <div style="text-align: right">
        <asp:Button runat="server" ID="btnSaveTop" Text="Save Settings"/>
    </div>
    <br />
    <div class="settings">
        <h1>Basic settings</h1>
        <label for="<%=txtName.ClientID %>">Name</label>
        <asp:TextBox runat="server" ID="txtName" Width="300" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Required" /><br />
        
        <label for="<%=txtDescription.ClientID %>">Description</label>
        <asp:TextBox runat="server" ID="txtDescription" Width="300" /><br />
    </div>
    
    <div class="settings">
        <h1>Advanced Settings</h1>
        
        <label for="<%=cbEnableCompression.ClientID %>">Enable HTTP compression</label>
        <asp:CheckBox runat="server" ID="cbEnableCompression" />Makes the pages load faster (recommended).<br />
        
        <label for="<%=cbRemoveWhitespaceInStyleSheets.ClientID %>">Trim stylesheets</label>
        <asp:CheckBox runat="server" ID="cbRemoveWhitespaceInStyleSheets" />Makes all stylesheets of any theme smaller by removing all whitespace at runtime.<br />
        
        <label for="<%=cbEnableOpenSearch.ClientID %>">Enable OpenSearch</label>
        <asp:CheckBox runat="server" ID="cbEnableOpenSearch" />Adds the search feature to all new browsers (recommended).<br />
        
        <label for="<%=rblWwwSubdomain.ClientID %>">www subdomain policy</label>
        <asp:RadioButtonList runat="server" ID="rblWwwSubdomain" RepeatLayout="flow" RepeatDirection="horizontal">
            <asp:ListItem Text="Remove" Value="remove" />
            <asp:ListItem Text="Enforce" Value="add" />
            <asp:ListItem Text="Ignore" Value="" Selected="true" />
        </asp:RadioButtonList>
    </div>
    <div class="settings">
        <h1>E-mail</h1>
        
        <label for="<%=txtEmail.ClientID %>">E-mail address</label>
        <asp:TextBox runat="server" ID="txtEmail" Width="300" /><br />
        
        <label for="<%=txtSmtpServer.ClientID %>">SMTP server</label>
        <asp:TextBox runat="server" ID="txtSmtpServer" Width="300" /><br />
        
        <label for="<%=txtSmtpServerPort.ClientID %>">Port number</label>
        <asp:TextBox runat="server" ID="txtSmtpServerPort" Width="35" /> Port 25 is the standard
        <asp:CompareValidator ID="CompareValidator2" runat="Server" ControlToValidate="txtSmtpServerPort" Operator="datatypecheck" Type="integer" ErrorMessage="Not a valid number" /><br />
        
        <label for="<%=txtSmtpUsername.ClientID %>">Username</label>
        <asp:TextBox runat="server" ID="txtSmtpUsername" Width="300" /><br />
        
        <label for="<%=txtSmtpPassword.ClientID %>">Password</label>
        <asp:TextBox runat="server" ID="txtSmtpPassword" Width="300" /><br />
        
        <label for="<%=cbEnableSsl.ClientID %>">Enable SSL</label>
        <asp:CheckBox runat="Server" ID="cbEnableSsl" /><br />
               
        <label for="<%=txtEmailSubjectPrefix.ClientID %>">Subject prefix</label>
        <asp:TextBox runat="server" ID="txtEmailSubjectPrefix" Width="300" /><br /><br />
        
        <asp:Button runat="server" CausesValidation="False" ID="btnTestSmtp" Text="Test mail settings" />
        <asp:Label runat="Server" ID="lbSmtpStatus" />
    </div>
    <div class="settings">
          <h1>Contact form</h1>
          <label for="<%=txtFormMessage.ClientID %>">Form message</label>
          <asp:TextBox runat="server" ID="txtFormMessage" TextMode="multiLine" Rows="5" Columns="40" /><br />
          
          <label for="<%=txtThankMessage.ClientID %>">Thank you message</label>
          <asp:TextBox runat="server" ID="txtThankMessage" TextMode="multiLine" Rows="5" Columns="40" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtThankMessage" ErrorMessage="Required" /><br />
          
          <label for="<%=cbEnableAttachments.ClientID %>">Enable attachments</label>
          <asp:CheckBox runat="Server" ID="cbEnableAttachments" />
    </div>
    <div class="settings">
        <h1>HTML head section</h1>
        <label for="<%=txtHtmlHeader.ClientID %>">
            Add custom code to the HTML head section
        </label>
        <asp:TextBox runat="server" ID="txtHtmlHeader" TextMode="multiLine" Rows="9" Columns="30"
            Width="400px" />
    </div>
    <div class="settings">
        <h1>Tracking script</h1>
        <label for="<%=txtTrackingScript.ClientID %>">
          Visitor tracking script - The JavaScript code from i.e. Google Analytics.
          Will be added in the bottom of each page regardless of the theme.(remember to add the &lt;script&gt; tags)
        </label>
        <asp:TextBox runat="server" ID="txtTrackingScript" TextMode="multiLine" 
            Rows="9" Columns="30" Width="400px" />
    </div>
    <div align="right">
        <asp:Button runat="server" ID="btnSave" Text="Save Settings" /></div>
    <br />
</asp:Content>

<asp:Content ContentPlaceHolderID="cphsidebar" ID="Content1" runat="server">
</asp:Content>