﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI.Model;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;


namespace ImageServiceGUI.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingModel m_settingModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<string> HandlersList { get; private set; }

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand RemoveCommand { get; private set; }

        public SettingsViewModel()
        {
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove,this.CanRemove);
            this.m_settingModel = new SettingModel();
            m_settingModel.OutPutDir = "shula";
            m_settingModel.PropertyChanged += propChangedMethod;

            string[] handlersListTemp = new[] { "hey", "there", "shula", "how", "are", "you", "today", "great", "thanks" };
            this.HandlersList = handlersListTemp;
        }

        public void propChangedMethod(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
            NotifyPropertyChanged(e.PropertyName);
        }

        private void OnRemove(object obj)
        {
            m_settingModel.OutPutDir = "or";
        }
        private bool CanRemove(object obj)
        {
            if (string.IsNullOrEmpty(m_settingModel.SelectedHandler))
            {
                return false;
            }
            return true;
        }

        //public string HandlersList
        //{
        //    get { return m_settingModel.SelectedHandler; }
        //    set
        //    {
        //        m_settingModel.SelectedHandler = value;
        //        NotifyPropertyChanged("SelectedHandler");
        //    }
        //}
        public string SelectedHandler
        {
            get { return m_settingModel.SelectedHandler; }
            set
            {
                m_settingModel.SelectedHandler = value;
                NotifyPropertyChanged("SelectedHandler");
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
                NotifyPropertyChanged("SourceName");
            }
        }

        public string LogName
        {
            get { return m_settingModel.LogName; }
            set
            {
                m_settingModel.LogName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        public string ThumbnailSize
        {
            get { return m_settingModel.ThumbnailSize; }
            set
            {
                m_settingModel.ThumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }

    }
    
}
