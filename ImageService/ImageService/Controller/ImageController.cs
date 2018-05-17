using ImageService.Commands;
using ImageService.Communication.Enums;
using ImageService.Modal;
using System.Collections.Generic;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;   //The Modal Object
        private AppConfig m_appConfig;
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public ImageController(IImageServiceModal modal, AppConfig appConfig)
            
        {
            m_appConfig = appConfig;
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int) CommandEnum.NewFileCommand, new NewFileCommand(m_modal)},
                {(int) CommandEnum.GetConfigCommand, new GetConfigCommand(m_appConfig)},
                { (int) CommandEnum.LogCommand, new LogCommand()},
                { (int) CommandEnum.RemoveHandler, new RemoveHandlerCommand(m_appConfig)},
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
