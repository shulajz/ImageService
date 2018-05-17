using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface IWindowsModel
    {
        string BackgroundColor { get; set; }
        void WriteToClient(CommandReceivedEventArgs e);
    }
}
