<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostList.ascx.cs" EnableViewState="false" EnableTheming="true" Inherits="UserControlsPostList" %>
<div runat="server" id="posts" class="posts" />

<div id="postPaging">
  <a runat="server" ID="hlPrev" style="float:left">&lt;&lt; Previous posts</a>
  <a runat="server" ID="hlNext" style="float:right">Next posts &gt;&gt;</a>
</div>