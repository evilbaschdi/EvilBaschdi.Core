using System;
using System.IO;
using JetBrains.Annotations;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public static class FileInfoExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        [CanBeNull]
        // ReSharper disable once UnusedMember.Global
        public static string GetProperFilePathCapitalization([NotNull] this FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            var dirInfo = fileInfo.Directory;
            return dirInfo != null
                ? Path.Combine(dirInfo.GetProperDirectoryCapitalization(),
                    dirInfo.GetFiles(fileInfo.Name)[0].Name).Trim()
                : null;
        }

        /// <summary>
        ///     Returns true, if a file is locked.
        /// </summary>
        /// <param name="file">FileInfo of tile to check.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="file" /> is <see langword="null" />.</exception>
        // ReSharper disable once UnusedMember.Global
        public static bool IsFileLocked(this FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }
    }
}