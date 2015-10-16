using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using MvcApplication1.Model_Department;

namespace MvcApplication1.Model_Custom
{
    public class Custom
    {
        public int id { get; set; } // auto increatement
        public string cname { get; set; }

        public int departID { get; set; }
        public Department department; 

        public int age { get; set; }
        public string ename { get; set; }
        public string password { get; set; }
    }
}
