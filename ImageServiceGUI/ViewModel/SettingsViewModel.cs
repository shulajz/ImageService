using System.Collections.Generic;
using ImageServiceGUI.Model;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Modal;
using ImageService.Communication.Enums;
using Prism.Commands;

namespace ImageServiceGUI.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingModel m_settingModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<string> HandlersList { get; private set; }
        public ObservableCollection<string> VM_model_setting { get { return m_settingModel.modelSettingsHandlers; } }



        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand RemoveCommand { get; private set; }

        public SettingsViewModel(ISettingModel settingModel)
        {
            

            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove,this.CanRemove);
            this.m_settingModel = settingModel;
            m_settingModel.PropertyChanged += propChangedMethod;
            this.HandlersList = VM_model_setting;
        }

        public void propChangedMethod(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
            NotifyPropertyChanged(e.PropertyName);
        }

        private void OnRemove(object obj)
        {
         
            CommandReceivedEventArgs e = new CommandReceivedEventArgs((int)CommandEnum.RemoveHandler, null,
                m_settingModel.SelectedHandler);
            m_settingModel.WriteToClient(e);
 
        }

        private bool CanRemove(object obj)
        {
            if (string.IsNullOrEmpty(m_settingModel.SelectedHandler))
            {
                return false;
            }
            return true;
        }

        public string SelectedHandler
        {
            get { return m_settingModel.SelectedHandler; }
            set
            {
                m_settingModel.SelectedHandler = value;
            }
        }

        public string OutPutDir
        {
            get { return m_settingModel.OutPutDir; }
            set
            {
                m_settingModel.OutPutDir = value;
            }
        }

        public string SourceName
        {
            get { return m_settingModel.SourceName; }
            set
            {
                m_settingModel.SourceName = value;
            }
        }

        public string LogName
        {
            get { return m_settingModel.LogName; }
            set
            {
                m_settingModel.LogName = value;
            }
        }

        public int ThumbnailSize
        {
            get { return m_settingModel.ThumbnailSize; }
            set
            {
                m_settingModel.ThumbnailSize = value;
            }
        }
    }
}
    
