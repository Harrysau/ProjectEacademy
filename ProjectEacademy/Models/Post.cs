using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please Enter Subject!!")]
        [Display(Name = "Subject")]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        [MaxLength(500)]
        public string Description { get; set; }

        public string Date { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}