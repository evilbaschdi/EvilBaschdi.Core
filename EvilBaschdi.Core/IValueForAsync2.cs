namespace EvilBaschdi.Core;

/// <summary>
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TIn1"></typeparam>
/// <typeparam name="TIn2"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IValueForAsync2<in TIn1, in TIn2, TResult>
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    Task<TResult> ValueForAsync(TIn1 valueIn1, TIn2 valueIn2);
}