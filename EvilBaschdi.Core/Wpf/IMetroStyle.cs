using System.Windows;
using System.Windows.Controls;

namespace EvilBaschdi.Core.Wpf
{
    public interface IMetroStyle
    {
        /// <summary>
        /// </summary>
        /// <param name="center"></param>
        /// <param name="resizeWithBorder400"></param>
        void Load(bool center = false, bool resizeWithBorder400 = false);

        /// <summary>
        ///     Accent of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetAccent(object sender, SelectionChangedEventArgs e);

        /// <summary>
        ///     Theme of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        void SetTheme(object sender, RoutedEventArgs routedEventArgs);

        /// <summary>
        ///     Theme of application style.
        /// </summary>
        /// <param name="sender"></param>
        void SetTheme(object sender);

        /// <summary>
        /// </summary>
        void SaveStyle();
    }
}