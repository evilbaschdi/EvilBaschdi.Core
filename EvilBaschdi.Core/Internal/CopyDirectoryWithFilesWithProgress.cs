namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class CopyDirectoryWithFilesWithProgress : ICopyDirectoryWithFilesWithProgress
{
    private readonly ICopyProgress _copyProgress;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="copyProgress"></param>
    public CopyDirectoryWithFilesWithProgress([NotNull] ICopyProgress copyProgress)
    {
        _copyProgress = copyProgress ?? throw new ArgumentNullException(nameof(copyProgress));
    }

    /// <inheritdoc />
    public async Task ValueFor([NotNull] DirectoryInfo source, [NotNull] DirectoryInfo target)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

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
            await ValueFor(diSourceSubDir, nextTargetSubDir);
        }
    }
}