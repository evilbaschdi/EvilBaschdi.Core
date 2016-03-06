using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EvilBaschdi.Core.Application;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
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
        private readonly ComboBox _accent;
        private readonly RadioButton _themeDark;
        private readonly RadioButton _themeLight;
        private readonly ISettings _settings;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <param name="mainWindow" />
        /// <param name="accent" />
        /// <param name="themeLight" />
        /// <param name="settings" />
        /// <param name="themeDark" />
        public MetroStyle(MetroWindow mainWindow, ComboBox accent, RadioButton themeDark, RadioButton themeLight, ISettings settings)
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
            _mainWindow = mainWindow;
            _accent = accent;
            _themeDark = themeDark;
            _themeLight = themeLight;
            _settings = settings;
        }

        /// <summary>
        /// </summary>
        public void Load()
        {
            Load(false, false);
        }

        /// <summary>
        /// </summary>
        public void Load(bool center, bool resizeWithBorder400)
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
            var themeManagerHelper = new ThemeManagerHelper();
            themeManagerHelper.RegisterSystemColorTheme();
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
                    _themeDark.IsChecked = true;
                    _themeLight.IsChecked = false;
                    break;

                case "BaseLight":
                    _themeDark.IsChecked = false;
                    _themeLight.IsChecked = true;
                    break;
            }

            SetStyle();

            foreach (var accent in ThemeManager.Accents.OrderBy(a => a.Name))
            {
                _accent.Items.Add(accent.Name);
            }
        }

        /// <summary>
        /// </summary>
        private void SetStyle()
        {
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, _styleAccent, _styleTheme);
        }

        /// <summary>
        ///     Accent of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetAccent(object sender, SelectionChangedEventArgs e)
        {
            _styleAccent = ThemeManager.GetAccent(_accent.SelectedValue.ToString());
            SetStyle();
        }

        /// <summary>
        ///     Theme of application style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void SetTheme(object sender, RoutedEventArgs routedEventArgs)
        {
            // get the theme from the current application
            var style = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

            var radiobutton = (RadioButton) sender;
            _styleTheme = style.Item1;

            switch (radiobutton.Name)
            {
                case "Dark":
                    _styleTheme = ThemeManager.GetAppTheme("BaseDark");
                    break;

                case "Light":
                    _styleTheme = ThemeManager.GetAppTheme("BaseLight");
                    break;

                default:
                    _styleTheme = style.Item1;
                    break;
            }

            SetStyle();
        }

        /// <summary>
        /// </summary>
        public void SaveStyle()
        {
            _settings.Accent = _styleAccent.Name;
            _settings.Theme = _styleTheme.Name;
        }
    }
}