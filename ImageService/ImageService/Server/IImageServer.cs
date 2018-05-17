using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    interface IImageServer
    {
        void sendCommand(string handlerToClose);
    }
}
