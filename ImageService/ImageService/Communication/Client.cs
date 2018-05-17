using System.IO;
using System.Net.Sockets;

namespace ImageService.Communication
{
    public class Client
    {
        private NetworkStream m_stream;
        public NetworkStream Stream
        {
            get { return m_stream; }
            set
            {
                m_stream = value;
            }
        }

        private StreamReader m_reader;
        public StreamReader Reader
        {
            get { return m_reader; }
            set
            {
                m_reader = value;
            }
        }

        private StreamWriter m_writer;
        public StreamWriter Writer
        {
            get { return m_writer; }
            set
            {
                m_writer = value;
            }
        }


        private TcpClient m_tcpClient;
        public TcpClient TcpClient
        {
            get { return m_tcpClient; }
            set
            {
                m_tcpClient = value;
            }
        }
    }
}
