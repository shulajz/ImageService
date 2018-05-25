using ImageService.Communication.Enums;
using ImageService.Modal;
using ImageServiceGUI.Model;
using Prism.Commands;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
    /// <summary>
    /// Class WindowsViewModel.
    /// </summary>
    class WindowsViewModel 
    {
        /// <summary>
        /// The m windows model
        /// </summary>
        private IWindowsModel m_windowsModel;

        /// <summary>
        /// Gets the window closing.
        /// </summary>
        /// <value>The window closing.</value>
        public ICommand WindowClosing { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsViewModel"/> class.
        /// </summary>
        /// <param name="windowsModel">The windows model.</param>
        public WindowsViewModel(IWindowsModel windowsModel)
        {
            this.WindowClosing = new DelegateCommand<object>(this.OnWindowClosing);
            m_windowsModel = windowsModel;
        }

        /// <summary>
        /// Called when [window closing].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnWindowClosing(object obj)
        {
            CommandReceivedEventArgs e =
             new CommandReceivedEventArgs(
             (int)CommandEnum.CloseClient,
             null,
             null);
            m_windowsModel.WriteToClient(e);
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
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
