<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <asp:Label ID="welcome" runat="server" Text="Label"></asp:Label>
    
    <br />
    <a href="CustomWeb/CustomList.aspx">custom</a>
    <a href="Index.aspx?action=custom">redirectToCustom</a>
    <br />
    <a href="DepartmentWeb/DepartmentList.aspx">department</a>
    <a href="Index.aspx?action=department">redirectToDepartment</a>

</body>
</html>
