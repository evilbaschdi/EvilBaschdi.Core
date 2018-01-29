using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// </summary>
        public static bool IsAccessible(this string directoryPath)
        {
            try
            {
                //if GetDirectories works then is accessible
                directoryPath.DirectoryInfo().GetDirectories();
                return true;
            }
            catch (Exception)
            {
                //if exception is not accessible
                return false;
            }
        }

        /// <summary>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public static DirectoryInfo DirectoryInfo(this string directoryPath)
        {
            return new DirectoryInfo(directoryPath);
        }

        /// <summary>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public static FileInfo FileInfo(this string filePath)
        {
            return new FileInfo(filePath);
        }

        /// <summary>
        /// </summary>
        /// <param name="directories"></param>
        /// <returns></returns>
        public static List<string> GetExistingDirectories(this IEnumerable<string> directories)
        {
            var list = new ConcurrentBag<string>();
            Parallel.ForEach(directories, directory =>
                                          {
                                              if (Directory.Exists(directory))
                                              {
                                                  list.Add(directory);
                                              }
                                          });
            return list.ToList();
        }
    }
}