using ImageService.Communication;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ImageServiceWeb.Models
{
    public class LogsModel
    {
        private ClientWebSingleton client;

        public List<Log> m_logs { get; }
        public LogsModel()
        {
            LogMessages = new List<Log>(); // for filtering the list according to users request
            m_logs = new List<Log>(); // the whole list

            client = ClientWebSingleton.getInstance;
            CommandReceivedEventArgs e = new CommandReceivedEventArgs(
                (int)CommandEnum.LogCommand, null, null);
            client.CommandReceivedEvent += logOnCommand;
            WriteToClient(e);
            client.wait();
        }

        public MessageTypeEnum chosenType { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public List<Log> LogMessages { get; set; }

        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
        }
        private void logOnCommand(object sender, ClientArgs e)
        {
            if (e.CommandID == (int)CommandEnum.LogCommand)
            {

                List<Log> logsList = JsonConvert.DeserializeObject<List<Log>>(e.Args);
                //Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                foreach (Log log in logsList)
                {
                    m_logs.Add(log);
                }
                //  }));
            }
        }
    }
}