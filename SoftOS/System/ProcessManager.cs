using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public static class ProcessManager
    {
        public static List<Proccess> processList = new List<Proccess>();
        public static Proccess focusedProcess;

        public static void Update()
        {
            foreach (Proccess process in processList)
            {
                process.Run();
            }
        }

        public static void Start(Proccess process)
        {
            processList.Add(process);
            process.Start();

        }

        public static void Stop(Proccess process)
        {
            processList.Remove(process);
            if(focusedProcess == process)
            {
                focusedProcess = null;
            }
        }
    }
}
