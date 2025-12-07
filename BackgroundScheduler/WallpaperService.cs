using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;

namespace BackgroundScheduler
{
    public partial class WallpaperService : ServiceBase
    {
        string folderImagesPath;
        string Logs;
        public WallpaperService()
        {
            InitializeComponent();
            folderImagesPath = ConfigurationManager.AppSettings["FolderImagesPath"];
            Logs = ConfigurationManager.AppSettings["Logs"];

            if (string.IsNullOrEmpty(folderImagesPath))
            {
                folderImagesPath = "C:\\SmartWallpaperService\\FolderImagesPath";
                Log("WARNING: FolderImagesPath is not set in App.config. Using default path.");
            }

            if (string.IsNullOrEmpty(Logs))
            {
                Logs = "C:\\SmartWallpaperService\\Logs";
                Log("WARNING: Logs path is not set in App.config. Using default path.");
            }

            if (!Directory.Exists(folderImagesPath))
                Directory.CreateDirectory(folderImagesPath); 
            
            if (!Directory.Exists(Logs))
                Directory.CreateDirectory(Logs);
        }

        private void Log(string Message)
        {
            string FilePath=Path.Combine(Logs, "Service.txt");

            string message = $"{DateTime.Now: yyy-MM-dd HH-MM-ss}";

            File.AppendAllText(FilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} => {Message}\n");
        }

        protected override void OnStart(string[] args)
        {
            Log("Service started successfully.");
            string[] files = Directory.GetFiles(folderImagesPath);

            if (files.Length == 0)
            {
                Log("No images found in the folder.");
                return;
            }
                

            try
            {
                 int index = Properties.Settings.Default.Setting;

                 string CurrentImage = files[index];
                 SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, CurrentImage, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
                 index = (index + 1) % files.Length;

                 Properties.Settings.Default.Setting = index;
                 Properties.Settings.Default.Save();
                 Log($"Wallpaper changed successfully to: {CurrentImage}");
            }
            catch (Exception ex) 
            {
                Log($"ERROR: {ex.Message}");
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(
       uint action, uint uParam, string vParam, uint winIni);

        public static readonly uint SPI_SETDESKWALLPAPER = 0x14;
        public static readonly uint SPIF_UPDATEINIFILE = 0x01;
        public static readonly uint SPIF_SENDCHANGE = 0x02;
        protected override void OnStop()
        {
            Log("Service stopped successfully.");
        }

        public void OnStartConslose()
        {
            Console.WriteLine("Service Start With Console Mode");
            OnStart(null);
            Console.WriteLine("Enter to stop the Service");
            Console.ReadLine();
            OnStop();
        }
    }
}
