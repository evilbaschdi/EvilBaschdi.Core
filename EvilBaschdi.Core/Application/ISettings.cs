namespace EvilBaschdi.Core.Application
{
    /// <summary>
    ///     ApplicationSettings wrapper Interface.
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

        /// <summary>
        ///     DisplayName of last screen position.
        /// </summary>
        string LastScreenDisplayName { get; set; }
    }
}