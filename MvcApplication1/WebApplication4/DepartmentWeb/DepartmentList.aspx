<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="Web.DepartmentWeb.DepartmentList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DepartmentList</title>
</head>
<body>
    <a href="/Index.aspx">Index</a>
    <table id="departmentList">
        <tr>
            <td>id</td>
            <td>departname</td>
            <td>description</td>
        </tr>

        <%if (list != null && list.Count > 0)
          {
              foreach (var item in list)
              { %>
                <tr>
                    <td><%=item.id%></td>
                    <td><%=item.departname%></></td>
                    <td><%=item.description%></td>
                    <td>
                        <a href="departmentCreate.aspx?id=<%=item.id %>">编辑</a>
                        <a href="departmentList.aspx?id=<%=item.id %>&action=del">删除</a>
                    </td>
                </tr>
            <%}
          } %>
    </table>
    
    <input type="button" value="刷新" onclick="location.href = 'departmentList.aspx';" />
    <input type="button" value="添加" onclick="location.href = 'departmentCreate.aspx'" />
</body>
</html>
