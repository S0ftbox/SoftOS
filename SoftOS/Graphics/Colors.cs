using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Graphics
{
    public class Colors
    {
        //----------------THEMES------------------------
        //defaultTheme - theme[0]
        public static Color defTextColor = Color.FromArgb(254, 254, 250);
        public static Color defMainColor = Color.FromArgb(30, 35, 41);
        public static Color defDarkColor = Color.FromArgb(25, 28, 33);
        //lightTheme - theme[1]
        public static Color lightTextColor = Color.FromArgb(13, 14, 14);
        public static Color lightMainColor = Color.FromArgb(251, 168, 182);
        public static Color lightDarkColor = Color.FromArgb(229, 160, 215);
        //greenTheme - theme[2]
        public static Color greenTextColor = Color.FromArgb(13, 14, 14);
        public static Color greenMainColor = Color.FromArgb(59, 129, 50);
        public static Color greenDarkColor = Color.FromArgb(39, 98, 33);

        //list of themes
        public static List<Tuple<Color, Color, Color>> theme = new List<Tuple<Color, Color, Color>>
        {
            Tuple.Create(defTextColor, defMainColor, defDarkColor),
            Tuple.Create(lightTextColor, lightMainColor, lightDarkColor),
            Tuple.Create(greenTextColor, greenMainColor, greenDarkColor)
        };

        //brushPalette
        public Color blackColor = Color.FromArgb(13, 14, 14);
        public Color silverColor = Color.FromArgb(206, 206, 203);
        public Color grayColor = Color.FromArgb(136, 136, 137);
        public Color whiteColor = Color.FromArgb(249, 246, 238);
        public Color maroonColor = Color.FromArgb(116, 11, 13);
        public Color redColor = Color.FromArgb(196, 2, 52);
        public Color purpleColor = Color.FromArgb(105, 32, 112);
        public Color fuchsiaColor = Color.FromArgb(215, 55, 118);
        public Color greenColor = Color.FromArgb(0, 159, 107);
        public Color limeColor = Color.FromArgb(150, 193, 45);
        public Color oliveColor = Color.FromArgb(113, 122, 84);
        public Color yellowColor = Color.FromArgb(255, 211, 0);
        public Color navyColor = Color.FromArgb(0, 21, 61);
        public Color blueColor = Color.FromArgb(0, 77, 152);
        public Color tealColor = Color.FromArgb(0, 94, 107);
        public Color cyanColor = Color.FromArgb(0, 197, 194);
    }
}
