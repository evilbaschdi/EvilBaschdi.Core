namespace EvilBaschdi.Core.Internal.Copy;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class CopyDirectoryWithFiles : ICopyDirectoryWithFiles
{
    /// <inheritdoc />
    public async Task RunForAsync([NotNull] DirectoryInfo source, [NotNull] DirectoryInfo target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        Directory.CreateDirectory(target.FullName);

        var files = source.GetFiles("*", SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            return;
        }

        // Copy each file into the new directory.
        foreach (var fileInfo in files)
        {
            //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);
        }

        // Copy each sub-directory using recursion.
        foreach (var diSourceSubDir in source.GetDirectories())
        {
            var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            await RunForAsync(diSourceSubDir, nextTargetSubDir);
        }
    }
}