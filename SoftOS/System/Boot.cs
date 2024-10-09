using Cosmos.System.Graphics;
using SoftOS.Graphics;
using SoftOS.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public static class Boot
    {
        public static void OnBoot()
        {
            Kernel.runGUI = true;
            if (!Directory.Exists(Kernel.Path + "SystemApps"))
            {
                string appPath = Kernel.Path + "SystemApps";
                Directory.CreateDirectory(appPath);
                File.Create(appPath + @"\Brush.exe");
                File.Create(appPath + @"\Notepad.exe");
            }
            if(!Directory.Exists(Kernel.Path + "SystemUsers"))
            {
                string userPath = Kernel.Path + "SystemUsers";
                Directory.CreateDirectory(userPath);
                userPath += @"\DefaultUser";
                Directory.CreateDirectory(userPath);
                File.Create(userPath + @"\Settings.cfg");
                Kernel.settingsFilePath = userPath + @"\Settings.cfg";
                File.WriteAllText(Kernel.settingsFilePath, "Wallpaper: 0 Cursor: 0 Theme: 0");
                Directory.CreateDirectory(userPath + @"\Desktop");
            }

            GUI.StartGUI();
            //Console.Beep(1046, 150);
            //Console.Beep(784, 150);
            //Console.Beep(659, 150);
        }
    }
}
