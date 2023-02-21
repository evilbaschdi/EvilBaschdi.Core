namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class CopyDirectory : ICopyDirectory
{
    private readonly ICopyDirectoryWithFiles _copyDirectoryWithFiles;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="copyDirectoryWithFiles"></param>
    public CopyDirectory(ICopyDirectoryWithFiles copyDirectoryWithFiles)
    {
        _copyDirectoryWithFiles = copyDirectoryWithFiles ?? throw new ArgumentNullException(nameof(copyDirectoryWithFiles));
    }

    /// <inheritdoc />
    public async Task ValueFor(string sourcePath, string destinationPath)
    {
        if (sourcePath == null)
        {
            throw new ArgumentNullException(nameof(sourcePath));
        }

        if (destinationPath == null)
        {
            throw new ArgumentNullException(nameof(destinationPath));
        }

        var diSource = new DirectoryInfo(sourcePath);
        var diTarget = new DirectoryInfo(destinationPath);

        await _copyDirectoryWithFiles.ValueFor(diSource, diTarget);
    }
}