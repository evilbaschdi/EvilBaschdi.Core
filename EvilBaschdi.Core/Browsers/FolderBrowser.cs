using System.Windows.Forms;

namespace EvilBaschdi.Core.Browsers
{
    public class FolderBrowser : IFolderBrowser
    {
        private string _selectedPath;

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