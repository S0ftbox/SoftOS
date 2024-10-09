using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class MessageBox : Proccess
    {
        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
        }
    }
}
