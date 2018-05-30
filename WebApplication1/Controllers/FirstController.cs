using ImageServiceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{
    public class FirstController : Controller
    {
        static FirstModel m = new FirstModel();
      
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