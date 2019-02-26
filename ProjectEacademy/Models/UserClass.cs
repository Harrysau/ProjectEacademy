using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class UserClass
    {
        [Key]
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherID { get; set; }
        public string Color { get; set; }
    }
}