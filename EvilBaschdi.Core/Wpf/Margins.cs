using System.Windows;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     Margins
    /// </summary>
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
}