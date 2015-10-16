<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomList.aspx.cs" Inherits="Web.CustomWeb.CustomList" EnableViewState="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CustomList</title>
</head>
<body>
    <a href="/Index.aspx">Index</a>
    <table id="customList">
        <tr>
            <td>id</td>
            <td>cname</td>
            <td>departname</td>
            <td>age</td>
            <td>ename</td>
            <td>password</td>
        </tr>

        <%if (list != null && list.Count > 0)
          {
              foreach (var item in list)
              { %>
                <tr>
                    <td><%=item.id%></td>
                    <td><%=item.cname%></></td>
                    <td><%=item.department.departname%></td>
                    <td><%=item.age%></td>
                    <td><%=item.ename%></td>
                    <td><%=item.password%></td>
                    <td>
                        <a href="CustomCreate.aspx?id=<%=item.id %>">编辑</a>
                        <a href="CustomList.aspx?id=<%=item.id %>&action=del">删除</a>
                    </td>
                </tr>
            <%}
          } %>
    </table>
    
    <form id="viewStateForm" action="CustomList.aspx" method="post"> 
        <input type="text" name="custom_cname" placeholder="query_cname" value="<%=cname %>"/>

        <input type="hidden" value="query_cname" name="action" />
        <input type="submit" value="模糊查询" />
    </form>
    <input type="button" value="还原" onclick="location.href = 'CustomList.aspx';" />
    <input type="button" value="添加" onclick="location.href= 'CustomCreate.aspx'" />
</body>
</html>
