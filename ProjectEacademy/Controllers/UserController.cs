using ProjectEacademy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using ProjectEacademy.Extension;

namespace ProjectEacademy.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db;

        public UserController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult TeacherViewPost()
        {
            if (User.Identity.GetAccountType().Equals("Student"))
            {
                return RedirectToAction("StudentViewPost");
            }

            var userId = User.Identity.GetUserId();

            var post =
                from p in db.Posts
                join c in db.UserClass on p.ClassId equals c.ClassID
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

            if (User.Identity.GetAccountType().Equals("Teacher"))
            {
                return RedirectToAction("TeacherViewPost");
            }

            var post =
                from p in db.Posts
                join c in db.UserClass on p.ClassId equals c.ClassID
                join t in db.Users on c.TeacherID equals t.Id
                join uic in db.UserInClasses on c.ClassID equals uic.ClassID
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

        // GET: User
        public ActionResult Index()
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

                    if (file != null && file.ContentLength > 0)
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

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}