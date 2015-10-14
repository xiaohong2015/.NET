using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = this.GetModel(controllerContext, bindingContext.ModelType, bindingContext.ValueProvider, bindingContext.ModelName);
            if (bindingContext.FallbackToEmptyPrefix && null == model)
            {
                model = this.GetModel(controllerContext, bindingContext.ModelType, bindingContext.ValueProvider, "");
            }
            return model;
        }

        public object GetModel(ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string key)
        {
            if (!valueProvider.ContainsPrefix(key))
            {
                return null;
            }
            ModelMetadata modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
            if (!modelMetadata.IsComplexType)
            {
                return valueProvider.GetValue(key).ConvertTo(modelType);
            }
            if (modelMetadata.IsComplexType)
            {
                return GetComplexModel(controllerContext, modelType, valueProvider, key);
            }
            return null;
        }

        protected virtual object GetComplexModel(ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string prefix)
        {
            object model = CreateModel(modelType);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(modelType))
            {
                if (property.IsReadOnly)
                {
                    continue;
                }
                string key = string.IsNullOrEmpty(prefix) ? property.Name : prefix + "." + property.Name;
                property.SetValue(model, GetModel(controllerContext, property.PropertyType, valueProvider, key));
            }
            return model;
        }

        private object CreateModel(Type modelType)
        {
            Type type = modelType;
            if (modelType.IsGenericType)
            {
                Type genericTypeDefinition = modelType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    type = typeof(Dictionary<,>).MakeGenericType(
                        modelType.GetGenericArguments());
                }
                else if (((genericTypeDefinition == typeof(IEnumerable<>))
                    || (genericTypeDefinition == typeof(ICollection<>)))
                    || (genericTypeDefinition == typeof(IList<>)))
                {
                    type = typeof(List<>).MakeGenericType(
                    modelType.GetGenericArguments());
                }
            }
            return Activator.CreateInstance(type);
        }
    }
}