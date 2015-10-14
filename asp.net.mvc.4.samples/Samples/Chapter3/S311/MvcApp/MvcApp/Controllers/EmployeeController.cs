using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeRepository Repository { get; private set; }

        public EmployeeController(IEmployeeRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult GetAllEmployees()
        {
            var employees = this.Repository.GetEmployees();
            return View("EmployeeList", employees);
        }

        public ActionResult GetEmployeeById(string id)
        {
            Employee employee = this.Repository.GetEmployees(id).FirstOrDefault();
            if (null == employee)
            {
                throw new HttpException(404, string.Format("ID为{0}的员工不存在", id));
            }
            return View("Employee", employee);
        }
    }
}
