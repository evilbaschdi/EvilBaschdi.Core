namespace EvilBaschdi.Core.Application
{
    /// <summary>
    ///     Interface for classes to get values from or set values in ApplicationSettingsBase
    /// </summary>
    public interface IApplicationSettingsBaseHelper
    {
        /// <summary>
        ///     Get value of type T
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="fallback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
        T Get<T>(string setting, T fallback = default);

        /// <summary>
        ///     Set value of type T
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        void Set(string setting, object value);
    }
}