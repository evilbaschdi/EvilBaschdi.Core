using System.IO;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc cref="IRunForAsync2{TIn1,TIn2}" />
    /// <inheritdoc cref="IValueForAsync2{TIn1,TIn2,TResult}" />
    public interface ICopyDirectoryWithFiles : IRunForAsync2<DirectoryInfo, DirectoryInfo>, IValueForAsync2<DirectoryInfo, DirectoryInfo, int>
    {
    }
}