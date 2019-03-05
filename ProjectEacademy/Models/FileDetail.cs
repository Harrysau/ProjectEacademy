using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}