
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    class ClientSingleton
    {
        public event EventHandler<ClientArgs> CommandReceivedEvent;
        private int m_port;
        private string m_ip;
        private TcpClient m_client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private bool listening;
        private static ClientSingleton instance;
        //private TCPClientChannel client;

        private ClientSingleton() {
           
            start();
        }

        public static ClientSingleton getInstance
        {
            get
            {
                if (instance == null)
                    //string j = "127.0.0.1";
                    instance = new ClientSingleton();
                return instance;
            }
        }

        public void write(string command)
        {
            writer.Write(command);
        }

        public void readCommands()
        {
            Task task = new Task(() => {
                while (listening)
                {
                    try
                    {
                        string info = reader.ReadString();
                        JObject infoObj = JObject.Parse(info);
                        CommandReceivedEvent?.Invoke(this, new ClientArgs((int)infoObj["commandID"], (string)infoObj["args"]));
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
    
    public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            listening = true;
            Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            readCommands();
        }

    }
}

