using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EvilBaschdi.Core.Browsers;
using EvilBaschdi.Core.DirectoryExtensions;
using EvilBaschdi.Core.Security;
using EvilBaschdi.Core.Wpf;

namespace EvilBaschdi.TestUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private INetworkBrowser _networkBrowser;
        private IFilePath _filePath;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => this.EnableGlassEffect();
            _filePath = new FilePath();
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
            if(txtInput.Text == txtOutput.Text)
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

            //var config = currentDirectory.Replace(@"TestUI\bin\" + configuration, @"EvilBaschdi.Core\packages.config");
            //var root = currentDirectory.Replace(@"EvilBaschdi.Core\TestUI\bin\" + configuration, "");

            //_filePath.GetFileList(root);

            //if(File.Exists(config))
            //{
            //    MessageBox.Show(config);
            //}
            //if(Directory.Exists(root))
            //{
            //    MessageBox.Show(root);
            //}


            //var project = new Project(current);
            //var isCoreIncluded = project.Items.Any(item => item.EvaluatedInclude == "EvilBaschdi.Core");
            //var mahAppsInclude = "";
            //var mahAppsHintPath = "";
            //var sysWinIntInclude = "";
            //var sysWinIntHintPath = "";

            //if (isCoreIncluded)
            //{
            //    foreach (var item in project.Items)
            //    {
            //        if (item.EvaluatedInclude.StartsWith("MahApps.Metro"))
            //        {
            //            mahAppsInclude = item.EvaluatedInclude;
            //            mahAppsHintPath = item.Metadata.First().EvaluatedValue;
            //            item.Rename("test"); //name from EvilBaschdi.Core Project
            //            item.RemoveMetadata("HintPath");
            //            //item.SetMetadataValue("test1", "test2");
            //        }
            //        if (item.EvaluatedInclude.StartsWith("System.Windows.Interactivity"))
            //        {
            //            sysWinIntInclude = item.EvaluatedInclude;
            //            sysWinIntHintPath = item.Metadata.First().EvaluatedValue;
            //        }
            //    }
            //}
            //project.Save();

            //var current2 = @"M:\dev\WinSPCheck\WinSPCheck\packages2.config";
            //var xmlWriterSettings = new XmlWriterSettings
            //{
            //    Indent = true,
            //    OmitXmlDeclaration = false,
            //    Encoding = Encoding.UTF8
            //};
            //var serializer = new XmlSerializer(typeof(PackageCollection));
            //var reader = new StreamReader(config);
            //var package = (PackageCollection) serializer.Deserialize(reader);
            //foreach(var pkg in package.Packages.Where(pkg => pkg.Id == "MahApps.Metro"))
            //{
            //    pkg.Version = "1.2.5.0";
            //}
            //reader.Close();
            //File.Delete(current2);

            //var xmlWriter = XmlWriter.Create(current2, xmlWriterSettings);
            //var ns = new XmlSerializerNamespaces();

            //ns.Add("", "");
            //serializer.Serialize(xmlWriter, package, ns);
        }


        public void UpdateCombo(ComboBox comboBox, ArrayList arrayList)
        {
            comboBox.Items.Clear();
            foreach(string value in arrayList)
            {
                comboBox.Items.Add(value);
            }
            comboBox.SelectedIndex = 0;
        }
    }
}