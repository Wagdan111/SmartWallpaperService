using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace BackgroundScheduler
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        ServiceProcessInstaller ServiceProcessInstaller;
        ServiceInstaller ServiceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();
            ServiceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            ServiceInstaller = new ServiceInstaller
            {
                ServiceName = "AutoWallpaperService",
                DisplayName = "Auto Wallpaper Service",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(ServiceProcessInstaller);
            Installers.Add(ServiceInstaller);
        }
    }
}
