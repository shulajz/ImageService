using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        
        public ImageServiceModal(string outPutFolder, int thumbnailSize)
        {
            m_OutputFolder = outPutFolder;
            m_thumbnailSize = thumbnailSize;
        }


        public string AddFile(string path, out bool result)
        {
            try
            {
                System.IO.Directory.CreateDirectory(m_OutputFolder); //create the directory if its not created already

                DateTime creation = File.GetCreationTime(path);
                int year = creation.Year;
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + year); // check if this year exist, if not creats it
                int month = creation.Month;
                // check if this month exist, if not creats it
                System.IO.Directory.CreateDirectory(m_OutputFolder + "\\" + year + "\\" + month);

                File.Copy(path, m_OutputFolder + "\\" + year + "\\" + month);
            


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
