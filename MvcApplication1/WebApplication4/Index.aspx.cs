using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Form["action"];
            if (action == null || action.Trim().Equals(""))
            {
                action = Request.QueryString["action"];
            }
            switch (action)
            {
                case "custom": RedirectTest("CustomWeb/CustomList.aspx"); break;
                case "department": RedirectTest("DepartmentWeb/DepartmentList.aspx"); break;
                default: InitNow(); break;
            }
        }
        public void InitNow()
        {
            welcome.Text = "*3设计一个基于Web的应用程序，采用3层结构的方式实现对custom，department表中的记录进行：插入、修改、删除、查询的操作。"
                + "\n(作为课程设计要求学生写出详细的设计文档或实验报告)";
        }
        public void RedirectTest(string str)
        {
            Response.Redirect(str);
        }
    }
}