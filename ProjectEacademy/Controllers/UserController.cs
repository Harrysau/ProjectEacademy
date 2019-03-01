using ProjectEacademy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

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
        // GET: User
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var post =
                from p in db.Posts
                where p.TeacherId == userId
                select new ViewPost()
                {
                    Subject = p.Subject,
                    Date = p.Date,
                    DeadLine = p.DeadLine,
                    Description = p.Description,
                    Type = p.Type,
                    FileDetails = p.FileDetails
                };
            return View(post);
        }

        public ActionResult UserHome()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserHome(Post post)
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
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}