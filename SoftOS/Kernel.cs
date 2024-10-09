using System;
using Sys = Cosmos.System;
using System.Collections.Generic;
using System.Text;
using SoftOS.System;
using Cosmos.System.FileSystem;
using SoftOS.Graphics;
using Cosmos.Core.Memory;


namespace SoftOS
{
    public class Kernel : Sys.Kernel
    {
        public static string Version = "1.0";
        public static string Path = @"0:\";
        public static string settingsFilePath = @"0:\\SystemUsers\DefaultUser\Settings.cfg";
        public static CosmosVFS fs;
        public static bool runGUI;
        int lastHeapCollect;
        protected override void BeforeRun()
        {
            //Console.SetWindowSize(90, 30);
            //Console.OutputEncoding = Sys.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Boot.OnBoot();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Booting SoftOS " + Version);
            //Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Run()
        {
            if (!runGUI)
            {
                Console.Write(Path + ">");
                var command = Console.ReadLine();
                ConsoleCommands.RunCommand(command);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                GUI.Update();
            }
            if(lastHeapCollect > 20)
            {
                Heap.Collect();
                lastHeapCollect = 0;
            }
            else
            {
                lastHeapCollect++;
            }
        }
    }
}
