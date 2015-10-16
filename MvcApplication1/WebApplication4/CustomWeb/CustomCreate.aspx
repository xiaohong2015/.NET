<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCreate.aspx.cs" Inherits="Web.CustomWeb.CustomCreate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CustomCreate</title>
</head>
<body>
    <a href="/Index.aspx">Index</a>
    <form action="CustomCreate.aspx" method="post">
        <input type="text" name="entity.cname" value="<%=entity.cname %>" placeholder="cname"/>
        
        <select name="entity.departID">
            <%foreach (var n in departmentList)
              {
                  if (entity.departID == n.id)
                  {%>
            <option value="<%=n.id %>" selected="selected"><%=n.departname %></option>
            <%}
                  else
                  { %>
            <option value="<%=n.id %>"><%=n.departname %></option>
            <%}
              }%>
        </select>

        <input type="text" name="entity.age" value="<%=entity.age %>" placeholder="age" />
        <input type="text" name="entity.ename" value="<%=entity.ename %>" placeholder="ename" />
        <input type="text" name="entity.password" value="<%=entity.password %>" placeholder="password" />

        <input type="hidden" name="entity.id" value="<%=entity.id %>" />
        <input type="hidden" name="action" value="create" />
        <input type="submit" value="保存" />
        <input type="button" value="返回" onclick="location.href = 'CustomList.aspx';" />
    </form>    
</body>
</html>
