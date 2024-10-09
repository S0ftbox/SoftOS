using SoftOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public static class Window
    {
        public static int topSize = 30;
        public static void DrawTop(Proccess proc)
        {
            CustomDrawing.DrawTopRoundedRectangle(proc.windowData.winPos.X, proc.windowData.winPos.Y, proc.windowData.winPos.Width, topSize, topSize / 2, GUI.activeTheme.Item3);
            GUI.mainCanvas.DrawString(proc.name, GUI.defaultFont, GUI.activeTheme.Item1, proc.windowData.winPos.X + topSize / 2, proc.windowData.winPos.Y + topSize / 4);
            GUI.mainCanvas.DrawFilledCircle(GUI.colors.redColor, proc.windowData.winPos.X + proc.windowData.winPos.Width - topSize / 2, proc.windowData.winPos.Y + topSize / 2, topSize / 4);
        }
    }
}
