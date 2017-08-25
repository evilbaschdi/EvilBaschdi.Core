using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvilBaschdi.Core.Threading;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <inheritdoc />
    public class FilePath : IFilePath
    {
        private readonly IMultiThreadingHelper _multiThreadingHelper;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="multiThreadingHelper" /> is <see langword="null" />.</exception>
        public FilePath(IMultiThreadingHelper multiThreadingHelper)
        {
            _multiThreadingHelper = multiThreadingHelper ?? throw new ArgumentNullException(nameof(multiThreadingHelper));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public List<string> GetSubdirectoriesContainingOnlyFiles(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Where(dir => dir.IsAccessible()).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Returns a list of file paths for given initial directory.
        /// </summary>
        /// <param name="initialDirectory">Directory to start search.</param>
        /// <param name="includeExtensionList">File extensions to include. No filtering if empty.</param>
        /// <param name="excludeExtensionList">File extensions to exclude. Not filtering if empty.</param>
        /// <param name="includeFileNameList">File name to include. No filtering if empty.</param>
        /// <param name="excludeFileNameList">File name to exclude. No filtering if empty.</param>
        /// <param name="includeFilePathList">File path to include. No filtering if empty.</param>
        /// <param name="excludeFilePathList">File path to exclude. No filtering if empty.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="initialDirectory" /> is <see langword="null" />.</exception>
        public List<string> GetFileList(string initialDirectory,
                                        List<string> includeExtensionList = null, List<string> excludeExtensionList = null,
                                        List<string> includeFileNameList = null, List<string> excludeFileNameList = null,
                                        List<string> includeFilePathList = null, List<string> excludeFilePathList = null)
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
            if (includeFileNameList == null)
            {
                includeFileNameList = new List<string>();
            }
            if (excludeFileNameList == null)
            {
                excludeFileNameList = new List<string>();
            }
            if (includeFilePathList == null)
            {
                includeFilePathList = new List<string>();
            }
            if (excludeFilePathList == null)
            {
                excludeFilePathList = new List<string>();
            }

            var fileList = new ConcurrentBag<string>();
            if (initialDirectory.IsAccessible())
            {
                //root directory.
                var initialDirectoryFileList = Directory.GetFiles(initialDirectory).Select(item => item.ToLower()).ToList();
                var dirList =
                    initialDirectoryFileList.Where(file =>
                                                       IsValidFileName(file, fileList,
                                                           includeExtensionList, excludeExtensionList, includeFileNameList, excludeFileNameList, includeFilePathList,
                                                           excludeFilePathList)).ToList();
                //sub directories.
                var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory).SelectMany(Directory.GetFiles).Select(item => item.ToLower());
                var dirSubList =
                    initialDirectorySubdirectoriesFileList.Where(file =>
                                                                     IsValidFileName(file, fileList,
                                                                         includeExtensionList, excludeExtensionList, includeFileNameList, excludeFileNameList, includeFilePathList,
                                                                         excludeFilePathList)).ToList();

                var processList = new List<string>();
                processList.AddRange(dirList);
                processList.AddRange(dirSubList);

                _multiThreadingHelper.CallInParallelByProcessorCount(processList,
                    range => Parallel.For(range.Item1, range.Item2,
                        i => { fileList.Add(processList[i]); }));
            }

            return fileList.ToList();
        }

        private bool IsValidFileName(string file, ConcurrentBag<string> fileList,
                                     List<string> includeExtensionList, List<string> excludeExtensionList,
                                     List<string> includeFileNameList, List<string> excludeFileNameList,
                                     List<string> includeFilePathList, List<string> excludeFilePathList)
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
                throw new ArgumentNullException(nameof(includeExtensionList));
            }
            if (excludeExtensionList == null)
            {
                throw new ArgumentNullException(nameof(excludeExtensionList));
            }
            if (includeFileNameList == null)
            {
                throw new ArgumentNullException(nameof(includeFileNameList));
            }
            if (excludeFileNameList == null)
            {
                throw new ArgumentNullException(nameof(excludeFileNameList));
            }
            if (includeFilePathList == null)
            {
                throw new ArgumentNullException(nameof(includeFilePathList));
            }
            if (excludeFilePathList == null)
            {
                throw new ArgumentNullException(nameof(excludeFilePathList));
            }


            var path = file.ToLower();
            var fileInfo = new FileInfo(file);
            var fileName = fileInfo.Name.ToLower();
            var fileExtension = fileInfo.Extension.ToLower().TrimStart('.');

            var alreadyContained = !fileList.Contains(file);
            var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);
            var includeExtention = !includeExtensionList.Any() || includeExtensionList.Contains(fileExtension);
            var includeFileName = !includeFileNameList.Any() || includeFileNameList.Contains(fileName);
            var includeFilePath = !includeFilePathList.Any() || includeFilePathList.Contains(path);
            var excludeExtention = excludeExtensionList.Contains(fileExtension);
            var excludeFileName = excludeFileNameList.Any(p => fileName.Contains(p));
            var excludeFilePath = excludeFilePathList.Any(p => path.Contains(p));

            return alreadyContained && hasFileExtension && includeExtention && !excludeExtention && includeFileName && !excludeFileName && includeFilePath && !excludeFilePath;
        }
    }
}