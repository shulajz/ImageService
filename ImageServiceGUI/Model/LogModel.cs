using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using ImageService.Logging.Modal;
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
                List <MessageReceivedEventArgs> list = JsonConvert.DeserializeObject<List<MessageReceivedEventArgs>>(e.Args);
                foreach (MessageReceivedEventArgs log in list)
                {
                    string temp = log.m_message.ToString();
                    model_log.Add(new Log() { Type = log.m_status.ToString(), Message = log.m_message });
                } 
               

            }
            
        }
    } 
}