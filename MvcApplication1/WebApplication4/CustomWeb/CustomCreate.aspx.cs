using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.CustomWeb
{
    public partial class CustomCreate : System.Web.UI.Page
    {
        private static DataAppDataContext db = new DataAppDataContext();

        public custom entity = new custom();
        public List<department> departmentList = new List<department>();
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 跳转指定action
            string action = Request.Form["action"];
            if (action == null || action.Trim().Equals(""))
            {
                action = Request.QueryString["action"];
            }
            switch (action)
            {
                case "create": Create(); break;
                default: InitCreateView(); break;
            }
        }
        // 获取 department 所有 departname 
        private void DepartmentList()
        {
            var dbList = from s in db.department
                         select s;
            departmentList = dbList.ToList();
        }
        public void InitCreateView()
        {
            if (id == 0)
            {
                // 根据 id 获取实体类
                id = Convert.ToInt32(Request.QueryString["id"]);
            }

            var dbList = from s in db.custom
                         join t in db.department on s.departID equals t.id
                         where s.id == id
                         select s;
            if (dbList != null&& dbList.Count()>0)
            {
                entity = dbList.First();
            }
            if (entity == null)
            {
                entity = new custom();
            }
            DepartmentList();  // 调用获取所有departname
        }
        public void Create()
        {
            // 写入变量custom
            entity = new custom();
            entity.id = Convert.ToInt32(Request.Form["entity.id"]);
            entity.cname = Request.Form["entity.cname"];
            entity.departID = Convert.ToInt32(Request.Form["entity.departID"]);
            entity.age = Convert.ToInt32(Request.Form["entity.age"]);
            entity.ename = Request.Form["entity.ename"];
            entity.password = Request.Form["entity.password"];

            id = entity.id;
            string message = "error";

            if (entity != null)
            {
                // insert check by the departID
                var dbList = from s in db.department
                             where s.id == entity.departID
                             select s;

                if (dbList != null && dbList.First() != null)
                {
                    message = "success";
                    if (id < 1)
                    {
                        // insert custom
                        db.custom.InsertOnSubmit(entity);
                        db.SubmitChanges();
                    }
                    else
                    {
                        // update Custom
                        custom b_custom = db.custom.SingleOrDefault(s => s.id == id);
                        b_custom.cname = entity.cname;
                        b_custom.departID = entity.departID;
                        b_custom.ename = entity.ename;
                        b_custom.password = entity.password;
                        db.SubmitChanges();
                    }
                }
            }

            if (id < 1)
            {
                // update
                id = entity.id;
            }

            // 重新初始化
            InitCreateView();
            
            // 弹窗交互
            Response.Write("<script language=javascript>alert('"+ message+"');</script>"); 
        }
    }
}