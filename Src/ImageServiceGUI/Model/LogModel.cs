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
    /// <summary>
    /// Class LogModel.
    /// </summary>
    /// <seealso cref="ImageServiceGUI.Model.ILogModel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The client
        /// </summary>
        private ClientSingleton client;
        /// <summary>
        /// Gets or sets the model log.
        /// </summary>
        /// <value>The model log.</value>
        public ObservableCollection<Log> model_log { get; set;}


        /// <summary>
        /// Initializes a new instance of the <see cref="LogModel"/> class.
        /// </summary>
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
        /// <summary>
        /// Writes to client.
        /// </summary>
        /// <param name="e">The <see cref="CommandReceivedEventArgs"/> instance containing the event data.</param>
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
        }
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Logs the on command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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