<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchControl.ascx.cs" Inherits="Shelf.Web.UserControls.SearchControlAscx" %>
<%@ Import Namespace="Shelf.Support.Searching" %>
<%@ Import Namespace="Shelf.Support" %>
<script runat="server">

</script>
<%--
Panel that is visible when the search page is first visited
--%>
<asp:Panel id="pnlHomeSearch" runat="server">
        <center>
        <p class="heading"><font color="red">S</font><font color="blue">h</font><font color="green">e</font><font color="orange">l</font><font color="navy">f</font>.<font color="maroon">Search</font> <font color="#990000"><sup>Beta</sup></font></p>
        <table align="center" cellspacing="0" cellpadding="4" frame="box" bgcolor="#ffffcc" rules="none" style=" border-color:#dcdcdc;border-collapse: collapse">
            <tr>
                <td colspan="2">
                <p class="intro">Search for ...<br /><font color="red"><%=_ErrorMessage%></font>
                    <asp:TextBox ID="SearchPage_TextBox" Columns="40" runat="server"></asp:TextBox>
                </p>
                </td>
            </tr>
            <tr>
		        <td align="center">
                    <asp:Button ID="SearchPage_SearchButton" runat="server" Text="Go !" Width="120" 
                        onclick="SearchPage_Search_Click" />
		        </td>
		        <td align="center">
                    <asp:CheckBox ID="SearchPage_Semantics" runat="server" Text="Enable Semantics" />
                </td>
	        </tr>
            <tr>
		        <td colspan="2" align="center"><a href="http://www.shelf-elibrary.net/">Shelf.Net</a> - <a href="http://www.chandruon.net/">T.C.</a>, <a href="http://www.intrepidkarthi.net/">N.G.K.</a>, <a href="http://www.fbi.gov/">K.P.</a></td>
	        </tr>
            <tr>
		        <td colspan="2" align="center"><p class="copyright">&copy; 2009 Shelf e-Library with Semantics</p></td>
	        </tr>
	        <tr>
		        <td colspan="2" align="center"><p>Searching <%=WordCount%> words</p></td>
	        </tr>
        </table>
        </center>
</asp:Panel>
<%--
Panel that is visible when search results are being shown
--%>
<asp:Panel id="pnlResultsSearch" runat="server">
        <center>
        <p class="heading" id="pHeading" runat="server"><font color="red">S</font><font color="blue">h</font><font color="green">e</font><font color="orange">l</font><font color="navy">f</font>.<font color="maroon">Search</font> <font color="#990000"><sup>Beta</sup></font></p>
        <table cellspacing="0" cellpadding="4" frame="box" rules="none" style="border-color:#dcdcdc; border-collapse: collapse" width="100%" bgcolor="#ffffcc">
            <tr>
                <td>
                <table cellpadding="3" cellspacing="3">
                    <tr>
                    <td>Search for :</td>
                    <td><asp:TextBox ID="ResultsPage_TextBox" runat="server" Columns="50"></asp:TextBox></td>
                    <td><asp:Button ID="ResultsPage_SearchButton" runat="server" Text="Go !" 
                            Width="120" onclick="ResultsPage_SearchButton_Click" /></td>
                    <td><asp:CheckBox ID="ResultsPage_Semantics" runat="server" Text="Enable Semantics" /></td>
                    </tr>
                </table>
                </td>
            </tr>
			<tr id="rowSummary" visible="true" runat="server"><td><p class="copyright">Searching <%=WordCount%> words</p></td></tr>
            <tr id="rowFooter1" visible="false" runat="server"><td>©2009
                <a href="http://www.shelf-elibrary.net/">Shelf.Net</a> -
                <a href="http://www.chandruon.net/">T.C.</a>,
                <a href="http://www.intrepidkarthi.net/">N.G.K.</a>,
                <a href="http://www.fbi.gov/">K.P.</a></td></tr>
            <tr id="rowFooter2" visible="false" runat="server"><td><p class="copyright">Shelf 
                e-Library with Semantics</p></td></tr>
        </table>
        </center>
</asp:Panel>