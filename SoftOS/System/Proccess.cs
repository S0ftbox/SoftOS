using Cosmos.Core;
using Cosmos.System;
using SoftOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public class Proccess
    {
        public virtual void Run() 
        {

        }

        public virtual void Start()
        {
            uint j = GCImplementation.GetUsedRAM();
            usageRAM = (GCImplementation.GetUsedRAM() - j) / 1024;
            GUI.Update();
            j = GCImplementation.GetUsedRAM();
            usageRAM += (GCImplementation.GetUsedRAM() - j) / 1024;
        }

        public string name;
        public uint usageRAM;
        public bool isFocused;
        public WindowData windowData = new WindowData();
    }

    public class WindowData
    {
        public Rectangle winPos = new Rectangle { X = 100, Y = 100, Height = 100, Width = 100 };
        public bool movable = true;
    }
}
