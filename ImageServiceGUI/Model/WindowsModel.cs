using ImageService.Modal;
using ImageServiceGUI.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class WindowsModel:IWindowsModel
    {
        private ClientSingleton client;
        public WindowsModel()
        {
            client = ClientSingleton.getInstance;
            if (client.CheckIfServerConnect())
            {
                m_backgroundColor = "White";
            }
            else
            {
                m_backgroundColor = "Gray";
            }
        }
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            //string outputCommand = JsonConvert.SerializeObject(e);
            client.write(e);
        }
        private string m_backgroundColor;
        public string BackgroundColor
        {
            get { return m_backgroundColor; }
            set
            {
                m_backgroundColor = value;
            }
        }
    }
}
