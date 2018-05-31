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
        static ConfigModel config_model = new ConfigModel();
        static LogsModel log_model = new LogsModel();
        
      
        // GET: First
        public ActionResult Index()
        {
            return View();
        }
            
        // GET: First
        public ActionResult Config()
        {
            return View(config_model);
        }

        // GET: First
        public ActionResult Photos()
        {
            return View();
        }

        // GET: First
        public ActionResult Logs()
        {
            return View(log_model);
        }
        // GET: First
        public ActionResult RemoveHandler()
        {
            return View();
        }

        [HttpPost]
        public bool RemoveHandlerMethod(string pathOfHandlerToRemove)
        {
            string temp = config_model.HandlersArr[0];
            //here we need to remove handler from the service
            //if succsess remove from the model handllers list
            config_model.HandlersArr.Remove(pathOfHandlerToRemove);
            return true;
        }

        [HttpPost]
        public List<Log> getLogsForType(string type)
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
                    
                    break;
            }
        
        List<Log> logsTemp = new List<Log>();
            foreach (Log log in log_model.m_logs) {
                if (log.Type == temp || type == null)
                {
                    logsTemp.Add(log);
                }
            }
            return logsTemp;
        }
    }
}