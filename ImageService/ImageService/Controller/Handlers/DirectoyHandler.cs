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


namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryCloseEvent;              // The Event That Notifies that the Directory is being closed

        public DirectoyHandler(string dirPath, IImageController controller)
        {
            m_dirWatcher = new FileSystemWatcher(dirPath);
            m_controller = controller;
            m_path = dirPath;


        }

        public void StartHandleDirectory(string dirPath)
        {
            m_dirWatcher.Created += new FileSystemEventHandler(OnCreated);

        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            if (e.RequestDirPath == m_path) {
               string  resultOfCommand = m_controller.ExecuteCommand((int)CommandEnum.NewFileCommand, e.Args, out result);
            }
            //check if command is meant for its directory, 
            //if yes – handle command (for now will just be to close handler)};
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            // get the file's extension 
            Regex rgx = new Regex(@"(\.bmp$|\.png$|\.jpg$|\.gif$)");
            Match m = rgx.Match(e.FullPath);


            if (m.Success)
            {
                //CommandRecievedEventArgs(int id, string[] args, string path
                string[] paths = { e.FullPath };
                CommandRecievedEventArgs e1 = new CommandRecievedEventArgs(
                    (int)CommandEnum.NewFileCommand, paths, m_path);
               
                OnCommandRecieved(this, e1);
                
            }
   
        }

        //close FileSystemWatcher and invoke onClose event
        public void closeHandler()
        {
            //DirectoryClose?.Invoke(this, new DirectoryCloseEventArgs(e));
        }
    }
}
