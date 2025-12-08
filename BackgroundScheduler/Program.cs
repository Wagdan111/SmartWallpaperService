using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundScheduler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                WallpaperService autoWallpaperService = new WallpaperService();
                autoWallpaperService.OnStartConslose();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new WallpaperService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
