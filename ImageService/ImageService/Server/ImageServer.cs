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
        // The event that notifies about a new Command being recieved
        public event EventHandler<CommandRecievedEventArgs> CommandRecievedEvent;  
        #endregion
        public ImageServer(ILoggingService mLogging, string[] arrHandlers, IImageController mController)
        {
            m_logging = mLogging;
            m_controller = mController;

            foreach (string path in arrHandlers)
            {
               createHandler(path);//create
            }

        }
        
         
        public void createHandler(string dirPath)
        {
            IDirectoryHandler handler = new DirectoyHandler(dirPath, m_controller,m_logging);
            CommandRecievedEvent += handler.OnCommandRecieved;
            handler.DirectoryCloseEvent += onCloseServer;
        }

        
        public void sendCommand()
        {
            string[] args = { };
            CommandRecievedEventArgs eventArgs= new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args , "*");
            CommandRecievedEvent?.Invoke(this, eventArgs);   
        }

        ////– handler will call this function to tell server it closed
        public void onCloseServer(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is DirectoyHandler)
            {
                DirectoyHandler handler = (DirectoyHandler)sender;
                CommandRecievedEvent -= handler.OnCommandRecieved;
                handler.DirectoryCloseEvent -= onCloseServer;
                m_logging.Log(e.Message, Logging.Modal.MessageTypeEnum.INFO);
            }

        }
    }
}