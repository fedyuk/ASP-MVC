using ASP.NET_Test.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Models
{
    public class Account
    {
        public int AccountId { get; set;}

        [StringLength(255, ErrorMessage = "Max 255 characters")]
        [AccountEmail(ErrorMessage ="Invalid email")]
        public String Email { get; set; }

        [StringLength(255, ErrorMessage = "Max 255 characters")]
        [AccountPassword(ErrorMessage = "Invalid password")]
        public String Password { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public Account()
        {
            Categories = new List<Category>();
        }
    }
}