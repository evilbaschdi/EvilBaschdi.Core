using System.Collections.Generic;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    public interface IFileListFromPath : IValueFor2<string, FileListFromPathFilter, List<string>>
    {
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<string> GetSubdirectoriesContainingOnlyFiles(string path);
    }
}