using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Artech.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.Content(this.GetType().FullName);
        }
    }
}

namespace Artech.MvcApp
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.Content(this.GetType().FullName);
        }
    }
}

