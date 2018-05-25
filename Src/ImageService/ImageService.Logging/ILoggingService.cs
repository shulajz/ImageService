using ImageService.Communication.Modal;
using System;

namespace ImageService.Logging
{
    public interface ILoggingService
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}
