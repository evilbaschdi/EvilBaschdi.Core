using System;
using System.IO;
using System.Linq;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// </summary>
        public static bool IsAccessible(this string path)
        {
            //get directory info
            var directoryInfo = new DirectoryInfo(path);
            try
            {
                //if GetDirectories works then is accessible
                directoryInfo.GetDirectories();
                return true;
            }
            catch (Exception)
            {
                //if exception is not accessible
                return false;
            }
        }

        /// <summary>
        ///     Returns true, if a file is locked.
        /// </summary>
        /// <param name="file">Path of tile to check.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="file" /> is <see langword="null" />.</exception>
        public static bool IsFileLocked(this string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            var fileInfo = new FileInfo(file);
            return fileInfo.IsFileLocked();
        }

        /// <summary>
        ///     Returns true, if a file is locked.
        /// </summary>
        /// <param name="file">FileInfo of tile to check.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="file" /> is <see langword="null" />.</exception>
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

        /// <summary>
        ///     Extension to get size of a directory.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="dirPath" /> is <see langword="null" />.</exception>
        public static double GetDirectorySize(this string dirPath)
        {
            if (dirPath == null)
            {
                throw new ArgumentNullException(nameof(dirPath));
            }
            var dir = new DirectoryInfo(dirPath);
            return dir.GetDirectorySize();
        }


        /// <summary>
        ///     Extension to get size of a directory.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="dir" /> is <see langword="null" />.</exception>
        public static double GetDirectorySize(this DirectoryInfo dir)
        {
            if (dir == null)
            {
                throw new ArgumentNullException(nameof(dir));
            }
            var sum = dir.GetFiles().Aggregate<FileInfo, double>(0, (current, file) => current + file.Length);
            return dir.GetDirectories().Aggregate(sum, (current, dir1) => current + GetDirectorySize(dir1));
        }

        /// <summary>
        ///     Extension to rename a directory.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException">New name cannot be null or blank</exception>
        /// <exception cref="ArgumentNullException"><paramref name="dirPath" /> is <see langword="null" />.</exception>
        public static void RenameTo(this string dirPath, string name)
        {
            if (dirPath == null)
            {
                throw new ArgumentNullException(nameof(dirPath), "Directory info to rename cannot be null");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("New name cannot be null or blank", nameof(name));
            }
            var dir = new DirectoryInfo(dirPath);
            dir.RenameTo(name);
        }

        /// <summary>
        ///     Extension to rename a directory.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"><paramref name="dir" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">New name cannot be null or blank</exception>
        public static void RenameTo(this DirectoryInfo dir, string name)
        {
            if (dir?.Parent == null)
            {
                throw new ArgumentNullException(nameof(dir), "Directory info to rename cannot be null");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("New name cannot be null or blank", nameof(name));
            }

            dir.MoveTo(Path.Combine(dir.Parent.FullName, name));
        }
    }
}