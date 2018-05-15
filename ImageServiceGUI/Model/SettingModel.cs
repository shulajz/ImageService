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
using ImageService.Modal;
using System.Threading;

namespace ImageServiceGUI.Model
{
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Mutex mutex;
        private Setting setting; 
        private ClientSingleton client;
        //public IEnumerable<string> HandlersList { get; private set; }
        public ObservableCollection<string> modelSettingsHandlers { get; set; }

        public SettingModel()
        {
            //string outputCommand = JsonConvert.SerializeObject((int)CommandEnum.GetConfigCommand);
            CommandReceivedEventArgs e = 
                new CommandReceivedEventArgs(
                (int)CommandEnum.GetConfigCommand,
                null,
                null);

            client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += settingsOnCommand;
            modelSettingsHandlers = new ObservableCollection<string>();
            //client.write(outputCommand);
            WriteToClient(e);
            client.wait();
        }

        public void WriteToClient(CommandReceivedEventArgs e)
        {
            
            string outputCommand = JsonConvert.SerializeObject(e);
            client.write(outputCommand);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void settingsOnCommand(object sender, ClientArgs e)
        {
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                setting = JsonConvert.DeserializeObject<Setting>(e.Args);
                foreach (string handler in setting.ArrHandlers)
                    modelSettingsHandlers.Add(handler);
            }
            else if (e.CommandID == (int)CommandEnum.RemoveHandler)
            {
                try
                {
                    string path = JsonConvert.DeserializeObject<string>(e.Args);
                    modelSettingsHandlers.Remove(path);
                }catch(Exception ev)
                {
                    Console.WriteLine(ev.Message);
                }
            }
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

        //public string[] ArrHandlers
        //{
        //    get { return setting.ArrHandlers; }
        //    set
        //    {
        //        setting.ArrHandlers = value;

        //    }
        //}
    }
}