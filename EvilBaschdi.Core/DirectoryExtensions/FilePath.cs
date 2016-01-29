using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    /// </summary>
    public class FilePath : IFilePath
    {
        public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles(string path)
        {
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Where(dir => dir.IsAccessible()).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="initialDirectory"></param>
        /// <returns></returns>
        public List<string> GetFileList(string initialDirectory)
        {
            if (initialDirectory == null)
            {
                throw new ArgumentNullException(nameof(initialDirectory));
            }
            return GetFileList(initialDirectory, new List<string>(), new List<string>());
        }

        /// <summary>
        /// </summary>
        /// <param name="initialDirectory">Directory to start search.</param>
        /// <param name="includeExtensionList">File extensions to include. No filtering if empty.</param>
        /// <param name="excludeExtensionList">File extensions to exclude. Not filtering if empty.</param>
        /// <returns></returns>
        public List<string> GetFileList(string initialDirectory, List<string> includeExtensionList, List<string> excludeExtensionList)
        {
            if (initialDirectory == null)
            {
                throw new ArgumentNullException(nameof(initialDirectory));
            }
            if (includeExtensionList == null)
            {
                includeExtensionList = new List<string>();
            }
            if (excludeExtensionList == null)
            {
                excludeExtensionList = new List<string>();
            }

            var fileList = new List<string>();
            if (initialDirectory.IsAccessible())
            {
                var initialDirectoryFileList = Directory.GetFiles(initialDirectory).ToList();
                Parallel.ForEach(initialDirectoryFileList.Where(file => IsValidFileName(file, fileList, includeExtensionList, excludeExtensionList)), file => fileList.Add(file));

                var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory).SelectMany(Directory.GetFiles);

                Parallel.ForEach(initialDirectorySubdirectoriesFileList.Where(file => IsValidFileName(file, fileList, includeExtensionList, excludeExtensionList)),
                    file => fileList.Add(file));
            }

            return fileList;
        }


        private bool IsValidFileName(string file, ICollection<string> fileList, List<string> includeExtensionList, List<string> excludeExtensionList)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (fileList == null)
            {
                throw new ArgumentNullException(nameof(fileList));
            }
            if (includeExtensionList == null)
            {
                includeExtensionList = new List<string>();
            }
            if (excludeExtensionList == null)
            {
                excludeExtensionList = new List<string>();
            }

            var fileExtension = Path.GetExtension(file).TrimStart('.');

            var directoryInfo = new FileInfo(file).Directory;

            if (directoryInfo == null)
            {
                return false;
            }

            var alreadyContained = !fileList.Contains(file);
            var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);
            var include = !includeExtensionList.Any() || includeExtensionList.Contains(fileExtension);
            var exclude = excludeExtensionList.Contains(fileExtension);

            return alreadyContained && hasFileExtension && include && !exclude;
        }
    }
}