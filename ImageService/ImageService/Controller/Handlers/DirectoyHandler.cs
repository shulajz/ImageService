﻿using ImageService.Modal;
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
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion
        // The Event That Notifies that the Directory is being closed
        public event EventHandler<DirectoryCloseEventArgs> DirectoryCloseEvent;  
        public DirectoyHandler(string dirPath, IImageController controller, ILoggingService mLogging)
        {
            m_logging = mLogging;
            m_controller = controller;
            m_path = dirPath;
            m_dirWatcher = new FileSystemWatcher();
            StartHandleDirectory(dirPath);
        }

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
        //A command from the server, for now - just "close" command
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
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
                    string resultOfCommand = m_controller.ExecuteCommand(
                        e.CommandID, e.Args, out result);

                    if (result == false)
                    {
                        m_logging.Log(resultOfCommand, MessageTypeEnum.FAIL);
                    }
                    else
                    {
                        m_logging.Log("copy image from " + m_path + " to "
                            + resultOfCommand + " successed", MessageTypeEnum.INFO);
                    }
                }
            }
        }

        //command within the handler, when a file is created
        public void OnCreated(object source, FileSystemEventArgs e)
        {
            // get the file's extension 
            Regex rgx = new Regex(@"(\.bmp$|\.png$|\.jpg$|\.gif$)");
            Match m = rgx.Match(e.FullPath.ToLower());
         
            if (m.Success)
            {
                string[] paths = { e.FullPath };
               
                // the string will return the new path if result = true,
                //and will return the error message if the result = false
                CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs(
                    (int)CommandEnum.NewFileCommand, paths, m_path);
                OnCommandRecieved(this, eventArgs);
               
            }
        }

        //close FileSystemWatcher and invoke onClose event
        public void closeHandler()
        {
            m_dirWatcher.EnableRaisingEvents = false;
            m_dirWatcher.Created -= new FileSystemEventHandler(OnCreated);
            m_dirWatcher.Dispose();
            DirectoryCloseEvent?.Invoke(this, new DirectoryCloseEventArgs(m_path, "close handler at path " + m_path));

        }
    }
}
