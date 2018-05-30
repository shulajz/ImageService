using ImageService.Communication.Modal;
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
        static FirstModel m = new FirstModel()
        {
      
                OutPutDir123 = "output dir 111111 ",
                SourceName = "source name 222222 ",
                LogName = "log name 3333333 ",
                ThumbnailSize = 120,
                //ArrHandlers = handlers
            
    };
        
      
        // GET: First
        public ActionResult Index()
        {
            return View();
        }
            
        // GET: First
        public ActionResult Config()
        {
            return View(m);
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