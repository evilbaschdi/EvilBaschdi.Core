using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    /// <summary>
    ///     Classes to get values from or set values in ApplicationSettingsBase
    /// </summary>
    public class ApplicationSettingsBaseHelper : IApplicationSettingsBaseHelper
    {
        private readonly SettingsBase _settingsBase;

        /// <summary>
        /// </summary>
        /// <param name="settingsBase"></param>
        public ApplicationSettingsBaseHelper(SettingsBase settingsBase)
        {
            _settingsBase = settingsBase ?? throw new ArgumentNullException(nameof(settingsBase));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get value of type T
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="fallback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
        public T Get<T>(string setting, T fallback = default(T))
        {
            if (!_settingsBase.Properties.OfType<string>().Contains(setting))
            {
                return fallback;
            }
            var value = (T) _settingsBase[setting];
            if (fallback != null)
            {
                if (IsValueEmpty(value))
                {
                    return fallback;
                }
            }
            return value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set value of type T
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public void Set(string setting, object value)
        {
            _settingsBase[setting] = value;
            _settingsBase.Save();
        }

        private bool IsValueEmpty<T>(T value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is string)
            {
                if (string.IsNullOrWhiteSpace(value as string))
                {
                    return true;
                }
            }
            if (value is StringCollection)
            {
                var collection = value as StringCollection;
                if (collection.Count == 0)
                {
                    return true;
                }
            }
            else
            {
                if (value.Equals(default(T)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}