using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.DepartmentWeb
{
    public partial class DepartmentList : System.Web.UI.Page
    {
        private static DataAppDataContext db = new DataAppDataContext();

        public department entity = new department(); // 实体
        public List<department> list = new List<department>(); // 列表

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Form["action"];
            if (action == null || action.Trim().Equals(""))
            {
                action = Request.QueryString["action"];
            }
            switch (action)
            {
                case "del": Delete(); break;
                default: InitList(); break;
            }
        }
        // 初始化列表
        public void InitList()
        {
            var dbList = from s in db.department
                         select s;
            list = dbList.ToList();
        }
        // 删除记录
        public void Delete()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            var dbList = from s in db.department
                         where s.id == id
                         select s;
            foreach (var s in dbList)
            {
                db.department.DeleteOnSubmit(s);
            }
            db.SubmitChanges();

            InitList();
        }
    }
}