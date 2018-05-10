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
        private StreamReader reader;
        private StreamWriter writer;
        private System.Diagnostics.EventLog m_eventLog1;
        public ClientHandler(IImageController controller, System.Diagnostics.EventLog eventLog1)
       {
            m_controller = controller;
            m_eventLog1 = eventLog1;
       }
       public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                bool result;
            while (true)
            {
                    try
                    {
                        string commandLine = reader.ReadLine();
                        int commandID = JsonConvert.DeserializeObject<int>(commandLine);
                
                        string args = m_controller.ExecuteCommand(commandID, null, out result);
                        if (result == true)
                        {
                            JObject configObj = new JObject();
                            configObj["commandID"] = commandID;
                            configObj["args"] = args;
                            writer.WriteLine(configObj.ToString());
                        }

                    }
                    catch (Exception e)
                    {
                        m_eventLog1.WriteEntry("Error:" + e.Message);
                        break;
                    }
                    writer.Flush();
                }

            }).Start();
        }
    }
}
