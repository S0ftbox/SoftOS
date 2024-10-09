using Cosmos.System;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class InfoBox : Proccess
    {
        public int msgType = 0; //0 - info; 1 - warning; 2 - error
        public string msgText;
        public string[] infoBoxTitle = { "Info", "Warning", "Error" };

        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            //GUI.mainCanvas.DrawFilledRectangle(GUI.colors.mainColor, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x, y + Window.topSize, sizeX, Window.topSize);
            GUI.mainCanvas.DrawString(msgText, GUI.defaultFont, GUI.activeTheme.Item1, x + Window.topSize / 2, y + Window.topSize + Window.topSize / 4);
            SetName();
        }

        void SetName()
        {
            name = infoBoxTitle[msgType];
        }
    }
}
