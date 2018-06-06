using System;

namespace ImageService.Communication
{
    public class ClientArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string Args { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRecievedEventArgs"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="path">The path.</param>
        public ClientArgs(int id, string args)
        {
            CommandID = id;
            Args = args;
        }
    }
}