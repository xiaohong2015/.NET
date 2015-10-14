using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public abstract class DemoController : Controller
    {
        public IModelBinder ModelBinder { get; private set; }

        public DemoController()
        {
            this.ModelBinder = new DefaultModelBinder();
            this.ValueProvider = this.CreateValueProvider();
        }

        protected abstract IValueProvider CreateValueProvider();

        protected ActionResult InvokeAction(string actionName)
        {
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(this.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(ControllerContext, actionName);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (ParameterDescriptor parameterDescriptor in actionDescriptor.GetParameters())
            {
                string modelName = parameterDescriptor.BindingInfo.Prefix?? parameterDescriptor.ParameterName;

                ModelBindingContext bindingContext = new ModelBindingContext
                {
                    FallbackToEmptyPrefix = parameterDescriptor.BindingInfo.Prefix == null,
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, parameterDescriptor.ParameterType),
                    ModelName = modelName,
                    ModelState = ModelState,
                    ValueProvider = this.ValueProvider
                };
                parameters.Add(parameterDescriptor.ParameterName,this.ModelBinder.BindModel(ControllerContext, bindingContext));
            }
            return (ActionResult)actionDescriptor.Execute(ControllerContext,parameters);
        }
    }

}
