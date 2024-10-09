using Cosmos.System;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class OpenLoadFile : Proccess
    {
        public string path = "a";
        public List<string> input { get; set; } = new List<string> { "" };
        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x, y + Window.topSize, sizeX, Window.topSize);
            GUI.mainCanvas.DrawString("Enter the file destination path:", GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + 20);
            GUI.mainCanvas.DrawString(path, GUI.defaultFont, GUI.activeTheme.Item1, x + 5, y + 36);
            if (KeyboardManager.KeyAvailable)
            {
                var key = KeyboardManager.ReadKey();
                WriteName(key, path);
            }
        }

        void WriteName(KeyEvent key, string path)
        {
            switch (key.Key)
            {
                case ConsoleKeyEx.Enter:
                    using (var streamWriter = new StreamWriter(path))
                    {
                        foreach (string line in input)
                        {
                            streamWriter.WriteLine(line);
                        }
                    }
                    path = "";
                    break;
                case ConsoleKeyEx.Backspace:
                    if (path.Length > 0)
                    {
                        path = path.Substring(0, path.Length - 1);
                    }
                    break;
                default:
                    if (char.IsLetterOrDigit(key.KeyChar) || char.IsPunctuation(key.KeyChar))
                    {
                        path += key.KeyChar;
                    }
                    break;
            }
        }
    }
}
