namespace EvilBaschdi.Core;

/// <summary>
///     Task&lt;TResult&gt; ValueForAsync(TIn value);
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfValueFor<in TIn, TResult>
{
    /// <summary>
    ///     ValueForAsync
    /// </summary>
    Task<TResult> ValueForAsync(TIn value);
}