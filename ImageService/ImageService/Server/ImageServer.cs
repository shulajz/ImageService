using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        // The event that notifies about a new Command being recieved
        public event EventHandler<CommandReceivedEventArgs> CommandRecievedEvent;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServer"/> class.
        /// </summary>
        /// <param name="mLogging">The logger.</param>
        /// <param name="arrHandlers">The array handlers.</param>
        /// <param name="mController">The controller.</param>
        public ImageServer(ILoggingService mLogging,
            ObservableCollection<string> arrHandlers, IImageController mController)
        {
            m_logging = mLogging;
            m_controller = mController;

            foreach (string path in arrHandlers)
            {
               createHandler(path);//create a handler
            }

        }


        /// <summary>
        /// Creates the handler.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        public void createHandler(string dirPath)
        {
            IDirectoryHandler handler = new DirectoryHandler(dirPath, m_controller,m_logging);
            CommandRecievedEvent += handler.OnCommandReceived;
            handler.DirectoryCloseEvent += onCloseServer;
        }


        /// <summary>
        /// Sends the command.
        /// </summary>
        public void sendCommand()
        {
            string[] args = { };
            CommandReceivedEventArgs eventArgs =
                new CommandReceivedEventArgs((int)CommandEnum.CloseCommand, args , "*");
            CommandRecievedEvent?.Invoke(this, eventArgs);   
        }

        ////– handler will call this function to tell server it closed
        /// <summary>
        /// On the close server. This function is closed when DirectoryClosed event
        /// is invoked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void onCloseServer(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is DirectoryHandler)
            {
                DirectoryHandler handler = (DirectoryHandler)sender;
                CommandRecievedEvent -= handler.OnCommandReceived;
                handler.DirectoryCloseEvent -= onCloseServer;
                m_logging.Log(e.Message, Logging.Modal.MessageTypeEnum.INFO);
            }

        }
    }
}