using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EvilBaschdi.Core.Browsers;
using EvilBaschdi.Core.DirectoryExtensions;
using EvilBaschdi.Core.MultiThreading;
using EvilBaschdi.Core.Security;
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
        private readonly IMultiThreadingHelper _multiThreadingHelper;

        public MainWindow()
        {
            InitializeComponent();
            //Loaded += (s, e) => this.EnableGlassEffect();
            _multiThreadingHelper = new MultiThreadingHelper();
            _filePath = new FilePath(_multiThreadingHelper);
            //LoadNetworkBrowserToArrayList();
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
    }
}