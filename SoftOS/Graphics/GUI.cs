using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SoftOS.Apps;
using SoftOS.System;
using System;
using Sys = System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio.IO;
using Cosmos.System.Audio;
using SoftOS.Resources;

namespace SoftOS.Graphics
{
    public static class GUI
    {
        public static int screenSizeX = 1920, screenSizeY = 1080;
        public static SVGAIICanvas mainCanvas;
        public static Cosmos.System.Graphics.Bitmap wallpaper, cursor, cursorBtn, cursorReg, cursorCross;
        public static Colors colors = new Colors();
        public static bool clicked, rClicked, startMenuOpened, settingsInitialized;
        public static Proccess currentProcess;
        public static int mX, mY, mOffset = 0;
        static int oldX, oldY;
        public static Tuple<Color, Color, Color> activeTheme;
        public static PCScreenFont defaultFont = PCScreenFont.Default;
        public static string text;

        public static void Update()
        {
            if (!settingsInitialized)
            {
                InitializeSettings();
            }
            mX = (int)MouseManager.X;
            mY = (int)MouseManager.Y;
            mainCanvas.DrawImage(wallpaper, 0, 0);
            Move();
            Close();
            ToggleStartMenu();
            StartMenuBtns();
            ProcessManager.Update();
            FillDesktop();
            mainCanvas.DrawImageAlpha(cursor, mX - mOffset, mY - mOffset);
            if(MouseManager.MouseState == MouseState.Left)
            {
                clicked = true;
            }
            else if(MouseManager.MouseState == MouseState.None && clicked)
            {
                clicked = false;
                currentProcess = null;
            }
            if (MouseManager.MouseState == MouseState.Right)
            {
                rClicked = true;
            }
            else if (MouseManager.MouseState == MouseState.None && rClicked)
            {
                rClicked = false;
                currentProcess = null;
            }
            mainCanvas.Display();
        }

        public static void InitializeSettings()
        {
            string settingsFile = "";
            int selectedWallpaper = 0, selectedCursor = 0, selectedTheme = 0;
            if (File.Exists(Kernel.settingsFilePath))
            {
                using (var streamReader = new StreamReader(Kernel.settingsFilePath))
                {
                    settingsFile = streamReader.ReadLine();
                }
            }
            else
            {
                settingsFile = "Wallpaper: 0 Cursor: 0 Theme: 0";
            }
            string[] parameters = settingsFile.Split(' ');
            selectedWallpaper = int.Parse(parameters[1]);
            selectedCursor = int.Parse(parameters[3]);
            selectedTheme = int.Parse(parameters[5]);
            wallpaper = Settings.wallpapers[selectedWallpaper];
            activeTheme = Colors.theme[selectedTheme];
            cursorReg = Settings.cursors[selectedCursor * 3];
            cursorBtn = Settings.cursors[(selectedCursor * 3) + 1];
            cursorCross = Settings.cursors[(selectedCursor * 3) + 2];
            Settings.selectedWallpaper = selectedWallpaper;
            Settings.selectedTheme = selectedTheme;
            cursor = cursorReg;

            
            settingsInitialized = true;

        }

        public static void Move()
        {
            if(currentProcess != null) 
            {
                currentProcess.windowData.winPos.X = (int)MouseManager.X - oldX;
                currentProcess.windowData.winPos.Y = (int)MouseManager.Y - oldY;
            }
            else if (MouseManager.MouseState == MouseState.Left && !clicked)
            {
                foreach(var proc in ProcessManager.processList)
                {
                    proc.isFocused = false;
                    if (!proc.windowData.movable)
                    {
                        continue;
                    }
                    if(mX > proc.windowData.winPos.X && mX < proc.windowData.winPos.X + proc.windowData.winPos.Width - Window.topSize)
                    {
                        if (mY > proc.windowData.winPos.Y && mY < proc.windowData.winPos.Y + Window.topSize)
                        {
                            cursor = cursorBtn;
                            currentProcess = proc;
                            proc.isFocused = true;
                            oldX = mX - proc.windowData.winPos.X;
                            oldY = mY - proc.windowData.winPos.Y;
                        }
                    }
                    cursor = cursorReg;
                }
            }
        }

        public static void Close()
        {
            foreach (var proc in ProcessManager.processList)
            {
                if (mX > proc.windowData.winPos.X + proc.windowData.winPos.Width - Window.topSize / 2 - Window.topSize / 4 && mX < proc.windowData.winPos.X + proc.windowData.winPos.Width - Window.topSize / 2 + Window.topSize / 4 && mY > proc.windowData.winPos.Y + Window.topSize / 2 - Window.topSize / 4 && mY < proc.windowData.winPos.Y + Window.topSize / 2 + Window.topSize / 4)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        currentProcess = proc;
                        proc.isFocused = true;
                        ProcessManager.Stop(currentProcess);
                        currentProcess = null;
                    }
                }
                else
                {
                    cursor = cursorReg;
                }
            }
        }

        public static void ToggleStartMenu()
        {
            bool selected = false;
            if (mX > 5 && mX < 75 && mY > 1005 && mY < 1075)
            {
                cursor = cursorBtn;
                if (MouseManager.MouseState == MouseState.Left && !clicked && !selected)
                {
                    if (!startMenuOpened)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new StartMenu { windowData = new WindowData { movable = false, winPos = new Rectangle(0, 600, 250, 400) }, name = "MenuStart" });
                    }
                    else
                    {
                        //startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                startMenuOpened = false;
                                ProcessManager.Stop(p);
                                return;
                            }
                        }
                    }
                    startMenuOpened = !startMenuOpened;
                    selected = true;
                }
            }
            else
            {
                cursor = cursorReg;
            }

            
        }

        public static void StartMenuBtns()
        {
            bool selected = false;
            if (startMenuOpened)
            {
                if (mX > 5 && mX < 75 && mY > 925 && mY < 995)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        Sys.Console.Beep(1046, 150);
                        Power.Shutdown();
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                return;
                            }
                        }
                    }
                }
                else if (mX > 85 && mX < 155 && mY > 925 && mY < 995)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        Power.Reboot();
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                return;
                            }
                        }
                    }
                }
                else if (mX > 165 && mX < 235 && mY > 925 && mY < 995)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "Settings")
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new Apps.Settings
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                            name = "Settings",
                            isFocused = true
                        });
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else if (mX > 5 && mX < 235 && mY > 855 && mY < 915)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name.StartsWith("File Explorer"))
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new FileExplorer
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                            name = "File Explorer",
                            isFocused = true
                        });
                        startMenuOpened = false;
                        FileExplorer.currentPath = Kernel.Path;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else if (mX > 5 && mX < 235 && mY > 785 && mY < 845)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "Brush")
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new Apps.Brush
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                            name = "Brush",
                            isFocused = true
                        });
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else if (mX > 5 && mX < 235 && mY > 715 && mY < 755)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "Notepad")
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new Notepad
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                            name = "Notepad",
                            isFocused = true
                        });
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else if (mX > 5 && mX < 235 && mY > 645 && mY < 685)
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "Calculator")
                            {
                                return;
                            }
                        }
                        ProcessManager.Start(new Calculator
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 265, 400) },
                            name = "Calculator",
                            isFocused = true
                        });
                        startMenuOpened = false;
                        foreach (var p in ProcessManager.processList)
                        {
                            if (p.name == "MenuStart")
                            {
                                ProcessManager.Stop(p);
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    cursor = cursorReg;
                }
                selected = true;
            }
        }

        public static void FillDesktop()
        {
            string path = @"0:\SystemUsers\DefaultUser\Desktop";
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            List<string> newDirPaths = new List<string>();
            List<string> newFilePaths = new List<string>();
            for (int i = 0; i < directories.Length; i++)
            {
                mainCanvas.DrawFilledRectangle(colors.yellowColor, 15 + (i * 65), 15, 60, 60);
                newDirPaths.Add(path + @"\" + directories[i]);
                if (mX > 15 + (i * 65) && mX < 75 + (i * 65) && mY > (Window.topSize * 2) && mY < 60 + (2 * Window.topSize))
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        ProcessManager.Start(new FileExplorer
                        {
                            windowData = new WindowData { winPos = new Rectangle(700, 500, 400, 300) },
                            name = "File Explorer",
                            isFocused = true
                        });
                        FileExplorer.currentPath = newDirPaths[i];
                    }
                    if(MouseManager.MouseState == MouseState.Right && !rClicked)
                    {
                        Directory.Delete(newDirPaths[i], true);
                    }
                }
                else
                {
                    cursor = cursorReg;
                }
            }
             
            for (int i = 0; i < files.Length; i++)
            {
                mainCanvas.DrawFilledRectangle(colors.whiteColor, 15 + (i * 65), 80 + (Window.topSize * 2), 60, 60);
                newFilePaths.Add(path + @"\" + files[i]);
                if (mX > 15 + (i * 65) && mX < 75 + (i * 65) && mY > 80 + (Window.topSize * 2) && mY < 140 + (2 * Window.topSize))
                {
                    cursor = cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !clicked)
                    {
                        if (files[i].EndsWith(".txt"))
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
                    }
                }
                else
                {
                    cursor = cursorReg;
                }
            }
        }

        public static void StartGUI()
        {
            mainCanvas = new SVGAIICanvas(new Mode((uint)screenSizeX, (uint)screenSizeY, ColorDepth.ColorDepth32));
            MouseManager.ScreenWidth = (uint)screenSizeX;
            MouseManager.ScreenHeight = (uint)screenSizeY;
            MouseManager.X = (uint)screenSizeX / 2;
            MouseManager.Y = (uint)screenSizeY / 2;
            ProcessManager.Start(new ProcessBar { windowData = new WindowData { movable = false, winPos = new Rectangle(0, 1000, 1920, 80) }, name = "ProcessBar" });
            ProcessManager.Start(new ProcInfo { windowData = new WindowData { movable = true, winPos = new Rectangle(700, 500, 400, 300) }, name = "ProcInfo" });
            //ProcessManager.Start(new MessageBox { windowData = new WindowData { winPos = new Rectangle(100, 100, 300, 200) }, name = "Window" });
            //ProcessManager.Start(new MessageBox { windowData = new WindowData { winPos = new Rectangle(100, 450, 300, 200) }, name = "Test" });
        }
    }
}
