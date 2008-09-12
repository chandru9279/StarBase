<%@ Page Language="C#" MasterPageFile="~/ThonMasterPage.master" AutoEventWireup="true" CodeFile="FSSearch.aspx.cs" Inherits="Thon.FSSearchAspx" Title="Full Site Search" %>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
<style type="text/css" >
.searchresult{
	margin-bottom: 20px;
}
.searchresult span.text{
	clear: both;
	display: block;
	margin: 3px 0;
}
.searchresult span.type{
	display: block;
}
.searchresult span.url{
	color: Gray;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
    
    <h1 runat="server" id="h1Headline" />
    <br />
    
    <div id="searchpage">      
      <p style="font-size:small;">Search - powered by Thon.NET!</p><br />
      <input type="text" name="q" id="q" value="<%=Request.QueryString["q"] %>" onkeypress="if(event.keyCode==13) SearchPage()" />
      <input type="button" value="Search" onclick="SearchPage()" onkeypress="SearchPage()" />
      <% if (BlogSettings.Instance.EnableCommentSearch){ %>
      <input type="checkbox" name="comment" id="comment" /><label for="comment">Search ZBlog comments</label>
      <%} %>
    </div>
    
    <script type="text/javascript">      
      var check = document.getElementById('comment');
      
      function SearchPage()
      {        
        var searchTerm = encodeURIComponent(document.getElementById('q').value);
        var include = check.checked;
        var comment = '&comment=true';
        
        if (!include)
        {
          comment = ''
        }
        
        location.href = 'Search.aspx?q=' + searchTerm + comment;
      }
      
      check.checked = <%=(Request.QueryString["comment"] != null).ToString().ToLowerInvariant() %>;
    </script>
    
    <asp:repeater runat="server" id="rep" onitemdatabound="rep_ItemDataBound">
      <ItemTemplate>
        <div class="searchresult">
          <a href="<%# Eval("RelativeLink") %>"><%# Eval("Title") %></a>
          <span class="text"><%# GetContent((string)Eval("Description"), (string)Eval("Content")) %></span>
         <span class="type" runat="server" id="type" />
          <span class="url"><%# ShortenUrl((String)Eval("RelativeLink")) %></span>
        </div>
      </ItemTemplate>
    </asp:repeater>
    
    <label id="lblNoResults" style="font-size:medium;color:Navy;" runat="server" visible="false"/>    
    <asp:PlaceHolder ID="Paging" runat="server" />
    
    </label>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
</asp:Content>

