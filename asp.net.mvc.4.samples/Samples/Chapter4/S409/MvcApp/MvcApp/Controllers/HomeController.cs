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
            Employee employee = new Employee { Name = "张三", Department = "IT", IsPartTime = true };
            return View(employee);
        }
    }
}
