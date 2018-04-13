using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class DirectoryCloseEventArgs : EventArgs
    {
        public string DirectoryPath { get; set; }

        public string Message { get; set; }             // The Message That goes to the logger

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCloseEventArgs"/> class.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        /// <param name="message">The message.</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}
