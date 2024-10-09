using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class StartMenu : Proccess
    {
        public override void Run()
        {
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x, y, sizeX, sizeY);
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 325, 70, 70, 5, GUI.colors.redColor); //shutdown
            CustomDrawing.DrawFullRoundedRectangle(x + 85, y + 325, 70, 70, 5, GUI.colors.yellowColor); //reboot
            CustomDrawing.DrawFullRoundedRectangle(x + 165, y + 325, 70, 70, 5, GUI.colors.greenColor); //tmp settings, later logout
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 255, 240, 60, 5, GUI.colors.whiteColor); //file explorer
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 185, 240, 60, 5, GUI.colors.whiteColor); //brush
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 115, 240, 60, 5, GUI.colors.whiteColor); //notepad
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 45, 240, 60, 5, GUI.colors.whiteColor); //notepad
        }
    }
}
