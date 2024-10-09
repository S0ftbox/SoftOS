using Cosmos.Core;
using Cosmos.System;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class ProcInfo : Proccess
    {
        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int i = 0;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawString("CPU: " + SysInfo.CPUName, GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + Window.topSize + 8);
            GUI.mainCanvas.DrawString("Used RAM: " + SysInfo.UsedRAM.ToString(), GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + Window.topSize + 8 + 24);
            GUI.mainCanvas.DrawString("Available RAM: " + SysInfo.AvailableRAM.ToString(), GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + Window.topSize + 8 + 40);
            foreach(var proc in ProcessManager.processList)
            {
                uint before = GCImplementation.GetUsedRAM();
                proc.usageRAM += GCImplementation.GetUsedRAM() - before;
                GUI.mainCanvas.DrawString(proc.name + " RAM: " + FormatBytes(proc.usageRAM), GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + Window.topSize + 8 + 56 + i);
                i += 16;
            }
        }

        public string FormatBytes(uint bytes)
        {
            if (bytes == 0) return "0 B";

            string[] units = { "B", "kB", "MB", "GB" };
            int i = 0;
            float value = bytes;

            while(value >= 1024 && i < units.Length - 1)
            {
                value /= 1024;
                i++;
            }

            string formatString = value < 10 ? "0.0" : "0";
            return $"{value.ToString(formatString)} {units[i]}";
        }
    }

    public static class SysInfo
    {
        public static string CPUName = CPU.GetCPUBrandString();
        public static uint InstalledRAM = CPU.GetAmountOfRAM();
        public static uint ReservedRAM = InstalledRAM - (uint)GCImplementation.GetAvailableRAM();
        public static double UsedRAM = (GCImplementation.GetUsedRAM() / (1024.0 * 1024.0)) + ReservedRAM;
        public static uint AvailableRAM = InstalledRAM - (uint)UsedRAM;
    }
}
