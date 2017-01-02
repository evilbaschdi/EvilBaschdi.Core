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
    ///     Class that handle metro style on wpf.
    /// </summary>
    public class MetroStyle : IMetroStyle
    {
        /// <summary>
        ///     Accent of Application MetroStyle.
        /// </summary>
        private Accent _styleAccent = ThemeManager.DetectAppStyle(System.Windows.Application.Current).Item2;

        /// <summary>
        ///     Theme of Application MetroStyle.
        /// </summary>
        private AppTheme _styleTheme = ThemeManager.DetectAppStyle(System.Windows.Application.Current).Item1;

        private readonly MetroWindow _mainWindow;
        private ComboBox _accent;
        private readonly RadioButton _themeDark;
        private readonly RadioButton _themeLight;
        private ToggleSwitch _themeSwitch;
        private readonly ISettings _settings;
        private readonly IThemeManagerHelper _themeManagerHelper;

        /// <summary>
        ///     Handle metro style by radio buttons.
        /// </summary>
        /// <param name="mainWindow" />
        /// <param name="accent" />
        /// <param name="themeLight" />
        /// <param name="settings" />
        /// <param name="themeDark" />
        /// <param name="themeManagerHelper"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="mainWindow" /> is <see langword="null" />.
        ///     <paramref name="accent" /> is <see langword="null" />.
        ///     <paramref name="themeDark" /> is <see langword="null" />.
        ///     <paramref name="themeLight" /> is <see langword="null" />.
        ///     <paramref name="settings" /> is <see langword="null" />.
        ///     <paramref name="themeManagerHelper" /> is <see langword="null" />.
        /// </exception>
        public MetroStyle(MetroWindow mainWindow, ComboBox accent, RadioButton themeDark, RadioButton themeLight, ISettings settings, IThemeManagerHelper themeManagerHelper)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            if (accent == null)
            {
                throw new ArgumentNullException(nameof(accent));
            }
            if (themeDark == null)
            {
                throw new ArgumentNullException(nameof(themeDark));
            }
            if (themeLight == null)
            {
                throw new ArgumentNullException(nameof(themeLight));
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
            _accent = accent;
            _themeDark = themeDark;
            _themeLight = themeLight;
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
            _accent = accent;
            _themeSwitch = themeSwitch;
            _settings = settings;
            _themeManagerHelper = themeManagerHelper;
        }

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
            _accent = Accent;
            Theme = new ToggleSwitch();
            _themeSwitch = Theme;
            _themeManagerHelper = themeManagerHelper;
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

            _accent.SelectedValue = _styleAccent.Name;

            switch (_styleTheme.Name)
            {
                case "BaseDark":
                    if (_themeDark != null && _themeLight != null)
                    {
                        _themeDark.IsChecked = true;
                        _themeLight.IsChecked = false;
                    }
                    else if (_themeSwitch != null)
                    {
                        _themeSwitch.IsChecked = true;
                    }
                    break;

                case "BaseLight":
                    if (_themeDark != null && _themeLight != null)
                    {
                        _themeDark.IsChecked = false;
                        _themeLight.IsChecked = true;
                    }
                    else if (_themeSwitch != null)
                    {
                        _themeSwitch.IsChecked = false;
                    }
                    break;
            }
            EnableDisableThemeControl();
            LoadSystemAppColor();
            SetStyle();

            foreach (var accent in ThemeManager.Accents.OrderBy(a => a.Name))
            {
                _accent.Items.Add(accent.Name);
            }
        }

        /// <summary>
        ///     Sets Style.
        /// </summary>
        private void SetStyle()
        {
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, _styleAccent, _styleTheme);
        }

        private void EnableDisableThemeControl()
        {
            var accent = _accent.SelectedValue.ToString();
            var isWindows10AndsystemStyle = VersionHelper.IsWindows10 && accent == "Accent from windows";
            if (_themeDark != null && _themeLight != null)
            {
                _themeDark.IsEnabled = !isWindows10AndsystemStyle;
                _themeLight.IsEnabled = !isWindows10AndsystemStyle;
            }
            else if (_themeSwitch != null)
            {
                _themeSwitch.IsEnabled = !isWindows10AndsystemStyle;
            }
        }

        private void LoadSystemAppColor()
        {
            var accent = _accent.SelectedValue.ToString();
            var isWindows10AndSystemStyle = VersionHelper.IsWindows10 && accent == "Accent from windows";
            if (isWindows10AndSystemStyle)
            {
                var personalize = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (personalize != null)
                {
                    var appsTheme = personalize.GetValue("AppsUseLightTheme") != null && personalize.GetValue("AppsUseLightTheme").ToString().Equals("0") ? "BaseDark" : "BaseLight";

                    switch (appsTheme)
                    {
                        case "BaseDark":
                            if (_themeDark != null && _themeLight != null)
                            {
                                _themeDark.IsChecked = true;
                                _themeLight.IsChecked = false;
                            }
                            else if (_themeSwitch != null)
                            {
                                _themeSwitch.IsChecked = true;
                            }
                            break;

                        case "BaseLight":
                            if (_themeDark != null && _themeLight != null)
                            {
                                _themeDark.IsChecked = false;
                                _themeLight.IsChecked = true;
                            }
                            else if (_themeSwitch != null)
                            {
                                _themeSwitch.IsChecked = false;
                            }
                            break;
                    }
                    _styleTheme = ThemeManager.GetAppTheme(appsTheme);
                }
            }
        }

        /// <summary>
        ///     Accent of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetAccent(object sender, SelectionChangedEventArgs e)
        {
            var accent = _accent.SelectedValue.ToString();
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
        public ComboBox Accent
        {
            get { return _accent; }
            set { _accent = value; }
        }

        /// <summary>
        ///     ToggleSwitch for choosing a theme.
        /// </summary>
        public ToggleSwitch Theme
        {
            get { return _themeSwitch; }
            set { _themeSwitch = value; }
        }
    }
}