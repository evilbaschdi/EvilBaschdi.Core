using System.IO.Enumeration;
using EvilBaschdi.Core.Model;

// ReSharper disable RedundantLambdaParameterType

namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class FileListFromPath : IFileListFromPath
{
    /// <inheritdoc />
    public IEnumerable<string> GetSubdirectoriesContainingOnlyFiles([NotNull] string directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        var enumeration = new FileSystemEnumerable<string>(
            directory,
            static (ref FileSystemEntry entry) => entry.ToFullPath(),
            new()
            {
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive,
                IgnoreInaccessible = true
            })
        {
            ShouldIncludePredicate = static (ref FileSystemEntry entry) => entry.IsDirectory
        };

        return enumeration.Distinct(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public IEnumerable<string> ValueFor([NotNull] string initialDirectory)
    {
        ArgumentNullException.ThrowIfNull(initialDirectory);
        return ValueFor(initialDirectory, new());
    }

    /// <inheritdoc />
    public IEnumerable<string> ValueFor([NotNull] string initialDirectory, [NotNull] FileListFromPathFilter filter)
    {
        ArgumentNullException.ThrowIfNull(initialDirectory);
        ArgumentNullException.ThrowIfNull(filter);

        var enumeration = new FileSystemEnumerable<string>(
            initialDirectory,
            static (ref FileSystemEntry entry) => entry.ToFullPath(),
            new()
            {
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive,
                IgnoreInaccessible = true
            })
        {
            ShouldIncludePredicate = (ref FileSystemEntry entry) =>
                !entry.IsDirectory && FileSystemEntryIsValid(entry, filter)
        };

        return enumeration.Distinct(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool FileSystemEntryIsValid(FileSystemEntry entry, [NotNull] FileListFromPathFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var fileName = entry.FileName.ToString();
        var fileExtension = Path.GetExtension(entry.ToFullPath()).TrimStart('.');
        var hasFileExtension = !string.IsNullOrWhiteSpace(fileExtension);

        var includeExtension = filter.FilterExtensionsToEqual.Count == 0 ||
                               filter.FilterExtensionsToEqual.Contains(fileExtension);
        var includeFileName =
            filter.FilterFileNamesToEqual.Count == 0 || filter.FilterFileNamesToEqual.Contains(fileName);

        var excludeExtension =
            filter.FilterExtensionsNotToEqual.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        var excludeFileName =
            filter.FilterFileNamesNotToEqual.Any(p => fileName.Contains(p, StringComparison.OrdinalIgnoreCase));

        return hasFileExtension && includeExtension && !excludeExtension && includeFileName && !excludeFileName;
    }
}