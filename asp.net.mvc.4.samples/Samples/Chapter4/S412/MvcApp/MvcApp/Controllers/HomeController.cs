using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Employee employee = new Employee
            {
                Name = "张三",
                Gender = "M",//男
                Education = "M",//硕士
                Departments = new string[] { "HR", "AD" },//人事部，行政部
                Skills = new string[] { "CSharp", "AdoNet" }//C#，ADO.NET
            };
            return View(employee);
        }
    }

}
