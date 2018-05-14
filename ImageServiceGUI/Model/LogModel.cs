using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using ImageService.Communication.Modal;
using ImageService.Communication.Enums;
using ImageService.Modal;



//using ImageService.Logging.Modal;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ClientSingleton client;
        public ObservableCollection<Log> model_log { get; set;}


        public LogModel()
        {
            //string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.LogCommand);
            CommandReceivedEventArgs e =
                new CommandReceivedEventArgs(
                (int)CommandEnum.LogCommand,
                null,
                null);
            client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += logOnCommand;
            WriteToClient(e);
            client.wait();
            model_log = new ObservableCollection<Log>();

        }
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            string outputCommand = JsonConvert.SerializeObject(e);
            client.write(outputCommand);
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void logOnCommand(object sender, ClientArgs e)
        {
           if( e.CommandID == (int)CommandEnum.LogCommand)
            {
                List<Log> logsList = JsonConvert.DeserializeObject<List<Log>>(e.Args);
                foreach (Log log in logsList)
                {
                    model_log.Add(log);
                } 
            }
            
        }
    } 
}