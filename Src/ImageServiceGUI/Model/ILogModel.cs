using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Communication.Modal;

namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<Log> model_log { get; set; }
    }
}