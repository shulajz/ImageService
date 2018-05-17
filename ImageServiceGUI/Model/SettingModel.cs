using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
using System.Windows;

namespace ImageServiceGUI.Model
{
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Setting setting; 
        private ClientSingleton client;
        
        public SettingModel()
        {
            CommandReceivedEventArgs e = new CommandReceivedEventArgs(
                (int)CommandEnum.GetConfigCommand, null, null);
            setting = new Setting();
            client = ClientSingleton.getInstance;
            client.CommandReceivedEvent += settingsOnCommand;
            modelSettingsHandlers = new ObservableCollection<string>();
            WriteToClient(e);
            client.wait();
        }

        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
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
            }
            else if (e.CommandID == (int)CommandEnum.RemoveHandler)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    modelSettingsHandlers.Remove(e.Args);
                }));
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
            }
        }

        public string SourceName
        {
            get { return setting.SourceName; }
            set
            {
                setting.SourceName = value;
            }
        }

        public string LogName
        {
            get { return setting.LogName; }
            set
            {
                setting.LogName = value;
            }
        }

        public int ThumbnailSize
        {
            get { return setting.ThumbnailSize; }
            set
            {
                setting.ThumbnailSize = value;
            }
        }
        public ObservableCollection<string> modelSettingsHandlers
        {
            get { return setting.ArrHandlers; }
            set
            {
                setting.ArrHandlers = value;
            }
        }
    }
}