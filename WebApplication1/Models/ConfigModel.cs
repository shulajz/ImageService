using ImageService.Communication;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ImageServiceWeb.Models
{
    public class ConfigModel
    {

        private ClientWebSingleton client;
        private Setting setting;
        public ConfigModel()
        {

            setting = new Setting();
            client = ClientWebSingleton.getInstance;
            client.CommandReceivedEvent += settingsOnCommand;

            CommandReceivedEventArgs e = new CommandReceivedEventArgs(
                (int)CommandEnum.GetConfigCommand, null, null);
            WriteToClient(e);
            client.wait();

        }
        /// <summary>
        /// Settingses the on command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void settingsOnCommand(object sender, ClientArgs e)
        {
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                setting = JsonConvert.DeserializeObject<Setting>(e.Args);
                OutPutDir = setting.OutPutDir;
                SourceName = setting.SourceName;
                LogName = setting.LogName;
                ThumbnailSize = setting.ThumbnailSize;
                HandlersArr = setting.ArrHandlers;

            }
            else if (e.CommandID == (int)CommandEnum.RemoveHandler)
            {
                //Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                HandlersArr.Remove(e.Args);
                //}));
            }
        }

        public void WriteToClient(CommandReceivedEventArgs e)
        {
            client.write(e);
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPutDir:")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName:")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize:")]
        public int ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "HandlersArr:")]
        public ObservableCollection<string> HandlersArr { get; set; }

    }
}