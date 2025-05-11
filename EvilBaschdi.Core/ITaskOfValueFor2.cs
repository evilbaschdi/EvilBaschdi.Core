namespace EvilBaschdi.Core;

/// <summary>
///     Task&lt;TResult&gt; ValueForAsync(TIn1 valueIn1, TIn2 valueIn2);
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfValueFor2<in TIn1, in TIn2, TResult>
{
    /// <summary>
    ///     ValueForAsync
    /// </summary>
    Task<TResult> ValueForAsync(TIn1 valueIn1, TIn2 valueIn2);
}