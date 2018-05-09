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
                    m_eventLog1.WriteEntry("wait for read ");
                    try
                    {
                        string commandLine = reader.ReadLine();
                        while (reader.Peek() > 0)
                        {
                            commandLine += reader.ReadLine();
                        }
                        m_eventLog1.WriteEntry("after read ");
                        int commandID = JsonConvert.DeserializeObject<int>(commandLine);
                        m_eventLog1.WriteEntry("command id in the client handler: " + commandID);
                        string args = m_controller.ExecuteCommand(commandID, null, out result);
                        m_eventLog1.WriteEntry("1:" + args);
                        JObject configObj = new JObject();

                        configObj["commandID"] = commandID;
                        configObj["args"] = args;
                        writer.Write(configObj.ToString());
                        writer.Flush();
                        m_eventLog1.WriteEntry("in cluent handler:" + configObj.ToString());

                    }
                    catch (Exception e)
                    {
                        m_eventLog1.WriteEntry("error hande client is = " + e.Message);
                    }
                }
                   
                   

                    //string  = m_controller.ExecuteCommand(commandID, null , out result);
                    //writer.WriteLine("d");
                    //Console.WriteLine("Got command: {0}", commandLine);
               
                
                client.Close();
            }).Start();
        }
    }
}
