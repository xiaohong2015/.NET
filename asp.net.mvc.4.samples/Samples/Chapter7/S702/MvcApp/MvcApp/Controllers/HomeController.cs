using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public Task<ActionResult> Article(string name)
        {
            return Task.Factory.StartNew(() =>
            {
                string path = ControllerContext.HttpContext.Server.MapPath(string.Format(@"\articles\{0}.html", name));
                using (StreamReader reader = new StreamReader(path))
                {
                    AsyncManager.Parameters["content"] = reader.ReadToEnd();
                }
            }).ContinueWith<ActionResult>(task =>
            {
                string content = (string)AsyncManager.Parameters["content"];
                return Content(content);
            });
        }
    }
}
