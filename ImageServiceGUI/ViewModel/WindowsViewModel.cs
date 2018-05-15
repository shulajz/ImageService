using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    class WindowsViewModel
    {
        private IWindowsModel m_windowsModel;
        public WindowsViewModel(IWindowsModel windowsModel)
        {
            m_windowsModel = windowsModel;
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
