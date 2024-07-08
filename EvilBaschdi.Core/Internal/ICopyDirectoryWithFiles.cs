namespace EvilBaschdi.Core.Internal;

/// <inheritdoc cref="ITaskWithInjection2{TIn1,TIn2}" />
/// <inheritdoc cref="ITaskOfWithInjection2{TIn1,TIn2,TResult}" />
public interface ICopyDirectoryWithFiles : ITaskWithInjection2<DirectoryInfo, DirectoryInfo>;