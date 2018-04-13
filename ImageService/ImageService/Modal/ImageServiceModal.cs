// ***********************************************************************
// Assembly         : ImageService
// Author           : user
// Created          : 04-08-2018
//
// Last Modified By : user
// Last Modified On : 04-13-2018
// ***********************************************************************
// <copyright file="ImageServiceModal.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
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



namespace ImageService.Modal
{
    /// <summary>
    /// Class ImageServiceModal.
    /// </summary>
    /// <seealso cref="ImageService.Modal.IImageServiceModal" />
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private ILoggingService m_logging;
        private string m_thumbnailDirFolderName;
        //we initialize this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r;
        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServiceModal"/> class.
        /// </summary>
        /// <param name="outPutFolder">The out put folder.</param>
        /// <param name="thumbnailSize">Size of the thumbnail.</param>
        /// <param name="logging">The logging.</param>
        public ImageServiceModal(string outPutFolder, int thumbnailSize, ILoggingService logging)
        {
            m_thumbnailDirFolderName = "Thumbnails";
            m_OutputFolder = outPutFolder;
            m_thumbnailSize = thumbnailSize;
            m_logging = logging;
            r = new Regex(":");
    }


        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>Indication if the Addition Was Successful</returns>
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

                string thumbnailImagePath = m_OutputFolder + "\\" 
                    + m_thumbnailDirFolderName + "\\" + year + "\\" + month + "\\" + fName;
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


        /// <summary>
        /// Creates the folder hierarchy.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
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
        /// <summary>
        /// Gets the date taken from image.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>DateTime.</returns>
        public DateTime GetDateTakenFromImage(string path)
        {
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

        /// <summary>
        /// Generates the stream from string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>Stream.</returns>
        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            
            return stream;
        }
    }
    
}
