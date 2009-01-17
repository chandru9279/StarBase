<%@ Page Language="C#" MasterPageFile="~/Shelf.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Shelf.Web.SearchAspx" Title="Search" %>
<%@ Import Namespace="Shelf.Web.UserControls" %>
<%@ Register Src="~/UserControls/SearchControl.ascx" TagName="ShelfSearchControl" TagPrefix="TCNGKKP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

        <TCNGKKP:ShelfSearchControl ID="ucSearchPanelHeader" runat="server" />
        
        <asp:Panel id="lblNoSearchResults" visible="false" runat="server">
			Your search - <b><%=_SearchTerm%></b> - did not match any documents. 
            <br />
            <br />It took <%=_DisplayTime%>.
            <p>
                Suggestions:</p>
            <ul>
                <li>Check your spelling</li>
                <li>Try similar meaning words (synonyms)</li>
                <li>Try fewer keywords: <%=_Matches%></li>
            </ul>
            <br />
            <br />
        </asp:Panel>
		
		<asp:Repeater id="SearchResults" runat="server">
		
		    <HeaderTemplate>
			    <p><%=_NumberOfMatches%> results for <%=_Matches%> took <%=_DisplayTime%></p>
		    </HeaderTemplate>
		    
		    <ItemTemplate>
		    <p>
		        <font color="blue" size="-1"><asp:literal ID="Literal1" runat="server" 
		            Visible='<%# (string)DataBinder.Eval(Container.DataItem, "Extension") != "html" %>'
			        Text='<%# DataBinder.Eval(Container.DataItem, "Extension") %>' /></font>
			        
			    <a href="<%# DataBinder.Eval(Container.DataItem, "PathAndName") %>"> <b><%# DataBinder.Eval(Container.DataItem, "Title") %></b></a>
			    
			    <a href="<%# DataBinder.Eval(Container.DataItem, "PathAndName") %>" target="_blank" title="open in new window" style="font-size:x-small">&uarr;</a>
			    
			    <font color="gray">(<%# DataBinder.Eval(Container.DataItem, "Rank") %>)</font>
			    
			    <br/><%# DataBinder.Eval(Container.DataItem, "Description") %>...
			    
			    <font color="brown">
			    <asp:literal ID="Literal3" runat="server" 
			        Visible='<%# (string)DataBinder.Eval(Container.DataItem, "KeywordString") != "" %>'
			        Text='<br /><%# DataBinder.Eval(Container.DataItem, "KeywordString") %>' />
			    </font>
			        
			    <br /><font color="green"><%# DataBinder.Eval(Container.DataItem, "PathAndName") %> - <%# DataBinder.Eval(Container.DataItem, "Size") %>bytes</font>
			    
			    <font color="gray"> - <%# DataBinder.Eval(Container.DataItem, "Extension") %> - <%# DataBinder.Eval(Container.DataItem, "CrawledDate") %></font>
			</p>
		    </ItemTemplate>
		    
		    <FooterTemplate>
			    <p><%=CreatePagerLinks(_PagedResults, Request.Url.ToString() )%></p>
		    </FooterTemplate>
		    
		</asp:Repeater>
		
		<TCNGKKP:ShelfSearchControl ID="ucSearchPanelFooter" runat="server" Visible="false" IsSearchResultsPage="true" IsFooter="true" />
		
</asp:Content>
