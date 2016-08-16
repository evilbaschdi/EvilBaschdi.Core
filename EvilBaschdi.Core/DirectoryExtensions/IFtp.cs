namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    ///     Interface for Ftp.
    /// </summary>
    public interface IFtp
    {
        /// <summary>
        ///     Returns a list of directories by given directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        string[] DirecotryListSimple(string directory);
    }
}