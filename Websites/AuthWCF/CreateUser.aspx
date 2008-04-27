<%@ Page Language="C#" AutoEventWireup="true" 
CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add New User</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <h2>Add New User</h2>

        <a href="Default.aspx">back to default page</a>

		<asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
		  OnCreatedUser="On_CreatedUser">
			<wizardsteps>
				<asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server"  />
				<asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server"  />
			</wizardsteps>
		</asp:CreateUserWizard>
		<p> 
		    Check the following box to assign the user to the administrator role.
		    Otherwise, the user will be assigned to the friends role by default. 
		</p>
		<span style="font-weight:bold; color:Red">Administrator</span> 
		<asp:CheckBox ID="CheckBox1" runat="server" />

    </div>
    </form>
</body>
</html>