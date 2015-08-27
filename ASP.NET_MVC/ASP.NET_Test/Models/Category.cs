using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Test.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Atleast 5 and max 255 characters")]
        public String CategoryName { get; set; }

        public int? AccountRefId { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public Category()
        {
            Tasks = new List<Task>();
        }

    }
}