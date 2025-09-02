namespace EvilBaschdi.Core.Internal.Copy;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="copyDirectoryWithFiles"></param>
// ReSharper disable once UnusedType.Global
public class CopyDirectory(
    [NotNull] ICopyDirectoryWithFiles copyDirectoryWithFiles) : ICopyDirectory
{
    private readonly ICopyDirectoryWithFiles _copyDirectoryWithFiles = copyDirectoryWithFiles ?? throw new ArgumentNullException(nameof(copyDirectoryWithFiles));

    /// <inheritdoc />
    // ReSharper disable once ReplaceAsyncWithTaskReturn
    public async Task RunForAsync([NotNull] string sourcePath, [NotNull] string destinationPath, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sourcePath);
        ArgumentNullException.ThrowIfNull(destinationPath);

        var diSource = new DirectoryInfo(sourcePath);
        var diTarget = new DirectoryInfo(destinationPath);

        await _copyDirectoryWithFiles.RunForAsync(diSource, diTarget, cancellationToken);
    }
}