using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.ObjectModel;


namespace ImageService.Server
{
    public class ImageServer : IImageServer
    {
        #region Members
        private IImageController m_controller;
        ObservableCollection<string> m_arrHandlers;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        // The event that notifies about a new Command being recieved
        private event EventHandler<CommandReceivedEventArgs> CommandRecievedEvent;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServer"/> class.
        /// </summary>
        /// <param name="mLogging">The logger.</param>
        /// <param name="arrHandlers">The array handlers.</param>
        /// <param name="mController">The controller.</param>
        public ImageServer(ILoggingService mLogging,
            ObservableCollection<string> arrHandlers)
        {
            m_logging = mLogging;
            //m_controller = mController;
            m_arrHandlers = arrHandlers;
            //foreach (string path in arrHandlers)
            //{
            //    createHandler(path);//create a handler
            //}
        }

        public void initImageServer(IImageController controller)
        {
            m_controller = controller;
            foreach (string path in m_arrHandlers)
            {
                createHandler(path);//create a handler
            }
        }


        /// <summary>
        /// Creates the handler.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        private void createHandler(string dirPath)
        {
            IDirectoryHandler handler = new DirectoryHandler(dirPath, m_controller,m_logging);
            CommandRecievedEvent += handler.OnCommandReceived;
            handler.DirectoryCloseEvent += onCloseServer;
        }


        /// <summary>
        /// Sends the command.
        /// </summary>
        public void sendRemoveCommand(string handlerToClose)
        {
            string[] args = { };
            CommandReceivedEventArgs eventArgs =
                new CommandReceivedEventArgs((int)CommandEnum.RemoveHandler, args , handlerToClose);
            CommandRecievedEvent?.Invoke(this, eventArgs);
        }

        ////– handler will call this function to tell server it closed
        /// <summary>
        /// On the close server. This function is closed when DirectoryClosed event
        /// is invoked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void onCloseServer(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is DirectoryHandler)
            {
                DirectoryHandler handler = (DirectoryHandler)sender;
                CommandRecievedEvent -= handler.OnCommandReceived;
                handler.DirectoryCloseEvent -= onCloseServer;
                m_logging.Log(e.Message,MessageTypeEnum.INFO);
            }

        }
    }
}