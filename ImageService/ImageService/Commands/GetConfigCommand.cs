using ImageService.Commands;
using ImageService.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            JObject configObj = new JObject();
            try
            {
                configObj["OutPutDir"] = m_appConfig.OutPutDir;
                configObj["SourceName"] = m_appConfig.SourceName;
                configObj["LogName"] = m_appConfig.LogName;
                configObj["ThumbnailSize"] = m_appConfig.ThumbnailSize;
                configObj["ArrHandlers"] = JsonConvert.SerializeObject(m_appConfig.ArrHandlers);
                result = true;
            } catch(Exception e) {
                result = false;
                return e.Message;

            }
            return configObj.ToString();



        }
    }
}
