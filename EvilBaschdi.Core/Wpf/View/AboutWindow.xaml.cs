using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Wpf.View
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    ///     Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        /// <inheritdoc />
        public AboutWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => this.EnableGlassEffect();
        }
    }
}