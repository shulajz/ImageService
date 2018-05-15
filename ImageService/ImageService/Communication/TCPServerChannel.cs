
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class TCPServerChannel
    {
        private static Mutex writerMutex = new Mutex();

        private int m_port;
        private TcpListener listener;
        private IClientHandler ch;
        private System.Diagnostics.EventLog m_eventLog1;
        private List<Client> listOfClients;
        public TCPServerChannel(int port, IClientHandler ch, System.Diagnostics.EventLog eventLog1)
        {
            listOfClients = new List<Client>();
            this.m_port = port;
            this.ch = ch;
            this.m_eventLog1 = eventLog1;


        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), m_port);
            listener = new TcpListener(ep);

            listener.Start();

            m_eventLog1.WriteEntry("Waiting for connections...");

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
                        m_eventLog1.WriteEntry("Got new connection");
                        ch.HandleClient(client, listOfClients);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                m_eventLog1.WriteEntry("Server stopped");
            });
            task.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
        public void SendLog(object sender, MessageReceivedEventArgs e)
        {
             foreach (Client clientItem in listOfClients)
            {
                List<Log> logList = new List<Log>();
                logList.Add(new Log() { Message = e.m_message, Type = e.m_status });
                string logs = JsonConvert.SerializeObject(logList);
                //m_eventLog1.WriteEntry("after foreach. the RequestDirPath is=" + e.RequestDirPath);
                writerMutex.WaitOne();
                JObject Obj = new JObject();
                Obj["commandID"] = (int)CommandEnum.LogCommand;
                Obj["args"] = logs;
                clientItem.Writer.WriteLine(Obj.ToString());
                writerMutex.ReleaseMutex();
            }
        }
       
    }
}
