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
    /// <summary>
    /// Class SettingsViewModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class SettingsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The m setting model
        /// </summary>
        private ISettingModel m_settingModel;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the handlers list.
        /// </summary>
        /// <value>The handlers list.</value>
        public IEnumerable<string> HandlersList { get; private set; }
        /// <summary>
        /// Gets the vm model setting.
        /// </summary>
        /// <value>The vm model setting.</value>
        public ObservableCollection<string> VM_model_setting { get { return m_settingModel.modelSettingsHandlers; } }



        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Gets the remove command.
        /// </summary>
        /// <value>The remove command.</value>
        public ICommand RemoveCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settingModel">The setting model.</param>
        public SettingsViewModel(ISettingModel settingModel)
        {
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove,this.CanRemove);
            this.m_settingModel = settingModel;
            m_settingModel.PropertyChanged += propChangedMethod;
            this.HandlersList = VM_model_setting;
        }

        /// <summary>
        /// Properties the changed method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public void propChangedMethod(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
            NotifyPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// Called when [remove].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnRemove(object obj)
        {
         
            CommandReceivedEventArgs e = new CommandReceivedEventArgs((int)CommandEnum.RemoveHandler, null,
                m_settingModel.SelectedHandler);
            m_settingModel.WriteToClient(e);
 
        }

        /// <summary>
        /// Determines whether this instance can remove the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if this instance can remove the specified object; otherwise, <c>false</c>.</returns>
        private bool CanRemove(object obj)
        {
            if (string.IsNullOrEmpty(m_settingModel.SelectedHandler))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets or sets the selected handler.
        /// </summary>
        /// <value>The selected handler.</value>
        public string SelectedHandler
        {
            get { return m_settingModel.SelectedHandler; }
            set
            {
                m_settingModel.SelectedHandler = value;
            }
        }

        /// <summary>
        /// Gets or sets the out put dir.
        /// </summary>
        /// <value>The out put dir.</value>
        public string OutPutDir
        {
            get { return m_settingModel.OutPutDir; }
            set
            {
                m_settingModel.OutPutDir = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>The name of the source.</value>
        public string SourceName
        {
            get { return m_settingModel.SourceName; }
            set
            {
                m_settingModel.SourceName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName
        {
            get { return m_settingModel.LogName; }
            set
            {
                m_settingModel.LogName = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the thumbnail.
        /// </summary>
        /// <value>The size of the thumbnail.</value>
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
    
