﻿using System.Collections.Generic;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    /// </summary>
    public interface IFilePath
    {
        /// <summary>
        /// </summary>
        /// <param name="initialDirectory"></param>
        /// <returns></returns>
        List<string> GetFileList(string initialDirectory);

        /// <summary>
        /// </summary>
        /// <param name="initialDirectory">Directory to start search.</param>
        /// <param name="includeExtensionList">File extensions to include. No filtering if empty.</param>
        /// <param name="excludeExtensionList">File extensions to exclude. Not filtering if empty.</param>
        /// <returns></returns>
        List<string> GetFileList(string initialDirectory, List<string> includeExtensionList, List<string> excludeExtensionList);

        IEnumerable<string> GetSubdirectoriesContainingOnlyFiles(string path);
    }
}