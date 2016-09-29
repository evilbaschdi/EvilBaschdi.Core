using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Wpf
{
    public class CustomFlyout : IFlyout
    {
        private readonly MetroWindow _mainWindow;
        private readonly IMetroStyle _style;
        private int _overrideProtection;

        /// <exception cref="ArgumentNullException"><paramref name="mainWindow" /> is <see langword="null" />.</exception>
        public CustomFlyout(MetroWindow mainWindow, IMetroStyle style)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (style == null)
            {
                throw new ArgumentNullException(nameof(style));
            }
            _mainWindow = mainWindow;
            _style = style;
        }

        public void Load()
        {
            var settingsButton = new Button
                                 {
                                     Content = ControlContentStackPanel(new ControlContent
                                                                        {
                                                                            Content = "settings",
                                                                            FillBrush = Brushes.White,
                                                                            ImageSize = 20,
                                                                            ImageResourceName = "appbar_settings"
                                                                        })
                                 };

            settingsButton.Click += SettingsButton_Click;

            if (_mainWindow.RightWindowCommands == null)
            {
                var windowCommands = new WindowCommands();
                windowCommands.Items.Add(settingsButton);
                _mainWindow.RightWindowCommands = windowCommands;
            }
            else
            {
                _mainWindow.RightWindowCommands.Items.Add(settingsButton);
            }

            if (_mainWindow.Flyouts == null)
            {
                var flyoutWidth = 275;
                var flyoutControl = new FlyoutsControl();
                var mainStackPanel = new StackPanel();


                var linkerTimeStackPanel = new StackPanel();

                mainStackPanel.Children.Add(ThemeStackPanel());
                mainStackPanel.Children.Add(AccentStackPanel(flyoutWidth));
                mainStackPanel.Children.Add(SaveStyleStackPanel(flyoutWidth));
                mainStackPanel.Children.Add(HorizontalLineStackPanel(flyoutWidth));
                mainStackPanel.Children.Add(linkerTimeStackPanel);

                var settingsFlyout = new Flyout
                                     {
                                         Name = "SettingsFlyout",
                                         Header = "settings",
                                         Width = flyoutWidth,
                                         AnimateOnPositionChange = true,
                                         AnimateOpacity = true,
                                         Position = Position.Right,
                                         Theme = FlyoutTheme.Accent,
                                         Content = mainStackPanel
                                     };
                flyoutControl.Items.Add(settingsFlyout);
                _mainWindow.Flyouts = flyoutControl;
                _overrideProtection = 1;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
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
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SaveStyle();
        }

        private void ThemeSwitchIsCheckedChanged(object sender, EventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            var routedEventArgs = e as RoutedEventArgs;
            if (routedEventArgs != null)
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
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SetAccent(sender, e);
        }

        #endregion MetroStyle

        private StackPanel ThemeStackPanel()
        {
            var themeStackPanel = new StackPanel
                                  {
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
            var themeSwitch = new ToggleSwitch
                              {
                                  Margin = new Thickness(10, 1, 0, 0),
                                  Name = "ThemeSwitch",
                                  HorizontalAlignment = HorizontalAlignment.Left,
                                  VerticalAlignment = VerticalAlignment.Top,
                                  FontSize = 12,
                                  OnSwitchBrush = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush"),
                                  OffSwitchBrush = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush"),
                                  OnLabel = "Dark",
                                  OffLabel = "Light"
                              };

            themeSwitch.IsCheckedChanged += ThemeSwitchIsCheckedChanged;
            themeStackPanel.Children.Add(themeSwitch);
            _style.Theme = themeSwitch;
            return themeStackPanel;
        }

        private StackPanel AccentStackPanel(double flyoutWidth)
        {
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


        private StackPanel HorizontalLineStackPanel(double flyoutWidth)
        {
            var horizontalLineStackPanel = new StackPanel
                                           {
                                               Orientation = Orientation.Vertical,
                                               Margin = new Thickness(0, 10, 0, 0)
                                           };
            horizontalLineStackPanel.Children.Add(new Rectangle
                                                  {
                                                      AllowDrop = false,
                                                      Width = flyoutWidth - 30,
                                                      VerticalAlignment = VerticalAlignment.Center,
                                                      Stroke = (SolidColorBrush) _mainWindow.FindResource("AccentColorBrush")
                                                  });
            return horizontalLineStackPanel;
        }

        private StackPanel SaveStyleStackPanel(double flyoutWidth)
        {
            var saveStyleButton = new Button
                                  {
                                      Name = "SaveStyle",
                                      Width = flyoutWidth - 85,
                                      Margin = new Thickness(60, 5, 0, 0),
                                      Content = ControlContentStackPanel(new ControlContent
                                                                         {
                                                                             Content = "save style",
                                                                             FillBrush = Brushes.Black,
                                                                             ImageResourceName = "appbar_save",
                                                                             ImageSize = 16
                                                                         })
                                  };

            var saveStyleStackPanel = new StackPanel
                                      {
                                          Orientation = Orientation.Horizontal,
                                          Margin = new Thickness(10, 5, 0, 0)
                                      };
            saveStyleStackPanel.Children.Add(saveStyleButton);
            return saveStyleStackPanel;
        }

        private StackPanel ControlContentStackPanel(ControlContent controlContent)
        {
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
    }
}