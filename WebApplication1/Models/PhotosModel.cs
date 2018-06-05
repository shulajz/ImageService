using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    
    public class PhotosModel
    {
        private string m_OutputDir;
        private string thumbnailPath;
        public List<Photo> images { get; set; }
        public PhotosModel(string OutputDir)
        {
            images = new List<Photo>();
            m_OutputDir = OutputDir;
            int id = 0;
            thumbnailPath = OutputDir + "\\" + "Thumbnails";
            foreach (string file in System.IO.Directory.GetFiles(
                thumbnailPath, "*.png", SearchOption.AllDirectories))
            {
               
                Photo photo = new Photo(file);
                photo.ID = id;
                images.Add(photo);
                id++;
            }
        }



    }
}