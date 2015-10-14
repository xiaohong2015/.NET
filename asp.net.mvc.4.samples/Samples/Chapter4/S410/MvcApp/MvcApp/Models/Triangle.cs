using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
    public class Triangle
    {
        [DataType("PointInfo")]
        public Point A { get; set; }

        [DataType("PointInfo")]
        public Point B { get; set; }

        [DataType("PointInfo")]
        public Point C { get; set; }
    }

}