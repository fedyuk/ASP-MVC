using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }

        [StringLength(255, MinimumLength = 5, ErrorMessage = "Atleast 5 and max 255 characters")]
        public String TaskItemName { get; set; }

        public bool Status { get; set; }

        public int? TaskRefId { get; set; }

        public virtual Task Task { get; set; }

        public TaskItem()
        {
            Status = false;
        }
    }
}