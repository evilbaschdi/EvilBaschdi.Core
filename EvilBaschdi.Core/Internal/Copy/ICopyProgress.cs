namespace EvilBaschdi.Core.Internal.Copy;

/// <summary>
/// </summary>
public interface ICopyProgress
{
    /// <summary>
    ///     IProgress{T}
    /// </summary>
    // ReSharper disable once UnusedMember.Global
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