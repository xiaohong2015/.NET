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
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Index(
            [ModelBinder(typeof(DefaultModelBinder))] Contact contact)
        {
            return View(contact);
        }
    }

}
