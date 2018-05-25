using ImageService.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public interface IImageServer
    {
        void sendRemoveCommand(string handlerToClose);
        void initImageServer(IImageController controller);
    }
}
