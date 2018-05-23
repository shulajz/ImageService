using ImageService.Communication.Enums;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImageService.Communication
{
    class ClientHandler : IClientHandler
    {
        private IImageController m_controller;
        private static Mutex writerMutex = new Mutex();
        private EventLog m_eventLog1;
        private bool listening = true;
        

        public ClientHandler(IImageController controller, 
            EventLog eventLog1)
       {
           
            m_controller = controller;
            m_eventLog1 = eventLog1;
       }
       public void HandleClient(Client client, List<Client> listOfClients)
        {
            new Task(() =>
            {
                listening = true;
                bool result;
                while (listening)
                {
                    try
                    {
                        string commandLine = client.Reader.ReadLine();
                        if (commandLine != null)
                        {

                            CommandReceivedEventArgs e = JsonConvert.DeserializeObject<CommandReceivedEventArgs>(commandLine);
                            if (e.CommandID == (int)CommandEnum.CloseClient)
                            {
                                listOfClients.Remove(client);
                                listening = false;
                            }
                            else
                            {
                                string[] path = new string[2];
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
                                        foreach (Client clientItem in listOfClients)
                                        {
                                            sendCommandToClient(clientItem, e.CommandID, args);
                                        }
                                    }
                                    else
                                    {
                                        sendCommandToClient(client, e.CommandID, args);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        m_eventLog1.WriteEntry("Error in clientHandler:" + e.Message);
                        break;
                    }
                }
            }).Start();
        }

        public void sendCommandToClient(Client client, int command, string args)
        {
            writerMutex.WaitOne();
            JObject Obj = new JObject();
            Obj["commandID"] = command;
            Obj["args"] = args;
            client.Writer.WriteLine(Obj.ToString());
            client.Writer.Flush();
            writerMutex.ReleaseMutex();
        }
    }
}
