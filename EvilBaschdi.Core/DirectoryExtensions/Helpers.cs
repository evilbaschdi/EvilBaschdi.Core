using System;
using System.IO;
using System.Linq;
using static System.String;

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
            var realpath = new DirectoryInfo(path);
            try
            {
                //if GetDirectories works then is accessible
                realpath.GetDirectories();
                return true;
            }
            catch (Exception)
            {
                //if exception is not accesible
                return false;
            }
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
            if (di == null)
            {
                throw new ArgumentNullException(nameof(di), "Directory info to rename cannot be null");
            }

            if (IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("New name cannot be null or blank", nameof(name));
            }

            di.MoveTo(Path.Combine(di.Parent.FullName, name));
        }
    }
}