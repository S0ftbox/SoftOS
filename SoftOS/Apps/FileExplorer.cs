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

namespace SoftOS.Apps
{
    public class FileExplorer : Proccess
    {
        int x, y;
        public static string currentPath;
        string displayPath = currentPath;
        string newName = "";
        bool namePromptVisibility = false;
        public override void Run()
        {
            Window.DrawTop(this);
            x = windowData.winPos.X;
            y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x, y + Window.topSize, sizeX, Window.topSize);
            GUI.mainCanvas.DrawString("Create  Back", GUI.defaultFont, GUI.activeTheme.Item1, x + Window.topSize / 2, y + Window.topSize + Window.topSize / 4);
            ManageFileSystem(currentPath);
            CreateDir(currentPath);
            SetName();
            DirGoBack();
        }

        void SetName()
        {
            if(currentPath.Length > 28)
            {
                displayPath = "..." + currentPath.Substring(currentPath.Length - 25);
            }
            else
            {
                displayPath = currentPath;
            }
            name = "File Explorer - " + displayPath;
        }

        void ManageFileSystem(string path)
        {
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            List<string> newDirPaths = new List<string>();
            List<string> newFilePaths = new List<string>();
            for(int i = 0; i < directories.Length; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.yellowColor, x + 5 + (i * 65), y + 5 + (Window.topSize * 2), 60, 60);
                newDirPaths.Add(path + @"\" + directories[i]);
                if(GUI.mX > x + 5 + (i * 65) && GUI.mX < x + 65 + (i * 65) && GUI.mY > y + (Window.topSize * 2) && GUI.mY < y + 60 + (2 * Window.topSize))
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                    {
                        currentPath = newDirPaths[i];
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
                if (GUI.mX > x + 5 + (i * 65) && GUI.mX < x + 65 + (i * 65) && GUI.mY > y + (Window.topSize * 2) && GUI.mY < y + 60 + (2 * Window.topSize))
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Right && !GUI.rClicked)
                    {
                        if (newDirPaths[i].StartsWith(@"0:\System") || newDirPaths[i].StartsWith(@"0:\\System"))
                        {
                            if (newDirPaths[i].Contains("Desktop") && !newDirPaths[i].EndsWith("Desktop"))
                            {
                                Directory.Delete(newDirPaths[i], true);
                            }
                            else
                            {
                                ProcessManager.Start(new InfoBox
                                {
                                    windowData = new WindowData { winPos = new Rectangle(750, 600, 270, 0) },
                                    isFocused = true,
                                    msgType = 2,
                                    msgText = "Cannot remove this directory!"
                                });
                            }
                        }
                        else
                        {
                            Directory.Delete(newDirPaths[i], true);
                        }
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }
            for (int i = 0; i < files.Length; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5 + (i * 65), y + 70 + (Window.topSize * 2), 60, 60);
                newFilePaths.Add(path + @"\" + files[i]);
                if (GUI.mX > x + 5 + (i * 65) && GUI.mX < x + 65 + (i * 65) && GUI.mY > y + 70 + (Window.topSize * 2) && GUI.mY < y + 130 + (2 * Window.topSize))
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                    {
                        if (files[i].EndsWith(".txt") || files[i].EndsWith(".cfg"))
                        {
                            ProcessManager.Start(new Notepad
                            {
                                windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                                name = "Notepad",
                                isFocused = true,
                                desiredPath = newFilePaths[i],
                                isOpenedFromExplorer = true
                            });
                        }
                        else if (files[i].EndsWith("Notepad.exe"))
                        {
                            ProcessManager.Start(new Notepad { 
                                windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) }, 
                                name = "Notepad", 
                                isFocused = true 
                            });
                        }
                        else if (files[i].EndsWith("Brush.exe"))
                        {
                            ProcessManager.Start(new Apps.Brush { 
                                windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) }, 
                                name = "Brush", 
                                isFocused = true 
                            });
                        }
                        ProcessManager.Stop(this);
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }
        }

        void DirGoBack()
        {
            if (GUI.mX > x + 70 && GUI.mX < x + 120 && GUI.mY > y + Window.topSize && GUI.mY < y + (2 * Window.topSize))
            {
                GUI.cursor = GUI.cursorBtn;
                if(MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                {
                    if (currentPath != @"0:\")
                    {
                        string tmpPath = currentPath.Substring(0, currentPath.Length - 1);
                        currentPath = tmpPath.Substring(0, tmpPath.LastIndexOf(@"\"));
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                GUI.cursor = GUI.cursorReg;
            }
        }

        void CreateDir(string path)
        {
            if(GUI.mX > x + 7 && GUI.mX < x + 63 && GUI.mY > y + Window.topSize && GUI.mY < y + (2 * Window.topSize))
            {
                GUI.cursor = GUI.cursorBtn;
                if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                {
                    namePromptVisibility = true;
                }
            }
            else
            {
                GUI.cursor = GUI.cursorReg;
            }

            if (namePromptVisibility)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item3, x + 20, y + 60, 200, 60);
                GUI.mainCanvas.DrawString("Enter new dir/file name:", GUI.defaultFont, GUI.activeTheme.Item1, x + 25, y + 64);
                GUI.mainCanvas.DrawString(newName, GUI.defaultFont, GUI.activeTheme.Item1, x + 25, y + 76);
                if (KeyboardManager.KeyAvailable)
                {
                    var key = KeyboardManager.ReadKey();
                    WriteName(key);
                }
            }
        }

        public void WriteName(KeyEvent key)
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
                        if (newName.EndsWith(".txt"))
                        {
                            File.Create(currentPath + @"\" + newName);
                        }
                        else
                        {
                            Directory.CreateDirectory(currentPath + @"\" + newName);
                        }
                        namePromptVisibility = false;
                        newName = "";
                        break;
                    case ConsoleKeyEx.Backspace:
                        if (newName.Length > 0)
                        {
                            newName = newName.Substring(0, newName.Length - 1);
                        }
                        break;
                    default:
                        if (char.IsLetterOrDigit(key.KeyChar) || key.KeyChar == '.')
                        {
                            newName += key.KeyChar;
                        }
                        break;
                }
            }
        }
    }
}
