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

//using System.Configuration;

using ImageService.Communication;
using ImageService.Commands;
using ImageService.Communication.Modal;

namespace ImageService
{
    public partial class ImageService : ServiceBase
    {
        private ImageServer m_imageServer;          // The Image Server.
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;
       


        //private System.ComponentModel.IContainer components;
        private System.Diagnostics.EventLog eventLog1;
        private int eventId = 1;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle,
           ref ServiceStatus serviceStatus);

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="args">The arguments sent.</param>
        public ImageService(string[] args)
        {
            
            InitializeComponent();
            //read from app config
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
        /// <summary>
        /// When implemented in a derived class,
        /// executes when a Start command is sent
        /// to the service by the Service Control Manager (SCM)
        /// or when the operating system starts
        /// (for a service that starts automatically).
        /// Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                Console.WriteLine("OnStart");
                
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

                //read from app config
                AppConfig appConfig = new AppConfig(eventLog1);
                eventLog1.WriteEntry("after app config");
                eventLog1.WriteEntry("the output dir=" + appConfig.OutPutDir);
                eventLog1.WriteEntry("the ArrHandlers =" + appConfig.ArrHandlers[0]);


                //create the LoggingService
                logging = new LoggingService();
                logging.MessageReceivedEvent += onMsg;
                logging.MessageReceivedEvent += LogCommand.onReceiveCommandLog;

                modal = new ImageServiceModal(appConfig.OutPutDir, appConfig.ThumbnailSize, logging);
                controller = new ImageController(modal, appConfig, eventLog1);


                
                m_imageServer = new ImageServer(logging, appConfig.ArrHandlers, controller);
                ClientHandler clientHandler = new ClientHandler(controller, eventLog1);

                TCPServerChannel server = new TCPServerChannel(8000, clientHandler, eventLog1);
                logging.MessageReceivedEvent += server.SendLog;


                server.Start();
                //create the ImageServer         

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
            }


        }

        /// <summary>
        /// This function is added to the eventlog, and is callen whens
        /// the event is invoked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void onMsg(object sender, MessageReceivedEventArgs e)
        {
            eventLog1.WriteEntry(e.m_status +": " + e.m_message); 

        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command
        /// is sent to the service by the Service Control Manager (SCM). 
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
            m_imageServer.sendCommand();
        }

        /// <summary>
        /// Handles the <see cref="E:Timer" /> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            //TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System",
            EventLogEntryType.Information, eventId++);
        }

        /// <summary>
        /// When implemented in a derived class, 
        /// <see cref="M:System.ServiceProcess.ServiceBase.OnContinue" />
        /// runs when a Continue command is sent to the
        /// service by the Service Control Manager (SCM). 
        /// Specifies actions to take when a service
        /// resumes normal functioning after being paused.
        /// </summary>
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