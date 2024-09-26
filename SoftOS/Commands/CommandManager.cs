using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Commands
{
    public class CommandManager
    {
        private List<Command> commands;

        public CommandManager() 
        { 
            this.commands = new List<Command>(1);
            this.commands.Add(new Help("help"));
            this.commands.Add(new File("file"));
            this.commands.Add(new LaunchGUI("gui"));
        }

        public string ProcessInput (string input)
        {
            string[] split = input.Split(' ');

            string label = split[0];

            List<string> args = new List<string>();

            int ctr = 0;
            foreach (string s in split)
            {
                if(ctr != 0)
                    args.Add(s);

                ++ctr;
            }

            foreach(Command cmd in this.commands)
            {
                if(cmd.name == label)
                {
                    return cmd.Execute(args.ToArray());
                }
            }

            return "The command \"" + label + "\" does not exist!";

        }
    }
}
