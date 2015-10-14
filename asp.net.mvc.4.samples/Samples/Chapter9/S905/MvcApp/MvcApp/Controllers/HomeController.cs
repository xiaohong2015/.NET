using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IHttpControllerSelector controllerSelector = GlobalConfiguration.Configuration.Services.GetHttpControllerSelector();
            ViewBag.ControllerSelector = controllerSelector;
            return View(controllerSelector.GetControllerMapping());
        }
    }
}
