using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class UserInClass
    {
        [Key,Column(Order = 0)]
        public string classdetail { get; set; }
        public string ClassID { get; set; }
        public string StudentID {get; set;}
    }
}