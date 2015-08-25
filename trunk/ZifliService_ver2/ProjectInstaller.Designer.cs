namespace ZifliService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceZifliProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceZifliInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceZifliProcessInstaller
            // 
            this.serviceZifliProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceZifliProcessInstaller.Password = null;
            this.serviceZifliProcessInstaller.Username = null;
            // 
            // serviceZifliInstaller
            // 
            this.serviceZifliInstaller.ServiceName = "ZifliService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceZifliProcessInstaller,
            this.serviceZifliInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceZifliProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceZifliInstaller;
    }
}