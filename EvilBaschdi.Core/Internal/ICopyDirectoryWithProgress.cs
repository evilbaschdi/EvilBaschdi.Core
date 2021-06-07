using System;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICopyDirectoryWithProgress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        Task RunForAsync(string source, string target, IProgress<double> progress);
    }
}