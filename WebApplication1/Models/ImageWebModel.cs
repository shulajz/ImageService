using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace ImageServiceWeb.Models
{
    public class ImageWebModel
    {
        private Student student { get; set; }
        static string path = HostingEnvironment.MapPath("~/App_Data/info.txt");
        static string[] lines = System.IO.File.ReadAllLines(@path);
        public List<Student> students { get; set; }
        private ClientWebSingleton client;

        public ImageWebModel(int numOfPhotos)
        {
            student = new Student();
            client = ClientWebSingleton.getInstance;
            updateServiceStatus();
            NumOfPhotos = numOfPhotos;
            students = new List<Student>()
            {
            new Student() {   FirstName = lines[0].Split(' ')[0],
                LastName = lines[0].Split(' ')[1],
                ID = lines[0].Split(' ')[2] },
            new Student() {   FirstName = lines[1].Split(' ')[0],
                LastName = lines[1].Split(' ')[1],
                ID = lines[1].Split(' ')[2]
            }
        };

        }

        public void updateServiceStatus()
        {
            if (client.CheckIfServerConnect())
            {
                ServiceStatus = "ON!";
            }
            else
            {
                ServiceStatus = "OFF!";

            }
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Number of photos:")]
        public int NumOfPhotos { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service Status:")]
        public string ServiceStatus { get; set; }

        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}