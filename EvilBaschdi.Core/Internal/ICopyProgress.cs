namespace EvilBaschdi.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICopyProgress
    {
        /// <summary>
        /// Temp size of files to copy
        /// </summary>
        double TempSize { get; set; }
        /// <summary>
        /// Total size of files to copy
        /// </summary>
        double TotalSize { get; set; }
    }
}