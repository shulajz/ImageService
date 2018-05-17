using ImageServiceGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
    class WindowsViewModel
    {
        private IWindowsModel m_windowsModel;
        public ICommand WindowClosing { get; private set; }

        public WindowsViewModel(IWindowsModel windowsModel)
        {
            this.WindowClosing = new DelegateCommand<object>(this.OnWindowClosing);
            m_windowsModel = windowsModel;
        }

        private void OnWindowClosing(object obj)
        {
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
