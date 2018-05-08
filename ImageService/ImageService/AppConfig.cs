using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    public class AppConfig
    { 

       
        public AppConfig()
        {
            m_arrHandlers = ConfigurationManager.AppSettings["Handler"].Split(';');
            m_OutPutDir = ConfigurationManager.AppSettings["OutputDir"];
            m_SourceName = ConfigurationManager.AppSettings["SourceName"];
            m_ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            m_LogName = ConfigurationManager.AppSettings["LogName"];

        }
       

        private string[] m_arrHandlers;
        public string[] ArrHandlers
        {
            get { return m_arrHandlers; }
            set
            {
                m_arrHandlers = value;

            }
        }


        private string m_OutPutDir;
        public string OutPutDir
        {
            get { return m_OutPutDir; }
            set
            {
                m_OutPutDir = value;

            }
        }

        private string m_SourceName;
        public string SourceName
        {
            get { return m_SourceName; }
            set
            {
                m_SourceName = value;

            }
        }

        private string m_LogName;
        public string LogName
        {
            get { return m_LogName; }
            set
            {
                m_LogName = value;

            }
        }

        private int m_ThumbnailSize;
        public int ThumbnailSize
        {
            get { return m_ThumbnailSize; }
            set
            {
                m_ThumbnailSize = value;

            }
        }

    }
}

