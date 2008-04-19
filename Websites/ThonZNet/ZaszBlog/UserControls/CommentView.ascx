<%@ Control Language="C#" EnableViewState="False" Inherits="Thon.ZaszBlog.Support.Web.Controls.CommentViewBase" %>

<div id="Div1" class="comment<%= Post.Author.Equals(Comment.Author, StringComparison.OrdinalIgnoreCase) ? " self" : "" %>">
  <p class="date"><%= Comment.DateCreated.ToString("MMMM d. yyyy HH:mm") %></p>
  <p class="gravatar"><%= Gravatar(80)%></p>
  <p class="content"><%= Text%></p>
  <p class="author">
    <%= Comment.Website != null ? "<a href=\"" + Comment.Website + "\">" + Comment.Author + "</a>" : Comment.Author %>
    <%= AdminLinks %>
  </p>
</div>