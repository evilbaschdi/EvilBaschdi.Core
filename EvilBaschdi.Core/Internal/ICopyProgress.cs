using System;

namespace EvilBaschdi.Core.Internal
{
    /// <summary>
    /// </summary>
    public interface ICopyProgress
    {
        /// <summary>
        ///     IProgress{T}
        /// </summary>
        IProgress<double> Progress { get; set; }

        /// <summary>
        ///     Temp size of files to copy
        /// </summary>
        double TempSize { get; set; }

        /// <summary>
        ///     Total size of files to copy
        /// </summary>
        double TotalSize { get; set; }
    }
}