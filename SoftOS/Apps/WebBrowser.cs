using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.Core;
using SoftOS.Graphics;
using System.Net.Sockets;

namespace SoftOS.Apps
{
    internal class WebBrowser : Proccess
    {
        
        public override void Run()
        {
            string ip = "188.184.67.127";
            string request = "GET / HTTP/1.1\r\nHost: info.cern.ch\r\nConnection: Close\r\n\r\n";

            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int i = 0;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);


            Address webServer = new Address(188,184,67,127);
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(webServer);
        }

    }
}
