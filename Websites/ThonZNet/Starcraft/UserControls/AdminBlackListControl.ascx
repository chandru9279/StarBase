<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminBlackListControl.ascx.cs" Inherits="Thon.Starcraft.AdminBlackListControlAscx" %>
<style type="text/css">
    .SaveStyle
    {
        background:none;        
    }
    #AdminBLT td
    {
        border-style:dotted;
        border-width:0.5px;
        border-color:White;
        width:253px;
    }
    #AdminBLT
    {
        width: 221px;
    }
</style>
<table id="AdminBLT">
<tr>
<td class="style1"><asp:Label ID="Label1" runat="server" Text="Name"></asp:Label></td>
<td><asp:TextBox ID="Name" runat="server"></asp:TextBox>*</td>
<td rowspan="2">

<asp:Label ID="Label3" runat="server" Text="Rate It :"></asp:Label>
&nbsp;*<asp:RadioButtonList ID="Rating" runat="server" Height="16px" 
    Width="65px">
    <asp:ListItem>1</asp:ListItem>
    <asp:ListItem>2</asp:ListItem>
    <asp:ListItem>3</asp:ListItem>
    <asp:ListItem>4</asp:ListItem>
    <asp:ListItem>5</asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
<tr>
<td><asp:Label ID="Label2" runat="server" Text="Genre"></asp:Label></td>
<td><asp:DropDownList ID="Genre" runat="server" ToolTip="Choose a Genre">
    <asp:ListItem>RTS</asp:ListItem>
    <asp:ListItem>RPG</asp:ListItem>
    <asp:ListItem>Sim</asp:ListItem>
    <asp:ListItem>CityBuilding</asp:ListItem>
    <asp:ListItem>FPS</asp:ListItem>
    <asp:ListItem>TPS</asp:ListItem>
    <asp:ListItem>Arcade</asp:ListItem>
    <asp:ListItem>Sports</asp:ListItem>
    <asp:ListItem>Racing</asp:ListItem>
    <asp:ListItem>Adventure</asp:ListItem>
    <asp:ListItem>Strategy</asp:ListItem>
    <asp:ListItem>Other</asp:ListItem>
    <asp:ListItem>TurnBased</asp:ListItem>
</asp:DropDownList>*</td>
</tr>
<tr><td colspan="3">
<center><asp:Button ID="Add" runat="server" Text="Add It Now!" BorderColor="Blue" 
        BorderStyle="Dashed" CssClass="SaveStyle" ForeColor="White" 
        onclick="Add_Click" /></center>
</td></tr>
</table>

* Required<br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="Name" ErrorMessage="Name Required"></asp:RequiredFieldValidator>
<br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ErrorMessage="Genre Required" ControlToValidate="Genre"></asp:RequiredFieldValidator>
<br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
    ErrorMessage="Rating is required" ControlToValidate="Rating"></asp:RequiredFieldValidator>


<br />
<asp:Label ID="ErrorLbl" runat="server" 
    Text="Sorry about this, the site is experiencing technical difficulties. Please try adding later!" 
    Visible="False"></asp:Label>



