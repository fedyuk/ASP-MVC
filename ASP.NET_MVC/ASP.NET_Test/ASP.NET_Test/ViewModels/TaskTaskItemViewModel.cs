using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.ViewModels
{
    public class TaskTaskItemViewModel
    {
        public int TaskId { get; set; }

        public TaskItem Task { get; set; }
    }
}