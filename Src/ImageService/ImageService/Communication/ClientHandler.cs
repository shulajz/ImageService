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
using ImageService.Logging;
using ImageService.Communication.Modal;

namespace ImageService.Communication
{
    /// <summary>
    /// Class ClientHandler.
    /// </summary>
    /// <seealso cref="ImageService.Communication.IClientHandler" />
    class ClientHandler : IClientHandler
    {
        /// <summary>
        /// The m controller
        /// </summary>
        private IImageController m_controller;
        /// <summary>
        /// The writer mutex
        /// </summary>
        private static Mutex writerMutex = new Mutex();
        /// <summary>
        /// </summary>
        /// <summary>
        /// The listening
        /// </summary>
        private bool listening = true;
        private ILoggingService m_logging;



        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="eventLog1">The event log1.</param>
        public ClientHandler(IImageController controller,
            ILoggingService logging)
       {
            m_logging = logging;

            m_controller = controller;
       }
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="listOfClients">The list of clients.</param>
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
                                m_logging.Log("A client has closed", MessageTypeEnum.INFO);
                                listening = false;
                            }
                            else
                            {
                                string[] path = new string[2];
                                if (e.RequestDirPath != null)
                                {
                                    path[0] = e.RequestDirPath; //if its not RemoveCommand this will be NULL!
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
                        m_logging.Log("Error in ClientHandler" + e.Message, MessageTypeEnum.FAIL);
                        break;
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Sends the command to client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="command">The command.</param>
        /// <param name="args">The arguments.</param>
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
