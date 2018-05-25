using ImageService.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageServiceGUI.Model
{
    interface ISettingModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        string SelectedHandler { get; set; }
        string OutPutDir { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        void WriteToClient(CommandReceivedEventArgs msg);
        ObservableCollection<string> modelSettingsHandlers { get; set; }
        int ThumbnailSize { get; set; }

    }
}
