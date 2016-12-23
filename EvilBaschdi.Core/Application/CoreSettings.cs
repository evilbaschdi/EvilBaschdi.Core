using System;
using System.Configuration;

namespace EvilBaschdi.Core.Application
{
    /// <summary>
    ///     Wrapper arround ApplicationSettings.
    /// </summary>
    public class CoreSettings : ISettings
    {
        private readonly ApplicationSettingsBase _settings;

        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        /// <param name="settings"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CoreSettings(ApplicationSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            _settings = settings;
        }

        /// <summary>
        ///     MahApps ThemeManager Accent.
        /// </summary>
        public string Accent
        {
            get
            {
                return string.IsNullOrWhiteSpace(_settings["Accent"]?.ToString())
                    ? ""
                    : _settings["Accent"].ToString();
            }
            set
            {
                _settings["Accent"] = value;
                _settings.Save();
            }
        }

        /// <summary>
        ///     MahApps ThemeManager Theme.
        /// </summary>
        public string Theme
        {
            get
            {
                return string.IsNullOrWhiteSpace(_settings["Theme"]?.ToString())
                    ? ""
                    : _settings["Theme"].ToString();
            }
            set
            {
                _settings["Theme"] = value;
                _settings.Save();
            }
        }
    }
}