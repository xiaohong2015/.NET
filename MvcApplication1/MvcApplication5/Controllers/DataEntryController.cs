using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using MvcApplication1.Model_Custom;
using MvcApplication1.Model_Department;

using System.Web.Script.Serialization;
using MvcApplication5;
using MvcApplication5.DataSet1TableAdapters;

namespace MvcApplication1.Controllers
{
    public class DataEntryController : Controller
    {
        customTableAdapter cta = new customTableAdapter();
        departmentTableAdapter dta = new departmentTableAdapter();

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
            DataSet1.customDataTable t = cta.GetData();
            if(t== null|| t.Count== 0)
            {
                return null;
            }

            List<Custom> list1 = new List<Custom>();
            Custom n = null;
            foreach(DataSet1.customRow r in t) {
                n = new Custom();
                n.department = new Department();

                n.id = r.id;
                n.cname = r.cname;
                n.age = r.age;
                n.ename = r.ename;
                n.password = r.password;
                n.departID = r.departID;

                n.department.id = r.departID;
                DataSet1.departmentRow r2 = dta.DepartmentFindByIdR(r.departID)[0];
                n.department.departname= r2.departname;
                n.department.departname = dta.DepartmentFindByIdR(r.departID)[0].departname;

                list1.Add(n);
            }
            return list1;
        }

        // 返回 Department 表所有行
        public List<Department> ListDepartment()
        {
            DataSet1.departmentDataTable t = dta.GetData();
            if (t == null || t.Count == 0)
            {
                return null;
            }

            List<Department> list2 = new List<Department>();
            Department m = null;
            foreach(DataSet1.departmentRow r in t)
            {
                m = new Department();

                m.id= r.id;
                m.departname= r.departname;
                m.description= r.description;

                list2.Add(m);
            }
            return list2;
        }

        // 删除一条 Custom 记录
        public void DeleteCustom(int id)
        {
            if (id < 1)  return;
            dta.Delete(id);
        }
        
        // 删除一条 Department 记录
        public void DeleteDepartment(int id)
        {
            if (id < 1) return;
            dta.Delete(id);
        }
        
        // 获取 id, departname 所有记录
        public void DepartnameList()
        {
            DataSet1.departmentDataTable t = dta.GetData();
            // department's id and departname list
            List<Department> list = new List<Department>();
            Department n = null;
            foreach (DataSet1.departmentRow r in t)
            {
                n = new Department();
                n.id = r.id;
                n.departname = r.departname;

                list.Add(n);
            }
            TakeJson(list);                
        }
        
        // 根据 id 获取 Custom 一条记录
        public void CreateCustomView(int id)
        {
            DataSet1.customDataTable t = cta.CustomFindByIdR(id);
            // custom class
            Custom n = null;
            foreach(DataSet1.customRow r in t) 
            {
                n = new Custom();
                n.id = r.id;
                n.cname = r.cname;
                
                n.department = new Department();
                n.department.departname = dta.DepartmentFindByIdR(r.departID)[0].departname;
                
                n.age = r.age;
                n.ename = r.ename;
                n.password = r.password;
            }
            if (n != null)
            {
                TakeJson(n);
            }    
        }
        
        // 根据 id 获取 Department 一条记录
        public void CreateDepartmentView(int id)
        {
            if(id <1) return;

            DataSet1.departmentDataTable t = dta.DepartmentFindByIdR(id);
            // department class
            Department n = null;
            foreach (DataSet1.departmentRow r in t)
            {
                n = new Department();
                n.id = r.id;
                n.departname = r.departname;
                n.description = r.description;
            }
            if (n != null)
            {
                TakeJson(n);
            }
        }

        // 插入或更新一条 Custom 记录
        public void CreateCustom(Custom f_custom)
        {
            string message = "error";
            if (f_custom != null)
            {
                int id = f_custom.id;
                // insert check by the departID

                DataSet1.departmentDataTable t = dta.DepartmentFindByIdR(f_custom.departID);
                if (t!= null && t.Count> 0)
                {
                    message = "success";
                    if (id < 1)
                    {
                        // insert custom
                        id= cta.CustomInsertQuery(f_custom.cname, f_custom.departID, f_custom.age, f_custom.ename, f_custom.password);
                        
                        // insert return the id                        
                        var re = new { id = id, message = message };
                        this.TakeJson(re);
                    }
                    else
                    {
                        cta.CustomUpdateQuery(f_custom.cname, f_custom.departID, f_custom.age, f_custom.ename, f_custom.password, id);

                        // update success
                        var re = new { id = 0, message = message };
                        this.TakeJson(re);
                    }
                }
                else
                {
                    // the departID is illegal
                    var re = new { id = 0, message = message };
                    this.TakeJson(re);
                }
            }
            else
            {
                // the f_custom data is empty
                var re = new { id = 0, message = message };
                this.TakeJson(re);
            }
        }

        // 插入或更新一条 Department 记录
        public void CreateDepartment(Department f_department)
        {
            string message = "error";
            if (f_department != null)
            {
                // insert check the unique column departname
                DataSet1.departmentDataTable t = dta.DepartmentFindByDepartname(f_department.departname);
                int checkId= 0;
                if (t!= null&& t.Count>0)
                {
                    checkId = t[0].id;
                }
                
                int id = f_department.id;
                if (id < 1)
                {
                    if (checkId == 0)
                    {
                        // insert department
                        id= dta.DepartmentInsertQuery(f_department.departname, f_department.description);
                    }
                    else
                    {
                        // the departname is already exist, so come back insert
                        var re = new { id = 0, message = message };
                        TakeJson(re);
                        return;
                    }
                }
                else
                {
                    if (checkId == 0 || id == checkId)
                    {
                        // update department
                        dta.DepartmentUpdateQuery(f_department.departname, f_department.description, f_department.id);
                    }
                    else
                    {
                        // the departname is already exist, so come back insert
                        var re = new { id = 0, message = message };
                        this.TakeJson(re);
                        return;
                    }
                }

                message = "success";
                if(id> 0) 
                {
                    // insert return the id
                    var re = new { id = id, message = message };
                    this.TakeJson(re);
                } 
                else 
                {
                    // update success
                    var re = new { id = 0, message = message };
                    this.TakeJson(re);
                }
            }
            else
            {
                // the f_department data is empty
                var re = new { id = 0, message = message };
                this.TakeJson(re);
            }
        }

        // 查询 cname 
        public void CnameQuery(string cname)
        {
            if(cname== null || cname.Equals("")) return;
            DataSet1.customDataTable t = cta.CustomFindByCname(cname);

            List<Custom> list = new List<Custom>();
            Custom n = null;
            foreach(DataSet1.customRow r in t)
            {
                n = new Custom();
                n.department = new Department();

                n.id = r.id;
                n.cname = r.cname;
                n.age = r.age;
                n.ename = r.ename;
                n.password = r.password;

                n.department.id = r.departID;
                n.department.departname = dta.DepartmentFindByIdR(r.departID)[0].departname;

                list.Add(n);
            }
            this.TakeJson(list);
        }

        // 查询 departname 
        public void DepartnameQuery(string departname)
        {
            if (departname == null || departname.Equals("")) return;
            DataSet1.departmentDataTable t = dta.DepartmentFindByDepartname(departname);

            List<Department> list = new List<Department>();
            Department m = null;
            foreach(DataSet1.departmentRow r in t)
            {
                m = new Department();

                m.id = r.id;
                m.departname = r.departname;
                m.description = r.description;

                list.Add(m);
            }
            this.TakeJson(list);
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
