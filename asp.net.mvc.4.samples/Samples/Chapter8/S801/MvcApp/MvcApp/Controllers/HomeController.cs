using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<ActionDescriptor, ActionResult> actionResults = new Dictionary<ActionDescriptor, ActionResult>();
            MethodInfo getControllerDescriptor = this.ActionInvoker.GetType().GetMethod("GetControllerDescriptor",BindingFlags.Instance | BindingFlags.NonPublic);
            ControllerDescriptor controllerDescriptor = (ControllerDescriptor)getControllerDescriptor.Invoke(this.ActionInvoker, new object[] { ControllerContext });
            MethodInfo invokeActionMethod = this.ActionInvoker.GetType().GetMethod("InvokeActionMethod",BindingFlags.Instance | BindingFlags.NonPublic);

            string[] actions = new string[] { "Foo", "Bar", "Baz", "Qux" };
            Array.ForEach(actions, action =>
            {
                ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(ControllerContext, action);
                ActionResult actionResult = (ActionResult)invokeActionMethod.Invoke(this.ActionInvoker, new object[] { ControllerContext, actionDescriptor, new Dictionary<string, object>() });
                actionResults.Add(actionDescriptor, actionResult);
            });
            return View(actionResults);
        }

        public ActionResult Foo()
        {
            return new RedirectResult("http://www.asp.net");
        }

        public void Bar()
        { }

        public ActionResult Baz()
        {
            return null;
        }

        public double Qux()
        {
            return 1.00;
        }
    }

}
