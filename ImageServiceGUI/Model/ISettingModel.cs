using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ISettingModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        //ObservableCollection<KeyValuePair<string, string>> setting { get; set; }

        //event PropertyChangedEventHandler PropertyChanged;
        string SelectedHandler { get; set; }
        string OutPutDir { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string[] ArrHandlers { get; set; }
        //ObservableCollection<string> modelSettingsHandlers { get; set; }

        int ThumbnailSize { get; set; }

    }
}
