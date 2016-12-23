using System.Reflection;
using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.Wpf;
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
            ISettings coreSettings = new CoreSettings(Properties.Settings.Default);
            IMetroStyle style = new MetroStyle(this, coreSettings);
            IFlyout flyout = new CustomFlyout(this, style, Assembly.GetExecutingAssembly().GetLinkerTime());
            style.Load(true);
            flyout.Load();
        }
    }
}