using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.ViewModels
{
    public class TaskTaskItemCountViewModel
    {
        public int Count { get; set; }

        public Task Task { get; set; }

        public int CategoryId { get; set; }
    }
}