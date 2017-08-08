using System;
using System.Configuration;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    /// <summary>
    ///     ApplicationSettings wrapper Interface implementation.
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
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />

        public string Accent
        {
            get => string.IsNullOrWhiteSpace(_settings["Accent"]?.ToString())
                ? ""
                : _settings["Accent"].ToString();
            set
            {
                _settings["Accent"] = value;
                _settings.Save();
            }
        }

        /// <inheritdoc />
        public string Theme
        {
            get => string.IsNullOrWhiteSpace(_settings["Theme"]?.ToString())
                ? ""
                : _settings["Theme"].ToString();
            set
            {
                _settings["Theme"] = value;
                _settings.Save();
            }
        }

        /// <inheritdoc />
        public string LastScreenDisplayName
        {
            get => string.IsNullOrWhiteSpace(_settings["LastScreenDisplayName"]?.ToString())
                ? ""
                : _settings["LastScreenDisplayName"].ToString();
            set
            {
                _settings["LastScreenDisplayName"] = value;
                _settings.Save();
            }
        }
    }
}