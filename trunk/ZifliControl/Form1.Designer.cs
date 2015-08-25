namespace ZifliControl
{
    partial class MainControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TrayNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.stopSvc = new System.Windows.Forms.ToolStripMenuItem();
            this.startSvc = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.cbxPollInterval = new System.Windows.Forms.ComboBox();
            this.lblPollInterval = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.TrayIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayNotify
            // 
            this.TrayNotify.Text = "TrayNotify";
            this.TrayNotify.Visible = true;
            this.TrayNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayNotify_MouseDoubleClick);
            // 
            // TrayIcon
            // 
            this.TrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showConfig,
            this.stopSvc,
            this.startSvc,
            this.exit});
            this.TrayIcon.Name = "TrayIcon";
            this.TrayIcon.Size = new System.Drawing.Size(181, 92);
            this.TrayIcon.Text = "ZifliTray";
            // 
            // showConfig
            // 
            this.showConfig.Name = "showConfig";
            this.showConfig.Size = new System.Drawing.Size(180, 22);
            this.showConfig.Text = "Show Configuration";
            // 
            // stopSvc
            // 
            this.stopSvc.Name = "stopSvc";
            this.stopSvc.Size = new System.Drawing.Size(180, 22);
            this.stopSvc.Text = "Stop Service";
            // 
            // startSvc
            // 
            this.startSvc.Name = "startSvc";
            this.startSvc.Size = new System.Drawing.Size(180, 22);
            this.startSvc.Text = "Start Service";
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(180, 22);
            this.exit.Text = "Exit";
            // 
            // cbxPollInterval
            // 
            this.cbxPollInterval.FormattingEnabled = true;
            this.cbxPollInterval.Items.AddRange(new object[] {
            "1 second",
            "5 seconds",
            "15 seconds",
            "30 seconds",
            "1 minute",
            "2 minutes",
            "5 minutes"});
            this.cbxPollInterval.Location = new System.Drawing.Point(12, 25);
            this.cbxPollInterval.Name = "cbxPollInterval";
            this.cbxPollInterval.Size = new System.Drawing.Size(121, 21);
            this.cbxPollInterval.TabIndex = 4;
            this.cbxPollInterval.SelectedIndexChanged += new System.EventHandler(this.cbxPollInterval_SelectedIndexChanged);
            // 
            // lblPollInterval
            // 
            this.lblPollInterval.AutoSize = true;
            this.lblPollInterval.Location = new System.Drawing.Point(12, 9);
            this.lblPollInterval.Name = "lblPollInterval";
            this.lblPollInterval.Size = new System.Drawing.Size(76, 13);
            this.lblPollInterval.TabIndex = 5;
            this.lblPollInterval.Text = "Polling Interval";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(316, 229);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Text = "Minimize";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(397, 229);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 10;
            this.btnQuit.Text = "Exit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 264);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblPollInterval);
            this.Controls.Add(this.cbxPollInterval);
            this.Name = "MainControl";
            this.Text = "Zifli Administration";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.TrayIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayNotify;
        private System.Windows.Forms.ContextMenuStrip TrayIcon;
        private System.Windows.Forms.ToolStripMenuItem showConfig;
        private System.Windows.Forms.ToolStripMenuItem stopSvc;
        private System.Windows.Forms.ToolStripMenuItem startSvc;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ComboBox cbxPollInterval;
        private System.Windows.Forms.Label lblPollInterval;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnQuit;
    }
}

