namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="copyProgress"></param>
// ReSharper disable once UnusedType.Global
public class CopyDirectoryWithFilesWithProgress(
    [NotNull] ICopyProgress copyProgress) : ICopyDirectoryWithFilesWithProgress
{
    private readonly ICopyProgress _copyProgress = copyProgress ?? throw new ArgumentNullException(nameof(copyProgress));

    /// <inheritdoc />
    public async Task RunForAsync([NotNull] DirectoryInfo source, [NotNull] DirectoryInfo target)
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(target);

        Directory.CreateDirectory(target.FullName);

        var files = source.GetFiles();

        // Copy each file into the new directory.
        foreach (var fileInfo in files)
        {
            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fileInfo.Name);
            fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);

            _copyProgress.TempSize += fileInfo.Length;
            _copyProgress.Progress.Report(_copyProgress.TempSize * 100 / _copyProgress.TotalSize);
        }

        // Copy each sub-directory using recursion.
        foreach (var diSourceSubDir in source.GetDirectories())
        {
            var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            await RunForAsync(diSourceSubDir, nextTargetSubDir);
        }
    }
}