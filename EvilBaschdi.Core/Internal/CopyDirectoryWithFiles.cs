namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class CopyDirectoryWithFiles : ICopyDirectoryWithFiles
{
    /// <inheritdoc />
    public async Task ValueFor(DirectoryInfo source, DirectoryInfo target)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        Directory.CreateDirectory(target.FullName);

        var files = source.GetFiles("*", SearchOption.AllDirectories);

        if (!files.Any())
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
            await ValueFor(diSourceSubDir, nextTargetSubDir);
        }
    }
}