using ImageService.Communication.Modal;
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
        public ObservableCollection<Log> VM_model_log { get { return m_logModel.model_log; } } 
        public event PropertyChangedEventHandler PropertyChanged;
        public LogViewModel(ILogModel logModel)
        {
            this.m_logModel = logModel; 
        }

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
