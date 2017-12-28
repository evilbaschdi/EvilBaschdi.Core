using System;
using System.Runtime.InteropServices;

namespace EvilBaschdi.Core.Application
{
    public partial class ScreenCapture
    {
        /// <summary>
        ///     Helper class containing User32 API functions
        /// </summary>
        // ReSharper disable once ClassNeverInstantiated.Local
        private class User32
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public readonly int left;
                public readonly int top;
                public readonly int right;
                public readonly int bottom;
            }
        }
    }
}