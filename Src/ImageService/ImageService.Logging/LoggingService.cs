﻿
using ImageService.Communication.Modal;
using System;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message we want to print to the log.</param>
        /// <param name="type">The type of message.</param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageReceivedEvent?.Invoke(this, new MessageReceivedEventArgs(message, type));
        }
    }
}
