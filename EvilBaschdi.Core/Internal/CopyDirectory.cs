using System;
using System.IO;
using System.Threading.Tasks;

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
    public async Task RunForAsync(string sourcePath, string destinationPath)
    {
        if (sourcePath == null)
        {
            throw new ArgumentNullException(nameof(sourcePath));
        }

        if (destinationPath == null)
        {
            throw new ArgumentNullException(nameof(destinationPath));
        }

        await ValueForAsync(sourcePath, destinationPath);
    }

    /// <inheritdoc />
    public async Task<int> ValueForAsync(string sourcePath, string destinationPath)
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

        return await _copyDirectoryWithFiles.ValueForAsync(diSource, diTarget);
    }
}