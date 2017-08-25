using System.Windows.Forms;

namespace EvilBaschdi.Core.Browsers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for FolderBrowser.
    /// </summary>
    public class FolderBrowser : IFolderBrowser
    {
        private string _selectedPath;

        /// <inheritdoc />
        /// <summary>
        ///     Shows FolderBrowser.
        /// </summary>
        public void ShowDialog()
        {
            var folderDialog = new FolderBrowserDialog
                               {
                                   SelectedPath = _selectedPath
                               };

            var result = folderDialog.ShowDialog();
            if (result.ToString() != "OK")
            {
                return;
            }
            _selectedPath = folderDialog.SelectedPath;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get or Set selected path.
        /// </summary>
        public string SelectedPath
        {
            get => string.IsNullOrWhiteSpace(_selectedPath)
                ? string.Empty
                : _selectedPath;
            set => _selectedPath = value;
        }
    }
}