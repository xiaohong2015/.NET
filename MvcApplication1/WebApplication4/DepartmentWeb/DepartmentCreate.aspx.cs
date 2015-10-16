using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.DepartmentWeb
{
    public partial class DepartmentCreate : System.Web.UI.Page
    {
        private static DataAppDataContext db = new DataAppDataContext();

        public department entity = new department();
        public List<department> list = new List<department>();
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
        public void InitCreateView()
        {
            if (id == 0)
            {
                // 根据 id 获取实体类
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            var dbList = from s in db.department
                         where s.id == id
                         select s;

            if (dbList != null && dbList.Count() > 0)
            {
                entity = dbList.First();
            }
            if (entity == null)
            {
                entity = new department();
            }
        }
        public void Create()
        {
            entity = new department();
            entity.id = Convert.ToInt32(Request.Form["entity.id"]);
            entity.departname = Request.Form["entity.departname"];
            entity.description = Request.Form["entity.description"];

            string message = "error";
            id = entity.id;
            if (entity != null)
            {
                var b_department = db.department.SingleOrDefault(s => s.departname == entity.departname);

                int checkId = 0;
                if (b_department != null)
                {
                    checkId = b_department.id;
                }

                if (id < 1)
                {
                    if (checkId == 0)
                    {
                        db.department.InsertOnSubmit(entity);
                        db.SubmitChanges();

                        message = "success";
                    }
                }
                else
                {
                    if (checkId == 0 || id == checkId)
                    {
                        var b_department2 = db.department.Single(s => s.id == entity.id);
                        b_department2.departname = entity.departname;
                        b_department2.description = entity.description;
                        db.SubmitChanges();

                        message = "success";
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
            Response.Write("<script language=javascript>alert('"+message+"');</script>");
        }
    }
}