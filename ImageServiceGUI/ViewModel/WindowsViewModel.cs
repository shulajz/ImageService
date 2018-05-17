using ImageService.Communication.Enums;
using ImageService.Modal;
using ImageServiceGUI.Model;
using Prism.Commands;
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
            CommandReceivedEventArgs e =
             new CommandReceivedEventArgs(
             (int)CommandEnum.CloseClient,
             null,
             null);
            m_windowsModel.WriteToClient(e);
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
