namespace EvilBaschdi.Core;

/// <summary>
///     Task RunForAsync(TIn1 valueIn1, TIn2 valueIn2)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskRunFor2<in TIn1, in TIn2>
{
    /// <summary>
    ///     RunForAsync
    /// </summary>
    Task RunForAsync(TIn1 valueIn1, TIn2 valueIn2);
}