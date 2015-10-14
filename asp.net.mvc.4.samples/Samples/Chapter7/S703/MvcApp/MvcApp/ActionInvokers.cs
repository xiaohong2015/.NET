using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace MvcApp
{
    public class SyncActionInvoker : IActionInvoker
    {
        public bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            throw new NotImplementedException();
        }
    }

    public class AsyncActionInvoker : IAsyncActionInvoker
    {
        public IAsyncResult BeginInvokeAction(ControllerContext controllerContext, string actionName, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public bool EndInvokeAction(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            throw new NotImplementedException();
        }
    }

}