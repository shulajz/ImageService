using ImageService.Communication.Enums;
using ImageService.Modal;
using ImageServiceGUI.Model;
//using Microsoft.Practices.Prism.Commands;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
    class WindowsViewModel 
    {
        private IWindowsModel m_windowsModel;
        //public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WindowClosing { get; private set; }

        public WindowsViewModel(IWindowsModel windowsModel)
        {
            //PropertyChanged += propChangedMethod;
            this.WindowClosing = new DelegateCommand<object>(this.OnWindowClosing);
            m_windowsModel = windowsModel;
        }

        //protected void NotifyPropertyChanged(string name)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}

        //public void propChangedMethod(object sender, PropertyChangedEventArgs e)
        //{
        //    var command = this.WindowClosing as DelegateCommand<object>;
        //    command.RaiseCanExecuteChanged();
        //    NotifyPropertyChanged(e.PropertyName);
        //}


        private void OnWindowClosing(object obj)
        {
            CommandReceivedEventArgs e =
             new CommandReceivedEventArgs(
             (int)CommandEnum.CloseClient,
             null,
             null);
            m_windowsModel.WriteToClient(e);
            //put code here
        }

        public string BackgroundColor
        {
            get { return m_windowsModel.BackgroundColor; }
            set
            {
                m_windowsModel.BackgroundColor = value;
            }
        }
    }
}
