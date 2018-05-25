using ImageService.Communication;
using ImageService.Communication.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Communication
{
    class ConverterAppConfig : IConverter
    {
        private Setting m_setting;
        public ConverterAppConfig(Setting settings)
        {
            m_setting = settings;
        }

        public void converterExec(IClientHandler ch)
        {

        }
    }
}
