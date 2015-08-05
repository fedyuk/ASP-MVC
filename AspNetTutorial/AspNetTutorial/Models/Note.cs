using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetTutorial.Models
{
    public class Note
    {
        public Note() { }

        [Key]
        public int NoteId { get; set; }

        public String Content { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserRefId { get; set; }

        [ForeignKey("UserRefId")]
        public virtual User user { get; set; }
    }
}