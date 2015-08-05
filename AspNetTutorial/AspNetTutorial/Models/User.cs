using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetTutorial.Models
{
    public class User
    {
        public User()
        {
            Notes = new List<Note>();
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please provide Username", AllowEmptyStrings = false)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}