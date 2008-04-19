<%@ Page Language="C#" MasterPageFile="~/ZaszBlog/Admin/ZaszBlogAdmin.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="AdminConfiguration"
    Title="Settings" %>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">
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
        
        <label for="<%=txtPostsPerPage.ClientID %>">Posts per page</label>
        <asp:TextBox runat="server" ID="txtPostsPerPage" Width="50" MaxLength="4" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPostsPerPage" ErrorMessage="Required" />
        <asp:CompareValidator runat="server" ControlToValidate="txtPostsPerPage" Operator="DataTypeCheck" Type="integer" ErrorMessage="Please enter a valid number" /><br />
                
        <label for="<%=cbShowRelatedPosts.ClientID %>">Show related posts</label>
        <asp:CheckBox runat="server" ID="cbShowRelatedPosts" /><br />
        
        <label for="<%=cbEnableRating.ClientID %>">Enable post ratings</label>
        <asp:CheckBox runat="server" ID="cbEnableRating" /><br />
        
        <label for="<%=cbShowDescriptionInPostList.ClientID %>">Only show description of posts</label>
        <asp:CheckBox runat="server" ID="cbShowDescriptionInPostList" /><br />
                
        <label for="<%=cbShowPostNavigation.ClientID %>">Show post navigation links</label>
        <asp:CheckBox runat="server" ID="cbShowPostNavigation" /><br />
        
    </div>    
    <div class="settings">
        <h1>Advanced Settings></h1>
        
        <label for="">Enable Trackbacks</label>
        <asp:CheckBox runat="server" ID="cbEnableTrackBackSend" />Send &nbsp;&nbsp;
        <asp:CheckBox runat="server" ID="cbEnableTrackBackReceive" />Receive<br />
        
        <label for="">Enable Pingbacks</label>
        <asp:CheckBox runat="server" ID="cbEnablePingBackSend" />Send &nbsp;&nbsp;
        <asp:CheckBox runat="server" ID="cbEnablePingBackReceive" />Receive<br />
        
        <label for="<%=rblWwwSubdomain.ClientID %>">www subdomain policy</label>
        <asp:RadioButtonList runat="server" ID="rblWwwSubdomain" RepeatLayout="flow" RepeatDirection="horizontal">
            <asp:ListItem Text="Remove" Value="remove" />
            <asp:ListItem Text="Enforce" Value="add" />
            <asp:ListItem Text="Ignore" Value="" Selected="true" />
        </asp:RadioButtonList>
    </div>
    <div class="settings">
        <h1>Comments</h1>
        
        <label for="<%=cbEnableComments.ClientID %>">Enable comments</label>
        <asp:CheckBox runat="server" ID="cbEnableComments" />If comments aren't enabled, nobody can write comments to any post.<br />
                
        <label for="<%=cbEnableCoComment.ClientID %>">Enable coComments</label>
        <asp:CheckBox runat="server" ID="cbEnableCoComment" /><br />
        
        <label for="<%=cbShowLivePreview.ClientID %>">Show live preview</label>
        <asp:CheckBox runat="server" ID="cbShowLivePreview" /><br />
        
        <label for="<%=rblAvatar.ClientID %>">Avatars</label>
        <asp:RadioButtonList runat="Server" ID="rblAvatar" RepeatLayout="flow" RepeatDirection="horizontal">
          <asp:ListItem Text="Gravatar" Value="gravatar" />
          <asp:ListItem Text="Monster" Value="monster" />
          <asp:ListItem Text="Combine" Value="combine" />
          <asp:ListItem Text="None" Value="none" />
        </asp:RadioButtonList><br />
        
        <label for="<%=cbEnableCommentsModeration.ClientID %>">Moderate comments</label>
        <asp:CheckBox runat="server" ID="cbEnableCommentsModeration" /><br />
        
        <label for="<%=ddlCloseComments.ClientID %>" style="position: relative; top: 4px">
            Close comments after
        </label>
        <asp:DropDownList runat="server" ID="ddlCloseComments">
            <asp:ListItem Text="Never" Value="0" />
            <asp:ListItem Text="1" />
            <asp:ListItem Text="2" />
            <asp:ListItem Text="3" />
            <asp:ListItem Text="7" />
            <asp:ListItem Text="10" />
            <asp:ListItem Text="14" />
            <asp:ListItem Text="21" />
            <asp:ListItem Text="30" />
            <asp:ListItem Text="60" />
            <asp:ListItem Text="90" />
        </asp:DropDownList>
        days.
    </div>
    <div class="settings">
        <h1>E-mail</h1>                
        <label for="<%=cbComments.ClientID %>">Send comment e-mail</label>
        <asp:CheckBox runat="Server" ID="cbComments" /><br />
    </div>
    <div class="settings">
        <h1>Feed Settings
        </h1>
        <label for="<%=ddlSyndicationFormat.ClientID %>" style="position: relative; top: 4px">Default feed output</label>
        <asp:DropDownList runat="server" ID="ddlSyndicationFormat">
            <asp:ListItem Text="RSS 2.0" Value="Rss" Selected="True" />
            <asp:ListItem Text="Atom 1.0" Value="Atom" />
        </asp:DropDownList>
        format.<br /><br />
        
        <label for="<%=txtPostsPerFeed.ClientID %>">Items shown in feed</label>
        <asp:TextBox runat="server" ID="txtPostsPerFeed" Width="50" MaxLength="4" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPostsPerFeed" ErrorMessage="Required" />
        <asp:CompareValidator runat="server" ControlToValidate="txtPostsPerPage" Operator="DataTypeCheck" Type="integer" ErrorMessage="Please enter a valid number" /><br />
                       
        <label for="<%=txtAlternateFeedUrl.ClientID %>">Alternate feed URL</label>
        <asp:TextBox runat="server" ID="txtAlternateFeedUrl"  Width="300" /> <em>(http://feeds.feedburner.com/username)</em>
        <asp:RegularExpressionValidator runat="Server" ControlToValidate="txtAlternateFeedUrl" ValidationExpression="(http://|https://|)([\w-]+\.)+[\w-]+(/[\w- ./?%&=;~]*)?" ErrorMessage="Please enter a valid URL" Display="Dynamic" />
    </div>
     
    <div class="settings">
      <h1>Import & Export</h1>
      <p>
        BlogEngine.NET uses BlogML as the format for exporting. The import support both BlogML and RSS.
        (<a href="http://blogml.org/">blogml.org</a>)
      </p>
      <input type="button" value="Import" onclick="location.href='http://dotnetblogengine.net/clickonce/blogimporter/blog.importer.application?url=<%=SupportUtilities.AbsoluteWebRoot %>&username=<%=Page.User.Identity.Name %>'" />&nbsp;&nbsp;
      <input type="button" value="Export" onclick="location.href='../../ZaszBlogHttpHandlers/BlogML.ashx'" />
    </div>
    <div align="right">
        <asp:Button runat="server" ID="btnSave" Text="Save Settings" /></div>
    <br />
</asp:Content>