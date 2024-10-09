using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public static class WriteMessage
    {
        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
        }

        public static void WriteWarning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARNING] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(warning);
        }

        public static void WriteInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(info);
        }
    }
}
