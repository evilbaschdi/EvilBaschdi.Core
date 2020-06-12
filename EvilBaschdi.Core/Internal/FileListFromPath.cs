using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.Core.Model;
using JetBrains.Annotations;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    public class FileListFromPath : IFileListFromPath
    {
        private ConcurrentBag<string> _fileList;
        private FileListFromPathFilter _fileListFromPathFilter = new FileListFromPathFilter();

        /// <inheritdoc />
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles([NotNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            var include = _fileListFromPathFilter.FilterFilePathsToEqual ?? new List<string>();
            var exclude = _fileListFromPathFilter.FilterFilePathsNotToEqual ?? new List<string>();

            var list = new List<string>();
            var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories)
                .Where(dir => dir.IsAccessible()).ToList();

            foreach (var directory in directories)
                if (include.Any() || exclude.Any())
                {
                    list.AddRange(from item in include
                        where directory.EndsWith($@"\{item}", StringComparison.OrdinalIgnoreCase) ||
                              directory.Contains($@"\{item}\", StringComparison.OrdinalIgnoreCase)
                        select directory);
                    list.AddRange(from item in exclude
                        where !directory.EndsWith($@"\{item}", StringComparison.OrdinalIgnoreCase) &&
                              !directory.Contains($@"\{item}\", StringComparison.OrdinalIgnoreCase)
                        select directory);
                }
                else
                {
                    list.Add(directory);
                }

            return list.Distinct();
        }

        /// <inheritdoc />
        public List<string> ValueFor([NotNull] string initialDirectory)
        {
            if (initialDirectory == null) throw new ArgumentNullException(nameof(initialDirectory));

            return ValueFor(initialDirectory, new FileListFromPathFilter());
        }

        /// <inheritdoc />
        public List<string> ValueFor([NotNull] string initialDirectory,
            [NotNull] FileListFromPathFilter fileListFromPathFilter)
        {
            if (initialDirectory == null) throw new ArgumentNullException(nameof(initialDirectory));

            _fileListFromPathFilter =
                fileListFromPathFilter ?? throw new ArgumentNullException(nameof(fileListFromPathFilter));
            _fileList = new ConcurrentBag<string>();

            if (!initialDirectory.IsAccessible()) return _fileList.ToList();

            //root directory.
            var initialDirectoryFileList = Directory.GetFiles(initialDirectory).Select(item => item.ToLower()).ToList();
            var dirList = initialDirectoryFileList.Where(FileIsValid).ToList();
            //sub directories.
            var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory)
                ?.SelectMany(Directory.GetFiles).Select(item => item.ToLower());
            var dirSubList = initialDirectorySubdirectoriesFileList?.Where(FileIsValid).ToList();

            _fileList.AddRange(dirList);
            _fileList.AddRange(dirSubList);

            return _fileList.ToList();
        }

        private bool FileIsValid([NotNull] string file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var includeExtensionList = _fileListFromPathFilter.FilterExtensionsToEqual ?? new List<string>();
            var excludeExtensionList = _fileListFromPathFilter.FilterExtensionsNotToEqual ?? new List<string>();
            var includeFileNameList = _fileListFromPathFilter.FilterFileNamesToEqual ?? new List<string>();
            var excludeFileNameList = _fileListFromPathFilter.FilterFileNamesNotToEqual ?? new List<string>();

            var fileInfo = new FileInfo(file);
            var fileName = fileInfo.Name.ToLower();
            var fileExtension = fileInfo.Extension.ToLower().TrimStart('.');

            var alreadyContained = !_fileList.Contains(file);
            var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);

            //!Any() => all allowed; else => list has to contain extension, name or path
            var includeExtension = !includeExtensionList.Any() || includeExtensionList.Contains(fileExtension);
            var includeFileName = !includeFileNameList.Any() || includeFileNameList.Contains(fileName);

            // .docx
            var excludeExtension =
                excludeExtensionList.Contains(fileExtension, StringComparer.InvariantCultureIgnoreCase);
            // ...file.x
            var excludeFileName =
                excludeFileNameList.Any(p => fileName.Contains(p, StringComparison.InvariantCultureIgnoreCase));

            return alreadyContained && hasFileExtension && includeExtension && !excludeExtension && includeFileName &&
                   !excludeFileName;
        }
    }
}