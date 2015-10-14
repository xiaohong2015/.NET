using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MvcApp.Controllers
{
    public class DemoController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public void GetXxx() { }

        [HttpGet]
        [HttpPost]
        public void PostXxx() { }

        [HttpGet]
        [HttpPost]
        public void PutXxx() { }

        [HttpGet]
        [HttpPost]
        public void DeleteXxx() { }

        [HttpGet]
        [HttpPost]
        public void HeadXxx() { }

        [HttpGet]
        [HttpPost]
        public void OptionsXxx() { }

        [HttpGet]
        [HttpPost]
        public void PatchXxx() { }

        [HttpGet]
        [HttpPost]
        public void Other() { }
    } 
}
