namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="copyDirectoryWithFiles"></param>
// ReSharper disable once UnusedType.Global
public class CopyDirectory(
    ICopyDirectoryWithFiles copyDirectoryWithFiles) : ICopyDirectory
{
    private readonly ICopyDirectoryWithFiles _copyDirectoryWithFiles = copyDirectoryWithFiles ?? throw new ArgumentNullException(nameof(copyDirectoryWithFiles));

    /// <inheritdoc />
    // ReSharper disable once ReplaceAsyncWithTaskReturn
    public async Task ValueFor(string sourcePath, string destinationPath)
    {
        ArgumentNullException.ThrowIfNull(sourcePath);

        ArgumentNullException.ThrowIfNull(destinationPath);

        var diSource = new DirectoryInfo(sourcePath);
        var diTarget = new DirectoryInfo(destinationPath);

        await _copyDirectoryWithFiles.ValueFor(diSource, diTarget);
    }
}