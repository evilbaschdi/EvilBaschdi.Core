using System.IO.Enumeration;
using EvilBaschdi.Core.Model;

// ReSharper disable RedundantLambdaParameterType

namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class FileListFromPath : IFileListFromPath
{
    /// <inheritdoc />
    /// <summary>
    ///     Gets a list of accessible directories that contain files.
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="directory" /> is <see langword="null" />.</exception>
    public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles([NotNull] string directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        var enumeration = new FileSystemEnumerable<string>(
                              directory,
                              (ref FileSystemEntry fileSystemEntry) => fileSystemEntry.ToFullPath(),
                              new()
                              {
                                  RecurseSubdirectories = true,
                                  MatchCasing = MatchCasing.CaseInsensitive,
                                  IgnoreInaccessible = true
                              })
                          {
                              ShouldIncludePredicate = (ref FileSystemEntry fileSystemEntry) => fileSystemEntry.IsDirectory
                          };
        return enumeration.Distinct(StringComparer.OrdinalIgnoreCase);
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public IEnumerable<string> ValueFor([NotNull] string initialDirectory)
    {
        ArgumentNullException.ThrowIfNull(initialDirectory);

        return ValueFor(initialDirectory, new());
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public IEnumerable<string> ValueFor([NotNull] string initialDirectory,
                                        [NotNull] FileListFromPathFilter fileListFromPathFilter)
    {
        ArgumentNullException.ThrowIfNull(initialDirectory);

        ArgumentNullException.ThrowIfNull(fileListFromPathFilter);

        var enumeration = new FileSystemEnumerable<string>(
                              initialDirectory,
                              (ref FileSystemEntry fileSystemEntry) => fileSystemEntry.ToFullPath(),
                              new()
                              {
                                  RecurseSubdirectories = true,
                                  MatchCasing = MatchCasing.CaseInsensitive,
                                  IgnoreInaccessible = true
                              })
                          {
                              ShouldIncludePredicate = (ref FileSystemEntry fileSystemEntry) =>
                                                           !fileSystemEntry.IsDirectory && FileSystemEntryIsValid(fileSystemEntry, fileListFromPathFilter)
                          };
        return enumeration.Distinct(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool FileSystemEntryIsValid(FileSystemEntry fileSystemEntry, [NotNull] FileListFromPathFilter fileListFromPathFilter)
    {
        ArgumentNullException.ThrowIfNull(fileListFromPathFilter);

        var includeExtensionList = fileListFromPathFilter.FilterExtensionsToEqual;
        var excludeExtensionList = fileListFromPathFilter.FilterExtensionsNotToEqual;
        var includeFileNameList = fileListFromPathFilter.FilterFileNamesToEqual;
        var excludeFileNameList = fileListFromPathFilter.FilterFileNamesNotToEqual;

        var fileName = fileSystemEntry.FileName.ToString();
        var fileExtension = Path.GetExtension(fileSystemEntry.ToFullPath()).TrimStart('.');

        var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);

        //!Any() => all allowed; else => list has to contain extension, name or path
        var includeExtension = includeExtensionList.Count == 0 || includeExtensionList.Contains(fileExtension);
        var includeFileName = includeFileNameList.Count == 0 || includeFileNameList.Contains(fileName);

        // .docx
        var excludeExtension =
            excludeExtensionList.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        // ...file.x
        var excludeFileName =
            excludeFileNameList.Any(p => fileName.Contains(p, StringComparison.OrdinalIgnoreCase));

        return hasFileExtension && includeExtension && !excludeExtension && includeFileName && !excludeFileName;
    }
}