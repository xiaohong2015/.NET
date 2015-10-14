﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artech.Mvc.Extensions;

namespace Artech.VM
{
    [ExceptionPolicy("defaultPolicy")]
    public class VmController : ExtendedController
    {
        public IList<IDisposable> DisposableObjects { get; private set; }
        public VmController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }
        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (null != disposable)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}