namespace EvilBaschdi.Core.Application
{
    /// <summary>
    ///     Wrapper arround ApplicationSettings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     MahApps ThemeManager Accent.
        /// </summary>
        string Accent { get; set; }

        /// <summary>
        ///     MahApps ThemeManager Theme.
        /// </summary>
        string Theme { get; set; }
    }
}