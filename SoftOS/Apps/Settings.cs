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
    public class Settings : Proccess
    {
        public static List<Cosmos.System.Graphics.Bitmap> wallpapers = new List<Cosmos.System.Graphics.Bitmap>
        {
            new Cosmos.System.Graphics.Bitmap(Resources.Files.defaultWallpaper),
            new Cosmos.System.Graphics.Bitmap(Resources.Files.landscapeWallpaper),
            new Cosmos.System.Graphics.Bitmap(Resources.Files.spaceWallpaper)
        };
        public static List<Cosmos.System.Graphics.Bitmap> cursors = new List<Cosmos.System.Graphics.Bitmap>
        {
            new Cosmos.System.Graphics.Bitmap(Resources.Files.defaultCursorRaw),
            new Cosmos.System.Graphics.Bitmap(Resources.Files.defaultCursorBtn),
            new Cosmos.System.Graphics.Bitmap(Resources.Files.defaultCursorCross)
        };
        public static int selectedWallpaper, selectedCursor = 0, selectedTheme;
        int x, y;
        string settingsText;
        public override void Run()
        {
            Window.DrawTop(this);
            x = windowData.winPos.X;
            y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            for (int i = 0; i < 3; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5 + (i * 65), y + 5 + Window.topSize, 60, 60);
                if(GUI.mX > x + 5 + (i * 65) && GUI.mX < x + 65 + (i * 65) && GUI.mY > y + 5 + Window.topSize && GUI.mY < y + 65 + Window.topSize)
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                    {
                        selectedWallpaper = i;
                        GUI.wallpaper = wallpapers[i];
                        settingsText = "Wallpaper: " + i + " Cursor: " + selectedCursor + " Theme: " + selectedTheme;
                        using (var streamWriter = new StreamWriter(Kernel.settingsFilePath))
                        {
                            streamWriter.WriteLine(settingsText);
                        }
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5 + (i * 65), y + 70 + Window.topSize, 60, 60);
                if (GUI.mX > x + 5 + (i * 65) && GUI.mX < x + 65 + (i * 65) && GUI.mY > y + 70 + Window.topSize && GUI.mY < y + 130 + Window.topSize)
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                    {
                        selectedTheme = i;
                        GUI.activeTheme = Colors.theme[i];
                        settingsText = "Wallpaper: " + selectedWallpaper + " Cursor: " + selectedCursor + " Theme: " + i;
                        using (var streamWriter = new StreamWriter(Kernel.settingsFilePath))
                        {
                            streamWriter.WriteLine(settingsText);
                        }
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }
        }

        void SaveSettings()
        {
            if (GUI.mX > x + 7 && GUI.mX < x + 63 && GUI.mY > y + Window.topSize && GUI.mY < y + (2 * Window.topSize))
            {
                GUI.cursor = GUI.cursorBtn;
                if (MouseManager.MouseState == MouseState.Left && !GUI.clicked)
                {
                    settingsText = "Wallpaper: " + selectedWallpaper + " Cursor: " + selectedCursor + " Theme: " + selectedTheme;
                    File.WriteAllText(Kernel.settingsFilePath, settingsText);
                    ProcessManager.Stop(this);
                }
            }
            else
            {
                GUI.cursor = GUI.cursorReg;
            }
        }
    }
}
