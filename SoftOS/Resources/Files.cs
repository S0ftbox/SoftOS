using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Resources
{
    public static class Files
    {
        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Wallpapers.Wallpaper.bmp")] public static byte[] defaultWallpaper;
        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Wallpapers.Wallpaper2.bmp")] public static byte[] landscapeWallpaper;
        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Wallpapers.Wallpaper3.bmp")] public static byte[] spaceWallpaper;

        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Cursors.Cursor.bmp")] public static byte[] defaultCursorRaw;
        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Cursors.CursorBtn.bmp")] public static byte[] defaultCursorBtn;
        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Cursors.CursorCrosshair.bmp")] public static byte[] defaultCursorCross;

        [ManifestResourceStream(ResourceName = "SoftOS.Resources.Audio.test.wav")] public static byte[] testSound;
    }
}
