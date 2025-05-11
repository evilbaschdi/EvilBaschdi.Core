namespace EvilBaschdi.Core;

/// <summary>
///     Task&lt;TResult&gt; ValueAsync();
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfValue<TResult>
{
    /// <summary>
    ///     ValueAsync
    /// </summary>
    Task<TResult> ValueAsync();
}