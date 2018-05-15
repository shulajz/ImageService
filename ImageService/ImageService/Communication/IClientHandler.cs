using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client, List<TcpClient> list);
    }
}
