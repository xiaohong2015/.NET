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
        protected override IValueProvider CreateValueProvider()
        {
            NameValueCollection dataSource = new NameValueCollection();
            dataSource.Add("Foo", "ABC");
            dataSource.Add("Bar", "123");
            dataSource.Add("Baz", "456.01");
            dataSource.Add("Qux", "789.01");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        public ActionResult Index()
        {
            return this.InvokeAction("DemoAction");
        }

        public ActionResult DemoAction(
           string foo,
           int bar,
           [Bind(Prefix = "qux")]
           double baz)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("foo", foo);
            parameters.Add("bar", bar);
            parameters.Add("baz", baz);
            return View("DemoAction", parameters);
        }
    }
}
