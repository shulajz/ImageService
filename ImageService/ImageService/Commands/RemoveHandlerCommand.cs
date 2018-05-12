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
        //private ImageServer m_imageServer;
        public RemoveHandlerCommand()
        {
            //m_imageServer = imageServer;
        }
        public string Execute(string[] args, out bool result)
        {
            //JObject configObj = new JObject();
            try
            {
                // m_imageServer.sendCommand();
                //string[] args1 = { };
                //CommandReceivedEventArgs eventArgs =
                //    new CommandReceivedEventArgs((int)CommandEnum.CloseCommand, args, "*");
                ////m_logging.Log("sendCommand1", MessageTypeEnum.FAIL);
                //m_imageServer.CommandRecievedEvent?.Invoke(this, eventArgs);
                //Setting setting = new Setting()
                //{
                //    OutPutDir = m_appConfig.OutPutDir,
                //    SourceName = m_appConfig.SourceName,
                //    LogName = m_appConfig.LogName,
                //    ThumbnailSize = m_appConfig.ThumbnailSize,
                //    ArrHandlers = m_appConfig.ArrHandlers
                //};
                //string settingJson = JsonConvert.SerializeObject(setting);
                string pathJson = JsonConvert.SerializeObject(args[0]);
                result = true;
                return pathJson;
               

                //return settingJson;
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;

            }
        }
    }
}
