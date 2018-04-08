using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
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
        public event EventHandler<CommandRecievedEventArgs> CommandRecievedEvent;  // The event that notifies about a new Command being recieved
        #endregion
        //
        public ImageServer(ILoggingService mLogging, string[] pathsForHandlers, string outputDir, int thumbnails)
        {
            m_logging = mLogging;
            ImageServiceModal imageServiceModal = new ImageServiceModal(outputDir, thumbnails, m_logging);
            m_controller = new ImageController(imageServiceModal);
          
            foreach (string path in pathsForHandlers)
            {
                m_logging.Log("this dir add to be handler:"+path, Logging.Modal.MessageTypeEnum.INFO);
                createHandler(path);//create
                //m_logging.Log("1a:", Logging.Modal.MessageTypeEnum.INFO);

            }

        }
        public void createHandler(string dirPath)
        {
            IDirectoryHandler handler = new DirectoyHandler(dirPath, m_controller);
            m_logging.Log("createHandler" , Logging.Modal.MessageTypeEnum.INFO);
            CommandRecievedEvent += handler.OnCommandRecieved;
            handler.DirectoryCloseEvent += onCloseServer;
        }

        public void sendCommand()
        {
            
            // CommandRecievedEvent(*, CommandRecievedEvent); //– closes handlers
            //CommandRecievedEvent?.Invoke(this, new CommandRecievedEventArgs(commandID, args, path));

        }
        public void onCloseServer(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is DirectoyHandler)
            {
                DirectoyHandler handler = (DirectoyHandler)sender;
                CommandRecievedEvent -= handler.OnCommandRecieved;
                ////– handler will call this function to tell server it closed
                //CommandRecievedEvent -= handler.closeHandler;
            }

        }



    }
}