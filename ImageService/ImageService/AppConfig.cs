using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageService
{
    public class AppConfig
    {


        public AppConfig()
        {
            m_arrHandlers = new ObservableCollection<string>();
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';'); 
            foreach (string handler in handlers)
            {
                m_arrHandlers.Add(handler);
            }
            m_OutPutDir = ConfigurationManager.AppSettings["OutputDir"];
            m_SourceName = ConfigurationManager.AppSettings["SourceName"];
            m_ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            m_LogName = ConfigurationManager.AppSettings["LogName"];
           
        }
       

        private ObservableCollection<string> m_arrHandlers;
        public ObservableCollection<string> ArrHandlers
        {
            get { return m_arrHandlers; }
            private set
            {
                m_arrHandlers = value;

            }
        }


        private string m_OutPutDir;
        public string OutPutDir
        {
            get { return m_OutPutDir; }
            private set
            {
                m_OutPutDir = value;

            }
        }

        private string m_SourceName;
        public string SourceName
        {
            get { return m_SourceName; }
            private set
            {
                m_SourceName = value;

            }
        }

        private string m_LogName;
        public string LogName
        {
            get { return m_LogName; }
            private set
            {
                m_LogName = value;

            }
        }

        private int m_ThumbnailSize;
        public int ThumbnailSize
        {
            get { return m_ThumbnailSize; }
            private set
            {
                m_ThumbnailSize = value;

            }
        }
        public void removeHandler(string handlerToRemove)
        {
            m_arrHandlers.Remove(handlerToRemove);
        }
    }
}

