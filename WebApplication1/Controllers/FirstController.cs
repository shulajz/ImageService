using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
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
       // ClientWebSingleton client = ClientWebSingleton.getInstance();
        static public List<Student> students = new List<Student>(){};
        static Photo photo;
        static ConfigModel config_Model = new ConfigModel();
        static LogsModel log_Model = new LogsModel();
        static PhotosModel photos_Model = new PhotosModel(config_Model.OutPutDir);
        static ImageWebModel image_Web_Model = new ImageWebModel(photos_Model.numberOfPhoto);
        


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
        public Photo GetPhotoByID(int id)
        {
            foreach(Photo photo in photos_Model.images)
            {
                if(photo.ID.Equals(id))
                {
                    return photo;
                }
            }
            return null;
        }
        public ActionResult ViewPhoto(int id)
        {
            photo = GetPhotoByID(id);
            return View(photo);
        }

        public ActionResult DeletePhoto(int id)
        {
            photo = GetPhotoByID(id);
            return View(photo);
        }

        [HttpPost]
        public void RemoveHandlerMethod(string pathOfHandlerToRemove)
        {
            //string temp = config_Model.HandlersArr[0];
            //here we need to remove handler from the service
            //if succsess remove from the model handllers list
            //config_Model.HandlersArr.Remove(pathOfHandlerToRemove);
            CommandReceivedEventArgs e = new CommandReceivedEventArgs((int)CommandEnum.RemoveHandler, null,
                pathOfHandlerToRemove);
            config_Model.WriteToClient(e);
           
        }

        [HttpPost]
        public void DeletePhotoMethod(int id)
        {
            photo = GetPhotoByID(id);
            string path = photo.OrignalPath;
           // System.IO.File.Delete("OutputDir\\2018\\6\\colors7x10(1).png");
            photos_Model.images.Remove(photo);
            image_Web_Model.NumOfPhotos--;


        }

        [HttpPost]
        public void getLogsForType(string type)
        {
            MessageTypeEnum temp = MessageTypeEnum.FAIL;
            switch (type.ToLower())
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
                    log_Model.LogMessages.Add(log);
                }
            }
            
            
        }
    }
}