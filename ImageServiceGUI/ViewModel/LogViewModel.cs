using ImageService.Communication.Modal;
using ImageServiceGUI.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageServiceGUI.ViewModel
{
    class LogViewModel
    {

        private ILogModel m_logModel;
        public ObservableCollection<Log> VM_model_log { get { return m_logModel.model_log; } }
        public LogViewModel(ILogModel logModel)
        {
            this.m_logModel = logModel;
        }
    }
}