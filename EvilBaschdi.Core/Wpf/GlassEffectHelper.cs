using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     this.Loaded += (s, e) => GlassEffectHelper.EnableGlassEffect(this);
    /// </summary>
    public static class GlassEffectHelper
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();

        public static bool EnableGlassEffect(this Window window)
        {
            window.MouseLeftButtonDown += (s, e) => window.DragMove();
            return EnableGlassEffect(window, true);
        }

        public static bool EnableGlassEffect(Window window, bool enabled)
        {
            return EnableGlassEffect(window, enabled, new Thickness(-1));
        }

        public static bool EnableGlassEffect(Window window, bool enabled, Thickness margin)
        {
            if (!VersionHelper.IsAtLeastVista)
            {
                // Go and buy Windows 7 ;-)
                return false;
            }

            if (!DwmIsCompositionEnabled())
            {
                return false;
            }

            if (enabled)
            {
                var hwnd = new WindowInteropHelper(window).Handle;

                // Hintergrundfarbe von Fenster Transparent darstellen
                window.Background = Brushes.Transparent;

                // Die Farbe festlegen die den Glaseffekt bekommt
                var hwndSource = HwndSource.FromHwnd(hwnd);
                if (hwndSource?.CompositionTarget != null)
                {
                    hwndSource.CompositionTarget.BackgroundColor =
                        Colors.Transparent;
                }

                // Den Bereich für den Glaseffekt definieren
                var margins = new Margins(margin);

                // Glasseffekt aktivieren
                DwmExtendFrameIntoClientArea(hwnd, ref margins);
            }
            else
            {
                // Hintergrundfarbe des Fensters zurück auf die
                // Systemfarbe stellen
                window.Background = SystemColors.WindowBrush;
            }

            return true;
        }
    }

    internal struct Margins
    {
        public int Bottom;
        public int Left;
        public int Right;
        public int Top;

        public Margins(Thickness t)
        {
            Left = (int) t.Left;
            Right = (int) t.Right;
            Top = (int) t.Top;
            Bottom = (int) t.Bottom;
        }
    }

    public class VersionHelper
    {
        /// <summary>
        ///     OS is at least Windows Vista
        /// </summary>
        public static bool IsAtLeastVista => Environment.OSVersion.Version.Major >= 6;

        /// <summary>
        ///     OS is Windows 7 or higher
        /// </summary>
        public static bool IsWindows7OrHigher => Environment.OSVersion.Version.Major == 6 &&
                                                 Environment.OSVersion.Version.Minor >= 1 ||
                                                 Environment.OSVersion.Version.Major > 6;
    }
}