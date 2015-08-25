using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZifliAdmin
{
    public partial class ZifliForm : Form
    {
        // _LOGLEVEL
        // 0 = info
        // 1 = warning
        // 2 = error
        // 3 = critical
        public const int _LOGLEVEL = 0;

        public const int _DEBUG = 0;

        public const int _INFO = 1;

        public const int _WARN = 2;

        public const int _ERR = 3;

        public const int _POLLS = 10;

        public const string _TIMEOUT = " 0, 0, 0, 30, 0 ";

        public ZifliForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.quitter();
        }

        private void quitter()
        {
            notifyZifli.Visible = false;
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnPoll_Click(object sender, EventArgs e)
        {
            this.labelResult.Text = "Result:";
            try
            {
                ClientRemoting.remotePollTcp(this);
            }
            catch
            {
                AdminLogger.WriteEvent("Exception handled trying to poll computer.",
                    ZifliForm._ERR);
            }
        }

        private void ZifliForm_Load(object sender, EventArgs e)
        {
            ClientRemoting.createChannelTcp();
            this.updateLabelsMenus();
            this.Show();
        }

        public void labelResult_Modify(bool boolResult)
        {
            if (boolResult)
            {
                this.labelResult.Text = "Result: Success";
            }
            else
            {
                this.labelResult.Text = "Result: Failure";
            }
        }

        private void notifyZifli_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void notifyZifli_MouseClick(object sender, MouseEventArgs e)
        {
            this.contextMenuStrip1.Show();
        }

        private void cmsStrip1_disable_stop()
        {
            this.stopToolStripMenuItem1.Enabled = false;
        }

        private void cmsStrip1_enable_stop()
        {
            this.stopToolStripMenuItem1.Enabled = true;
        }

        private void cmsStrip1_disable_start()
        {
            this.startToolStripMenuItem1.Enabled = false;
        }

        private void cmsStrip1_enable_start()
        {
            this.startToolStripMenuItem1.Enabled = true;
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SvcController svcC = new SvcController();
            int intSvcRunning = svcC.isSvcRunning();
            int intSvcStart;
            if (intSvcRunning == 1)
            {
                AdminLogger.WriteEvent("Trying to start service.",
                        ZifliForm._DEBUG);
                try
                {
                    intSvcStart = svcC.startSvc();
                }
                catch
                {
                    AdminLogger.WriteEvent("Exception occurred starting service.",
                        ZifliForm._ERR);
                    intSvcStart = 2;
                }
            }
            else
            {
                AdminLogger.WriteEvent("Service was already running.",
                    ZifliForm._WARN);
                intSvcStart = 0;
            }
            AdminLogger.WriteEvent("intSvcStart: " + intSvcStart.ToString(),
                ZifliForm._DEBUG);
            this.updateLabelsMenus();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SvcController svcC = new SvcController();
            int intSvcRunning = svcC.isSvcRunning();
            if (intSvcRunning == 0)
            {
                AdminLogger.WriteEvent("Trying to stop service.",
                        ZifliForm._DEBUG);
                try
                {
                    int intSvcStop = svcC.stopSvc();
                }
                catch
                {
                    AdminLogger.WriteEvent("Exception occurred stopping service.",
                        ZifliForm._ERR);
                }
            }
            else
            {
                AdminLogger.WriteEvent("Service was already stopped.",
                    ZifliForm._ERR);
            }
            this.updateLabelsMenus();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.quitter();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        public void updateLabelsMenus()
        {
            SvcController svcC = new SvcController();
            int intSvcRunning;
            try
            {
                intSvcRunning = svcC.isSvcRunning();
            }
            catch
            {
                AdminLogger.WriteEvent("Exception when trying to get service status.",
                    ZifliForm._ERR);
                intSvcRunning = 2;
                this.cmsStrip1_disable_start();
                this.cmsStrip1_disable_stop();
            }
            AdminLogger.WriteEvent("updateLabelsMenus, intSvcRunning: " +
                intSvcRunning.ToString(),
                ZifliForm._DEBUG);
            if (intSvcRunning == 0)
            {
                this.labelStatus.Text = "Service Status: RUNNING";
                this.cmsStrip1_disable_start();
                this.cmsStrip1_enable_stop();
            }
            else if (intSvcRunning == 1)
            {
                this.labelStatus.Text = "Service Status: STOPPED";
                this.cmsStrip1_enable_start();
                this.cmsStrip1_disable_stop();
            }
            else
            {
                this.labelStatus.Text = "Service Status: UNKNOWN";
                this.cmsStrip1_disable_start();
                this.cmsStrip1_disable_stop();
            }
        }
    }
}