using ImageService.Modal;
using ImageServiceGUI.Communication;

namespace ImageServiceGUI.Model
{
    /// <summary>
    /// Class WindowsModel.
    /// </summary>
    /// <seealso cref="ImageServiceGUI.Model.IWindowsModel" />
    class WindowsModel :IWindowsModel
    {
        /// <summary>
        /// The client
        /// </summary>
        private ClientSingleton client;
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsModel"/> class.
        /// </summary>
        public WindowsModel()
        {
            client = ClientSingleton.getInstance;
            if (client.CheckIfServerConnect())
            {
                m_backgroundColor = "White";
            }
            else
            {
                m_backgroundColor = "Gray";
            }
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
        /// The m background color
        /// </summary>
        private string m_backgroundColor;
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public string BackgroundColor
        {
            get { return m_backgroundColor; }
            set
            {
                m_backgroundColor = value;
            }
        }
    }
}
