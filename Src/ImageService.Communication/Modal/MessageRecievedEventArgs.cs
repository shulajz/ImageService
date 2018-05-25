using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Modal
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageTypeEnum m_status { get; set; }
        public string m_message { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public MessageReceivedEventArgs(string message, MessageTypeEnum status)
        {
            m_status = status;
            m_message = message;
        }
    }
}
