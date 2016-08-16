using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using EvilBaschdi.Core.DotNetExtensions;

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

        /// <summary>
        ///     Enables glass effect.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="window" /> is <see langword="null" />.</exception>
        public static bool EnableGlassEffect(this Window window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            window.MouseLeftButtonDown += (s, e) => window.DragMove();
            return EnableGlassEffect(window, true);
        }

        /// <summary>
        ///     Enables glass effect.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="window" /> is <see langword="null" />.</exception>
        public static bool EnableGlassEffect(Window window, bool enabled)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            return EnableGlassEffect(window, enabled, new Thickness(-1));
        }

        /// <summary>
        ///     Enables glass effect.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="window" /> is <see langword="null" />.</exception>
        public static bool EnableGlassEffect(Window window, bool enabled, Thickness margin)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            if (!VersionHelper.IsVista || !VersionHelper.IsWindows7)
            {
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
}