using ImageService.Server;
using System.Collections.Generic;

namespace ImageService.Communication
{
    interface IClientHandler
    {
        void HandleClient(Client client, List<Client> list);
        void sendCommandToClient(Client client, int command, string args);
    }
}
