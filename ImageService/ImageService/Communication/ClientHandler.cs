using ImageService.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class ClientHandler : IClientHandler
    {
       private IImageController m_controller;
       public ClientHandler(IImageController controller)
       {
            m_controller = controller;
       }
       public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    bool result;
                    string commandLine = reader.ReadLine();
                    int commandID = JsonConvert.DeserializeObject<int>(commandLine);
                    //string  = m_controller.ExecuteCommand(commandID, null , out result);
                    //writer.WriteLine("d");
                    //Console.WriteLine("Got command: {0}", commandLine);
                    string args = m_controller.ExecuteCommand(commandID, null, out result);
                    JObject configObj = new JObject();
                    configObj["commandID"] = commandID;
                    configObj["args"] = args;
                    writer.Write(configObj.ToString());
                }
                client.Close();
            }).Start();
        }
    }
}
