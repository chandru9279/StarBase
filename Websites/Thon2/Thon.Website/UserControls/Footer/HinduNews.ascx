<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HinduNews.ascx.cs" Inherits="HinduNewsAscx" %>
<%@ Reference Control="~/UserControls/Footer/NewsItem.ascx" %>
<div style="overflow:scroll;float:left;width:410px;height:190px;">
<div class="tab_text">
    <p class="tab_head">News from The Hindu :</p>
</div>
<asp:PlaceHolder runat="server" ID="nph" />
</div>