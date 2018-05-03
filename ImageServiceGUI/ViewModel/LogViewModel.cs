using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    class LogViewModel: INotifyPropertyChanged
    {
        
        private ILogModel m_logModel;
      
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<string> Data { get; private set; }
        public LogViewModel()
        {
            
            this.m_logModel = new LogModel();
            this.Data = new[] { "info", "jkcd" };
        }

        public string Type
        {
            get { return m_logModel.Type; }
            set
            {
                m_logModel.Type = value;
                NotifyPropertyChanged("type");
            }
        }

        public string Message
        {
            get { return m_logModel.Message; }
            set
            {
                m_logModel.Message = value;
                NotifyPropertyChanged("message");
            }
        }

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
