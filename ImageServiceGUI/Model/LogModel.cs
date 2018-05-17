using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using ImageService.Communication.Modal;
using ImageService.Communication.Enums;
using ImageService.Modal;
using System.Windows;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ClientSingleton client;
        public ObservableCollection<Log> model_log { get; set;}


        public LogModel()
        {
            CommandReceivedEventArgs e =new CommandReceivedEventArgs(
                (int)CommandEnum.LogCommand, null, null);
            client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += logOnCommand;
            WriteToClient(e);
            client.wait();
            model_log = new ObservableCollection<Log>();

        }
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
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
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    foreach (Log log in logsList)
                    {

                        model_log.Add(log);
                    }
                }));
            }
        }
    } 
}