<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsItem.ascx.cs" Inherits="NewsItemAscx" %>
<div class="tab_text">
    <p><span class="tab_head1"><%=Title %></span> - <%=Date %><br />
    <%=Content %></p>
</div>
<div class="tab_readmore">
    <p align="right" class="tab_head"><a href="<%=Link %>" class="readmore">Read More </a></p>
</div>