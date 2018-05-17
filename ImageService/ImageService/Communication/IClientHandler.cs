using ImageService.Server;
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
        void HandleClient(Client client, List<Client> list, ImageServer imageServer);
        void sendCommandToClient(Client client, int command, string args);
    }
}
