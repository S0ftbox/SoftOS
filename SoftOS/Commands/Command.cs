using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Commands
{
    public class Command
    {
        public readonly string name;

        public Command(string name) { this.name = name; }

        public virtual String Execute(string[] args) { return ""; }
    }
}
