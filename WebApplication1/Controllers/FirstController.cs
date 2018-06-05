using ImageService.Communication.Modal;
using ImageServiceWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{

    public class FirstController : Controller
    {
        static public List<Student> students = new List<Student>()
        {

        };
        static ConfigModel config_Model = new ConfigModel();
        static LogsModel log_Model = new LogsModel();
        static ImageWebModel image_Web_Model = new ImageWebModel();
        static PhotosModel photos_Model = new PhotosModel(config_Model.OutPutDir);


        // GET: First
        public ActionResult ImageWebModel()
        {
            return View(image_Web_Model);
        }
            
        // GET: First
        public ActionResult Config()
        {
            return View(config_Model);
        }

        // GET: First
        public ActionResult Photos()
        {
            return View(photos_Model);
        }

        // GET: First
        public ActionResult Logs()
        {
            return View(log_Model);
        }
        // GET: First
        public ActionResult RemoveHandler()
        {
            return View();
        }

        public ActionResult ViewPhoto()
        {
            return View();
        }

        [HttpPost]
        public bool RemoveHandlerMethod(string pathOfHandlerToRemove)
        {
            string temp = config_Model.HandlersArr[0];
            //here we need to remove handler from the service
            //if succsess remove from the model handllers list
            config_Model.HandlersArr.Remove(pathOfHandlerToRemove);
            return true;
        }

        [HttpPost]
        public void getLogsForType(string type)
        {
            MessageTypeEnum temp = MessageTypeEnum.FAIL;
            switch (type)
            {
                case "fail":
                    temp = MessageTypeEnum.FAIL;
                    break;
                case "info":
                    temp = MessageTypeEnum.INFO;
                    
                    break;
                case "warning":
                    temp = MessageTypeEnum.WARNING;
                    break;
                default:
                    type = null;
                    break;
            }

            log_Model.LogMessages.Clear();
            foreach (Log log in log_Model.m_logs) {
                if (log.Type == temp || type == null)
                {
                    log_Model.LogMessages.Add(log.Message);
                }
            }
            
            
        }
    }
}