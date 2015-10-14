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

            dataSource.Add("contacts.index", "first");
            dataSource.Add("contacts.index", "second");

            dataSource.Add("contacts[first].Key", "张三");
            dataSource.Add("contacts[first].Value.Name", "张三");
            dataSource.Add("contacts[first].Value.PhoneNo", "123456789");
            dataSource.Add("contacts[first].Value.EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("contacts[first].Value.Address.Province", "江苏");
            dataSource.Add("contacts[first].Value.Address.City", "苏州");
            dataSource.Add("contacts[first].Value.Address.District", "工业园区");
            dataSource.Add("contacts[first].Value.Address.Street", "星湖街328号");

            dataSource.Add("contacts[second].Key", "李四");
            dataSource.Add("contacts[second].Value.Name", "李四");
            dataSource.Add("contacts[second].Value.PhoneNo", "987654321");
            dataSource.Add("contacts[second].Value.EmailAddress", "lisi@gmail.com");
            dataSource.Add("contacts[second].Value.Address.Province", "江苏");
            dataSource.Add("contacts[second].Value.Address.City", "苏州");
            dataSource.Add("contacts[second].Value.Address.District", "工业园区");
            dataSource.Add("contacts[second].Value.Address.Street", "金鸡湖路328号");

            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        public ActionResult DemoAction(IDictionary<string, Contact> contacts)
        {
            var contactArray = contacts.ToArray();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (var item in contacts)
            {
                string address = string.Format("{0}省{1}市{2}{3}",item.Value.Address.Province, item.Value.Address.City,item.Value.Address.District, item.Value.Address.Street);
                parameters.Add(string.Format("contacts[\"{0}\"].Name", item.Key),item.Value.Name);
                parameters.Add(string.Format("contacts[\"{0}\"].PhoneNo", item.Key),item.Value.PhoneNo);
                parameters.Add(string.Format("contacts[\"{0}\"].EmailAddress",item.Key), item.Value.EmailAddress);
                parameters.Add(string.Format("contacts[\"{0}\"].Address", item.Key),address);
            }
            return View("DemoAction", parameters);
        }
    }
}
