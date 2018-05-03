using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        event PropertyChangedEventHandler PropertyChanged;

        //event PropertyChangedEventHandler PropertyChanged;
        string Type { get; set; }
        string Message { get; set; }
    }
}