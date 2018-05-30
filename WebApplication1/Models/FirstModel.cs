using ImageService.Communication.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FirstModel
    {

        FirstModel()
        {
            ObservableCollection<string> handlers = new ObservableCollection<string>();
            handlers.Add("or");
            handlers.Add("shula");
            Setting setting = new Setting()
            {
                OutPutDir = "output dir",
                SourceName = "source name",
                LogName = "log name",
                ThumbnailSize = 120,
                ArrHandlers = null
            };
        
        }
    }