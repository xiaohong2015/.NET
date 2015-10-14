using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = false)]
    public class HandleErrorActionAttribute : Attribute
    {
        public string HandleErrorAction { get; private set; }
        public HandleErrorActionAttribute(string handleErrorAction = "")
        {
            this.HandleErrorAction = handleErrorAction;
        }
    }

}
