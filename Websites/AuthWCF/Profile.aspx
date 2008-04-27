<%@ Page Language="C#" AutoEventWireup="true" 
CodeFile="Profile.aspx.cs" Inherits="ProfileInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Profile Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Current Authenticated User Profile Information</h3> 

        <a href="Default.aspx">back to default page</a>

        <h4>Read Profile Information</h4>
        
        <h5>Calendar</h5>
        <table>
        <thead>
        <tr>
        <th>Profile MIME</th>
        <th>Value</th>
        </tr>
        </thead>
            <tr>
                <td align="left">User Name</td>
                <td align="left">
                    <asp:Label ID="Label1" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">View</td>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">CustomViewDays</td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">TimeFormat</td>
                <td>    
                    <asp:Label ID="Label4" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">ShowNonBusinessHours</td>
                <td>    
                    <asp:Label ID="Label5" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">ExcludeRepositories</td>
                <td>    
                    <asp:Label ID="Label6" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">ExcludeMyDaybook</td>
                <td>    
                    <asp:Label ID="Label7" runat="server" Text="Label"/>
                </td>
            </tr>

        </table>
        <h5>Tasks</h5>
        <table>
        <thead>
        <tr>
        <th>Profile MIME</th>
        <th>Value</th>
        </tr>
        </thead>
            <tr>
                <td align="left">HideCompletedTasks</td>
                <td align="left">
                    <asp:Label ID="Label8" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">HideTasksNotDueRecently</td>
                <td align="left">
                    <asp:Label ID="Label9" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">ExcludeRepositories</td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Label"/>
                </td>
            </tr>
            <tr>
                <td align="left">ExcludeMyDaybook</td>
                <td>    
                    <asp:Label ID="Label11" runat="server" Text="Label"/>
                </td>
            </tr>
        </table>
        
        
	    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
		    Text="Read Profile Information" />
	    
	    <hr />
    	
    	<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
		Text="Update Profile Data" />
		
    </div>
    </form>
</body>
</html>