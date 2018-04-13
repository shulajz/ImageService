
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageReceivedEvent?.Invoke(this, new MessageReceivedEventArgs(message, type));

        }



    }
}
