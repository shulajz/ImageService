using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageService.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ImageService.Communication;

namespace ImageServiceWeb
{
    public class ClientWebSingleton
    {
        public event EventHandler<ClientArgs> CommandReceivedEvent;
        /// <summary>
        /// The writer mutex
        /// </summary>
        private static Mutex writerMutex = new Mutex();
        /// <summary>
        /// The stream
        /// </summary>
        NetworkStream stream;
        /// <summary>
        /// The reader
        /// </summary>
        private StreamReader reader;
        /// <summary>
        /// The writer
        /// </summary>
        private StreamWriter writer;
        /// <summary>
        /// The listening
        /// </summary>
        private bool listening;
        /// <summary>
        /// The instance
        /// </summary>
        private static ClientWebSingleton instance;
        /// <summary>
        /// The need to wait
        /// </summary>
        private bool needToWait;
        /// <summary>
        /// The server connect
        /// </summary>
        private bool serverConnect;

        /// <summary>
        /// Prevents a default instance of the <see cref="ClientSingleton"/> class from being created.
        /// </summary>
        private ClientWebSingleton()
        {

            start();
        }

        /// <summary>
        /// Gets the get instance.
        /// </summary>
        /// <value>The get instance.</value>
        public static ClientWebSingleton getInstance
        {
            get
            {
                if (instance == null)
                    instance = new ClientWebSingleton();
                return instance;
            }
        }

        /// <summary>
        /// Writes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="CommandReceivedEventArgs"/> instance containing the event data.</param>
        public void write(CommandReceivedEventArgs e)
        {
            string command = JsonConvert.SerializeObject(e);
            if (serverConnect)
            {
                writer.WriteLine(command);
                writer.Flush();
            }
        }



        /// <summary>
        /// Reads the commands.
        /// </summary>
        public void readCommands()
        {
            needToWait = true;
            Task task = new Task(() =>
            {
                while (listening)
                {
                    try
                    {
                        writerMutex.WaitOne();
                        string info = reader.ReadLine();
                        while (reader.Peek() > 0)
                        {
                            info += reader.ReadLine();
                        }
                        JObject infoObj = JObject.Parse(info);

                        CommandReceivedEvent?.Invoke(this, new ClientArgs((int)infoObj["commandID"],
                            (string)infoObj["args"]));
                        writerMutex.ReleaseMutex();
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

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(ep);
                serverConnect = true;
                listening = true;
                Console.WriteLine("You are connected");
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                readCommands();
            }
            catch (Exception)///if exception the service close
            {
                serverConnect = false;
            }
        }


        /// <summary>
        /// Waits this instance.
        /// </summary>
        public void wait()
        {
            while (needToWait) { }
        }

        /// <summary>
        /// Checks if server connect.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckIfServerConnect()
        {
            return serverConnect;
        }

    }
}