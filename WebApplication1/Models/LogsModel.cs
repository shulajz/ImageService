using ImageService.Communication.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ImageServiceWeb.Models
{
    public class LogsModel
    {
        public List<Log> m_logs { get; }
        public LogsModel()
        {
            LogMessages = new List<string>();
            m_logs = new List<Log>() ;
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "msgINFO" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "msg_info" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.INFO, Message = "msg_ggg" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "msg_error" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "msg_FAIL" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.FAIL, Message = "msg_fail" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.WARNING, Message = "msg_warning" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.WARNING, Message = "msg_warning" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.WARNING, Message = "msg_warning" });
            m_logs.Add(new Log() { Type = MessageTypeEnum.WARNING, Message = "msg_warning" });
        }
       
        public MessageTypeEnum chosenType { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public List<string> LogMessages { get; set; }

    }
}