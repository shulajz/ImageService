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



//using ImageService.Logging.Modal;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Log> model_log { get; set;}


        public LogModel()
        {
            string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.LogCommand);
            ClientSingleton client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += logOnCommand;
            client.write(outputCommand);
            client.wait();
            model_log = new ObservableCollection<Log>();

            //model_log.Add(new Log() { Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() { Type = "INFO", Message = "hgvvvvvvvvvvvvvcci" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            //model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            //model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            //model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });

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