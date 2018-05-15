using ImageService.Commands;
using ImageService.Communication.Modal;
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
        private System.Diagnostics.EventLog eventlog1_m;
        private AppConfig m_appConfig;
        public GetConfigCommand(AppConfig appConfig, System.Diagnostics.EventLog eventlog1)
        {
            eventlog1_m = eventlog1;
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
            //JObject configObj = new JObject();
            try
            {
                eventlog1_m.WriteEntry("before setting");
                Setting setting = new Setting() {
                    OutPutDir = m_appConfig.OutPutDir,
                    SourceName = m_appConfig.SourceName,
                    LogName = m_appConfig.LogName,
                    ThumbnailSize = m_appConfig.ThumbnailSize,
                    ArrHandlers = m_appConfig.ArrHandlers};
               

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
