using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ImageServiceWeb.Models
{

    public class PhotosModel
    {
        public int numberOfPhoto { get; set; }
        private string m_OutputDir;
        private string thumbnailPath;
        public List<Photo> images { get; set; }

        public PhotosModel(string OutputDir)
        {
            if (OutputDir != null)
            {
                m_OutputDir = OutputDir;
                thumbnailPath = m_OutputDir + "\\" + "Thumbnails";
                updatePhotoList();
            }
        }

        public void updatePhotoList()
        {
            images = new List<Photo>();
            numberOfPhoto = 0;

            int index = m_OutputDir.LastIndexOf("\\");
            string realNameOfOutputDir = m_OutputDir.Substring(index + 1);
            if (Directory.Exists(thumbnailPath))
            {
                foreach (string file in System.IO.Directory.GetFiles(
                    thumbnailPath, "*", SearchOption.AllDirectories))
                {
                    Regex rgx = new Regex(@"(\.bmp$|\.png$|\.jpg$|\.gif$|\.jpeg$)");
                    Match m = rgx.Match(file.ToLower());

                    if (m.Success)
                    {
                        Photo photo = new Photo(file, realNameOfOutputDir);
                        photo.ID = numberOfPhoto;
                        images.Add(photo);
                        numberOfPhoto++;
                    }
                }
            }
        }





    }
}