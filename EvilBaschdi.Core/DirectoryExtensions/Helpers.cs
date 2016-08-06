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
                //if exception is not accesible
                return false;
            }
        }

        public static bool IsFileLocked(this string file)
        {
            var fileInfo = new FileInfo(file);
            return fileInfo.IsFileLocked();
        }

        public static bool IsFileLocked(this FileInfo file)
        {
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
        /// </summary>
        public static double GetDirectorySize(this DirectoryInfo dir)
        {
            var sum = dir.GetFiles().Aggregate<FileInfo, double>(0, (current, file) => current + file.Length);
            return dir.GetDirectories().Aggregate(sum, (current, dir1) => current + GetDirectorySize(dir1));
        }

        public static void RenameTo(this DirectoryInfo di, string name)
        {
            if (di?.Parent == null)
            {
                throw new ArgumentNullException(nameof(di), "Directory info to rename cannot be null");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("New name cannot be null or blank", nameof(name));
            }

            di.MoveTo(Path.Combine(di.Parent.FullName, name));
        }
    }
}