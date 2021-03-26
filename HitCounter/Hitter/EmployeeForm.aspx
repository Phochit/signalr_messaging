<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="Hitter.EmployeeForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <br />
        <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox> <br />
        <asp:TextBox ID="txtDept" runat="server"></asp:TextBox> <br />
        <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox> <br />
        <asp:Button ID="btnSaveEmployee" runat="server" Text="Save Employee" OnClick="btnSaveEmployee_Click" /> <br />
    </form>
</body>
</html>
