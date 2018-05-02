using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ImageServiceGUI.Model
{
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //private string[] m_handlersList;
        //public string HandlersList
        //{
        //    get { return m_handlersList; }
        //    set
        //    {
        //        m_settingModel.SelectedHandler = value;
        //        NotifyPropertyChanged("SelectedHandler");
        //    }
        //}

        private string m_selectedHandler;
        public string SelectedHandler
        {
            get { return m_selectedHandler; }
            set
            {
                m_selectedHandler = value;
                OnPropertyChanged("SelectedHandler");
            }
        }

        private string m_OutPutDir;
        public string OutPutDir
        {
            get { return m_OutPutDir; }
            set
            {
                m_OutPutDir = value;
                OnPropertyChanged("OutPutDir");
            }
        }

        private string m_SourceName;
        public string SourceName
        {
            get { return m_SourceName; }
            set
            {
                m_SourceName = value;
                OnPropertyChanged("SourceName");
            }
        }

        private string m_LogName;
        public string LogName
        {
            get { return m_LogName; }
            set
            {
                m_LogName = value;
                OnPropertyChanged("LogName");
            }
        }

        private string m_ThumbnailSize;
        public string ThumbnailSize
        {
            get { return m_ThumbnailSize; }
            set
            {
                m_ThumbnailSize = value;
                OnPropertyChanged("ThumbnailSize");
            }
        }

    }
}
