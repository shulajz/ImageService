using ImageService.Server;
using System;

namespace ImageService.Commands
{
    class RemoveHandlerCommand : ICommand
    {
        private AppConfig m_appConfig;
        private IImageServer m_imageServer;
        public RemoveHandlerCommand(AppConfig appConfig, IImageServer imageServer)
        {
            m_imageServer = imageServer;
            m_appConfig = appConfig;
        }
        public string Execute(string[] args, out bool result)
        {
            m_imageServer.sendRemoveCommand(args[0]);
            
            try
            {
                //remove handler in this path in the app config
                m_appConfig.removeHandler(args[0]);
                result = true;
                return args[0];
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;
            }
        }
    }
}
