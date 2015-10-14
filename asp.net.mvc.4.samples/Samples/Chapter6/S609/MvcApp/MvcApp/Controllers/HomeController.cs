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
            ModelState.AddModelError("Name", "请输入姓名");
            ModelState.AddModelError("PhoneNo", "请输入电话号码");
            ModelState.AddModelError("EmailAddress", "请输入电子邮箱地址");
            return View(new Contact());
        }
    }

}
