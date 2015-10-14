using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace MvcApp.Controllers
{
    public class Controller1 : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new AsyncControllerActionInvoker();
        }

        public void FooAsync()
        { }
        public void FooCompleted()
        { }
        public Task<ActionResult> Bar()
        {
            throw new NotImplementedException();
        }
    }

    public class Controller2 : AsyncController
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new ControllerActionInvoker();
        }

        public void FooAsync()
        { }
        public void FooCompleted()
        { }
        public Task<ActionResult> Bar()
        {
            throw new NotImplementedException();
        }
    }

}
