﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectEacademy.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string User { get; set; }
        public AccountType AccType { get; set; }
        public string FullName { get; set; }
        public string ProfileImg { get; set; }
        public string SchoolName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            try
            {
                userIdentity.AddClaim(new Claim("User", User));
                userIdentity.AddClaim(new Claim("AccType", AccType.ToString()));
                userIdentity.AddClaim(new Claim("FullName", FullName));
                userIdentity.AddClaim(new Claim("SchoolName", SchoolName));
            }
            catch (System.Exception)
            {
                
            }
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserClass> UserClass { get; set; }
        public DbSet<UserInClass> UserInClasses { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}