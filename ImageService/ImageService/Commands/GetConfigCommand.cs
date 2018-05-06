using ImageService.Commands;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands
{
    class GetConfigCommand : ICommand

    {
        private IImageServiceModal m_modal;


        /// <summary>
        /// Initializes a new instance of the <see cref="NewFileCommand"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public GetConfigCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }


        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, out bool result)
        {
            {
                // the string will return the new path if result = true,
                //and will return the error message if the result = false
                return m_modal.AddFile(args[0], out result);

            }
        }
    }
}
