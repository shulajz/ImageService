using ImageService.Communication.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ImageServiceWeb.Models
{
    public class LogsModel
    {
        public List<Log> m_logs { get; }
        public LogsModel()
        {
            m_logs = new List<Log>() ;
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "INFO" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "info" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "ggg" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "error" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "FAIL" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "fail" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.WARNING, Message = "warning" });
        }
       
        public MessageTypeEnum chosenType { get; set; }

    }
}