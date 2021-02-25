using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using EvilBaschdi.Core.Model;
using JetBrains.Annotations;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class FileListFromPath : IFileListFromPath
    {
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

            var enumeration = new FileSystemEnumerable<string>(
                                  directory: path,
                                  transform: (ref FileSystemEntry entry) => entry.ToFullPath(),
                                  options: new EnumerationOptions
                                           {
                                               RecurseSubdirectories = true,
                                               MatchCasing = MatchCasing.CaseInsensitive,
                                               IgnoreInaccessible = true
                                           })
                              {
                                  ShouldIncludePredicate = (ref FileSystemEntry entry) => entry.IsDirectory
                              };
            return enumeration.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
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
            return enumeration.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        private bool FileSystemEntryIsValid(FileSystemEntry entry)
        {
            var file = entry.ToFullPath();
            var includeExtensionList = _fileListFromPathFilter.FilterExtensionsToEqual;
            var excludeExtensionList = _fileListFromPathFilter.FilterExtensionsNotToEqual;
            var includeFileNameList = _fileListFromPathFilter.FilterFileNamesToEqual;
            var excludeFileNameList = _fileListFromPathFilter.FilterFileNamesNotToEqual;

            var fileName = entry.FileName.ToString();
            var fileExtension = Path.GetExtension(entry.ToFullPath()).TrimStart('.');

            var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);

            //!Any() => all allowed; else => list has to contain extension, name or path
            var includeExtension = !includeExtensionList.Any() || includeExtensionList.Contains(fileExtension);
            var includeFileName = !includeFileNameList.Any() || includeFileNameList.Contains(fileName);

            // .docx
            var excludeExtension =
                excludeExtensionList.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
            // ...file.x
            var excludeFileName =
                excludeFileNameList.Any(p => fileName.Contains(p, StringComparison.OrdinalIgnoreCase));

            return hasFileExtension && includeExtension && !excludeExtension && includeFileName && !excludeFileName;
        }
    }
}