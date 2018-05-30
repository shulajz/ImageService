using ImageService.Communication.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class FirstModel
    {
        private Setting m_setting;
        FirstModel()
        {
            ObservableCollection<string> handlers = new ObservableCollection<string>();
            handlers.Add("or");
            handlers.Add("shula");
            m_setting = new Setting()
            {
                OutPutDir = "output dir",
                SourceName = "source name",
                LogName = "log name",
                ThumbnailSize = 120,
                ArrHandlers = handlers
            };
            OutPutDir = m_setting.OutPutDir;
        }
      
        [Required]
        [DataType(DataType.Text)]
        //[Display(Name = "OutPutDir")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize")]
        public int ThumbnailSize { get; set; }
    }
}