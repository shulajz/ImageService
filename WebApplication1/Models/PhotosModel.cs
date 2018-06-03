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
            m_OutputDir = OutputDir;
            thumbnailPath = OutputDir + "\\" + "Thumbnails";
            foreach (string file in System.IO.Directory.GetFiles(
                thumbnailPath, "*.thumb", SearchOption.AllDirectories))
            {
                Photo photo = new Photo();

            }
        }



    }
}