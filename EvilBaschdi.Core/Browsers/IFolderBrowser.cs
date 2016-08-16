namespace EvilBaschdi.Core.Browsers
{
    /// <summary>
    ///     Interface for FolderBrowser.
    /// </summary>
    public interface IFolderBrowser
    {
        /// <summary>
        ///     Shows FolderBrowser.
        /// </summary>
        void ShowDialog();

        /// <summary>
        ///     Get or Set selected path.
        /// </summary>
        string SelectedPath { get; set; }
    }
}