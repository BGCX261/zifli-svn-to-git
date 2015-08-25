using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace ZifliService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}