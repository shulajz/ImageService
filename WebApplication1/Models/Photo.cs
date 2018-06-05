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
            Date = GetDateTakenFromImage(path);


        }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ID { get; set; }

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