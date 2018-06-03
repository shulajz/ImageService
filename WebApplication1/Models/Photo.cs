using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace ImageServiceWeb.Models
{
    public class Photo
    {
        private string m_path;
        public Photo(string path)
        {
            m_path = path;
            m_Date = GetDateTakenFromImage(path);


        }
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;

            }
        }

        private DateTime m_Date;
        public DateTime Date
        {
            get { return m_Date; }
            set
            {
                m_Date = value;

            }
        }

        private string m_id;
        public string ID
        {
            get { return m_id; }
            set
            {
                m_id = value;

            }
        }

        /// <summary>
        /// Gets the date taken from image.
        /// </summary>
        /// <param name="path">The path of the image.</param>
        /// <returns>DateTime of when the image was created.</returns>
        public DateTime GetDateTakenFromImage(string path)
        {
            Regex r = new Regex(":");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = null;
                try
                {
                    propItem = myImage.GetPropertyItem(36867);
                }
                catch { }
                if (propItem != null)
                {
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
                else
                    return new FileInfo(path).LastWriteTime;
            }
        }
    }
}