namespace EvilBaschdi.Core;

/// <summary>
///     Task RunForAsync(TIn value);
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskRunFor<in TIn>
{
    /// <summary>
    ///     RunForAsync
    /// </summary>
    Task RunForAsync(TIn value);
}