using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ProjectEacademy.Extension
{
    public static class IdentityExtensions
    {
        public static string GetUser(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("User");
            }
            return null;
        }
        public static string GetAccountType(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("AccType").ToString();
            }
            return null;
        }
        public static string GetFullName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci == null)
            {
                return String.Format("");
            }
            if (ci != null)
            {
                return ci.FindFirstValue("FullName");
            }
            return "";
        }
        public static string GetSchoolName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("SchoolName");
            }
            return "";
        }
        
    }
}