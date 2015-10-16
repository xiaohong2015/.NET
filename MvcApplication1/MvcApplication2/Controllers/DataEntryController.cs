using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using MvcApplication1.Model_Custom;
using MvcApplication1.Model_Department;

using System.Web.Script.Serialization;
using System.Data;

namespace MvcApplication1.Controllers
{
    public class DataEntryController : Controller
    {
        // 数据库连接字符串 connect--command--execute
        private string strcon = "Data Source=XIAOHONG-PC;Initial Catalog=asp;Persist Security Info=True;User ID=sa;Password=xiaohong";
        
        // 写入 json
        public void TakeJson(Object o) {
            JavaScriptSerializer jss = new JavaScriptSerializer();            
            Response.ContentType = "application/json";
            Response.Write(jss.Serialize(o));
            Response.End();
        }

        // 获取 Custom、Department两个表的所有数据
        public ActionResult DataEntry()
        {
            ViewBag.list1 = ListCustom();

            ViewBag.list2 = ListDepartment();

            return View();
        }

        // 返回 Custom 表所有行
        public List<Custom> ListCustom()
        {
            /*
            
            create proc ListCustom
            as
            select * from Custom a, Department b where a.departID=b.id
            
            */
            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string strsql1 = "select * from Custom a, Department b where a.departID=b.id";
            string strsql1 = "ListCustom";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql1;
            SqlDataReader rd1 = cmd.ExecuteReader();
            List<Custom> list1 = new List<Custom>();
            Custom n = null;
            while (rd1.Read())
            {
                n = new Custom();
                n.department = new Department();

                n.id = Convert.ToInt32(rd1["id"]);
                n.cname = rd1["cname"].ToString();
                n.age = Convert.ToInt32(rd1["age"]);
                n.ename = rd1["ename"].ToString();
                n.password = rd1["password"].ToString();

                //n.departID = Convert.ToInt32(rd["departID"]);
                n.department.id = Convert.ToInt32(rd1["departID"]);
                n.department.departname = rd1["departname"].ToString();
                //n.department.description = rd1["description"].ToString();

                list1.Add(n);
            }
            rd1.Close();
            con.Close();
            return list1;
        }

        // 返回 Department 表所有行
        public List<Department> ListDepartment()
        {
            /*
            
            create proc ListDepartment
            as
            select * from Department 
            
            */
            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Department list
//            string strsql2 = "select * from Department";
            string strsql2 = "ListDepartment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql2;
            SqlDataReader rd2 = cmd.ExecuteReader();
            List<Department> list2 = new List<Department>();
            Department m = null;
            while (rd2.Read())
            {
                m = new Department();

                m.id = Convert.ToInt32(rd2["id"]);
                m.departname = rd2["departname"].ToString();
                m.description = rd2["description"].ToString();

                list2.Add(m);
            }
            rd2.Close();
            con.Close();
            return list2;
        }

        // 删除一条 Custom 记录
        public void DeleteCustom(int id)
        {
            /*
            
            create proc DeleteCustom
            @id int
            as
            delete from Custom where id=@id
            
            */
            if (id < 1)  return;

            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string strsql = "delete from Custom where id=@id";
            string strsql = "DeleteCustom";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql;
            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        
        // 删除一条 Department 记录
        public void DeleteDepartment(int id)
        {
            /*
            
            create proc DeleteDepartment
            @id int
            as
            delete from Department where id=@id
            
            */
            if (id < 1) return;

            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string strsql = "delete from Department where id=@id";
            string strsql = "DeleteDepartment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql;
            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        
        // 获取 id, departname 所有记录
        public void DepartnameList()
        {
            /*
            
            create proc DepartnameList
            as
            select id, departname from Department
            
            */
            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string strsql = "select id, departname from Department";
            string strsql = "DepartnameList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql;
            SqlDataReader rd = cmd.ExecuteReader();

            // department's id and departname list
            List<Department> list = new List<Department>();
            Department n = null;
            while (rd.Read())
            {
                n = new Department();
                n.id = Convert.ToInt32(rd["id"]);
                n.departname = rd["departname"].ToString();

                list.Add(n);
            }
            rd.Close();
            con.Close();        

            this.TakeJson(list);                
        }
        
        // 根据 id 获取 Custom 一条记录
        public void CreateCustomView(int id)
        {
            /*
            
            create proc CreateCustomView
            @id int
            as
            select * from Custom a, Department b where a.id=@id and a.departID=b.id
            
            */
            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string strsql = "select * from Custom a, Department b where a.id=@id and a.departID=b.id";
            string strsql = "CreateCustomView";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql;
            cmd.Parameters.Add(new SqlParameter("id", id));
            SqlDataReader rd = cmd.ExecuteReader();

            // custom class
            Custom n = null;
            if (rd.Read())
            {
                n = new Custom();
                n.id = Convert.ToInt32(rd["id"]);
                n.cname = rd["cname"].ToString();
                n.department = new Department();
                //n.department.id = Convert.ToInt32(rd["departID"]);
                n.department.departname = rd["departname"].ToString();
                n.age = Convert.ToInt32(rd["age"]);
                n.ename = rd["ename"].ToString();
                n.password = rd["password"].ToString();
            }
            rd.Close();
            con.Close();        

            if (n != null)
            {
                this.TakeJson(n);
            }    
        }
        
        // 根据 id 获取 Department 一条记录
        public void CreateDepartmentView(int id)
        {
            /*
            
            create proc CreateDepartmentView
            @id int
            as
            select * from Department where id=@id
            
            */
            if (id <1) return;

            SqlConnection con= new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd= new SqlCommand();
            cmd.Connection= con;
            
            //string strsql= "select * from Department where id=@id";
            string strsql = "CreateDepartmentView";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText= strsql;
            cmd.Parameters.Add(new SqlParameter("id", id));
            SqlDataReader rd = cmd.ExecuteReader();

            // department class
            Department n = null;
            if (rd.Read())
            {
                n = new Department();
                n.id = Convert.ToInt32(rd["id"]);
                n.departname = rd["departname"].ToString();
                n.description = rd["description"].ToString();
            }
            rd.Close();
            con.Close();

            if (n != null)
            {
                this.TakeJson(n);
            }
        }

        // 插入或更新一条 Custom 记录
        public void CreateCustom(Custom f_custom)
        {
            if (f_custom != null)
            {
                SqlConnection con = new SqlConnection(this.strcon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                int id = f_custom.id;
                // insert check by the departID


                /*
                 
                 同 CreateDepartmentView
            
                */
                //string strsql1 = "select * from Department where id=@id";
                string strsql1 = "CreateDepartmentView";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strsql1;
                cmd.Parameters.Add(new SqlParameter("id", f_custom.departID));
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    rd.Close();
                    if (id < 1)
                    {
                        /*
            
                        create proc InsertCustom
                        @cname nvarchar(50),
                        @departID int,
                        @age int, 
                        @ename nvarchar(50),
                        @password nvarchar(50)
                        as
                        insert into Custom values(@cname, @departID, @age, @ename, @password);select @@IDENTITY as id
            
                        */

                        // insert custom
                        //string strsql2 = "insert into Custom values(@cname, @departID, @age, @ename, @password);select @@IDENTITY as id";
                        string strsql2 = "InsertCustom";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = strsql2;
                        cmd.Parameters.Clear();
                    }
                    else
                    {
                        /*
            
                        create proc UpdateCustom
                        @id int,
                        @cname nvarchar(50),
                        @departID int,
                        @age int, 
                        @ename nvarchar(50),
                        @password nvarchar(50)
                        as
                        update Custom set cname=@cname, departID=@departID, age=@age, ename=@ename, password=@password where id=@id
            
                        */
                        // update department
                        //string strsql3 = "update Custom set cname=@cname, departID=@departID, age=@age, ename=@ename, password=@password where id=@id";
                        string strsql3 = "UpdateCustom";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = strsql3;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("id", id));
                    }
                    cmd.Parameters.Add(new SqlParameter("cname", f_custom.cname));
                    cmd.Parameters.Add(new SqlParameter("departID", f_custom.departID));
                    cmd.Parameters.Add(new SqlParameter("age", f_custom.age));
                    cmd.Parameters.Add(new SqlParameter("ename", f_custom.ename));
                    cmd.Parameters.Add(new SqlParameter("password", f_custom.password));

                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        // insert return the id
                        var re = new { id = Convert.ToInt32(rd["id"]), message = "success" };
                        this.TakeJson(re);
                    }
                    else
                    {
                        // update success
                        var re = new { id = 0, message = "success" };
                        this.TakeJson(re);
                    }
                    rd.Close();
                    con.Close();
                }
                else
                {
                    // the departID is illegal
                    var re = new { id = 0, message = "error" };
                    this.TakeJson(re);
                }
            }
            else
            {
                // the f_custom data is empty
                var re = new { id = 0, message = "error" };
                this.TakeJson(re);
            }
        }

        // 插入或更新一条 Department 记录
        public void CreateDepartment(Department f_department)
        {
            if (f_department != null)
            {
                SqlConnection con = new SqlConnection(this.strcon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                /*
            
                create proc CreateDepartmentCheck
                @departname nvarchar(50)
                as
                select id from Department where departname=@departname
            
                */
                // insert check the unique column departname
                //string strsql1 = "select id from Department where departname=@departname";
                string strsql1 = "CreateDepartmentCheck";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strsql1;
                cmd.Parameters.Add(new SqlParameter("departname", f_department.departname));
                SqlDataReader rd= cmd.ExecuteReader();
                int checkId= 0;
                if (rd.Read())
                {
                    checkId = Convert.ToInt32(rd["id"]);
                }
                rd.Close();
                cmd.Parameters.Clear();

                
                int id = f_department.id;
                if (id < 1)
                {
                    if (checkId == 0)
                    {
                        /*
            
                        create proc InsertDepartment
                        @departname nvarchar(50),
                        @description nvarchar(50)
                        as
                        insert into Department values(@departname, @description);select @@IDENTITY as id
                        
                        */
                        // insert department
                        //string strsql2 = "insert into Department values(@departname, @description);select @@IDENTITY as id";
                        string strsql2 = "InsertDepartment";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = strsql2;
                    }
                    else
                    {
                        // the departname is already exist, so come back insert
                        var re = new { id = 0, message = "error" };
                        this.TakeJson(re);
                        return;
                    }
                }
                else
                {
                    if (checkId == 0 || id == checkId)
                    {
                        /*
            
                        create proc UpdateDepartment
                        @id int,
                        @departname nvarchar(50),
                        @description nvarchar(50)
                        as
                        update Department set departname=@departname, description=@description where id=@id
            
                        */
                        // update department
                        //string strsql3 = "update Department set departname=@departname, description=@description where id=@id";
                        string strsql3 = "UpdateDepartment";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = strsql3;
                        cmd.Parameters.Add(new SqlParameter("id", id));
                    }
                    else
                    {
                        // the departname is already exist, so come back insert
                        var re = new { id = 0, message = "error" };
                        this.TakeJson(re);
                        return;
                    }
                }
                cmd.Parameters.Add(new SqlParameter("departname", f_department.departname));
                cmd.Parameters.Add(new SqlParameter("description", f_department.description));

                rd= cmd.ExecuteReader();
                if(rd.Read()) 
                {
                    // insert return the id
                    var re = new { id = Convert.ToInt32(rd["id"]), message = "success" };
                    this.TakeJson(re);
                } 
                else 
                {
                    // update success
                    var re = new { id = 0, message = "success" };
                    this.TakeJson(re);
                }
                rd.Close();
                con.Close();
            }
            else
            {
                // the f_department data is empty
                var re = new { id = 0, message = "error" };
                this.TakeJson(re);
            }
        }

        // 查询 cname 
        public void CnameQuery(string cname)
        {
            if(cname== null || cname.Equals("")) return;

            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            /*
            
            create proc CnameQuery
            @cname nvarchar(50)
            as
            select * from Custom a, Department b where cname=@cname and a.departID=b.id
            
            */
            //string strsql = "select * from Custom a, Department b where cname=@cname and a.departID=b.id";
            string strsql = "CnameQuery";
            cmd.CommandText = strsql;
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cname", cname));

            List<Custom> list = new List<Custom>();
            Custom n = null;
            SqlDataReader rd= cmd.ExecuteReader();
            while (rd.Read())
            {
                n = new Custom();
                n.department = new Department();

                n.id = Convert.ToInt32(rd["id"]);
                n.cname = rd["cname"].ToString();
                n.age = Convert.ToInt32(rd["age"]);
                n.ename = rd["ename"].ToString();
                n.password = rd["password"].ToString();

                //n.departID = Convert.ToInt32(rd["departID"]);
                n.department.id = Convert.ToInt32(rd["departID"]);
                n.department.departname = rd["departname"].ToString();
                //n.department.description = rd1["description"].ToString();

                list.Add(n);
            }
            this.TakeJson(list);
            rd.Close();
            con.Close();
        }

        // 查询 departname 
        public void DepartnameQuery(string departname)
        {
            if (departname == null || departname.Equals("")) return;

            SqlConnection con = new SqlConnection(this.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            /*
            
            create proc DepartnameQuery
            @departname nvarchar(50)
            as
            select * from Department where departname=@departname
            
            */
            //string strsql = "select * from Department where departname=@departname";
            string strsql = "DepartnameQuery";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strsql;
            cmd.Parameters.Add(new SqlParameter("departname", departname));

            SqlDataReader rd = cmd.ExecuteReader();
            List<Department> list = new List<Department>();
            Department m = null;
            while (rd.Read())
            {
                m = new Department();

                m.id = Convert.ToInt32(rd["id"]);
                m.departname = rd["departname"].ToString();
                m.description = rd["description"].ToString();

                list.Add(m);
            }
            this.TakeJson(list);
            rd.Close();
            con.Close();
        }

        // 还原 Custom 列表
        public void CustomQuery()
        {
            this.TakeJson(this.ListCustom());
        }

        // 还原 Department 列表
        public void DepartmentQuery()
        {
            this.TakeJson(this.ListDepartment());
        }
    
    }
}
