using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.Wpf;
using FlyOutTest.Core;
using MahApps.Metro.Controls;

namespace FlyOutTest
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ISettings coreSettings = new CoreSettings();
            IMetroStyle style = new MetroStyle(this, coreSettings);
            IFlyout flyout = new CustomFlyout(this, style);
            style.Load(true);
            flyout.Load();
        }
    }
}