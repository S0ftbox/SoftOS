using Cosmos.HAL;
using SoftOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.System
{
    public class ProcessBar : Proccess
    {
        string currentDate, currentTime;
        public override void Run()
        {
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y, sizeX, sizeY);
            CustomDrawing.DrawFullRoundedRectangle(x + 5, y + 5, 70, 70, 5, GUI.colors.whiteColor);
            GUI.mainCanvas.DrawString(currentTime, GUI.defaultFont, GUI.activeTheme.Item1, x + 1840, y + 20);
            GUI.mainCanvas.DrawString(currentDate, GUI.defaultFont, GUI.activeTheme.Item1, x + 1820, y + 40);
            GetTime();
        }

        void GetTime()
        {
            int year = (RTC.Century * 100) + RTC.Year;
            int month = RTC.Month;
            int day = RTC.DayOfTheMonth;
            int hour = RTC.Hour;
            int minute = RTC.Minute;

            currentDate = year + "-" + month.ToString("D2") + "-" + day.ToString("D2");
            currentTime = hour.ToString("D2") + ":" + minute.ToString("D2");
        }
    }
}
