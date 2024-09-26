using Cosmos.Core_Plugs.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftOS.Graphics;

namespace SoftOS.Commands
{
    class LaunchGUI : Command
    {
        public LaunchGUI(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (Kernel.gui != null)
                return "You are already in GUI";

            Kernel.gui = new GUI();

            return "Launched GUI";
        }
    }
}
