namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="List{T}" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IListOfWithInjection<in TIn, TResult> : IValueFor<TIn, List<TResult>>;