<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentCreate.aspx.cs" Inherits="Web.DepartmentWeb.DepartmentCreate" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DeparmentCreate</title>
</head>
<body>    
    <a href="/Index.aspx">Index</a>
    <form action="departmentCreate.aspx" method="post">
        <input type="text" name="entity.departname" value="<%=entity.departname %>" placeholder="departname"/>        
        <input type="text" name="entity.description" value="<%=entity.description %>" placeholder="description" />

        <input type="hidden" name="entity.id" value="<%=entity.id %>" />
        <input type="hidden" name="action" value="create" />
        <input type="submit" value="保存" />
        <input type="button" value="返回" onclick="location.href = 'departmentList.aspx';" />
    </form>    
</body>
</html>
