using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using SoftOS.Commands;
using Cosmos.System.FileSystem;
using SoftOS.Graphics;

namespace SoftOS
{
    public class Kernel : Sys.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;
        public static GUI gui;

        protected override void BeforeRun()
        {
            this.vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
            this.commandManager = new CommandManager();
            Console.WriteLine("Soft OS ver. 0.1");
        }

        protected override void Run()
        {
            if(Kernel.gui != null)
            {
                Kernel.gui.HandleGIUInput();
                return;
            }

            string response;
            response = this.commandManager.ProcessInput(Console.ReadLine());
            Console.WriteLine(response);
        }
    }
}
