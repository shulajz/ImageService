using ImageService.Commands;
using ImageService.Communication.Modal;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class LogCommand : ICommand
    {
        private static List<Log> logList;

        public LogCommand()
        {
            logList = new List<Log>();
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, out bool result)
        {
            string logs;
            try
            {
                logs = JsonConvert.SerializeObject(logList);
                result = true;
                return logs;
            }
            catch(Exception e){
                result = false;
                return e.Message;
            }
        }

        public static void onReceiveCommandLog(object sender, MessageReceivedEventArgs e)
        {
            logList.Add(new Log() { Message = e.m_message, Type = e.m_status });
        }

    }
}
