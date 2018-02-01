using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    public class FileListFromPath : IFileListFromPath
    {
        private readonly IMultiThreading _multiThreading;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="multiThreading" /> is <see langword="null" />.</exception>
        public FileListFromPath(IMultiThreading multiThreading)
        {
            _multiThreading = multiThreading ?? throw new ArgumentNullException(nameof(multiThreading));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Where(dir => dir.IsAccessible()).ToList();
        }

        /// <inheritdoc />
        public List<string> ValueFor(string initialDirectory, FileListFromPathFilter fileListFromPathFilter)
        {
            if (initialDirectory == null)
            {
                throw new ArgumentNullException(nameof(initialDirectory));
            }

            if (fileListFromPathFilter == null)
            {
                throw new ArgumentNullException(nameof(fileListFromPathFilter));
            }

            var fileList = new ConcurrentBag<string>();

            if (initialDirectory.IsAccessible())
            {
                //root directory.
                var initialDirectoryFileList = Directory.GetFiles(initialDirectory).Select(item => item.ToLower()).ToList();
                var dirList = initialDirectoryFileList.Where(file => IsValidFileName(file, fileList, fileListFromPathFilter)).ToList();
                //sub directories.
                var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory).SelectMany(Directory.GetFiles).Select(item => item.ToLower());
                var dirSubList = initialDirectorySubdirectoriesFileList.Where(file => IsValidFileName(file, fileList, fileListFromPathFilter)).ToList();

                var processList = new List<string>();
                processList.AddRange(dirList);
                processList.AddRange(dirSubList);

                _multiThreading.RunFor(processList,
                    range => Parallel.For(range.Item1, range.Item2,
                        i => { fileList.Add(processList[i]); }));
            }

            return fileList.ToList();
        }

        private bool IsValidFileName(string file, ConcurrentBag<string> fileList, FileListFromPathFilter fileListFromPathFilter)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (fileList == null)
            {
                throw new ArgumentNullException(nameof(fileList));
            }

            var includeExtensionList = fileListFromPathFilter.FilterExtensionsToEqual;
            var excludeExtensionList = fileListFromPathFilter.FilterExtensionsNotToEqual;
            var includeFileNameList = fileListFromPathFilter.FilterFileNamesToEqual;
            var excludeFileNameList = fileListFromPathFilter.FilterFileNamesNotToEqual;
            var includeFilePathList = fileListFromPathFilter.FilterFilePathsToEqual;
            var excludeFilePathList = fileListFromPathFilter.FilterFilePathsNotToEqual;

            var path = file.ToLower();
            var fileInfo = new FileInfo(file);
            var fileName = fileInfo.Name.ToLower();
            var fileExtension = fileInfo.Extension.ToLower().TrimStart('.');

            var alreadyContained = !fileList.Contains(file);
            var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);

            //!Any() => all allowed; else => list has to contain extension, name or path
            var includeExtention = includeExtensionList == null || !includeExtensionList.Any() || includeExtensionList.Contains(fileExtension);
            var includeFileName = includeFileNameList == null || !includeFileNameList.Any() || includeFileNameList.Contains(fileName);
            var includeFilePath = includeFilePathList == null || !includeFilePathList.Any() || includeFilePathList.Contains(path);

            // .docx
            var excludeExtention = excludeExtensionList != null && excludeExtensionList.Contains(fileExtension);
            // ...file.x
            var excludeFileName = excludeFileNameList != null && excludeFileNameList.Any(p => fileName.Contains(p));
            // C:\Temp\... 
            var excludeFilePath = excludeFilePathList != null && excludeFilePathList.Any(p => path.Contains(p));

            return alreadyContained && hasFileExtension && includeExtention && !excludeExtention && includeFileName && !excludeFileName && includeFilePath && !excludeFilePath;
        }
    }
}