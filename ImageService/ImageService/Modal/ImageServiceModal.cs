﻿using ImageService.Infrastructure;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private ILoggingService m_logging;

        public ImageServiceModal(string outPutFolder, int thumbnailSize)
        {
            m_OutputFolder = outPutFolder;
            m_thumbnailSize = thumbnailSize;
        }


        public string AddFile(string path, out bool result)
        {
            try
            {
                //create the directory if its not created already
                System.IO.Directory.CreateDirectory(m_OutputFolder);
                m_logging.Log("Directory was created successfully", MessageTypeEnum.INFO);
                DateTime creation = File.GetCreationTime(path);
                int year = creation.Year;
                // check if this year exist, if not - creats it
               
                    System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + year);
                m_logging.Log("year folder was created successfully", MessageTypeEnum.INFO);
                int month = creation.Month;
                // check if this month exist, if not - creats it
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + year + "\\" + month);
                m_logging.Log("month folder was created successfully", MessageTypeEnum.INFO);

                File.Copy(path, m_OutputFolder + "\\" + year + "\\" + month);
                m_logging.Log("copying the picture successfully", MessageTypeEnum.INFO);

                //thumbnails
                //create the folder if its not already created
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails");
                m_logging.Log("Thumbnails was created successfully", MessageTypeEnum.INFO);
                // check if this year exist, if not creats it
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + 
                    "Thumbnails" + "\\" + year);
                m_logging.Log("Thumbnails year was created successfully", MessageTypeEnum.INFO);

                // check if this month exist, if not - creats it
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + year + "\\" + 
                    "Thumbnails" + "\\" + month);
                m_logging.Log("Thumbnails month was created successfully", MessageTypeEnum.INFO);


                //File.Copy(path, m_OutputFolder + "\\" + "Thumbnails" +  "\\" + year + "\\" + month);
                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                System.Drawing.Image thumb = image.GetThumbnailImage(
                    m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                thumb.Save(Path.ChangeExtension(m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month, ""));
                m_logging.Log("Thumbnails picture was created", MessageTypeEnum.INFO);

                string newPath = m_OutputFolder + "\\" + year + "\\" + month;
            result = true;
                return newPath;
            } catch (Exception exp) {
                result = false;
                return exp.Message;
            }

        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            
            return stream;
        }
        #endregion

    }
}
