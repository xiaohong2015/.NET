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

            dataSource.Add("Name", "张三");
            dataSource.Add("PhoneNo", "123456789");
            dataSource.Add("EmailAddress", "zhangsan@gmail.com");

            dataSource.Add("Address.Province", "江苏");
            dataSource.Add("Address.City", "苏州");
            dataSource.Add("Address.District", "工业园区");
            dataSource.Add("Address.Street", "星湖街328号");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        public ActionResult DemoAction(Contact foo, Contact bar)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("foo.Name", foo.Name);
            parameters.Add("foo.PhoneNo", foo.PhoneNo);
            parameters.Add("foo.EmailAddress", foo.EmailAddress);
            Address address = foo.Address;
            parameters.Add("foo.Address", string.Format("{0}省{1}市{2}{3}", address.Province, address.City, address.District, address.Street));

            parameters.Add("bar.Name", bar.Name);
            parameters.Add("bar.PhoneNo", bar.PhoneNo);
            parameters.Add("bar.EmailAddress", bar.EmailAddress);
            address = bar.Address;
            parameters.Add("bar.Address", string.Format("{0}省{1}市{2}{3}", address.Province, address.City, address.District, address.Street));
            return View("DemoAction", parameters);
        }
    }
}
