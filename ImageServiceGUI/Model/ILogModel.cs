using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<Log> model_log { get; set; }

        //event PropertyChangedEventHandler PropertyChanged;
        //string Type { get; set; }
        //string Message { get; set; }
    }
}