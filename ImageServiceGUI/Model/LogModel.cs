using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string m_type;
        public string Type
        {
            get { return m_type; }
            set
            {
                m_type = value;
                OnPropertyChanged("type");
            }
        }

        private string m_message;
        public string Message
        {
            get { return m_message; }
            set
            {
                m_message = value;
                OnPropertyChanged("message");
            }
        }


    }
}