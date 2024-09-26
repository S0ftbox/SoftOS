using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System;

namespace SoftOS.Graphics
{
    public class GUI
    {
        public Canvas canvas;
        private List<Tuple<uint, uint, Color>> savedPixels;
        private TabBar tabBar;

        private MouseState prevMouseState;

        private Color pen;

        private uint pX, pY;

        public GUI()
        {
            this.canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(800,600, ColorDepth.ColorDepth32));
            this.canvas.Clear(Color.Black);

            this.prevMouseState=MouseState.None;

            this.pen = Color.White;

            this.pX = 3;
            this.pY = 3;
            this.savedPixels = new List<Tuple<uint, uint, Color>>();

            this.tabBar = new TabBar(this.canvas);

            MouseManager.ScreenHeight = canvas.Mode.Height;
            MouseManager.ScreenWidth = canvas.Mode.Width;

            this.canvas.Display();
        }

        public void HandleGIUInput()
        {
            if(this.pX!=MouseManager.X || this.pY != MouseManager.Y)
            {
                if (MouseManager.X < 2 || MouseManager.Y < 2 || MouseManager.X > (MouseManager.ScreenWidth-2) || MouseManager.Y > (MouseManager.ScreenHeight-2))
                    return;

                this.pX = MouseManager.X;
                this.pY = MouseManager.Y;

                foreach(Tuple<uint,uint,Color> pixelData in this.savedPixels)
                {
                    this.canvas.DrawPoint(pixelData.Item3, (int)pixelData.Item1, (int)pixelData.Item2);
                }

                this.savedPixels.Clear();

                this.savedPixels.Add(new Tuple<uint, uint, Color>(MouseManager.X, MouseManager.Y, this.canvas.GetPointColor((int)MouseManager.X, (int)MouseManager.Y)));
                this.canvas.DrawPoint(pen, (int)MouseManager.X, (int)MouseManager.Y);
                this.savedPixels.Add(new Tuple<uint, uint, Color>(MouseManager.X-1, MouseManager.Y, this.canvas.GetPointColor((int)MouseManager.X-1, (int)MouseManager.Y)));
                this.canvas.DrawPoint(pen, (int)MouseManager.X-1, (int)MouseManager.Y);
                this.savedPixels.Add(new Tuple<uint, uint, Color>(MouseManager.X+1, MouseManager.Y, this.canvas.GetPointColor((int)MouseManager.X+1, (int)MouseManager.Y)));
                this.canvas.DrawPoint(pen, (int)MouseManager.X+1, (int)MouseManager.Y);
                this.savedPixels.Add(new Tuple<uint, uint, Color>(MouseManager.X, MouseManager.Y-1, this.canvas.GetPointColor((int)MouseManager.X, (int)MouseManager.Y-1)));
                this.canvas.DrawPoint(pen, (int)MouseManager.X, (int)MouseManager.Y-1);
                this.savedPixels.Add(new Tuple<uint, uint, Color>(MouseManager.X, MouseManager.Y+1, this.canvas.GetPointColor((int)MouseManager.X, (int)MouseManager.Y+1)));
                this.canvas.DrawPoint(pen, (int)MouseManager.X, (int)MouseManager.Y+1);
            }
            this.canvas.Display();

            if (MouseManager.MouseState == MouseState.Left && this.prevMouseState != MouseState.Left)
            {
                this.tabBar.TryProcessTabBarClick((int)MouseManager.X, (int)MouseManager.Y);
                Tab.TryProcessTabLMBDown((int)MouseManager.X, (int)MouseManager.Y);
            }
            else if(MouseManager.MouseState == MouseState.None && this.prevMouseState == MouseState.Left)
            {
                Tab.TryProcessTabLMBRelease((int)MouseManager.X, (int)MouseManager.Y);
            }

            this.prevMouseState=MouseManager.MouseState;
        }
    }
}
