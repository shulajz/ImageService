using ImageService.Commands;
using ImageService.Logging.Modal;
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
        private List<MessageReceivedEventArgs> logList;

        public LogCommand()
        {
            logList = new List<MessageReceivedEventArgs>();
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
            }catch(Exception e){
                result = false;
                return e.Message;
            }
            result = true;
            return logs;

        }

        public void onReceiveCommandLog(object sender, MessageReceivedEventArgs e)
        {

            //eventLog1.WriteEntry(e.m_status + ": " + e.m_message);
            logList.Add(new MessageReceivedEventArgs(e.m_message, e.m_status));
        }

    }
}
