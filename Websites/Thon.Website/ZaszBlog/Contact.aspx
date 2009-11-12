<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="ContactAspx" ValidateRequest="false" Title="Contact" MasterPageFile="~/ZaszBlog/ZaszBlogMasterPage.master" %>
<%@ Import Namespace="Thon.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <div id="contact">
    <div id="divForm" runat="server">
      <h1>Contact</h1>
      
      <div><%=ThonSettings.Instance.ContactFormMessage %></div>
      
      <label for="<%=txtName.ClientID %>">Name</label>
      <asp:TextBox runat="server" id="txtName" cssclass="field" />
      <asp:requiredfieldvalidator runat="server" controltovalidate="txtName" ErrorMessage="Please specify your name" /><br />
      
      <label for="<%=txtEmail.ClientID %>">E-Mail</label>
      <asp:TextBox runat="server" id="txtEmail" cssclass="field" />
      <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" display="dynamic" ErrorMessage="Please enter a valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
      <asp:requiredfieldvalidator runat="server" controltovalidate="txtEmail" ErrorMessage="Please specify your e-mail" /><br />
      
      <label for="<%=txtSubject.ClientID %>">Subject</label>
      <asp:TextBox runat="server" id="txtSubject" cssclass="field" />
      <asp:requiredfieldvalidator runat="server" controltovalidate="txtSubject" ErrorMessage="Please specify a subject" /><br />
      
      <label for="<%=txtMessage.ClientID %>">Message</label>
      <asp:TextBox runat="server" id="txtMessage" textmode="multiline" rows="5" columns="30" />
      <asp:requiredfieldvalidator runat="server" controltovalidate="txtMessage" ErrorMessage="Please write a message" display="dynamic" />    
      
      <asp:placeholder runat="server" id="phAttachment">      
        <label for="<%=txtAttachment.ClientID %>">Attach File</label>
        <asp:FileUpload runat="server" id="txtAttachment" />
      </asp:placeholder>
      
      <br /><br />
      
      <asp:button runat="server" id="btnSend" Text="Send" />    
      <asp:label runat="server" id="lblStatus" visible="false">This form does not work at the moment. Sorry for the inconvenience.</asp:label>
    </div>
    
    <div id="divThank" runat="Server" visible="False">      
      <%=ThonSettings.Instance.ContactThankMessage %>
    </div>
  </div>
</asp:Content>