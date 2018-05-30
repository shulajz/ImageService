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
        public FirstModel()
        {
            ObservableCollection<string> handlers = new ObservableCollection<string>();
            handlers.Add("or");
            handlers.Add("shula");
            m_setting = new Setting()
            {
                OutPutDir = "output dir 111",
                SourceName = "source name",
                LogName = "log name 3333333 ",
                ThumbnailSize = 120,
                ArrHandlers = handlers
            };
            OutPutDir = m_setting.OutPutDir;
            SourceName = m_setting.SourceName;
            LogName = m_setting.LogName;
            ThumbnailSize = m_setting.ThumbnailSize;
            //ArrHandlers = m_setting
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPutDir:")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName:")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize:")]
        public int ThumbnailSize { get; set; }
    }
}