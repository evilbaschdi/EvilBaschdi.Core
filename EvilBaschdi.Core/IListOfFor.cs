namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="List{T}" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IListOfFor<in TIn, TResult> : IValueFor<TIn, List<TResult>>;