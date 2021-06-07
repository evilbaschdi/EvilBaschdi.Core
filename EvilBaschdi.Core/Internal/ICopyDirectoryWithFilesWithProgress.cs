using System;
using System.IO;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICopyDirectoryWithFilesWithProgress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        Task RunForAsync(DirectoryInfo source, DirectoryInfo target, IProgress<double> progress);
    }
}