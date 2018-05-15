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
using System.Threading;



//using ImageService.Logging.Modal;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Log> model_log { get; set;}
        private ClientSingleton client;


        public LogModel()
        {
            client = ClientSingleton.getInstance;
            model_log = new ObservableCollection<Log>();
            client.CommandReceivedEvent += logOnCommand;
            sendLogCommand();

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

        private void sendLogCommand()
        {
          
            string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.LogCommand);
            client.write(outputCommand);
            client.wait();

        }
    } 
}