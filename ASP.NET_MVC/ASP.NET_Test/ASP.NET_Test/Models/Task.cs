using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [StringLength(255, MinimumLength = 5, ErrorMessage = "Atleast 5 and max 255 characters")]
        public String TaskName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }

        public Task()
        {
            Categories = new List<Category>();
            TaskItems = new List<TaskItem>();
        }
    }
}