namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="IEnumerable{T}" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IIEnumerableOfFor<in TIn, out TResult> : IValueFor<TIn, IEnumerable<TResult>>;