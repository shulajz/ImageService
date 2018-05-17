using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class CloseClientCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            result = true;
            args[0] = "x";
            return args[0];
        }
    }
}
