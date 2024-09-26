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
    public class TabComponent
    {
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

        private readonly Byte size;

        public TabComponent(int X, int Y, Byte size)
        {
            this.X = X;
            this.Y = Y;
            this.size = size;
        }

        public virtual void Draw(Tab sender) { }
    }
}
