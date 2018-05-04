using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class Log: INotifyPropertyChanged
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

                setColorDependType();
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

        private string m_color;
        public string Color
        {
           get { return m_color; }
           set {
                m_color = value;

            }
         
        }

        private void setColorDependType()
        {
            if (Type == "INFO")
            {
                m_color = "YellowGreen";
            }
            else if (Type == "ERROR")
            {
                m_color = "Red";
            }
            else if (Type == "WARNNING")
            {
                m_color = "Yellow";
            }
            else
            {
                m_color = "White";
            }
        }

    }
}
