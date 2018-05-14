using ImageService.Communication.Enums;
using ImageService.Controller;
using ImageService.Modal;
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
       private IImageController m_controller;
        private StreamReader reader;
        private StreamWriter writer;
        private static Mutex writerMutex = new Mutex();
        private System.Diagnostics.EventLog m_eventLog1;
        public ClientHandler(IImageController controller, System.Diagnostics.EventLog eventLog1)
       {
            m_controller = controller;
            m_eventLog1 = eventLog1;
       }
       public void HandleClient(TcpClient TcpClient, List<TcpClient> listOfClients)
        {
            new Task(() =>
            {
                NetworkStream stream = TcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                bool result;
            while (true)
            {
                    try
                    {
                        string commandLine = reader.ReadLine();
                        m_eventLog1.WriteEntry("after commandline:" + commandLine);

                        CommandReceivedEventArgs e = JsonConvert.DeserializeObject<CommandReceivedEventArgs>(commandLine);
                        m_eventLog1.WriteEntry("after CommandReceivedEventArgs:" + e.CommandID.ToString());
                        string[] path = new string[2];
                        m_eventLog1.WriteEntry("the dirPath is =" + e.RequestDirPath);
                       

                        if (e.RequestDirPath != null)
                        {
                           
                            path[0] = e.RequestDirPath; //if its not RemoveCommand this will be NULL!
                            m_eventLog1.WriteEntry("after args. the path[0] is=" + path[0]);
                        }

                        string args = m_controller.ExecuteCommand(e.CommandID, path, out result);
                        

                        if (result == true)
                        {
                            if (e.CommandID == (int)CommandEnum.RemoveHandler)
                            {
                                foreach (TcpClient client in listOfClients)
                                {
                                    m_eventLog1.WriteEntry("after foreach. the RequestDirPath is=" + e.RequestDirPath);
                                    JObject Obj = new JObject();
                                    Obj["commandID"] = e.CommandID;
                                    Obj["args"] = e.RequestDirPath;
                                    writerMutex.WaitOne();
                                    writer.WriteLine(Obj.ToString());
                                    writerMutex.ReleaseMutex();
                                }
                            }
                            else
                            {
                                JObject Obj = new JObject();
                                Obj["commandID"] = e.CommandID;
                                Obj["args"] = args;
                                writer.WriteLine(Obj.ToString());
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        m_eventLog1.WriteEntry("Error in clientHandler:" + e.Message);
                        break;
                    }
                    writer.Flush();
                }

            }).Start();
        }
    }
}
