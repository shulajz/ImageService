using ImageService.Modal;

namespace ImageServiceGUI.Model
{
    interface IWindowsModel
    {
        string BackgroundColor { get; set; }
        void WriteToClient(CommandReceivedEventArgs e);
    }
}
