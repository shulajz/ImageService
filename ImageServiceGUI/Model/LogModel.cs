using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Log> model_log { get; set;}
        public LogModel()
        {
            model_log = new ObservableCollection<Log>();
            model_log.Add(new Log() { Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() { Type = "INFO", Message = "hgvvvvvvvvvvvvvcci" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });
            model_log.Add(new Log() {Type = "INFO", Message = "hi" });
            model_log.Add(new Log() { Type = "ERROR", Message = "hi" });
            model_log.Add(new Log() { Type = "WARNNING", Message = "hi" });

        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}