using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MvcApplication1.Model_Department
{
    public class Department
    {
        public int id { get; set; } // auto increatement
        public string departname { get; set; }
        public string description { get; set; }
    }
}