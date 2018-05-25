using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class TCPServerChannel :ITCPServerChannel
    {
        private static Mutex writerMutex = new Mutex();
        private int m_port;
        private TcpListener listener;
        private IClientHandler m_ch;
        private List<Client> listOfClients;

        //constructor
        public TCPServerChannel(int port, IClientHandler ch)
        {
            listOfClients = new List<Client>();
            m_port = port;
            m_ch = ch;
        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), m_port);
            listener = new TcpListener(ep);

            listener.Start();


            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        Client client = new Client();
                        TcpClient tcpClient = listener.AcceptTcpClient();
                        client.TcpClient = tcpClient;
                        client.Stream = tcpClient.GetStream();
                        client.Reader = new StreamReader(client.Stream);
                        client.Writer = new StreamWriter(client.Stream);
                        listOfClients.Add(client);
                        m_ch.HandleClient(client, listOfClients);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            task.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }

        /// <summary>
        /// Sends the log.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessageReceivedEventArgs"/> instance containing the event data.</param>
        public void SendLog(object sender, MessageReceivedEventArgs e)
        {
            Task task = new Task(() =>
            {
                List<Log> logList = new List<Log>();
                logList.Add(new Log() { Message = e.m_message, Type = e.m_status });
                foreach (Client clientItem in listOfClients)
                {
                    string logs = JsonConvert.SerializeObject(logList);
                    m_ch.sendCommandToClient(clientItem, (int)CommandEnum.LogCommand, logs);
                    Thread.Sleep(5);
                }
            });
            task.Start();
        }
       
    }
}
