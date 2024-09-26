using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;

namespace SoftOS.Graphics
{
    public class Tab
    {
        private static List<Tab> tabs = new List<Tab>(1);
        private static Color outlinePen = Color.White, xBtnPen = Color.Red;

        internal const int defaultWindowSize = 300, windowTopSize = 50, closeBtnSize = 50;

        private List<TabComponent> components;

        public int X
        {
            get;

            private set;
        }

        public int Y
        {
            get;

            private set;
        }

        private protected Boolean beingDragged;
        private protected int dragDiffX, dragDiffY;

        public Tab(int _x, int _y)
        {
            int x = _x;
            int y = _y;

            if((x>(Kernel.gui.canvas.Mode.Width-Tab.defaultWindowSize)) || (y > (Kernel.gui.canvas.Mode.Height - Tab.defaultWindowSize + 100)))
            {
                x = (int)Kernel.gui.canvas.Mode.Width - (Tab.defaultWindowSize + 1);
                y = (int)Kernel.gui.canvas.Mode.Height - (Tab.defaultWindowSize + 101);
            }

            this.components = new List<TabComponent>();
            this.X = x;
            this.Y = y;
            this.beingDragged = false;

            this.Draw();

            tabs.Add(this);
        }

        private void Draw()
        {
            Kernel.gui.canvas.DrawRectangle(outlinePen, this.X, this.Y, Tab.defaultWindowSize, Tab.windowTopSize);
            Kernel.gui.canvas.DrawRectangle(outlinePen, this.X+(Tab.defaultWindowSize-Tab.closeBtnSize), this.Y, Tab.closeBtnSize, Tab.closeBtnSize);
            Kernel.gui.canvas.DrawFilledRectangle(xBtnPen, this.X + (Tab.defaultWindowSize - Tab.closeBtnSize - 1), this.Y + 1, Tab.closeBtnSize - 1, Tab.closeBtnSize - 1);
            Kernel.gui.canvas.DrawRectangle(outlinePen, this.X,this.Y+Tab.windowTopSize,Tab.defaultWindowSize, Tab.defaultWindowSize-Tab.windowTopSize);
        }

        public void AddComponent(TabComponent tabComponent)
        {
            this.components.Add(tabComponent);
            tabComponent.Draw(this);
        }

        public void Move(int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
            this.Draw();
        }

        public void Close()
        {
            Tab.tabs.Remove(this);
        }

        public static void TryProcessTabLMBDown(int mouseX, int mouseY)
        {
            if(Tab.tabs.Count == 0)
                return;

            foreach(Tab t in Tab.tabs)
            {
                Rectangle mouseLocation = new Rectangle(mouseX, mouseY, 1, 1);
                if(mouseLocation.IntersectsWith(new Rectangle(t.X, t.Y, Tab.defaultWindowSize - Tab.closeBtnSize, Tab.windowTopSize)))
                {
                    t.dragDiffX = (mouseX - t.X);
                    t.dragDiffY = (mouseY - t.Y);
                    t.beingDragged = true;
                }
                else if(mouseLocation.IntersectsWith(new Rectangle(t.X + (Tab.defaultWindowSize - Tab.closeBtnSize), t.Y, Tab.closeBtnSize, Tab.closeBtnSize)))
                {
                    t.Close();
                }
            }
        }

        public static void TryProcessTabLMBRelease(int mouseX, int mouseY)
        {
            if(Tab.tabs.Count == 0)
                return;

            foreach (Tab t in Tab.tabs)
            {
                if (t.beingDragged)
                {
                    t.beingDragged = false;
                    t.Move(mouseX - t.dragDiffX, mouseY - t.dragDiffY);
                }
            }
        }
    }
}
