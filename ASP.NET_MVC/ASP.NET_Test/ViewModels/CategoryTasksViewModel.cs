using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.ViewModels
{
    public class CategoryTasksViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }

        public int CategoryId { get; set; }
    }
}