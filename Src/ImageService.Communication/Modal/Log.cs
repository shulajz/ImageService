using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Modal
{
    public class Log
    {

        private MessageTypeEnum m_type;
        public MessageTypeEnum Type
        {
            get { return m_type; }
            set
            {
                m_type = value;

                setColorDependType();
            }
        }

        private string m_message;
        public string Message
        {
            get { return m_message; }
            set
            {
                m_message = value;
            }
        }

        private string m_color;
        public string Color
        {
           get { return m_color; }
           private set {
                m_color = value;

            }
         
        }

        private void setColorDependType()
        {
            if (Type == MessageTypeEnum.INFO)
            {
                m_color = "YellowGreen";
            }
            else if (Type == MessageTypeEnum .FAIL)
            {
                m_color = "Red";
            }
            else if (Type == MessageTypeEnum.WARNING)
            {
                m_color = "Yellow";
            }
            else
            {
                m_color = "White";
            }
        }

    }
}
