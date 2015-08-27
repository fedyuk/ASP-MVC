using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.ViewModels
{
    public class CategoryTaskCountViewModel
    {

        public int Count { get; set; }

        public Category Category { get; set; }
    }
}