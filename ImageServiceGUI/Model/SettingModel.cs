using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageService.Infrastructure.Enums;
using ImageServiceGUI.Communication;
using Newtonsoft.Json.Linq;

namespace ImageServiceGUI.Model
{
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public ObservableCollection<string> modelSettingsHandlers { get; set; }

        public SettingModel()
        {
            string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.GetConfigCommand);
            ClientSingleton client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += settingsOnCommand;
           // modelSettingsHandlers = new ObservableCollection<string>();

            client.write(outputCommand);
            
          
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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

        private int m_ThumbnailSize;
        public int ThumbnailSize
        {
            get { return m_ThumbnailSize; }
            set
            {
                m_ThumbnailSize = value;
                OnPropertyChanged("ThumbnailSize");
            }
        }
        private string[] m_arrHandlers;
        public string[] ArrHandlers
        {
            get { return m_arrHandlers; }
            set
            {
                m_arrHandlers = value;

            }
        }

        private void settingsOnCommand(object sender, ClientArgs e)
        {
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                JObject info = JObject.Parse(e.Args);
                OutPutDir = (string)info["OutPutDir"];
                SourceName = (string)info["SourceName"];
                LogName = (string)info["LogName"];
                ThumbnailSize = (int)info["ThumbnailSize"];
                ArrHandlers = JsonConvert.DeserializeObject<string[]>((string)info["ArrHandlers"]);
               //modelSettingsHandlers = JsonConvert.DeserializeObject<s>((string)info["ArrHandlers"]);

            }
        }

    }
}