using Cosmos.System;
using Cosmos.System.Graphics;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class Brush : Proccess
    {
        bool isDrawing;
        List<Tuple<Point, int>> allPoints = new List<Tuple<Point, int>>();
        int activeColor = 0;
        List<Color> availableColor = new List<Color> { GUI.colors.blackColor, GUI.colors.silverColor, GUI.colors.grayColor, GUI.colors.whiteColor,
                                                    GUI.colors.maroonColor, GUI.colors.redColor, GUI.colors.purpleColor, GUI.colors.fuchsiaColor,
                                                    GUI.colors.greenColor, GUI.colors.limeColor, GUI.colors.oliveColor, GUI.colors.yellowColor,
                                                    GUI.colors.navyColor, GUI.colors.blueColor, GUI.colors.tealColor, GUI.colors.cyanColor };
        List<int> brushSize = new List<int> { 5, 7, 9 };
        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x+45, y+5+Window.topSize, sizeX - 50, sizeY - 10 - Window.topSize);
            for (int i = 0; i < availableColor.Count; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(availableColor[i], x + 5 + ((i % 2) * 20), y + 5 + Window.topSize + ((i / 2) * 20), 15, 15);
                if(GUI.mX > x + 5 + ((i % 2) * 20) && GUI.mX < x + 20 + ((i % 2) * 20) && GUI.mY > y + 5 + Window.topSize + ((i / 2) * 20) && GUI.mY < y + 20 + Window.topSize + ((i / 2) * 20))
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                    {
                        activeColor = i;
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }

            for (int i = 0; i < brushSize.Count; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5, y + 185 + Window.topSize + (i * 20), 15, 15);
            }
            foreach (Tuple<Point, int> t in allPoints)
            {
                GUI.mainCanvas.DrawFilledCircle(availableColor[t.Item2], x + t.Item1.X, y +t.Item1.Y, brushSize[0]);
            }

            if (GUI.mX > x + 45 && GUI.mX < x + sizeX - 5 && GUI.mY > y + 5 + Window.topSize && GUI.mY < y + sizeY - 5)
            {
                GUI.mOffset = 10;
                GUI.cursor = GUI.cursorCross;
                if (MouseManager.MouseState == MouseState.Left)
                {
                    isDrawing = true;
                }
                else
                {
                    isDrawing = false;
                }
            }
            else
            {
                GUI.mOffset = 0;
                GUI.cursor = GUI.cursorReg;
                isDrawing = false;
            }

            if (isDrawing)
            {
                Point p = new Point(GUI.mX - x, GUI.mY - y);
                allPoints.Add(Tuple.Create(p , activeColor));
            }
        }
    }
}
