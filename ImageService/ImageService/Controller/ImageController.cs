using ImageService.Commands;
//using ImageService.ImageService.Commands;
using ImageService.Communication.Enums;
//using ImageService.ImageService.Commands;
using ImageService.Modal;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;   //The Modal Object
        private AppConfig m_appConfig;
        private Dictionary<int, ICommand> commands;
        //private ImageServer m_imageServer;
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public ImageController(IImageServiceModal modal,
            AppConfig appConfig,
            System.Diagnostics.EventLog eventlog1)
            
        {
            //m_imageServer = imageServer;
            m_appConfig = appConfig;
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int) CommandEnum.NewFileCommand, new NewFileCommand(m_modal)},
                {(int) CommandEnum.GetConfigCommand, new GetConfigCommand(m_appConfig,eventlog1)},
                { (int) CommandEnum.LogCommand, new LogCommand()},
                { (int) CommandEnum.RemoveHandler, new RemoveHandlerCommand()}
            };
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandID">The command identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="resultSuccessful">if set to <c>true</c> [result successful].</param>
        /// <returns>System.String.</returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccessful)
        {
            ICommand commandObj = commands[commandID];
            string  value = commandObj.Execute(args, out resultSuccessful);
            return value;
           
        }
    }
}
