using System.Windows;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     Margins
    /// </summary>
    internal struct Margins
    {
        private int _bottom;
        private int _left;
        private int _top;
        private int _right;

        public Margins(Thickness t)
        {
            _left = (int) t.Left;
            _right = (int) t.Right;
            _top = (int) t.Top;
            _bottom = (int) t.Bottom;
        }
    }
}