using System.Collections.Generic;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    /// </summary>
    public interface IFilePath
    {
        /// <summary>
        /// </summary>
        /// <param name="initialDirectory">Directory to start search.</param>
        /// <param name="includeExtensionList">File extensions to include. No filtering if empty.</param>
        /// <param name="excludeExtensionList">File extensions to exclude. Not filtering if empty.</param>
        /// <param name="includeFileNameList">File name to include. No filtering if empty.</param>
        /// <param name="excludeFileNameList">File name to exclude. No filtering if empty.</param>
        /// <param name="includeFilePathList">File path to include. No filtering if empty.</param>
        /// <param name="excludeFilePathList">File path to exclude. No filtering if empty.</param>
        /// <returns></returns>
        List<string> GetFileList(string initialDirectory,
                                 List<string> includeExtensionList = null, List<string> excludeExtensionList = null,
                                 List<string> includeFileNameList = null, List<string> excludeFileNameList = null,
                                 List<string> includeFilePathList = null, List<string> excludeFilePathList = null);

        List<string> GetSubdirectoriesContainingOnlyFiles(string path);
    }
}