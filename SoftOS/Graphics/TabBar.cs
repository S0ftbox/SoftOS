using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Cosmos.System.Graphics;

namespace SoftOS.Graphics
{
    public class TabBar
    {
        private Color pen;
        private int rows, columns;

        public TabBar(Canvas canvas)
        {
            this.pen = Color.White;
            this.rows = (int)canvas.Mode.Height;
            this.columns = (int)canvas.Mode.Width;

            canvas.DrawFilledRectangle(Color.DarkCyan, 0, this.rows - 100, this.columns - 1, 99, true);
            canvas.DrawFilledRectangle(Color.White, 0, this.rows - 100, 100, 99, true);
            canvas.DrawLine(Color.Red, 10, this.rows - 90, 90, this.rows - 10);
            canvas.DrawLine(Color.Red, 10, this.rows - 10, 90, this.rows - 90);
        }

        public void TryProcessTabBarClick(int mouseX, int mouseY)
        {
            if(new Rectangle(mouseX,mouseY,1,1).IntersectsWith(new Rectangle(0, this.rows - 100, 100, 99)))
            {
                //System.Console.Beep();
                new Tab(0, 0);
            }
        }
    }
}
