using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using Newtonsoft.Json.Linq;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;

namespace ImageServiceGUI.Model
{
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Setting setting; 
        //public IEnumerable<string> HandlersList { get; private set; }
        //public ObservableCollection<string> modelSettingsHandlers { get; set; }

        public SettingModel()
        {
            string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.GetConfigCommand);
            ClientSingleton client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += settingsOnCommand;
            //modelSettingsHandlers = new ObservableCollection<string>();
            client.write(outputCommand);
            client.wait();
            
          
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


        private void settingsOnCommand(object sender, ClientArgs e)
        {
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                setting = JsonConvert.DeserializeObject<Setting>(e.Args);
            
            }
        }
        public string OutPutDir
        {
            get { return setting.OutPutDir; }
            set
            {
                setting.OutPutDir = value;
                OnPropertyChanged("OutPutDir");
            }
        }

        public string SourceName
        {
            get { return setting.SourceName; }
            set
            {
                setting.SourceName = value;
                OnPropertyChanged("SourceName");
            }
        }

        public string LogName
        {
            get { return setting.LogName; }
            set
            {
                setting.LogName = value;
                OnPropertyChanged("LogName");
            }
        }

        public int ThumbnailSize
        {
            get { return setting.ThumbnailSize; }
            set
            {
                setting.ThumbnailSize = value;
                OnPropertyChanged("ThumbnailSize");
            }
        }

        public string[] ArrHandlers
        {
            get { return setting.ArrHandlers; }
            set
            {
                setting.ArrHandlers = value;

            }
        }
    }
}