using ImageService.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Communication
{
    interface IConverter
    {
        void converterExec(IClientHandler ch);
    }
}
