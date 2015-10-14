using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        [ValidateInput(false)]
        public void Action1(string foo, string bar)
        {
            Response.Write(string.Format("{0}: {1}<br/>", "foo", HttpUtility.HtmlEncode(foo)));
            Response.Write(string.Format("{0}: {1}<br/>", "bar", HttpUtility.HtmlEncode(bar)));
        }

        public void Action2(string foo, string bar)
        {
            Response.Write(string.Format("{0}: {1}<br/>", "foo", HttpUtility.HtmlEncode(foo)));
            Response.Write(string.Format("{0}: {1}<br/>", "bar", HttpUtility.HtmlEncode(bar)));
        }
    }

}
