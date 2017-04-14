using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.Browsers;
using EvilBaschdi.Core.DirectoryExtensions;
using EvilBaschdi.Core.DotNetExtensions;
using EvilBaschdi.Core.Security;
using EvilBaschdi.Core.Threading;
using EvilBaschdi.Core.Wpf;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace EvilBaschdi.TestUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private INetworkBrowser _networkBrowser;
        private readonly IFilePath _filePath;
        private readonly ISettings _coreSettings;

        public MainWindow()
        {
            InitializeComponent();
            //Loaded += (s, e) => this.EnableGlassEffect();
            IToast toast = new Toast("");
            IMultiThreadingHelper multiThreadingHelper = new MultiThreadingHelper();
            _filePath = new FilePath(multiThreadingHelper);
            //LoadNetworkBrowserToArrayList();
            //MessageBox.Show(VersionHelper.GetWindowsClientVersion());

            _coreSettings = new CoreSettings(Properties.Settings.Default);
            IThemeManagerHelper themeManagerHelper = new ThemeManagerHelper();
            IMetroStyle style = new MetroStyle(this, _coreSettings, themeManagerHelper);
            IFlyout flyout = new CustomFlyout(this, style, Assembly.GetExecutingAssembly().GetLinkerTime());
            style.Load(true);
            flyout.Run();


            var filePath = Assembly.GetEntryAssembly().Location;
            if (filePath != null)
            {
                TestTaskbarIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
            }
            var contextMenu = new ContextMenu();

            foreach (string accentItem in style.Accent.Items)
            {
                var menuItem = new MenuItem();
                menuItem.Header = accentItem;
                menuItem.Click += MenuItem_Click;
                contextMenu.Items.Add(menuItem);
            }

            TestTaskbarIcon.ContextMenu = contextMenu;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("bla");
        }

        private void LoadNetworkBrowserToArrayList()
        {
            _networkBrowser = new NetworkBrowser();
            UpdateCombo(cboNetwork, _networkBrowser.GetNetworkComputers);
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            txtEncrypted.Text = Encryption.EncryptString(txtInput.Text, "abc");
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = Encryption.DecryptString(txtEncrypted.Text, "abc");
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == txtOutput.Text)
            {
                txtInput.Background = Brushes.GreenYellow;
                txtOutput.Background = Brushes.GreenYellow;
            }
            else
            {
                txtInput.Background = Brushes.DarkRed;
                txtOutput.Background = Brushes.DarkRed;
            }

            //var currentDirectory = Directory.GetCurrentDirectory();
            //var configuration = currentDirectory.EndsWith("Release") ? "Release" : "Debug";
            //var root = currentDirectory.Replace($@"EvilBaschdi.Core\TestUI\bin\{configuration}", "");
            //var coreProject = new CoreProject();
            //var coreNuGetPackagesConfig = currentDirectory.Replace($@"TestUI\bin\{configuration}", @"EvilBaschdi.Core\packages.config");
            //var includeList = new List<string>
            //                  {
            //                      "csproj"
            //                  };
            //var childProjects = _filePath.GetFileList(root, includeList, null).Where(file => !file.ToLower().Contains("evilbaschdi.core"));
            //var childConfigs = new ConcurrentBag<string>();

            #region cs project

            //Parallel.ForEach(childProjects,
            //    childProject =>
            //    {
            //        try
            //        {
            //            var targetProject = new Project(childProject);

            //            var isCoreIncluded = targetProject.Items.Any(item => item.EvaluatedInclude == "EvilBaschdi.Core");
            //            var isMahAppsVersionDifferent =
            //                targetProject.Items.Any(item => item.EvaluatedInclude.StartsWith("MahApps.Metro,") && item.EvaluatedInclude != coreProject.MahApps.Key);
            //            if (isCoreIncluded && isMahAppsVersionDifferent)
            //            {
            //                foreach (var item in targetProject.Items)
            //                {
            //                    if (item.EvaluatedInclude.StartsWith("MahApps.Metro"))
            //                    {
            //                        item.Rename(coreProject.MahApps.Key);
            //                        item.RemoveMetadata("HintPath");
            //                        item.SetMetadataValue("HintPath", coreProject.MahApps.Value);
            //                    }
            //                    if (item.EvaluatedInclude.StartsWith("System.Windows.Interactivity"))
            //                    {
            //                        item.Rename(coreProject.SysWinInt.Key);
            //                        item.RemoveMetadata("HintPath");
            //                        item.SetMetadataValue("HintPath", coreProject.SysWinInt.Value);
            //                    }
            //                }
            //                targetProject.Save();
            //                var currentPath = Path.GetDirectoryName(childProject);
            //                var configPath = $@"{currentPath}\packages.config";
            //                if (File.Exists(configPath))
            //                {
            //                    childConfigs.Add(configPath);
            //                }
            //            }
            //        }
            //        catch (Exception exception)
            //        {
            //            MessageBox.Show(exception.Message);
            //        }
            //    });

            #endregion cs project

            #region nuget

            //var mahAppsId = "MahApps.Metro";
            //var corePackageConfig = new PackageConfig();
            //var coreCollection = corePackageConfig.Read(coreNuGetPackagesConfig);
            //var coreMahAppsVersion = corePackageConfig.Version(mahAppsId, coreCollection);
            //var coreMahAppsTargetFramework = corePackageConfig.TargetFramework(mahAppsId, coreCollection);
            //Parallel.ForEach(childConfigs,
            //    childConfig =>
            //    {
            //        var targetPackageConfig = new PackageConfig();
            //        var targetCollection = targetPackageConfig.Read(childConfig);
            //        var targetMahAppsVersion = targetPackageConfig.Version(mahAppsId, targetCollection);
            //        if (!string.IsNullOrWhiteSpace(targetMahAppsVersion) && targetMahAppsVersion != coreMahAppsVersion)
            //        {
            //            targetPackageConfig.SetVersion(mahAppsId, coreMahAppsVersion, targetCollection);
            //            targetPackageConfig.Write(childConfig, targetCollection);
            //        }
            //    });

            #endregion nuget
        }


        public void UpdateCombo(ComboBox comboBox, ArrayList arrayList)
        {
            comboBox.Items.Clear();
            foreach (string value in arrayList)
            {
                comboBox.Items.Add(value);
            }
            comboBox.SelectedIndex = 0;
        }

        private void CustomColorOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CustomColor.Text))
            {
                try
                {
                    var themeManagerHelper = new ThemeManagerHelper();
                    themeManagerHelper.CreateAppStyleBy(CustomColor.Text.ToColor(), CustomColor.Text);
                    var styleAccent = ThemeManager.GetAccent(CustomColor.Text);
                    var styleTheme = ThemeManager.GetAppTheme(_coreSettings.Theme);
                    ThemeManager.ChangeAppStyle(Application.Current, styleAccent, styleTheme);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        #region Flyout

        private void ToggleSettingsFlyoutClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index, bool stayOpen = false)
        {
            var activeFlyout = (Flyout) Flyouts.Items[index];
            if (activeFlyout == null)
            {
                return;
            }

            foreach (var nonactiveFlyout in Flyouts.Items.Cast<Flyout>().Where(nonactiveFlyout => nonactiveFlyout.IsOpen && nonactiveFlyout.Name != activeFlyout.Name))
            {
                nonactiveFlyout.IsOpen = false;
            }

            if (activeFlyout.IsOpen && stayOpen)
            {
                activeFlyout.IsOpen = true;
            }
            else
            {
                activeFlyout.IsOpen = !activeFlyout.IsOpen;
            }
        }

        #endregion Flyout
    }
}