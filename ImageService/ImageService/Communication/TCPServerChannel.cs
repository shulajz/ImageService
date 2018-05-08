using System;
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

        public TCPServerChannel(int port, IClientHandler ch)
        {
            this.m_port = port;
            this.ch = ch;
        }
        public void Start()
        {
           IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), m_port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");

            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }


    }
}
