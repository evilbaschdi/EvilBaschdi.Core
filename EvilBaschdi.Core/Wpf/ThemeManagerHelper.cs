using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using EvilBaschdi.Core.DotNetExtensions;
using MahApps.Metro;
using Microsoft.Win32;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     ThemeManagerHelper class.
    /// </summary>
    public class ThemeManagerHelper
    {
        /// <summary>
        ///     Creates a new app style by color and name.
        /// </summary>
        /// <param name="color">Color to create app style for.</param>
        /// <param name="accentName">Name of the new app style.</param>
        public void CreateAppStyleBy(Color color, string accentName)
        {
            // create a runtime accent resource dictionary
            var resourceDictionary = new ResourceDictionary
                                     {
                                         { "HighlightColor", Color.FromArgb(255, color.R.Subtract(30), color.G.Subtract(30), color.B.Subtract(30)) },
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

            //resourceDictionary.Add("IdealForegroundColor", Colors.White);
            resourceDictionary.Add("IdealForegroundColor", (int) Math.Sqrt(color.R*color.R*.241 + color.G*color.G*.691 + color.B*color.B*.068) < 130 ? Colors.White : Colors.Black);
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

        /// <summary>
        ///     Gets Color of current (applied) system settings, generates an app style and adds it to available accents.
        /// </summary>
        public void RegisterSystemColorTheme()
        {
            var dwm = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\DWM");
            var thememanager = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ThemeManager");
            if (dwm != null && thememanager != null)
            {
                var colorizationColor = dwm.GetValue("ColorizationColor") != null ? ((int) dwm.GetValue("ColorizationColor")).ToString("X") : string.Empty;
                var colorPrevalence = dwm.GetValue("ColorPrevalence") != null && dwm.GetValue("ColorPrevalence").ToString().Equals("1");
                var themeActive = thememanager.GetValue("ThemeActive") != null && thememanager.GetValue("ThemeActive").ToString().Equals("1");

                var accentColor = SystemColors.ActiveCaptionColor;

                if (themeActive && !string.IsNullOrWhiteSpace(colorizationColor))
                {
                    accentColor = colorizationColor.ToColor();
                }

                if (VersionHelper.IsWindows10 && !colorPrevalence)
                {
                    accentColor = "#FFCCCCCC".ToColor();
                }

                CreateAppStyleBy(accentColor, "Accent from windows");
            }
        }
    }
}