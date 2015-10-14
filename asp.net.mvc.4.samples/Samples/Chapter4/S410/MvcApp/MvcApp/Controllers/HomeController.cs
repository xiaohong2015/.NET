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
            Triangle triangle = new Triangle
            {
                A = new Point(1, 2),
                B = new Point(2, 3),
                C = new Point(3, 4)
            };
            return View(triangle);
        }
    }

}
