﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class FirstController : Controller
    {
        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        // GET: First
        public ActionResult Config()
        {
            return View();
        }

        // GET: First
        public ActionResult Photos()
        {
            return View();
        }

        // GET: First
        public ActionResult Logs()
        {
            return View();
        }
    }
}