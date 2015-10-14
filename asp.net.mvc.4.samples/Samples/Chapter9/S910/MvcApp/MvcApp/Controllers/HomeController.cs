using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpControllerDescriptor controllerDescriptor =
                new HttpControllerDescriptor
                {
                    ControllerType = typeof(FooController)
                };
            MethodInfo methodInfo = typeof(FooController).GetMethod("Bar");
            HttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor(controllerDescriptor, methodInfo) { Configuration = GlobalConfiguration.Configuration };
            IActionValueBinder valueBinder = GlobalConfiguration.Configuration.Services.GetActionValueBinder();
            HttpActionBinding actionBinding =valueBinder.GetBinding(actionDescriptor);
            return View(actionBinding.ParameterBindings);
        }
    }
}
