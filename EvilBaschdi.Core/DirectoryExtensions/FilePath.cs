using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvilBaschdi.Core.MultiThreading;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    /// </summary>
    public class FilePath : IFilePath
    {
        private readonly IMultiThreadingHelper _multiThreadingHelper;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public FilePath(IMultiThreadingHelper multiThreadingHelper)
        {
            if (multiThreadingHelper == null)
            {
                throw new ArgumentNullException(nameof(multiThreadingHelper));
            }
            _multiThreadingHelper = multiThreadingHelper;
        }

        public List<string> GetSubdirectoriesContainingOnlyFiles(string path)
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

            var fileList = new ConcurrentBag<string>();
            if (initialDirectory.IsAccessible())
            {
                //root directory.
                var initialDirectoryFileList = Directory.GetFiles(initialDirectory).Select(item => item.ToLower()).ToList();
                var dirList = initialDirectoryFileList.Where(file => IsValidFileName(file, fileList, includeExtensionList, excludeExtensionList)).ToList();
                //sub directories.
                var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory).SelectMany(Directory.GetFiles).Select(item => item.ToLower());
                var dirSubList = initialDirectorySubdirectoriesFileList.Where(file => IsValidFileName(file, fileList, includeExtensionList, excludeExtensionList)).ToList();

                var processList = new List<string>();
                processList.AddRange(dirList);
                processList.AddRange(dirSubList);

                _multiThreadingHelper.CallInParallelByProcessorCount(processList,
                    range => Parallel.For(range.Item1, range.Item2,
                        i => { fileList.Add(processList[i]); }));
            }

            return fileList.ToList();
        }


        private bool IsValidFileName(string file, ConcurrentBag<string> fileList, List<string> includeExtensionList, List<string> excludeExtensionList)
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