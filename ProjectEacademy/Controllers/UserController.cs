using Microsoft.AspNet.Identity;
using ProjectEacademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEacademy.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserHome()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }

        //[Route("User/EditProfile/{UserID}")]
        public ActionResult EditProfile(string UserID)
        {
            UserEdit models = new UserEdit();
            var userquery = _context.Users.Single(u => u.Id == UserID);
            models.Username = userquery.User;
            models.UID = userquery.Id;
            models.SchoolName = userquery.SchoolName;
            models.FullName = userquery.FullName;
            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile (UserEdit models)
        {
            var userquery = _context.Users.Single(u => u.Id == models.UID);

            try
            {
                userquery.FullName = models.FullName;
                userquery.SchoolName = models.SchoolName;
                userquery.User = models.Username;

                _context.SaveChanges();
            }
            catch(Exception)
            {
                ModelState.AddModelError("EditError", "Cant Edit this model");
                return View(models);
            }
            return RedirectToAction("UserProfile");
        }
    }
}