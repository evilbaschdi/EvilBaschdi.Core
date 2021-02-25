using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.Core.Model;
using JetBrains.Annotations;
#if !NETSTANDARD2_0
using System.IO.Enumeration;

#endif

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class FileListFromPath : IFileListFromPath
    {
        private List<string> _fileList;
        private FileListFromPathFilter _fileListFromPathFilter = new();

        /// <inheritdoc />
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles([NotNull] string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var include = _fileListFromPathFilter.FilterFilePathsToEqual;
            var exclude = _fileListFromPathFilter.FilterFilePathsNotToEqual;

            var list = new List<string>();
            var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories)
                                       .Where(dir => dir.IsAccessible()).ToList();

            foreach (var directory in directories)
            {
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
            }

            return list.Distinct();
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc />
        public List<string> ValueFor([NotNull] string initialDirectory)
        {
            if (initialDirectory == null)
            {
                throw new ArgumentNullException(nameof(initialDirectory));
            }

            return ValueFor(initialDirectory, new FileListFromPathFilter());
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc />
        public List<string> ValueFor([NotNull] string initialDirectory,
                                     [NotNull] FileListFromPathFilter fileListFromPathFilter)
        {
            if (initialDirectory == null)
            {
                throw new ArgumentNullException(nameof(initialDirectory));
            }

            _fileListFromPathFilter =
                fileListFromPathFilter ?? throw new ArgumentNullException(nameof(fileListFromPathFilter));


#if NETSTANDARD2_0
            _fileList = new List<string>();

            if (!initialDirectory.IsAccessible())
            {
                return _fileList.ToList();
            }

            //root directory.
            var initialDirectoryFileList = Directory.GetFiles(initialDirectory).Select(item => item.ToLower()).ToList();
            var dirList = initialDirectoryFileList.Where(FileIsValid)?.ToList();
            //sub directories.
            var initialDirectorySubdirectoriesFileList = GetSubdirectoriesContainingOnlyFiles(initialDirectory)
                                                         ?.SelectMany(Directory.GetFiles).Select(item => item.ToLower());
            var dirSubList = initialDirectorySubdirectoriesFileList?.Where(FileIsValid).ToList();

            if (dirSubList != null)
            {
                _fileList.AddRange(dirList);
            }

            if (dirSubList != null)
            {
                _fileList.AddRange(dirSubList);
            }
#endif

#if !NETSTANDARD2_0
            var enumeration = new FileSystemEnumerable<string>(
                                  directory: initialDirectory,
                                  transform: (ref FileSystemEntry entry) => entry.ToFullPath(),
                                  options: new EnumerationOptions
                                           {
                                               RecurseSubdirectories = true,
                                               MatchCasing = MatchCasing.CaseInsensitive,
                                               IgnoreInaccessible = true
                                           })
                              {
                                  ShouldIncludePredicate = (ref FileSystemEntry entry) => !entry.IsDirectory && FileSystemEntryIsValid(entry)
                              };
            _fileList = enumeration.ToList();
#endif


            return _fileList.ToList();
        }
#if NETSTANDARD2_0
        private bool FileIsValid([NotNull] string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var includeExtensionList = _fileListFromPathFilter.FilterExtensionsToEqual;
            var excludeExtensionList = _fileListFromPathFilter.FilterExtensionsNotToEqual;
            var includeFileNameList = _fileListFromPathFilter.FilterFileNamesToEqual;
            var excludeFileNameList = _fileListFromPathFilter.FilterFileNamesNotToEqual;

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
#endif
#if !NETSTANDARD2_0
        private bool FileSystemEntryIsValid(FileSystemEntry entry)
        {
            var file = entry.ToFullPath();
            var includeExtensionList = _fileListFromPathFilter.FilterExtensionsToEqual;
            var excludeExtensionList = _fileListFromPathFilter.FilterExtensionsNotToEqual;
            var includeFileNameList = _fileListFromPathFilter.FilterFileNamesToEqual;
            var excludeFileNameList = _fileListFromPathFilter.FilterFileNamesNotToEqual;

            var fileName = entry.FileName.ToString();
            var fileExtension = Path.GetExtension(entry.ToFullPath()).TrimStart('.');

            var alreadyContained = !_fileList.Contains(file, StringComparer.OrdinalIgnoreCase);
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
#endif
    }
}