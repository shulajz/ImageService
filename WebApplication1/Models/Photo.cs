using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ThumbPath { get; set; }
        public string OrignalPath { get; set; }
        public Photo(string pathStr)
        {
           
            DateTime date = GetDateTakenFromImage(pathStr);
            Year = date.Year;
            Month = date.Month;
            Name = Path.GetFileName(pathStr);
            OrignalPath = "..\\..\\OutputDir\\" + Year + "\\" + Month + "\\" + Name;
            // int index = pathStr.LastIndexOf('\\');
            ThumbPath = "..\\OutputDir\\Thumbnails\\" + Year + "\\" + Month + "\\" + Name;
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year:")]
        public int Year { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month:")]
        public int Month { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID:")]
        public int ID { get; set; }

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