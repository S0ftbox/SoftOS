using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Commands
{
    class Help : Command
    {
        public Help(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            return "Welcome to Soft OS. This is the help command";
        }
    }
}
