using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using ImageService.Server;
using ImageService.Modal;
using ImageService.Controller;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Configuration;


namespace ImageService
{
    public partial class ImageService : ServiceBase
    {
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;


        private System.ComponentModel.IContainer components;
        private System.Diagnostics.EventLog eventLog1;
        private int eventId = 1;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle,
           ref ServiceStatus serviceStatus);

        public ImageService(string[] args)
        {
            
            InitializeComponent();
            //read frop appconfig
            string eventSourceName = "MySource1";
            string logName = "MyLogFile1";
            if (args.Count() > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = ConfigurationManager.AppSettings["SourceName"];
            eventLog1.Log = ConfigurationManager.AppSettings["LogName"];
        }


        //Here You will use app config
        protected override void OnStart(string[] args)
        {
            try
            {
                
                // Update the service state to Start Pending.  
                ServiceStatus serviceStatus = new ServiceStatus();
                serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
                serviceStatus.dwWaitHint = 100000;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);
                eventLog1.WriteEntry("In OnStart");
                // Set up a timer to trigger every minute.  
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 60000; // 60 seconds  //
                timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
                timer.Start();
 
                // Update the service state to Running.  
                serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);

                //read frop appconfig
                string outPutDir = ConfigurationManager.AppSettings["OutputDir"];
                int thumbnailSize = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
                string[] arrHandlers = ConfigurationManager.AppSettings["Handler"].Split(';');
                //create the LoggingService
                logging = new LoggingService();
                eventLog1.WriteEntry("1");
                logging.MessageRecievedEvent += onMsg;
                eventLog1.WriteEntry("2");

                //create the ImageServer         
                m_imageServer = new ImageServer(logging,arrHandlers, outPutDir, thumbnailSize);
                eventLog1.WriteEntry("3");


            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
            }


        }

        private void onMsg(object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.m_message);  
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            //TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System",
            EventLogEntryType.Information, eventId++);
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
    }
}