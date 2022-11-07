using System.IO.Enumeration;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class FileListFromPath : IFileListFromPath
{
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
                              path,
                              (ref FileSystemEntry entry) => entry.ToFullPath(),
                              new()
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

        return ValueFor(initialDirectory, new());
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

        if (fileListFromPathFilter == null)
        {
            throw new ArgumentNullException(nameof(fileListFromPathFilter));
        }

        var enumeration = new FileSystemEnumerable<string>(
                              initialDirectory,
                              (ref FileSystemEntry entry) => entry.ToFullPath(),
                              new()
                              {
                                  RecurseSubdirectories = true,
                                  MatchCasing = MatchCasing.CaseInsensitive,
                                  IgnoreInaccessible = true
                              })
                          {
                              ShouldIncludePredicate = (ref FileSystemEntry entry) => !entry.IsDirectory && FileSystemEntryIsValid(entry, fileListFromPathFilter)
                          };
        return enumeration.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    /// <inheritdoc />
    public bool FileSystemEntryIsValid(FileSystemEntry entry, [NotNull] FileListFromPathFilter fileListFromPathFilter)
    {
        if (fileListFromPathFilter == null)
        {
            throw new ArgumentNullException(nameof(fileListFromPathFilter));
        }

        var includeExtensionList = fileListFromPathFilter.FilterExtensionsToEqual;
        var excludeExtensionList = fileListFromPathFilter.FilterExtensionsNotToEqual;
        var includeFileNameList = fileListFromPathFilter.FilterFileNamesToEqual;
        var excludeFileNameList = fileListFromPathFilter.FilterFileNamesNotToEqual;

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