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
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new Person{ Name="Artech", BirthDate= new DateTime(1981,8,24)});
        }

        [HttpPost]
        public ActionResult Index(Person person)
        {
            return View(person);
        }

    }
}
