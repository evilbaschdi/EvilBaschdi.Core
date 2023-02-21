namespace EvilBaschdi.Core.Internal;

/// <inheritdoc cref="ITaskValueFor2{TIn1,TIn2}" />
/// <inheritdoc cref="ITaskWithResultValueFor2{TIn1,TIn2,TResult}" />
public interface ICopyDirectoryWithFiles : ITaskValueFor2<DirectoryInfo, DirectoryInfo>
{
}