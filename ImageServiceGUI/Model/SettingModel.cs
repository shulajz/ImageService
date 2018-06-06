using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageServiceGUI.Communication;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
using System.Windows;
using ImageService.Communication;

namespace ImageServiceGUI.Model
{
    /// <summary>
    /// Class SettingModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="ImageServiceGUI.Model.ISettingModel" />
    class SettingModel : INotifyPropertyChanged, ISettingModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The setting
        /// </summary>
        private Setting setting;
        /// <summary>
        /// The client
        /// </summary>
        private ClientSingleton client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingModel"/> class.
        /// </summary>
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

        /// <summary>
        /// Writes to client.
        /// </summary>
        /// <param name="e">The <see cref="CommandReceivedEventArgs"/> instance containing the event data.</param>
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Settingses the on command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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

        /// <summary>
        /// The m selected handler
        /// </summary>
        private string m_selectedHandler;
        /// <summary>
        /// Gets or sets the selected handler.
        /// </summary>
        /// <value>The selected handler.</value>
        public string SelectedHandler
        {
            get { return m_selectedHandler; }
            set
            {
                m_selectedHandler = value;
                OnPropertyChanged("SelectedHandler");
            }
        }

        /// <summary>
        /// Gets or sets the out put dir.
        /// </summary>
        /// <value>The out put dir.</value>
        public string OutPutDir
        {
            get { return setting.OutPutDir; }
            set
            {
                setting.OutPutDir = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>The name of the source.</value>
        public string SourceName
        {
            get { return setting.SourceName; }
            set
            {
                setting.SourceName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName
        {
            get { return setting.LogName; }
            set
            {
                setting.LogName = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the thumbnail.
        /// </summary>
        /// <value>The size of the thumbnail.</value>
        public int ThumbnailSize
        {
            get { return setting.ThumbnailSize; }
            set
            {
                setting.ThumbnailSize = value;
            }
        }
        /// <summary>
        /// Gets or sets the model settings handlers.
        /// </summary>
        /// <value>The model settings handlers.</value>
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