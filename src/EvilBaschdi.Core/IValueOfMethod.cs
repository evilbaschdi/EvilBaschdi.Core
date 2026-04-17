namespace EvilBaschdi.Core;

/// <summary>
///     Generic Interface construct to encapsulate methods
/// </summary>
/// <typeparam name="TOut"></typeparam>
public interface IValueOfMethod<out TOut>
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    TOut Value();
}