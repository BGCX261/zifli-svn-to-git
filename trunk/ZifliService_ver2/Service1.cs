using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;

namespace ZifliService
{
    public partial class ZifliService : ServiceBase
    {
        // _LOGLEVEL
        // 0 = info
        // 1 = warning
        // 2 = error
        // 3 = critical
        public const int _LOGLEVEL = 0;

        public const int _POLLS = 10;

        public const int _DEBUG = 0;

        public const int _INFO = 1;

        public const int _WARN = 2;

        public const int _ERR = 3;

        public const int _PORT = 11975;

        private System.Timers.Timer tmrZifli = new System.Timers.Timer();

        public ZifliService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            // TODO: Add code here to start your service.
            Logger.WriteEvent("Zifli service started", ZifliService._INFO);
            this.Initialize();
            this.tmrZifli.Enabled = true;
            this.tmrZifli.Interval = 300000;
            this.tmrZifli.Start();
            this.tmrZifli.Elapsed += new System.Timers.ElapsedEventHandler(tmrZifli_Elapsed);
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            tmrZifli.Enabled = false;
            Logger.WriteEvent("Zifli service stopped", ZifliService._INFO);
        }

        private Computer oComputer;

        private int intPollInterval = 300;

        private string strMACAddress;

        private string strMachineName;

        private long lUnixTS;

        private string strDSName = "cpu01";

        private string strEmailAddress = "";

        private string strPassword = "";

        public void Initialize()
        {
            Logger.WriteEvent("Initializing...", ZifliService._INFO);

            ServerRemoting.createChannelTcp();

            this.tmrZifli.Interval = (this.intPollInterval * 1000);

            Misc oMisc = new Misc();
            this.strMachineName = oMisc.GetMachineName();
            this.strMACAddress = oMisc.GetMacAddress(4);

            DateTime dt = DateTime.Now;
            dt = dt.AddSeconds(intPollInterval);

            Computer oComputer = new Computer();
            oComputer.addALInterfaces();
            this.oComputer = oComputer;
            ArrayList alIfaceNames = new ArrayList();
            alIfaceNames = oComputer.getInterfaceNames();

            string strMatcher = "[Gg]igabit";
            foreach (string strIfaceName in alIfaceNames)
            {
                if (Regex.IsMatch(strIfaceName, strMatcher))
                {
                    Logger.WriteEvent("mac address: " +
                        oComputer.getActiveMACAdress(oComputer.getInterfaceByName(strIfaceName)),
                        ZifliService._INFO);
                }
            }
            Logger.WriteEvent("...finished initializing.", ZifliService._INFO);
        }

        void tmrZifli_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.WriteEvent("Tick", ZifliService._INFO);

            DateTime dt = DateTime.Now;
            Misc dtConvert = new Misc();
            this.lUnixTS = dtConvert.getUnixTimeStamp(dt);
            dt = dt.AddSeconds(intPollInterval);
            Poller oPoller = new Poller();
            Result aResult = oPoller.queryProviderByName("Win32_PerfFormattedData_PerfOS_Processor",
                "_Total", "PercentProcessorTime");
            Misc oUploader = new Misc();
            try
            {
                oUploader.uploadResults(this.strMACAddress,
                    this.strDSName, aResult, this.strMachineName, this.lUnixTS,
                    this.strEmailAddress, this.strPassword);
                Logger.WriteEvent("Uploaded results successfully.",
                    ZifliService._DEBUG);
            }
            catch
            {
                Logger.WriteEvent("Failed to upload results.", ZifliService._WARN);
            }
        }
    }
}
