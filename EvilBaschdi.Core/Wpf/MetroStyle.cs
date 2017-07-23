using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.DotNetExtensions;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     Class that handle metro style on Wpf.
    /// </summary>
    public class MetroStyle : IMetroStyle
    {
        private readonly MetroWindow _mainWindow;
        private readonly IMoveToScreen _moveToScreen;
        private readonly ISettings _settings;
        private readonly IThemeManagerHelper _themeManagerHelper;

        /// <summary>
        ///     Accent of Application MetroStyle.
        /// </summary>
        private Accent _styleAccent = ThemeManager.DetectAppStyle(System.Windows.Application.Current).Item2;

        /// <summary>
        ///     Theme of Application MetroStyle.
        /// </summary>
        private AppTheme _styleTheme = ThemeManager.DetectAppStyle(System.Windows.Application.Current).Item1;

        /// <summary>
        ///     Handle metro style by ToggleSwitch.
        /// </summary>
        /// ///
        /// <param name="mainWindow" />
        /// <param name="settings" />
        /// <param name="themeManagerHelper"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="mainWindow" /> is <see langword="null" />.
        ///     <paramref name="settings" /> is <see langword="null" />.
        ///     <paramref name="themeManagerHelper" /> is <see langword="null" />.
        /// </exception>
        public MetroStyle(MetroWindow mainWindow, ISettings settings, IThemeManagerHelper themeManagerHelper)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (themeManagerHelper == null)
            {
                throw new ArgumentNullException(nameof(themeManagerHelper));
            }

            _mainWindow = mainWindow;
            _settings = settings;
            Accent = new ComboBox();
            Accent = Accent;
            Theme = new ToggleSwitch();
            Theme = Theme;
            _themeManagerHelper = themeManagerHelper;
        }

        /// <summary>
        ///     Handle metro style by ToggleSwitch.
        /// </summary>
        /// ///
        /// <param name="mainWindow" />
        /// <param name="settings" />
        /// <param name="themeManagerHelper"></param>
        /// <param name="moveToScreen"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="mainWindow" /> is <see langword="null" />.
        ///     <paramref name="settings" /> is <see langword="null" />.
        ///     <paramref name="themeManagerHelper" /> is <see langword="null" />.
        /// </exception>
        public MetroStyle(MetroWindow mainWindow, ISettings settings, IThemeManagerHelper themeManagerHelper, IMoveToScreen moveToScreen)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (themeManagerHelper == null)
            {
                throw new ArgumentNullException(nameof(themeManagerHelper));
            }

            if (moveToScreen == null)
            {
                throw new ArgumentNullException(nameof(moveToScreen));
            }

            _mainWindow = mainWindow;
            _settings = settings;
            Accent = new ComboBox();
            Accent = Accent;
            Theme = new ToggleSwitch();
            Theme = Theme;
            _themeManagerHelper = themeManagerHelper;
            _moveToScreen = moveToScreen;
        }

        /// <summary>
        ///     Handle metro style by ToggleSwitch.
        /// </summary>
        /// ///
        /// <param name="mainWindow" />
        /// <param name="accent" />
        /// <param name="themeSwitch" />
        /// <param name="settings" />
        /// <param name="themeManagerHelper"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="mainWindow" /> is <see langword="null" />.
        ///     <paramref name="accent" /> is <see langword="null" />.
        ///     <paramref name="themeSwitch" /> is <see langword="null" />.
        ///     <paramref name="settings" /> is <see langword="null" />.
        ///     <paramref name="themeManagerHelper" /> is <see langword="null" />.
        /// </exception>
        public MetroStyle(MetroWindow mainWindow, ComboBox accent, ToggleSwitch themeSwitch, ISettings settings, IThemeManagerHelper themeManagerHelper)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (accent == null)
            {
                throw new ArgumentNullException(nameof(accent));
            }
            if (themeSwitch == null)
            {
                throw new ArgumentNullException(nameof(themeSwitch));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (themeManagerHelper == null)
            {
                throw new ArgumentNullException(nameof(themeManagerHelper));
            }
            _mainWindow = mainWindow;
            Accent = accent;
            Theme = themeSwitch;
            _settings = settings;
            _themeManagerHelper = themeManagerHelper;
        }

        /// <summary>
        ///     Handle metro style by ToggleSwitch.
        /// </summary>
        /// ///
        /// <param name="mainWindow" />
        /// <param name="accent" />
        /// <param name="themeSwitch" />
        /// <param name="settings" />
        /// <param name="themeManagerHelper"></param>
        /// <param name="currentScreen"></param>
        /// <param name="moveToScreen"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="mainWindow" /> is <see langword="null" />.
        ///     <paramref name="accent" /> is <see langword="null" />.
        ///     <paramref name="themeSwitch" /> is <see langword="null" />.
        ///     <paramref name="settings" /> is <see langword="null" />.
        ///     <paramref name="themeManagerHelper" /> is <see langword="null" />.
        /// </exception>
        public MetroStyle(MetroWindow mainWindow, ComboBox accent, ToggleSwitch themeSwitch, ISettings settings, IThemeManagerHelper themeManagerHelper,
                          IMoveToScreen moveToScreen)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (accent == null)
            {
                throw new ArgumentNullException(nameof(accent));
            }
            if (themeSwitch == null)
            {
                throw new ArgumentNullException(nameof(themeSwitch));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (themeManagerHelper == null)
            {
                throw new ArgumentNullException(nameof(themeManagerHelper));
            }

            if (moveToScreen == null)
            {
                throw new ArgumentNullException(nameof(moveToScreen));
            }
            _mainWindow = mainWindow;
            Accent = accent;
            Theme = themeSwitch;
            _settings = settings;
            _themeManagerHelper = themeManagerHelper;
            _moveToScreen = moveToScreen;
        }

        /// <summary>
        ///     Load.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="resizeWithBorder400"></param>
        public void Load(bool center = false, bool resizeWithBorder400 = false)
        {
            if (center)
            {
                _mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            if (!string.IsNullOrWhiteSpace(_settings.LastScreenDisplayName))
            {
                _moveToScreen?.RunFor(_mainWindow, _settings.LastScreenDisplayName);
            }

            if (resizeWithBorder400)
            {
                _mainWindow.Width = SystemParameters.PrimaryScreenWidth - 400;
                _mainWindow.Height = SystemParameters.PrimaryScreenHeight - 400;
            }

            _themeManagerHelper.RegisterSystemColorTheme();

            if (!string.IsNullOrWhiteSpace(_settings.Accent))
            {
                _styleAccent = ThemeManager.GetAccent(_settings.Accent);
            }
            if (!string.IsNullOrWhiteSpace(_settings.Theme))
            {
                _styleTheme = ThemeManager.GetAppTheme(_settings.Theme);
            }

            Accent.SelectedValue = _styleAccent.Name;

            if (Theme != null)
            {
                switch (_styleTheme.Name)
                {
                    case "BaseDark":
                        Theme.IsChecked = true;
                        break;
                    case "BaseLight":
                        Theme.IsChecked = false;
                        break;
                }
            }
            EnableDisableThemeControl();
            LoadSystemAppColor();
            SetStyle();

            foreach (var accent in ThemeManager.Accents.OrderBy(a => a.Name))
            {
                Accent.Items.Add(accent.Name);
            }
        }


        /// <summary>
        ///     Accent of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetAccent(object sender, SelectionChangedEventArgs e)
        {
            var accent = Accent.SelectedValue.ToString();
            _styleAccent = ThemeManager.GetAccent(accent);

            EnableDisableThemeControl();
            LoadSystemAppColor();

            SetStyle();
        }

        /// <summary>
        ///     Theme of application style.
        /// </summary>
        /// <param name="sender"></param>
        public void SetTheme(object sender)
        {
            // get the theme from the current application
            var style = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

            var radiobutton = sender as RadioButton;
            var toggleSwitch = sender as ToggleSwitch;

            //BaseDark, BaseLight
            var themeName = style.Item1.Name;

            if (radiobutton != null)
            {
                //BaseDark, BaseLight
                themeName = $"Base{radiobutton.Name}";
            }
            else if (toggleSwitch != null)
            {
                //BaseDark, BaseLight
                themeName = toggleSwitch.IsChecked.HasValue && toggleSwitch.IsChecked.Value ? "BaseDark" : "BaseLight";
            }

            _styleTheme = ThemeManager.GetAppTheme(themeName);

            SetStyle();
        }

        /// <summary>
        ///     Theme of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void SetTheme(object sender, RoutedEventArgs routedEventArgs)
        {
            SetTheme(sender);
        }

        /// <summary>
        ///     Save Style.
        /// </summary>
        public void SaveStyle()
        {
            _settings.Accent = _styleAccent.Name;
            _settings.Theme = _styleTheme.Name;
        }

        /// <summary>
        ///     ComboBox for choosing an accent.
        /// </summary>
        public ComboBox Accent { get; set; }

        /// <summary>
        ///     ToggleSwitch for choosing a theme.
        /// </summary>
        public ToggleSwitch Theme { get; set; }

        /// <summary>
        ///     Sets Style.
        /// </summary>
        private void SetStyle()
        {
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, _styleAccent, _styleTheme);
        }

        private void EnableDisableThemeControl()
        {
            var accent = Accent.SelectedValue.ToString();
            var isWindows10AndsystemStyle = VersionHelper.IsWindows10 && accent == "Accent from windows";
            if (Theme != null)
            {
                Theme.IsEnabled = !isWindows10AndsystemStyle;
            }
        }

        private void LoadSystemAppColor()
        {
            var accent = Accent.SelectedValue.ToString();
            var isWindows10AndSystemStyle = VersionHelper.IsWindows10 && accent == "Accent from windows";
            if (isWindows10AndSystemStyle)
            {
                var personalize = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (personalize != null)
                {
                    var appsTheme = personalize.GetValue("AppsUseLightTheme") != null && personalize.GetValue("AppsUseLightTheme").ToString().Equals("0")
                        ? "BaseDark"
                        : "BaseLight";
                    if (Theme != null)
                    {
                        switch (appsTheme)
                        {
                            case "BaseDark":

                                Theme.IsChecked = true;

                                break;

                            case "BaseLight":

                                Theme.IsChecked = false;

                                break;
                        }
                    }
                    _styleTheme = ThemeManager.GetAppTheme(appsTheme);
                }
            }
        }
    }
}