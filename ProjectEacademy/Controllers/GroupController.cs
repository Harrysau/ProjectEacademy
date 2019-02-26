using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectEacademy.Models;
using Microsoft.AspNet.Identity;

namespace ProjectEacademy.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        private ApplicationDbContext _context;

        public GroupController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentGroupView()
        {
            string userid = User.Identity.GetUserId();
            var group =
                from g in _context.UserClass
                join u in _context.UserInClasses on g.ClassID equals u.ClassID
                join t in _context.Users on g.TeacherID equals t.Id
                where u.StudentID == userid
                select new StudentGroupViewModels() { ClassName = g.ClassName, TeacherName = t.User, SubjectName = g.SubjectName };
            return View(group);
        }

        public ActionResult TeacherGroupView()
        {
            string username = User.Identity.GetUserId();
            var group =
                from g in _context.UserClass
                where g.TeacherID == username
                select new TeacherGroupViewModels() { ClassID = g.ClassID, ClassName = g.ClassName, SubjectName = g.SubjectName};
            return View(group);
        }

        [Route("Group/TeacherGroupDetailView/{ClassID}")]
        public ActionResult TeacherGroupDetailView(string ClassID)
        {

            TeacherGroupDetailViews m = new TeacherGroupDetailViews();
            string username = User.Identity.GetUserId();
            var groupname =
                from grp in _context.UserClass
                where grp.ClassID == ClassID
                select new ClassName
                {
                    Classname = grp.ClassName.ToString()
                };
            var groupdetail =
                (from stu in _context.Users
                 join u in _context.UserInClasses on stu.Id equals u.StudentID
                 join g in _context.UserClass on u.ClassID equals g.ClassID
                 where g.ClassID == u.ClassID && g.TeacherID == username
                 select new StudentListModels
                 {
                     StudentName = stu.User
                 }).ToList();
            m.lstStudent = groupdetail;
            m.Classname = groupname.First();
            m.ClassID = ClassID;
            return View(m);
        }

        public ActionResult CreateGroup()
        {
            string GroupID = RandomCode.RandomString(6);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(CreateGroupModels models)
        {
            models.TeacherID = User.Identity.GetUserId();
            models.ClassID = RandomCode.RandomString(6);
            try
            {
                _context.UserClass.Add(new UserClass() { ClassID = models.ClassID, ClassName = models.ClassName, SubjectName = models.SubjectName, TeacherID = models.TeacherID, Color = models.Color});
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", errorMessage: "Cannot Create Class");
                return View(models);
            }
            return RedirectToAction("TeacherGroupDetailView", new { ClassID = models.ClassID });
        }

        public ActionResult JoinGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinGroup(JoinGroupModels models)
        {
            models.StudentID = User.Identity.GetUserId();

            var query =
                from dup in _context.UserInClasses
                where (dup.ClassID == models.ClassID && dup.StudentID == models.StudentID)
                select new
                {
                    dupname = dup.StudentID
                };
            foreach (var i in query)
            {
                if (i.dupname == models.StudentID)
                {
                    ModelState.AddModelError("JoinDupGroupError", errorMessage: "You Already Join This Group");
                    return View(models);
                }
            }

            try
            {
                _context.UserInClasses.Add(new UserInClass() { ClassID = models.ClassID, StudentID = models.StudentID });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("JoinGroupError", errorMessage: "Cannot Join Group At This Time");
                return View(models);
            }
            
            return RedirectToAction("StudentGroupView");
        }
    }
}