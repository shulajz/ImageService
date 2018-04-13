using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    public interface IDirectoryHandler
    {
        event EventHandler<DirectoryCloseEventArgs> DirectoryCloseEvent;              // The Event That Notifies that the Directory is being closed

        /// <summary>
        /// Starts the handle directory.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        void StartHandleDirectory(string dirPath);             // The Function Receives the directory to Handle

        /// <summary>
        /// Handles the <see cref="E:CommandReceived" /> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        void OnCommandReceived(object sender, CommandReceivedEventArgs e);     // The Event that will be activated upon new Command

        /// <summary>
        /// Closes the handler.
        /// </summary>
        void closeHandler();
    }
}
