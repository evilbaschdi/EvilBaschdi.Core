using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace EvilBaschdi.Core.Wpf
{
    /// <inheritdoc />
    /// <summary>
    ///     Custom Flyout. Adds theme, accent and linker time to an existing flyout or adds a new one.
    /// </summary>
    public class CustomFlyout : IFlyout
    {
        private readonly DateTime _linkerTime;
        private readonly MetroWindow _mainWindow;
        private readonly IMetroStyle _style;
        private int _overrideProtection;

        /// <exception cref="ArgumentNullException"><paramref name="mainWindow" /> is <see langword="null" />.</exception>
        public CustomFlyout(MetroWindow mainWindow, IMetroStyle style, DateTime linkerTime)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            if (linkerTime == null)
            {
                throw new ArgumentNullException(nameof(linkerTime));
            }
            _linkerTime = linkerTime;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Run()
        {
            #region settings button

            var settingsButton = new Button
                                 {
                                     Content = new StackPanel
                                               {
                                                   Orientation = Orientation.Horizontal,
                                                   Children =
                                                   {
                                                       new PackIconMaterial
                                                       {
                                                           Width = 20,
                                                           Height = 20,
                                                           Foreground = Brushes.White,
                                                           Kind = PackIconMaterialKind.Settings
                                                       },
                                                       new TextBlock
                                                       {
                                                           Margin = new Thickness(5, 0, 0, 0),
                                                           VerticalAlignment = VerticalAlignment.Center,
                                                           Text = "settings"
                                                       }
                                                   }
                                               }
                                 };


            settingsButton.Click += SettingsButton_Click;

            if (_mainWindow.RightWindowCommands == null)
            {
                var windowCommands = new WindowCommands();
                windowCommands.Items.Add(settingsButton);
                _mainWindow.RightWindowCommands = windowCommands;
            }
            else if (
                !_mainWindow.RightWindowCommands.Items.OfType<Button>()
                            .Any(
                                button =>
                                    button.Name.Equals("SettingsButton", StringComparison.InvariantCultureIgnoreCase) ||
                                    ((StackPanel) button.Content).Children.OfType<TextBlock>()
                                                                 .Any(textBlock => textBlock.Text.Equals("settings", StringComparison.InvariantCultureIgnoreCase))))
            {
                _mainWindow.RightWindowCommands.Items.Add(settingsButton);
            }

            #endregion settings button

            #region flyout

            var flyoutWidth = 275d;

            if (_mainWindow.Flyouts == null)
            {
                var flyoutControl = new FlyoutsControl();


                var settingsFlyout = new Flyout
                                     {
                                         Name = "SettingsFlyout",
                                         Header = "settings",
                                         Width = flyoutWidth,
                                         AnimateOnPositionChange = true,
                                         AnimateOpacity = true,
                                         Position = Position.Right,
                                         Theme = FlyoutTheme.Accent,
                                         Content = MainStackPanel(flyoutWidth, null)
                                     };
                flyoutControl.Items.Add(settingsFlyout);
                _mainWindow.Flyouts = flyoutControl;
            }
            else
            {
                foreach (Flyout flyout in _mainWindow.Flyouts.Items)
                {
                    if (flyout.Name != "SettingsFlyout")
                    {
                        continue;
                    }
                    flyoutWidth = flyout.Width;
                    var content = (StackPanel) flyout.Content;
                    content.Parent.RemoveChild(content);
                    var mainStackPanel = MainStackPanel(flyoutWidth, content);
                    flyout.Content = mainStackPanel;
                }
            }

            #endregion flyout

            _overrideProtection = 1;

            _style.Load(true);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index, bool stayOpen = false)
        {
            var activeFlyout = (Flyout) _mainWindow.Flyouts.Items[index];
            if (activeFlyout == null)
            {
                return;
            }

            foreach (var nonactiveFlyout in _mainWindow.Flyouts.Items.Cast<Flyout>().Where(nonactiveFlyout => nonactiveFlyout.IsOpen && nonactiveFlyout.Name != activeFlyout.Name))
            {
                nonactiveFlyout.IsOpen = false;
            }

            activeFlyout.IsOpen = activeFlyout.IsOpen && stayOpen || !activeFlyout.IsOpen;
        }

        #region MetroStyle

        private void SaveStyleClick(object sender, RoutedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SaveStyle();
        }

        private void ThemeSwitchIsCheckedChanged(object sender, EventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            if (_overrideProtection == 0)
            {
                return;
            }
            if (e is RoutedEventArgs routedEventArgs)
            {
                _style.SetTheme(sender, routedEventArgs);
            }
            else
            {
                _style.SetTheme(sender);
            }
        }

        private void AccentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SetAccent(sender, e);
            var accentColorBrush = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush");
            foreach (Flyout flyout in _mainWindow.Flyouts.Items)
            {
                if (flyout.Name != "SettingsFlyout" || ((StackPanel) flyout.Content).Name != "MainStackPanel")
                {
                    continue;
                }
                var mainStackPanel = (StackPanel) flyout.Content;
                var horizontalLineRectangles = mainStackPanel.FindChildren<Rectangle>().Where(item => item.Name == "HorizontalLineRectangle");
                var themeStackPanel = mainStackPanel.FindChild<StackPanel>("ThemeStackPanel");


                foreach (var horizontalLineRectangle in horizontalLineRectangles)
                {
                    horizontalLineRectangle.Stroke = accentColorBrush;
                    horizontalLineRectangle.Fill = accentColorBrush;
                }


                var themeSwitch = themeStackPanel.FindChild<ToggleSwitch>("ThemeSwitch");
                themeSwitch.OnSwitchBrush = accentColorBrush;
                themeSwitch.OffSwitchBrush = accentColorBrush;
            }
        }

        #endregion MetroStyle

        #region StackPanels

        private StackPanel MainStackPanel(double flyoutWidth, StackPanel stackPanelToMerge)
        {
            if (flyoutWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(flyoutWidth));
            }
            var mainStackPanel = new StackPanel
                                 {
                                     Name = "MainStackPanel"
                                 };

            if (stackPanelToMerge != null)
            {
                mainStackPanel.Children.Add(stackPanelToMerge);
                mainStackPanel.Children.Add(HorizontalLineStackPanel(flyoutWidth));
            }

            mainStackPanel.Children.Add(ThemeStackPanel());
            mainStackPanel.Children.Add(AccentStackPanel(flyoutWidth));
            mainStackPanel.Children.Add(SaveStyleStackPanel(flyoutWidth));
            mainStackPanel.Children.Add(HorizontalLineStackPanel(flyoutWidth));
            mainStackPanel.Children.Add(LinkerTimeStackPanel());

            return mainStackPanel;
        }

        private StackPanel ControlContentStackPanel(ControlContent controlContent)
        {
            if (controlContent == null)
            {
                throw new ArgumentNullException(nameof(controlContent));
            }
            return new StackPanel
                   {
                       Orientation = Orientation.Horizontal,
                       Children =
                       {
                           new Rectangle
                           {
                               Width = controlContent.ImageSize,
                               Height = controlContent.ImageSize,
                               Fill = controlContent.FillBrush,
                               OpacityMask = new VisualBrush
                                             {
                                                 Stretch = Stretch.Fill,
                                                 Visual = (Visual) _mainWindow.FindResource(controlContent.ImageResourceName)
                                             }
                           },
                           new TextBlock
                           {
                               Margin = new Thickness(5, 0, 0, 0),
                               VerticalAlignment = VerticalAlignment.Center,
                               Text = controlContent.Content
                           }
                       }
                   };
        }

        private StackPanel ThemeStackPanel()
        {
            var themeStackPanel = new StackPanel
                                  {
                                      Name = "ThemeStackPanel",
                                      Orientation = Orientation.Horizontal,
                                      Margin = new Thickness(10, 5, 0, 0)
                                  };
            themeStackPanel.Children.Add(new Label
                                         {
                                             Width = 50,
                                             Margin = new Thickness(0, 5, 0, 0),
                                             HorizontalAlignment = HorizontalAlignment.Left,
                                             VerticalAlignment = VerticalAlignment.Top,
                                             Content = "Theme"
                                         });

            var themeSwitch = _style.Theme;
            themeSwitch.Margin = new Thickness(10, 1, 0, 0);
            themeSwitch.Name = "ThemeSwitch";
            themeSwitch.HorizontalAlignment = HorizontalAlignment.Left;
            themeSwitch.VerticalAlignment = VerticalAlignment.Top;
            themeSwitch.FontSize = 12;
            themeSwitch.OnSwitchBrush = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush");
            themeSwitch.OffSwitchBrush = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush");
            themeSwitch.OnLabel = "Dark";
            themeSwitch.OffLabel = "Light";
            //themeSwitch.IsChecked = _style.Theme.IsChecked;
            themeSwitch.IsCheckedChanged += ThemeSwitchIsCheckedChanged;
            themeStackPanel.Children.Add(themeSwitch);
            _style.Theme = themeSwitch;
            return themeStackPanel;
        }

        private StackPanel AccentStackPanel(double flyoutWidth)
        {
            if (flyoutWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(flyoutWidth));
            }
            var accentStackPanel = new StackPanel
                                   {
                                       Orientation = Orientation.Horizontal,
                                       Margin = new Thickness(10, 5, 0, 0)
                                   };
            accentStackPanel.Children.Add(new Label
                                          {
                                              Width = 50,
                                              Margin = new Thickness(0, 5, 0, 0),
                                              HorizontalAlignment = HorizontalAlignment.Left,
                                              VerticalAlignment = VerticalAlignment.Top,
                                              Content = "Accent"
                                          });


            var accent = _style.Accent;
            accent.Name = "Accent";
            accent.Margin = new Thickness(10, 5, 0, 0);
            accent.Width = flyoutWidth - 85;
            accent.HorizontalAlignment = HorizontalAlignment.Left;
            accent.VerticalAlignment = VerticalAlignment.Top;
            accent.SelectionChanged += AccentOnSelectionChanged;
            accentStackPanel.Children.Add(accent);
            _style.Accent = accent;
            return accentStackPanel;
        }

        private StackPanel SaveStyleStackPanel(double flyoutWidth)
        {
            var saveStyleButton = new Button
                                  {
                                      Name = "SaveStyle",
                                      Width = flyoutWidth - 85,
                                      Margin = new Thickness(60, 5, 0, 0),
                                      Content = new StackPanel
                                                {
                                                    Orientation = Orientation.Horizontal,
                                                    Children =
                                                    {
                                                        new PackIconMaterial
                                                        {
                                                            Width = 20,
                                                            Height = 20,
                                                            Foreground = Brushes.Black,
                                                            Kind = PackIconMaterialKind.ContentSaveSettings
                                                        },
                                                        new TextBlock
                                                        {
                                                            Margin = new Thickness(5, 0, 0, 0),
                                                            VerticalAlignment = VerticalAlignment.Center,
                                                            Text = "save style"
                                                        }
                                                    }
                                                }
                                  };
            saveStyleButton.Click += SaveStyleClick;

            var saveStyleStackPanel = new StackPanel
                                      {
                                          Orientation = Orientation.Horizontal,
                                          Margin = new Thickness(10, 5, 0, 0)
                                      };
            saveStyleStackPanel.Children.Add(saveStyleButton);
            return saveStyleStackPanel;
        }

        private StackPanel HorizontalLineStackPanel(double flyoutWidth)
        {
            if (flyoutWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(flyoutWidth));
            }
            var horizontalLineStackPanel = new StackPanel
                                           {
                                               Name = "HorizontalLineStackPanel",
                                               Orientation = Orientation.Vertical,
                                               Height = 1,
                                               Margin = new Thickness(0, 10, 0, 0)
                                           };

            horizontalLineStackPanel.Children.Add(new Rectangle
                                                  {
                                                      Name = "HorizontalLineRectangle",
                                                      AllowDrop = false,
                                                      Width = flyoutWidth - 30,
                                                      Height = 1,
                                                      VerticalAlignment = VerticalAlignment.Center,
                                                      Stroke = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush")
                                                  });
            return horizontalLineStackPanel;
        }

        private StackPanel LinkerTimeStackPanel()
        {
            //< Label Width = "50" Margin = "10,5,0,0" HorizontalAlignment = "Left" VerticalAlignment = "Top" Content = "Build" />
            // < Label Width = "310" Margin = "15,5,0,0" HorizontalAlignment = "Left" VerticalAlignment = "Top" Name = "LinkerTime" />

            var linkerTimeStackPanel = new StackPanel
                                       {
                                           Orientation = Orientation.Horizontal
                                       };
            linkerTimeStackPanel.Children.Add(new Label
                                              {
                                                  Width = 50,
                                                  Margin = new Thickness(10, 5, 0, 0),
                                                  HorizontalAlignment = HorizontalAlignment.Left,
                                                  VerticalAlignment = VerticalAlignment.Top,
                                                  Content = "Build"
                                              });

            linkerTimeStackPanel.Children.Add(new Label
                                              {
                                                  Name = "LinkerTime",
                                                  Width = 310,
                                                  Margin = new Thickness(5, 5, 0, 0),
                                                  HorizontalAlignment = HorizontalAlignment.Left,
                                                  VerticalAlignment = VerticalAlignment.Top,
                                                  Content = _linkerTime.ToString(CultureInfo.InvariantCulture)
                                              });
            return linkerTimeStackPanel;
        }

        #endregion StackPanels
    }
}