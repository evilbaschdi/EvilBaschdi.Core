namespace EvilBaschdi.Core.Browsers
{
    public interface IFolderBrowser
    {
        void ShowDialog();
        string SelectedPath { get; set; }
    }
}