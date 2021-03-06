﻿using ImageService.Communication.Modal;
using ImageService.Logging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
        /// The Function Addes A file to the system.
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

                //move file and if name file already exist change is name.
                newPath = moveFile(newPath, path, year, month);
               

                //thumbnails
                createFolderHierarchy(m_OutputFolder + "\\" 
                    + m_thumbnailDirFolderName,
                    year, month);
                
                Image image = Image.FromFile(newPath);
                
                Image thumb = image.GetThumbnailImage(
                    m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);

                fName = Path.GetFileName(newPath);

                string thumbnailImagePath = m_OutputFolder + "\\" 
                    + m_thumbnailDirFolderName + "\\" + year + "\\" + month + "\\" + fName;
                
                thumb.Save(Path.ChangeExtension(thumbnailImagePath, Path.GetExtension(newPath)));
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
        /// <param name="path">The path of the image.</param>
        /// <param name="year">The year of the image.</param>
        /// <param name="month">The month of the image.</param>
        public void createFolderHierarchy(string path, int year, int month)
        {
            //create the directory if its not created already
            DirectoryInfo dir = Directory.CreateDirectory(path);
            //if outPutDir then hide directory
            if (path.Equals(m_OutputFolder)){
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            m_logging.Log("Directory"+ path + " was created successfully", MessageTypeEnum.INFO);

            // check if this year exist, if not - creates it
            System.IO.Directory.CreateDirectory(path + "\\" + year);
            m_logging.Log(path + "\\" + year +" folder was created successfully", MessageTypeEnum.INFO);

            // check if this month exist, if not - creates it
            System.IO.Directory.CreateDirectory(path + "\\" + year + "\\" + month);
            m_logging.Log(path + "\\" + year + "\\" + month +" folder was created successfully", MessageTypeEnum.INFO);

        }
        /// <summary>
        /// Gets the date taken from image.
        /// </summary>
        /// <param name="path">The path of the image.</param>
        /// <returns>DateTime of when the image was created.</returns>
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
        /// <param name="s">The string.</param>
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



        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="newPath">The new path.</param>
        /// <param name="oldPath">The old path.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        public string moveFile(string newPath, string oldPath, int year, int month)
        {
            newPath = checkIfFileNameExist(newPath, oldPath, year, month);
            File.Copy(oldPath, newPath);
            File.Delete(oldPath);
            m_logging.Log("picture was copied successfully", MessageTypeEnum.INFO);
            return newPath;
        }

        public string checkIfFileNameExist(string newPath, string oldPath, int year, int month)
        {
            bool nameFileAlreadyExist = false;
            int count = 1;
            bool onlyOneTime = true;
            string fileNameOnly = Path.GetFileNameWithoutExtension(newPath);
            string extension = Path.GetExtension(newPath);
            while (File.Exists(newPath))
            {
                nameFileAlreadyExist = true;
                if (onlyOneTime)
                {
                    m_logging.Log("name of file "+ newPath + " already exist",
                        MessageTypeEnum.INFO);
                    onlyOneTime = false;
                }
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newPath = Path.Combine(m_OutputFolder + "\\" +
                    year + "\\" + month + "\\", tempFileName + extension);
             
            }
            if (nameFileAlreadyExist)
            {
                m_logging.Log("change the name file to " + Path.GetFileName(newPath),
                 MessageTypeEnum.INFO);
            }

            return newPath;
        }
    }
    
}
