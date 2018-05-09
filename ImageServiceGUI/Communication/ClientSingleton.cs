
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
        //private int m_port;
        //private string m_ip;
        //private TcpClient m_client;
        private StreamReader reader;
        private StreamWriter writer;
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

            Console.WriteLine("BEFORE WRITE: "+command);
            writer.WriteLine(command);
            writer.Flush();
            Console.WriteLine("AFTER WRITE: " + command);

        }

        public void readCommands()
        {
            Task task = new Task(() => {
                while (listening)
                {
                    try
                    {
                        Console.WriteLine("Wait for read command");
                        string info = reader.ReadLine();//if null the service close
                        while (reader.Peek() > 0)
                        {
                            info += reader.ReadLine();
                        }
                        Console.WriteLine("after read");
                        JObject infoObj = JObject.Parse(info);
                        CommandReceivedEvent?.Invoke(this, new ClientArgs((int)infoObj["commandID"], (string)infoObj["args"]));
                        Console.WriteLine("after json");
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("Error in read: " + e.Message);
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
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            readCommands();
        }

    }
}

