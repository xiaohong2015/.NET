using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    [ExceptionPolicy("defaultPolicy")]
    public class HomeController : ExtendedController
    {
        public ActionResult Index()
        {
            return View(new LoginInfo());
        }

        [HttpPost]
        [HandleErrorAction("OnIndexError")]
        public ActionResult Index(LoginInfo loginInfo)
        {
            if (string.Compare(loginInfo.UserName, "foo", true) != 0)
            {              
                throw new InvalidUserNameException();
            }

            if (loginInfo.Password != "password")
            {
                throw new InvalidPasswordException();
            }
            return View(loginInfo);
        }

        [HttpPost]
        public ActionResult OnIndexError(LoginInfo loginInfo)
        {
            return View(loginInfo);
        }
    }

}
