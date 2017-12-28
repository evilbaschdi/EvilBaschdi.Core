using System;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    /// <summary>
    ///     ApplicationSettings wrapper Interface implementation.
    /// </summary>
    public class CoreSettings : ISettings
    {
        private readonly IApplicationSettingsBaseHelper _applicationSettingsBaseHelper;

        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        /// <param name="applicationSettingsBaseHelper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CoreSettings(IApplicationSettingsBaseHelper applicationSettingsBaseHelper)
        {
            _applicationSettingsBaseHelper = applicationSettingsBaseHelper ?? throw new ArgumentNullException(nameof(applicationSettingsBaseHelper));
        }

        /// <inheritdoc />

        public string Accent
        {
            get => _applicationSettingsBaseHelper.Get("Accent", "");
            set => _applicationSettingsBaseHelper.Set("Accent", value);
        }

        /// <inheritdoc />
        public string Theme
        {
            get => _applicationSettingsBaseHelper.Get("Theme", "");
            set => _applicationSettingsBaseHelper.Set("Theme", value);
        }
    }
}