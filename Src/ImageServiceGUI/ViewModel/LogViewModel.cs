using ImageService.Communication.Modal;
using ImageServiceGUI.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageServiceGUI.ViewModel
{
    /// <summary>
    /// Class LogViewModel.
    /// </summary>
    class LogViewModel
    {

        /// <summary>
        /// The m log model
        /// </summary>
        private ILogModel m_logModel;
        /// <summary>
        /// Gets the vm model log.
        /// </summary>
        /// <value>The vm model log.</value>
        public ObservableCollection<Log> VM_model_log { get { return m_logModel.model_log; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewModel"/> class.
        /// </summary>
        /// <param name="logModel">The log model.</param>
        public LogViewModel(ILogModel logModel)
        {
            this.m_logModel = logModel;
        }
    }
}