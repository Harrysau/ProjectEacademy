using Microsoft.AspNet.Identity;
using ProjectEacademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectEacademy.Extension;
using System.IO;

namespace ProjectEacademy.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TeacherViewPost()
        {
            if (User.Identity.GetAccountType().Equals("Student"))
            {
                return RedirectToAction("StudentViewPost");
            }

            var userId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUser();

            var teacherClass =
                from c in _context.UserClass
                where c.TeacherID == userId
                select new ListClass()
                {
                    ClassName = c.ClassName,
                    ClassId = c.ClassID
                };

            ViewBag.ListClass = teacherClass;
            ViewBag.ClassCount = teacherClass.Count();

            var post =
                from p in _context.Posts
                join c in _context.UserClass on p.ClassId equals c.ClassID
                orderby p.PostId descending
                where p.TeacherId == userId
                select new TeacherViewPost()
                {
                    ClassName = c.ClassName,
                    Subject = p.Subject,
                    Date = p.Date,
                    DeadLine = p.DeadLine,
                    Description = p.Description,
                    Type = p.Type,
                    FileDetails = p.FileDetails
                };

            return View(post);
        }
        public ActionResult StudentViewPost()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUser();

            if (User.Identity.GetAccountType().Equals("Teacher"))
            {
                return RedirectToAction("TeacherViewPost");
            }

            var studentClass =
                from c in _context.UserClass
                join uic in _context.UserInClasses on c.ClassID equals uic.ClassID
                where uic.StudentID == userid
                select new ListClass()
                {
                    ClassId = c.ClassID,
                    ClassName = c.ClassName
                };

            ViewBag.ListClass = studentClass;
            ViewBag.ClassCount = studentClass.Count();

            var post =
                from p in _context.Posts
                join c in _context.UserClass on p.ClassId equals c.ClassID
                join t in _context.Users on c.TeacherID equals t.Id
                join uic in _context.UserInClasses on c.ClassID equals uic.ClassID
                where uic.StudentID.Equals(userid)
                select new StudentViewPost()
                {
                    TeacherName = t.User,
                    ClassName = c.ClassName,
                    Date = p.Date,
                    DeadLine = p.DeadLine,
                    Description = p.Description,
                    FileDetails = p.FileDetails,
                    Subject = p.Subject,
                    Type = p.Type
                };

            return View(post);
        }

        public ActionResult UserHome()
        {
            if (User.Identity.GetAccountType().Equals("Teacher"))
            {
                return RedirectToAction("TeacherViewPost");
            }
            else
            {
                return RedirectToAction("StudentViewPost");
            }
        }

        [Route("User/CreatePost/{ClassID}")]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/CreatePost/{ClassID}")]
        public ActionResult CreatePost(Post post, string ClassID)
        {
            if (ModelState.IsValid)
            {
                List<FileDetail> fileDetails = new List<FileDetail>();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0 && file.ContentLength < 36700160)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };

                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                post.FileDetails = fileDetails;
                post.Date = DateTime.Now.ToString();
                post.TeacherId = User.Identity.GetUserId();
                post.ClassId = ClassID;

                _context.Posts.Add(post);
                _context.SaveChanges();

                return RedirectToAction("UserHome");
            }

            return View(post);
        }

        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
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