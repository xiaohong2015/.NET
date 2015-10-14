﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ModelMetadataInfo metadataInfo = new ModelMetadataInfo(typeof(DemoModel), metadata => metadata.DataTypeName);
            return View(metadataInfo);
        }
    }
}
