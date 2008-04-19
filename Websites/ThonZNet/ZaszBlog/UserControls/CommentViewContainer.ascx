<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentViewContainer.ascx.cs" Inherits="UserControlsCommentView" EnableTheming="true"%>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>
<%@ Reference Control="~/ZaszBlog/UserControls/CommentView.ascx" %>

<% if (Post.Comments.Count > 0){ %>
<h1 id="comment">Comments</h1>
<%} %>

<div id="commentlist">
  <asp:PlaceHolder runat="server" ID="phComments" />
</div>

<asp:PlaceHolder runat="Server" ID="phAddComment">
    
<img src="<%=SupportUtilities.RelativeWebRoot %>Images/Ajax-Loader.gif" alt="Saving the comment" style="display:none" id="ajaxLoader" />  
<span id="status"></span>

<div class="commentForm">
  <h1 id="addcomment">Add comment</h1>

  <label for="<%=txtName.ClientID %>">Name*</label>
  <asp:TextBox runat="Server" ID="txtName" TabIndex="1" ValidationGroup="AddComment" />
  <asp:CustomValidator runat="server" ControlToValidate="txtName" ErrorMessage=" Please choose another name" Display="dynamic" ClientValidationFunction="CheckAuthorName" EnableClientScript="true" ValidationGroup="AddComment" />
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="Required" Display="dynamic" ValidationGroup="AddComment" /><br />

  <label for="<%=txtEmail.ClientID %>">E-Mail*</label>
  <asp:TextBox runat="Server" ID="txtEmail" TabIndex="2" ValidationGroup="AddComment" />
  <span id="gravatarmsg">
  
  <%if (BlogSettings.Instance.Avatar != "none" && BlogSettings.Instance.Avatar != "monster"){ %>
  (Will show your <a href="http://www.gravatar.com" target="blank">Gravatar</a> icon.)
  <%} %>
  </span>
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" Display="dynamic" ValidationGroup="AddComment" />
  <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AddComment" /><br />

  <label for="<%=txtWebsite.ClientID %>">Website</label>
  <asp:TextBox runat="Server" ID="txtWebsite" TabIndex="3" ValidationGroup="AddComment" />
  <asp:RegularExpressionValidator runat="Server" ControlToValidate="txtWebsite" ValidationExpression="(http://|https://|)([\w-]+\.)+[\w-]+(/[\w- ./?%&=;~]*)?" ErrorMessage="Please enter a valid URL" Display="Dynamic" ValidationGroup="AddComment" /><br />
  
  <label for="<%=txtContent.ClientID %>">Comment*</label> <span class="bbcode" title="BBCode tags">[b][/b] - [i][/i] - [u][/u]- [quote][/quote]</span>
  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContent" ErrorMessage="Required" Display="dynamic" ValidationGroup="AddComment" /><br />
  <asp:TextBox runat="server" ID="txtContent" TextMode="multiLine" Columns="50" Rows="10" TabIndex="5" onkeyup="ShowCommentPreview('livepreview', this)" ValidationGroup="AddComment" /><br />
    
  <input type="checkbox" id="cbNotify" style="width: auto" tabindex="6" />
  <label for="cbNotify" style="width:auto;float:none;display:inline">Notify me when new comments are added</label><br /><br />
 
  <input type="button" id="btnSave" value="Save Comment" onclick="if(Page_ClientValidate()){AddComment()}" tabindex="7" />  
  <asp:Button runat="server" ID="btnSave" style="display:none" Text="Add comment" UseSubmitBehavior="false" TabIndex="7" ValidationGroup="AddComment" />
  
  <% if (BlogSettings.Instance.ShowLivePreview) { %>  
  <h2>Live preview</h2> 
  <div id="livepreview">
    <asp:PlaceHolder runat="Server" ID="phLivePreview" />
  </div>
  <%} %>
</div>

<script type="text/javascript">
<!--//
if (!isAjaxSupported)
{
  $('<%=btnSave.ClientID %>').style.display = "inline";
  $('btnSave').style.display = "none";
}

function AddComment()
{
  $("btnSave").disabled = true;
  $("ajaxLoader").style.display = "inline";
  $("status").className = "";
  $("status").innerHTML = "Saving the comment...";
  
  var author = $("<%=txtName.ClientID %>").value;
  var email = $("<%=txtEmail.ClientID %>").value;
  var website = $("<%=txtWebsite.ClientID %>").value;
  var content = $("<%=txtContent.ClientID %>").value;
  var notify = $("cbNotify").checked;
  var argument = author + "-|-" + email + "-|-" + website + "-|-" + content + "-|-" + notify;
  <%=Page.ClientScript.GetCallbackEventReference(this, "argument", "AppendComment", "'comment'") %>
  
  if (typeof OnComment != "undefined")
    OnComment(author, email, website, country, content);
}

function AppendComment(args, context)
{
  if (context == "comment")
  {
    if ($("commentlist").innerHTML == "")
      $("commentlist").innerHTML = "<h1 id='comment'>The comment was saved. Thank you for the feedback.</h1>"
    $("commentlist").innerHTML += args;
    $("<%=txtContent.ClientID %>").value = "";
    $("ajaxLoader").style.display = "none";
    $("status").className = "success";
    $("status").innerHTML = "The comment was saved. Thank you for the feedback";
  }
  
  $("btnSave").disabled = false;
}

function CheckAuthorName(sender, args)
{
  args.IsValid = true;
  
  <% if (!Page.User.Identity.IsAuthenticated){ %>
  var author = "<%=Post.Author %>";
  var visitor = $("<%=txtName.ClientID %>").value;  
  args.IsValid = !Equal(author, visitor);
  <%} %>
}
//-->
</script>

<% if (BlogSettings.Instance.IsCoCommentEnabled){ %>
<script type="text/javascript">
// this ensures coComment gets the correct values
coco =
{
     tool          : "BlogEngine",
     siteurl       : "<%=SupportUtilities.AbsoluteWebRoot %>",
     sitetitle     : "<%=BlogSettings.Instance.Name %>",
     pageurl       : "<%=Request.Url %>",
     pagetitle     : "<%=this.Post.Title %>",
     author        : "<%=this.Post.Author %>",
     formID        : "<%=Page.Form.ClientID %>",
     textareaID    : "<%=txtContent.UniqueID %>",
     buttonID      : "<%=btnSave.ClientID %>"
}
</script>
<script id="cocomment-fetchlet" src="http://www.cocomment.com/js/enabler.js" type="text/javascript">
</script>
<%} %>
</asp:PlaceHolder>

<asp:label runat="server" id="lbCommentsDisabled" visible="false">Comments are closed.</asp:label>

<%=VirtualPathUtility.ToAbsolute("~/ZaszBlog/category/")%>