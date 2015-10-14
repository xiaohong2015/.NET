using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;

namespace Artech.Mvc.Extensions.IoC
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionCallHandlerAttribute: HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TransactionCallHandler { Order = this.Order };
        }
    }
}