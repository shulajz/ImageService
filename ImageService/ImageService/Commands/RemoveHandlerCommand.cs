using ImageService.Communication.Enums;
using ImageService.Modal;
using ImageService.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                m_appConfig.removeHandler(args[0]);
                //string pathJson = JsonConvert.SerializeObject(args[0]); //return the handler thats being removed
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
