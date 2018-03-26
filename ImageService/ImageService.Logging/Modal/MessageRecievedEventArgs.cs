using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum m_status { get; set; }
        public string m_message { get; set; }

        public MessageRecievedEventArgs(string message, MessageTypeEnum status)
        {
            m_status = status;
            m_message = message;
        }
    }
}
