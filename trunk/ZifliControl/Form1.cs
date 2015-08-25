using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZifliService_ver2;

namespace ZifliControl
{
    public partial class MainControl : Form
    {
        private int intPollInterval;

        public MainControl()
        {
            InitializeComponent();
        }

        private void TrayNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.quitter();
        }

        private void quitter()
        {
            this.TrayNotify.Visible = false;
            Application.Exit();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cbxPollInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxPollInterval.SelectedItem.ToString())
            {
                case "1 second":
                    this.intPollInterval = 1;
                    break;

                case "5 seconds":
                    this.intPollInterval = 5;
                    break;

                case "10 seconds":
                    this.intPollInterval = 10;
                    break;

                case "15 seconds":
                    this.intPollInterval = 15;
                    break;

                case "20 seconds":
                    this.intPollInterval = 20;
                    break;

                case "30 seconds":
                    this.intPollInterval = 30;
                    break;

                case "1 minute":
                    this.intPollInterval = 60;
                    break;

                case "2 minutes":
                    this.intPollInterval = 120;
                    break;

                case "5 minutes":
                    this.intPollInterval = 300;
                    break;
            }
        }
    }
}