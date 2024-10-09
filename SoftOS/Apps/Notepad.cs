using Cosmos.System;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SoftOS.Apps
{
    public class Notepad : Proccess
    {
        public List<string> input { get; set; } = new List<string> { "" };
        private int currentLine = 0;

        public string desiredPath;
        public bool isOpenedFromExplorer, isFileLoaded;
        int x, y, sizeY;

        public override void Run()
        {
            Window.DrawTop(this);
            x = windowData.winPos.X;
            y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x, y + Window.topSize, sizeX, Window.topSize);
            GUI.mainCanvas.DrawString(" Save  Load", GUI.defaultFont, GUI.activeTheme.Item1, x + Window.topSize / 2, y + Window.topSize + Window.topSize / 4);
            for (int i = 0; i < input.Count; i++)
            {
                GUI.mainCanvas.DrawString(input[i], GUI.defaultFont, GUI.activeTheme.Item1, x + 10, y + 70 + (i * 16));
            }
            if (isOpenedFromExplorer && !isFileLoaded)
            {
                LoadFromExplorer(desiredPath);
                isFileLoaded = true;
            }
            SaveFile(desiredPath);
            LoadFile(desiredPath);
            if (KeyboardManager.KeyAvailable)
            {
                var key = KeyboardManager.ReadKey();
                WriteText(key);
            }
        }

        public void WriteText(KeyEvent key)
        {
            if (!isFocused)
            {
                return;
            }

            if (isFocused)
            {
                switch (key.Key)
                {
                    case ConsoleKeyEx.Enter:
                        if (input.Count * 16 < sizeY - Window.topSize)
                        {
                            input.Insert(++currentLine, "");
                        }
                        break;
                    case ConsoleKeyEx.Backspace:
                        if (input[currentLine].Length > 0)
                        {
                            input[currentLine] = input[currentLine].Substring(0, input[currentLine].Length - 1);
                        }
                        else if (input[currentLine].Length == 0)
                        {
                            currentLine--;
                            input.RemoveAt(currentLine + 1);
                        }
                        break;
                    default:
                        if (char.IsLetterOrDigit(key.KeyChar) || char.IsPunctuation(key.KeyChar) || char.IsSymbol(key.KeyChar) || (key.KeyChar == ' '))
                        {
                            input[currentLine] += key.KeyChar;
                        }
                        break;
                }
            }
        }

        /*void WriteName(KeyEvent key, string path)
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
                    namePromptVisibility = false;
                    isSaved = true;
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
        }*/

        void SaveFile(string path)
        {
            if (GUI.mX > x + 7 && GUI.mX < x + 63 && GUI.mY > y + Window.topSize && GUI.mY < y + (2 * Window.topSize))
            {
                GUI.cursor = GUI.cursorBtn;
                if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                {
                    using (var streamWriter = new StreamWriter(path))
                    {
                        foreach (string line in input)
                        {
                            streamWriter.WriteLine(line);
                        }
                    }
                }
            }
            else
            {
                GUI.cursor = GUI.cursorReg;
            }
        }

        void LoadFile(string path)
        {
            if (GUI.mX > x + 70 && GUI.mX < x + 120 && GUI.mY > y + Window.topSize && GUI.mY < y + (2 * Window.topSize))
            {
                GUI.cursor = GUI.cursorBtn;
                if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                {
                    if (File.Exists(path))
                    {
                        input.Clear();
                        using (var streamReader = new StreamReader(path))
                        {
                            string line;
                            while ((line = streamReader.ReadLine()) != null)
                            {
                                input.Add(line);
                            }
                            if(input.Count == 0)
                            {
                                input.Add("");
                            }
                        }
                        currentLine = input.Count - 1;
                    }
                }
            }
            else
            {
                GUI.cursor = GUI.cursorReg;
            }
        }

        void LoadFromExplorer(string path)
        {
            if (File.Exists(path))
            {
                input.Clear();
                using (var streamReader = new StreamReader(path))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        input.Add(line);
                    }
                    if (input.Count == 0)
                    {
                        input.Add("");
                    }
                }
                currentLine = input.Count - 1;
            }
        }
    }
}
