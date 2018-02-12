using System;
using System.IO;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    public class CopyDirectoryWithFiles : ICopyDirectoryWithFiles
    {
        /// <inheritdoc />
        public void RunFor(DirectoryInfo source, DirectoryInfo target)
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

            // Copy each file into the new directory.
            foreach (var fileInfo in source.GetFiles())
            {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                RunFor(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}