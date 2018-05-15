using ImageService.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class ClientHandler : IClientHandler
    {
        private static IImageController m_controller;
        private static Mutex writerMutex = new Mutex();
        private StreamReader reader;
        private static StreamWriter writer;
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
               
            while (true)
            {
                    try
                    {
                        
                        string commandLine = reader.ReadLine();
                        int commandID = JsonConvert.DeserializeObject<int>(commandLine);
                        sendCommand(commandID);
                        
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

        public static void sendCommand(int commandID)
        {
            bool result;
            string args = m_controller.ExecuteCommand(commandID, null, out result);
            if (result == true)
            {
                JObject configObj = new JObject();
                configObj["commandID"] = commandID;
                configObj["args"] = args;
                writerMutex.WaitOne();
                writer.WriteLine(configObj.ToString());
                writerMutex.ReleaseMutex();
            }
        }

    }
}
