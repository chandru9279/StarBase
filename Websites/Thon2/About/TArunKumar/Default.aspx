<%@ Page Language="C#" MasterPageFile="~/About/TArunKumar/TAKMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Thon.About.TAKDefaultAspx" Title="T Arun Kumar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbody" Runat="Server">
<table border="0" width="100%" id="table158" cellspacing="0" cellpadding="0">
    <tr>
    <td height="21" colspan="2">
    <p align="center">
    <font size="6" face="Verdana">&nbsp;About T Arun Kumar</font>
    </p>
    </td>
    </tr>
    
    <tr>
    <td colspan="2">
    <table width="100%">
    <tr>
    <td><p align="left">
    <font size="2" face="Verdana">T Arun Kumar, B.Com. NIIT.<br /> Head, <br /> Trade, <br />Scope International, <br /> Tianjin, China.</font>
    <br /><br />
    <font size="3" face="Verdana">Contact :</font>
    <br />
    <font size="2" face="Verdana">Plot no 66-A, Door No. 10, <br /> Second Street, <br /> Venkatesa Nagar, <br /> Virugambakkam, <br /> Chennai - 600 092. <br />
    <% if (Page.User.Identity.IsAuthenticated)
       { %>
        Mobile : 9486149264 <br />Landline : 044 - 23766766
    <% }
       else
       { %>
       <i>Other contact details are shown only for Registered Persons</i>
    <% } %>
    </font>
    </p></td>
    <td align="right"><img src="../Images/ArunKumarRed.jpg" alt="T Arun Kumar" width="150" height="217"/></td>        
    </tr>
    </table>
    </td>
    </tr>
    
    <tr>
    <td height="21" colspan="2">
    <a href="http://www.jackclaver.com/Default.aspx" style="text-decoration:none;color:Maroon;">
    <p align="center" style="margin:20px 20px 20px 20px;border-style:double">
    <font size="5" face="Verdana">Go to his Website www.JackClaver.com</font>
    </p>
    </a>
    </td>
    </tr>
    
    <tr>
    <td height="21" colspan="2">
    <p align="center">
    <img border="0" alt="Under Construction" src="../../Images/UnderConstruction.gif" width="200" height="200"></p></td>
    </tr>
    
    <tr>
    <td height="21" colspan="2">
    <p align="center">
    <font size="5" color="#FF9900" face="Verdana">This part of the website is currently under 
    construction&nbsp; 
    </font>
    </p>
    </td>
    </tr>
    
</table>
</asp:Content>

