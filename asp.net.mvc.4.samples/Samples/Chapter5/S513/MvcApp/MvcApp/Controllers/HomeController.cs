using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : DemoController
    {
        public ActionResult Index()
        {
            return this.InvokeAction("DemoAction");
        }

        protected override IValueProvider CreateValueProvider()
        {
            NameValueCollection dataSource = new NameValueCollection();

            dataSource.Add("foo", "abc");
            dataSource.Add("foo", "ijk");
            dataSource.Add("foo", "xyz");

            dataSource.Add("bar", "123");
            dataSource.Add("bar", "456");
            dataSource.Add("bar", "789");

            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        public ActionResult DemoAction(string[] foo, int[] bar)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            for (int i = 0; i < foo.Length; i++)
            {
                parameters.Add(string.Format("foo[{0}]", i), foo[i]);
            }
            for (int i = 0; i < bar.Length; i++)
            {
                parameters.Add(string.Format("bar[{0}]", i), bar[i]);
            }
            return View("DemoAction", parameters);
        }
    }
}
