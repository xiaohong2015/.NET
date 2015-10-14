using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp
{
    public class DefaultModelBinder : IModelBinder
    {
        #region Public Methods
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

            if (modelType.IsArray)
            {
                return GetArrayModel(controllerContext, modelType, valueProvider,key);
            }

            Type dictionaryType = ExtractGenericInterface(modelType,
            typeof(IDictionary<,>));
            if (null != dictionaryType)
            {
                return GetDictionaryModel(controllerContext, modelType, valueProvider, key);
            }

            Type enumerableType = ExtractGenericInterface(modelType, typeof(IEnumerable<>));
            if (null != enumerableType)
            {
                return GetCollectionModel(controllerContext, modelType, valueProvider, key);
            }           
           
            if (modelMetadata.IsComplexType)
            {
                return GetComplexModel(controllerContext, modelType, valueProvider, key);
            }
            return null;
        }
        #endregion

        #region Protected Methods

        protected virtual object GetDictionaryModel(ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string prefix)
        {
            List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();

            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);
            Type[] genericArguments = modelType.GetGenericArguments();
            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            foreach (var index in indexes)
            {
                string indexPrefix = prefix + "[" + index + "]";
                if (!valueProvider.ContainsPrefix(indexPrefix) && numericIndex)
                {
                    break;
                }
                string keyPrefix = indexPrefix + ".Key";
                string valulePrefix = indexPrefix + ".Value";
                object key = GetModel(controllerContext, keyType,
                                           valueProvider, keyPrefix);
                object value = GetModel(controllerContext, valueType,
                                           valueProvider, valulePrefix);
                list.Add(new KeyValuePair<object, object>(key, value));
            }
            object model = CreateModel(modelType);
            ReplaceHelper.ReplaceDictionary(keyType, valueType, model, list);
            return model;
        }

        protected virtual object GetCollectionModel(ControllerContext controllerContext, Type modelType,IValueProvider valueProvider, string prefix)
        {
            Type elementType = modelType.GetGenericArguments()[0];
            List<object> list = GetListModel(controllerContext, modelType,
                elementType, valueProvider, prefix);
            object model = CreateModel(modelType);
            ReplaceHelper.ReplaceCollection(elementType, model, list);
            return model;
        }

        protected virtual object GetArrayModel( ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string prefix)
        {
            List<object> list = GetListModel(controllerContext, modelType, modelType.GetElementType(), valueProvider, prefix);
            object[] array = (object[])Array.CreateInstance(modelType.GetElementType(), list.Count);
            list.CopyTo(array);
            return array;
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
        #endregion

        #region Private Methods

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

        private IEnumerable<string> GetIndexes(string prefix, IValueProvider valueProvider, out bool numericIndex)
        {
            string key = string.IsNullOrEmpty(prefix) ? "index" : prefix + "." + "index";
            ValueProviderResult result = valueProvider.GetValue(key);
            if (null != result)
            {
                string[] indexes = result.ConvertTo(typeof(string[])) as string[];
                if (null != indexes)
                {
                    numericIndex = false;
                    return indexes;
                }
            }
            numericIndex = true;
            return GetZeroBasedIndexes();
        }

        private static IEnumerable<string> GetZeroBasedIndexes()
        {
            int iteratorVariable = 0;
            while (true)
            {
                yield return iteratorVariable.ToString();
                iteratorVariable++;
            }
        }

        private List<object> GetListModel(ControllerContext controllerContext, Type modelType, Type elementType, IValueProvider valueProvider, string prefix)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(prefix) && valueProvider.ContainsPrefix(prefix))
            {
                ValueProviderResult result = valueProvider.GetValue(prefix);
                if (null != result)
                {
                    IEnumerable enumerable = result.ConvertTo(modelType) as IEnumerable;
                    foreach (var value in enumerable)
                    {
                        list.Add(value);
                    }
                }
            }
            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);
            foreach (var index in indexes)
            {
                string indexPrefix = prefix + "[" + index + "]";
                if (!valueProvider.ContainsPrefix(indexPrefix) && numericIndex)
                {
                    break;
                }
                list.Add(GetModel(controllerContext, elementType, valueProvider, indexPrefix));
            }
            return list;
        }

        private Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> predicate = t => t.IsGenericType && (t.GetGenericTypeDefinition() == interfaceType);
            if (!predicate(queryType))
            {
                return queryType.GetInterfaces().FirstOrDefault<Type>(predicate);
            }
            return queryType;
        }
        #endregion
    }
}