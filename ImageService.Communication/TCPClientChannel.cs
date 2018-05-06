using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class TCPClientChannel : TCPChannel
    {
        private int m_port;
        private string m_ip;
        private TcpClient m_client;
        //private handler of client
        public TCPClientChannel(string ip, int port)
        {
            this.m_port = port;
            this.m_ip = ip;
            //handler = null;
        }

        protected void connect(string ip, int port) {

        }

        protected void write(string command)
        {
            
        }
    }
}
