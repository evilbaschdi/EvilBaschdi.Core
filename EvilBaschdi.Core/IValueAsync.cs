namespace EvilBaschdi.Core;

/// <summary>
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IValueAsync<TResult>
{
    /// <summary>
    /// </summary>

    // ReSharper disable once UnusedMember.Global
    Task<TResult> ValueAsync();
}