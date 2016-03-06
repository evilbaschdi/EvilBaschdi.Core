using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using EvilBaschdi.Core.DotNetExtensions;
using MahApps.Metro;
using Microsoft.Win32;

namespace EvilBaschdi.Core.Wpf
{
    public class ThemeManagerHelper
    {
        public void CreateAppStyleBy(Color color, string accentName)
        {
            // create a runtime accent resource dictionary

            var resourceDictionary = new ResourceDictionary
                                     {
                                         { "HighlightColor", Color.FromArgb(255, color.R.Subtract(10), color.G.Subtract(10), color.B.Subtract(10)) },
                                         //{ "AccentColor", Color.FromArgb(204, color.R, color.G, color.B) },
                                         { "AccentColor", Color.FromArgb(255, color.R, color.G, color.B) },
                                         { "AccentColor2", Color.FromArgb(153, color.R, color.G, color.B) },
                                         { "AccentColor3", Color.FromArgb(102, color.R, color.G, color.B) },
                                         { "AccentColor4", Color.FromArgb(51, color.R, color.G, color.B) }
                                     };


            resourceDictionary.Add("HighlightBrush", new SolidColorBrush((Color) resourceDictionary["HighlightColor"]));
            resourceDictionary.Add("AccentColorBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("AccentColorBrush2", new SolidColorBrush((Color) resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("AccentColorBrush3", new SolidColorBrush((Color) resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("AccentColorBrush4", new SolidColorBrush((Color) resourceDictionary["AccentColor4"]));
            resourceDictionary.Add("WindowTitleColorBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));

            resourceDictionary.Add("ProgressBrush", new LinearGradientBrush(
                new GradientStopCollection(new[]
                                           {
                                               new GradientStop((Color) resourceDictionary["HighlightColor"], 0),
                                               new GradientStop((Color) resourceDictionary["AccentColor3"], 1)
                                           }), new Point(1.002, 0.5), new Point(0.001, 0.5)));

            resourceDictionary.Add("CheckmarkFill", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("RightArrowFill", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));

            resourceDictionary.Add("IdealForegroundColor", Colors.White);
            resourceDictionary.Add("IdealForegroundColorBrush", new SolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("IdealForegroundDisabledBrush", new SolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("AccentSelectedColorBrush", new SolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));

            // DataGrid brushes since latest alpha after 1.1.2
            resourceDictionary.Add("MetroDataGrid.HighlightBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.HighlightTextBrush", new SolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("MetroDataGrid.MouseOverHighlightBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("MetroDataGrid.FocusBorderBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightBrush", new SolidColorBrush((Color) resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightTextBrush", new SolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));

            // applying theme to MahApps
            var resDictName = $"ApplicationAccent_{accentName}.xaml";
            var fileName = Path.Combine(Path.GetTempPath(), resDictName);
            using (var writer = System.Xml.XmlWriter.Create(fileName, new System.Xml.XmlWriterSettings
                                                                      {
                                                                          Indent = true
                                                                      }))
            {
                System.Windows.Markup.XamlWriter.Save(resourceDictionary, writer);
                writer.Close();
            }

            resourceDictionary = new ResourceDictionary
                                 {
                                     Source = new Uri(fileName, UriKind.Absolute)
                                 };


            var newAccent = new Accent
                            {
                                Name = accentName,
                                Resources = resourceDictionary
                            };
            ThemeManager.AddAccent(newAccent.Name, newAccent.Resources.Source);
        }


        public void RegisterSystemColorTheme()
        {
            var dwm = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\DWM");
            var thememanager = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ThemeManager");
            if (dwm != null && thememanager != null)
            {
                var colorizationColor = ((int) dwm.GetValue("ColorizationColor")).ToString("X");
                var colorPrevalence = dwm.GetValue("ColorPrevalence").ToString();
                var themeActive = thememanager.GetValue("ThemeActive").ToString().Equals("1");

                var useColor = !(!string.IsNullOrWhiteSpace(colorPrevalence) && colorPrevalence == "0");
                var accentColor = SystemColors.ActiveCaptionColor;

                if (useColor && themeActive && colorizationColor != null)
                {
                    accentColor = colorizationColor.ToColor();
                }
                else
                {
                    if (IsWindows10())
                    {
                        accentColor = "#FFCCCCCC".ToColor();
                    }
                }

                CreateAppStyleBy(accentColor, "Accent from windows");
            }
        }

        private bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            var productName = reg.GetValue("ProductName").ToString();

            return productName.StartsWith("Windows 10");
        }
    }
}