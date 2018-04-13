using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
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
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int) CommandEnum.NewFileCommand, new NewFileCommand(m_modal) }
        
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
