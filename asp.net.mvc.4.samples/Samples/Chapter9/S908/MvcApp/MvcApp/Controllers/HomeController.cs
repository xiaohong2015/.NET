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
            return View(this.GetActionDescriptors(typeof(DemoController)));
        }

        private IEnumerable<HttpActionDescriptor> GetActionDescriptors(Type controllerType)
        {
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor();
            controllerDescriptor.ControllerType = controllerType;
            MethodInfo[] methods = controllerType.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Public |BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (method.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(ApiController)))
                {
                    continue;
                }
                yield return new ReflectedHttpActionDescriptor(controllerDescriptor, method);
            }
        }
    }
}
