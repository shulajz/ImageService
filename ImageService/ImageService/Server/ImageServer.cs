using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
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
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion
        public void createHandler(string dirPath)
        { 
          IDirectoryHandler handler = new DirectoyHandler(dirPath, m_controller);

            //CommandRecieved += handler.OnCommandRecieved();
            //handler.onClose += onCloseServer
        }
        public void  sendCommand() {
            
            //CommandRecieved(“*”, CloseHandler) //– closes handlers


        } 
        public void  onCloseServer(object sender) {
            
            //handler = sender;
            //CommandRecieved -= handler.onCommandReceived();
            ////– handler will call this function to tell server it closed
            //CommandRecieved -= handler.onCloseServer();

        } 



    }
    }
