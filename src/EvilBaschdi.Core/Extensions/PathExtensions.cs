using System.Collections.Concurrent;

namespace EvilBaschdi.Core.Extensions;

/// <summary>
/// </summary>
// ReSharper disable once UnusedType.Global
public static class PathExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="directoryPath"></param>
    extension(string directoryPath)
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public bool IsAccessible()
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
        public DirectoryInfo DirectoryInfo()
        {
            return new(directoryPath);
        }

        /// <summary>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedMember.Global
        public FileInfo FileInfo()
        {
            return new(directoryPath);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="directories"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static ConcurrentBag<string> GetExistingDirectories(this IEnumerable<string> directories)
    {
        var list = new ConcurrentBag<string>();
        Parallel.ForEach(directories, directory =>
                                      {
                                          if (Directory.Exists(directory))
                                          {
                                              list.Add(directory);
                                          }
                                      });
        return list;
    }
}