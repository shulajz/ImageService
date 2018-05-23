using System.IO;
using System.Net.Sockets;

namespace ImageService.Communication
{
    /// <summary>
    /// Class Client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The m stream
        /// </summary>
        private NetworkStream m_stream;
        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public NetworkStream Stream
        {
            get { return m_stream; }
            set
            {
                m_stream = value;
            }
        }

        /// <summary>
        /// The m reader
        /// </summary>
        private StreamReader m_reader;
        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>The reader.</value>
        public StreamReader Reader
        {
            get { return m_reader; }
            set
            {
                m_reader = value;
            }
        }

        /// <summary>
        /// The m writer
        /// </summary>
        private StreamWriter m_writer;
        /// <summary>
        /// Gets or sets the writer.
        /// </summary>
        /// <value>The writer.</value>
        public StreamWriter Writer
        {
            get { return m_writer; }
            set
            {
                m_writer = value;
            }
        }


        /// <summary>
        /// The m TCP client
        /// </summary>
        private TcpClient m_tcpClient;
        /// <summary>
        /// Gets or sets the TCP client.
        /// </summary>
        /// <value>The TCP client.</value>
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
