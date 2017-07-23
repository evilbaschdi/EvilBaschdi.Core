using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Wpf.View
{
    /// <summary>
    ///     Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => this.EnableGlassEffect();
        }
    }
}