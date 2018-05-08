using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ImageService.Controller.Handlers
{
    public class DirectoryHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion
        // The Event That Notifies that the Directory is being closed
        public event EventHandler<DirectoryCloseEventArgs> DirectoryCloseEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoyHandler"/> class.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="mLogging">The logger.</param>
        public DirectoryHandler(string dirPath, IImageController controller, ILoggingService mLogging)
        {
            m_logging = mLogging;
            m_controller = controller;
            m_path = dirPath;
            m_dirWatcher = new FileSystemWatcher();
            StartHandleDirectory(dirPath);
        }

        /// <summary>
        /// Starts the handle directory.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        public void StartHandleDirectory(string dirPath)
        {

            try
            {
                m_dirWatcher.Path = dirPath;
                m_dirWatcher.Created += new FileSystemEventHandler(OnCreated);
                m_dirWatcher.EnableRaisingEvents = true;
                m_logging.Log("this dir add to be handler:" + dirPath, Logging.Modal.MessageTypeEnum.INFO);
            } catch (Exception e){
                m_logging.Log(e.Message , MessageTypeEnum.FAIL);
            }

        }

        /// <summary>
        /// Handles the <see cref="E:CommandRecieved" /> event.
        /// A command from the server, for now - just "close" command
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void OnCommandReceived(object sender, CommandReceivedEventArgs e)
        {
            bool result;
            //check if command is meant for its directory, 
            //if yes – handle command (for now will just be to close handler)};
            if (e.RequestDirPath.Equals(m_path) || e.RequestDirPath.Equals("*"))
            {
                //check if the command is close
                if (e.CommandID == (int)CommandEnum.CloseCommand)
                {
                    //close
                    closeHandler();
                }

                else
                {   
                    // the string will return the new path if result = true,
                    //and will return the error message if the result = false
                    string resultOfCommand = m_controller.ExecuteCommand(
                        e.CommandID, e.Args, out result);

                    if (result == false)
                    {
                        m_logging.Log(resultOfCommand, MessageTypeEnum.FAIL);
                    }
                    else
                    {
                        m_logging.Log("copy image from " + m_path + " to "
                            + resultOfCommand + " successes", MessageTypeEnum.INFO);
                    }
                }
            }
        }

        //command within the handler, when a file is created
        /// <summary>
        /// Handles the <see cref="E:Created" /> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void OnCreated(object source, FileSystemEventArgs e)
        {
            // get the file's extension 
            Regex rgx = new Regex(@"(\.bmp$|\.png$|\.jpg$|\.gif$)");
            Match m = rgx.Match(e.FullPath.ToLower());
         
            if (m.Success)
            {
                string[] paths = { e.FullPath };
        
                CommandReceivedEventArgs eventArgs = new CommandReceivedEventArgs(
                    (int)CommandEnum.NewFileCommand, paths, m_path);
                OnCommandReceived(this, eventArgs);
               
            }
        }

        //close FileSystemWatcher and invoke onClose event

        /// <summary>
        /// Closes the handler.
        /// </summary>
        public void closeHandler()
        {
            m_dirWatcher.EnableRaisingEvents = false;
            m_dirWatcher.Created -= new FileSystemEventHandler(OnCreated);
            m_dirWatcher.Dispose();
            DirectoryCloseEvent?.Invoke(this, new DirectoryCloseEventArgs(m_path,
                "close handler at path " + m_path));

        }
    }
}
