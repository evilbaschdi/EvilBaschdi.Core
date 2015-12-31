﻿using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EvilBaschdi.Core.Browsers;
using EvilBaschdi.Core.Security;
using EvilBaschdi.Core.Wpf;

namespace EvilBaschdi.TestUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList _networkList;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => GlassEffectHelper.EnableGlassEffect(this);

            //LoadNetworkBrowserToArrayList();
        }

        private void LoadNetworkBrowserToArrayList()
        {
            _networkList = NetworkBrowser.GetNetworkComputers();
            UpdateCombo(cboNetwork, _networkList);
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