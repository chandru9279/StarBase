<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SidePanel.ascx.cs" EnableViewState="false" EnableTheming="true"
    Inherits="UserControlsSidePanel" %>
<%@ Register Src="~/ZaszBlog/Admin/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<%@ Register Namespace="Thon.ZaszBlog.Controls" TagPrefix="ZaszBlog" %>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>


<% if (Page.User.Identity.IsAuthenticated)
   { %>
<div class="links">
    <br />
    <h2>
        Administration</h2>
    <uc1:menu ID="Menu1" runat="server" />
</div>
<%} %>
<div class="links">
    <br />
    <h2>
        Calendar</h2>
    <div style="text-align: center">
        <ZaszBlog:PostCalendar runat="Server" NextMonthText=">>" DayNameFormat="FirstTwoLetters"
            FirstDayOfWeek="monday" PrevMonthText="<<" CssClass="calendar" BorderWidth="0"
            WeekendDayStyle-CssClass="weekend" OtherMonthDayStyle-CssClass="other" UseAccessibleHeader="true"
            EnableViewState="false" />
        <br />
        <a href="<%=SupportUtilities.AbsoluteWebRoot %>calendar/default.aspx" />View posts in large calendar</a>
    </div>
</div>
<div class="links">
    <br />
    <h2>
        Pages
    </h2>
    <ZaszBlog:PageList runat="Server" />
</div>
<div class="links">
    <br />
    <h2>
        Archive
    </h2>
    <ZaszBlog:MonthList runat="server" />
</div>
<div class="links">
    <br />
    <h2>
        Authors
    </h2>
    <ZaszBlog:AuthorList runat="Server" />
</div>
<div class="links">
    <br />
    <h2>
        Tags
    </h2>
    <ZaszBlog:TagCloud runat="server" />
</div>
<div class="links">
    <br />
    <h2>
        Categories
    </h2>
    <ZaszBlog:CategoryList runat="Server" />
    <br />
    <a href="~/ZaszBlog/Archive.aspx" runat="Server">Archive</a>
</div>
<div class="links">
    <br />
    <h2>
        Blog Roll
    </h2>
    <ZaszBlog:Blogroll runat="server" />
    <a href="<%=SupportUtilities.RelativeWebRoot%>ZaszBlogHttpHandlers/Opml.ashx" style="display: block; text-align: right" title="Download OPML file">
        Download OPML file
        <asp:Image ID="Image1" runat="server" ImageUrl="~/ZaszBlog/Images/Opml.png" AlternateText="OPML" /></a>
</div>
<div class="links">
    <br />
    <h2>
        Disclaimer</h2>
    <ul>
        The opinions and content expressed herein are solely the author's own personal opinions.
        They do not represent the views of any person or organization related to the author or the 
        author's employer in anyway.<br />
        <br />
        &copy; Copyright
        <%=DateTime.Now.Year %>
        <br />
        <br />
        <asp:LoginStatus runat="Server" LoginText="Sign In" LogoutText="Sign Out" EnableViewState="false" />
    </ul>
</div>
