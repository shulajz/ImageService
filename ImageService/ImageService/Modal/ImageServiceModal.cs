using ImageService.Infrastructure;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private string m_thumbnailDirFolderName;
        #endregion
        public ImageServiceModal(string outPutFolder, int thumbnailSize, ILoggingService logging)
        {
            m_thumbnailDirFolderName = "Thumbnails";
            m_OutputFolder = outPutFolder;
            m_thumbnailSize = thumbnailSize;
            m_logging = logging;
        }


        public string AddFile(string path, out bool result)
        {
            try
            {

                DateTime creation = GetDateTakenFromImage(path);
                int year = creation.Year;
                int month = creation.Month;

                createFolderHierarchy(m_OutputFolder, year, month);

                string fName = Path.GetFileName(path);
                string newPath = m_OutputFolder + "\\" + year + "\\" + month + "\\" + fName;

                File.Copy(path, newPath);
                File.Delete(path);
               
                m_logging.Log("picture was copied successfully", MessageTypeEnum.INFO);

                //thumbnails
                createFolderHierarchy(m_OutputFolder + "\\" + m_thumbnailDirFolderName, year, month);
                
                System.Drawing.Image image = System.Drawing.Image.FromFile(newPath);
                
                System.Drawing.Image thumb = image.GetThumbnailImage(
                    m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);

                string thumbnailImagePath = m_OutputFolder + "\\" + m_thumbnailDirFolderName + "\\" + year + "\\" + month + "\\" + fName;
                thumb.Save(Path.ChangeExtension(thumbnailImagePath, "thumb"));
                image.Dispose();

               
                result = true;
                return newPath;
            }
            catch (Exception exp)
            {
                result = false;
                return exp.Message;
            }

        }


        public void createFolderHierarchy(string path, int year, int month)
        {
            //create the directory if its not created already
            System.IO.Directory.CreateDirectory(path);
            m_logging.Log("Directory"+ path + " was created successfully", MessageTypeEnum.INFO);

            // check if this year exist, if not - creats it
            System.IO.Directory.CreateDirectory(path + "\\" + year);
            m_logging.Log(path + "\\" + year +" folder was created successfully", MessageTypeEnum.INFO);

            // check if this month exist, if not - creats it
            System.IO.Directory.CreateDirectory(path + "\\" + year + "\\" + month);
            m_logging.Log(path + "\\" + year + "\\" + month +" folder was created successfully", MessageTypeEnum.INFO);

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
        


        public DateTime GetDateTakenFromImage(string path)
        {
            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();
            DateTime date = File.GetCreationTimeUtc(path)+localOffset;
            return date;
        }

    }
    
}
