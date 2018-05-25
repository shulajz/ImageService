using ImageService.Communication.Modal;
using Newtonsoft.Json;
using System;

namespace ImageService.Commands
{
    class GetConfigCommand : ICommand
    {
        private AppConfig m_appConfig;
        public GetConfigCommand(AppConfig appConfig)
        {
            m_appConfig = appConfig;
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                Setting setting = new Setting() { OutPutDir = m_appConfig.OutPutDir,
                SourceName = m_appConfig.SourceName, LogName = m_appConfig.LogName,
                ThumbnailSize = m_appConfig.ThumbnailSize, ArrHandlers = m_appConfig.ArrHandlers};
                string settingJson = JsonConvert.SerializeObject(setting);
                result = true;
                return settingJson;
            } catch(Exception e) {
                result = false;
                return e.Message;

            }
        }
    }
}
