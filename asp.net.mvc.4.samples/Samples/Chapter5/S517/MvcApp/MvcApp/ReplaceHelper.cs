using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcApp
{
    internal static class ReplaceHelper
    {
        private static MethodInfo replaceCollectionMethod = typeof(ReplaceHelper).GetMethod("ReplaceCollectionImpl",BindingFlags.Static | BindingFlags.NonPublic);

        public static void ReplaceCollection(Type elementType, object model, object list)
        {
            replaceCollectionMethod.MakeGenericMethod(new Type[] { elementType }).Invoke(null,new object[] { model, list });
        }
        private static void ReplaceCollectionImpl<T>(ICollection<T> model, IEnumerable list)
        {
            model.Clear();
            if (list != null)
            {
                foreach (object obj2 in list)
                {
                    T item = (obj2 is T) ? ((T)obj2) : default(T);
                    model.Add(item);
                }
            }
        }

        private static MethodInfo replaceDictionaryMethod = typeof(ReplaceHelper).GetMethod("ReplaceDictionaryImpl", BindingFlags.Static| BindingFlags.NonPublic);

        public static void ReplaceDictionary(Type keyType, Type valueType,object dictionary, object newContents)
        {
            replaceDictionaryMethod.MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(null,new object[] { dictionary, newContents });
        }

        private static void ReplaceDictionaryImpl<TKey, TValue>(IDictionary<TKey, TValue> dictionary,IEnumerable<KeyValuePair<object, object>> newContents)
        {
            dictionary.Clear();
            foreach (KeyValuePair<object, object> pair in newContents)
            {
                TKey key = (TKey)pair.Key;
                TValue local2 = (TValue)((pair.Value is TValue)
                    ? pair.Value : default(TValue));
                dictionary[key] = local2;
            }
        }

    }

}