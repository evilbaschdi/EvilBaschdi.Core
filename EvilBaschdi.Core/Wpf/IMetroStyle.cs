using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     Interface for classes that handle metro style on wpf.
    /// </summary>
    public interface IMetroStyle
    {
        /// <summary>
        ///     Load.
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
        ///     Save Style.
        /// </summary>
        void SaveStyle();

        /// <summary>
        ///     ComboBox for choosing an accent.
        /// </summary>
        ComboBox Accent { get; set; }

        /// <summary>
        ///     ToggleSwitch for choosing a theme.
        /// </summary>
        ToggleSwitch Theme { get; set; }
    }
}