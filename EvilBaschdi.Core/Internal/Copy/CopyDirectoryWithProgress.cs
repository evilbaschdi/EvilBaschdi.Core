using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Internal.Copy;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="copyDirectoryWithFilesWithProgress"></param>
/// <param name="copyProgress"></param>
/// <exception cref="ArgumentNullException"></exception>
// ReSharper disable once UnusedType.Global
public class CopyDirectoryWithProgress(
    [NotNull] ICopyDirectoryWithFilesWithProgress copyDirectoryWithFilesWithProgress,
    [NotNull] ICopyProgress copyProgress) : ICopyDirectoryWithProgress
{
    private readonly ICopyDirectoryWithFilesWithProgress _copyDirectoryWithFilesWithProgress =
        copyDirectoryWithFilesWithProgress ?? throw new ArgumentNullException(nameof(copyDirectoryWithFilesWithProgress));

    private readonly ICopyProgress _copyProgress = copyProgress ?? throw new ArgumentNullException(nameof(copyProgress));

    /// <inheritdoc />
    // ReSharper disable once ReplaceAsyncWithTaskReturn
    public async Task RunForAsync([NotNull] string source, [NotNull] string target, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(target);

        var diSource = new DirectoryInfo(source);
        var diTarget = new DirectoryInfo(target);

        _copyProgress.TotalSize = diSource.GetDirectorySize();
        _copyProgress.TempSize = 0d;

        await _copyDirectoryWithFilesWithProgress.RunForAsync(diSource, diTarget, cancellationToken);
    }
}