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
            IHttpControllerTypeResolver controllerTypeResolver = GlobalConfiguration.Configuration.Services.GetHttpControllerTypeResolver();
            IAssembliesResolver assembliesResolver = GlobalConfiguration.Configuration.Services.GetAssembliesResolver();

            ViewBag.ControllerTypeResolver = controllerTypeResolver;
            ViewBag.AssembliesResolver = assembliesResolver;

            return View(controllerTypeResolver.GetControllerTypes(assembliesResolver));
        }
    }

}
