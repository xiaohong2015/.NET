using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Model;

namespace Web.CustomWeb
{
    public partial class CustomList : System.Web.UI.Page
    {
        private static DataAppDataContext db = new DataAppDataContext();

        public custom entity = new custom();  // 实体
        public List<custom> list = new List<custom>(); // 列表
        public string cname = ""; // 查询项

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Form["action"];
            if (action == null || action.Trim().Equals(""))
            {
                action = Request.QueryString["action"];
            }
            switch (action)
            {
                case "query_cname": QueryCname(); break;
                case "del": Delete(); break;
                default: InitList(); break;
            }
        }

        // 初始化列表
        public void InitList()
        {
            var dbList = from s in db.custom
                         select s;
            list = dbList.ToList();
        }
        // 查询cname
        public void QueryCname()
        {
            cname = Request.Form["custom_cname"];
            if (cname != null && !cname.Trim().Equals(""))
            {
                var dbList = from s in db.custom
                             where s.cname == cname
                             select s;
                list = dbList.ToList();
            }
        }
        // 删除记录
        public void Delete()
        {
            int id= Convert.ToInt32(Request.QueryString["id"]);
            var dbList = from s in db.custom
                         where s.id == id
                         select s;
            foreach (var s in dbList)
            {
                db.custom.DeleteOnSubmit(s);
            }
            db.SubmitChanges();

            InitList();
        }
        
        // 可以删除该方法
        public void JsonTest()
        {
            // 转换 json
            var data = new[] {
                new { name="xiaohong", age=25},
                new {name="xiaoming", age=22}
            };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Response.Write(jss.Serialize(data));
            Response.End();
        }
    }
}