﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEacademy.Controllers
{
    public class UserController : Controller
    {
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
    }
}