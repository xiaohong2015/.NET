using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class FooController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new SyncActionInvoker();
        }
    }

    public class BarController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new AsyncActionInvoker();
        }
    }

}
