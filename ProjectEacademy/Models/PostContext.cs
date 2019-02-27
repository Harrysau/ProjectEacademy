using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class PostContext : DbContext
    {
        public PostContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
    }
}