using System.Windows.Forms;

namespace EvilBaschdi.Core.Browsers
{
    /// <summary>
    ///     Class for FolderBrowser.
    /// </summary>
    public class FolderBrowser : IFolderBrowser
    {
        private string _selectedPath;

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

        /// <summary>
        ///     Get or Set selected path.
        /// </summary>
        public string SelectedPath
        {
            get
            {
                return string.IsNullOrWhiteSpace(_selectedPath)
                    ? string.Empty
                    : _selectedPath;
            }
            set { _selectedPath = value; }
        }
    }
}