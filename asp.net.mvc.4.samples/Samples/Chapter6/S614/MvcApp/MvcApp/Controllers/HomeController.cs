using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    [ValidationRule("Rule3")]
    public class HomeController : RuleBasedController
    {
        public ActionResult Index()
        {
            return View("person", new Person());
        }
        [HttpPost]
        public ActionResult Index(Person person)
        {
            return View("person", person);
        }

        [ValidationRule("Rule1")]
        public ActionResult Rule1()
        {
            return View("person", new Person());
        }
        [HttpPost]
        [ValidationRule("Rule1")]
        public ActionResult Rule1(Person person)
        {
            return View("person", person);
        }

        [ValidationRule("Rule2")]
        public ActionResult Rule2()
        {
            return View("person", new Person());
        }
        [HttpPost]
        [ValidationRule("Rule2")]
        public ActionResult Rule2(Person person)
        {
            return View("person", person);
        }
    }
 
}
