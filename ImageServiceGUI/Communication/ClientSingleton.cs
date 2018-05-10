
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
        NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private bool listening;
        private static ClientSingleton instance;
        private bool needToWait;
        //private TCPClientChannel client;

        private ClientSingleton()
        {

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

            Console.WriteLine("BEFORE WRITE: " + command);
            writer.WriteLine(command);
            writer.Flush();
            Console.WriteLine("AFTER WRITE: " + command);

        }

        public void readCommands()
        {
            needToWait = true;
            Task task = new Task(() =>
            {
                while (listening)
                {
                    try
                    {
                       

                        string info = reader.ReadLine();
                        while (reader.Peek() > 0)
                        {
                            info += reader.ReadLine();
                        }
                        
                        JObject infoObj = JObject.Parse(info);
                        CommandReceivedEvent?.Invoke(this, new ClientArgs((int)infoObj["commandID"], (string)infoObj["args"]));
                        needToWait = false;
                      
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in read: " + e.Message);
                        break;
                    }

                    
                }
            });
            task.Start();
        }

        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);///if exception the service close
            listening = true;
            Console.WriteLine("You are connected");
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            readCommands();
        }


        public void wait()
        {
            while (needToWait) { }
        }
    }
}

