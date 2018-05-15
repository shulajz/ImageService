﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class TCPServerChannel
    {
        private int m_port;
        private TcpListener listener;
        private IClientHandler ch;
        private System.Diagnostics.EventLog m_eventLog1;
        private List<TcpClient> listOfClients;
        public TCPServerChannel(int port, IClientHandler ch, System.Diagnostics.EventLog eventLog1)
        {
            listOfClients = new List<TcpClient>();
            this.m_port = port;
            this.ch = ch;
            this.m_eventLog1 = eventLog1;


        }
        public void Start()
        {
           IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),m_port);
            listener = new TcpListener(ep);

            listener.Start();
        
            m_eventLog1.WriteEntry("Waiting for connections...");

            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
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


    }
}
