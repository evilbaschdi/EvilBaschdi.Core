using System;
using System.IO;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
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
        public void RunFor(string sourcePath, string destinationPath)
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

            _copyDirectoryWithFiles.RunFor(diSource, diTarget);
        }
    }
}