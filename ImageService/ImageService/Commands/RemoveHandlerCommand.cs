using System;

namespace ImageService.Commands
{
    class RemoveHandlerCommand : ICommand
    {
        private AppConfig m_appConfig;
        public RemoveHandlerCommand(AppConfig appConfig)
        {
            m_appConfig = appConfig;
        }
        public string Execute(string[] args, out bool result)
        {
            
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
