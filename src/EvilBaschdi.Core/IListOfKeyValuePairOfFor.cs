namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="List{T}" /> of <see cref="KeyValuePair{TKey,TValue}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
// ReSharper disable once UnusedType.Global
#pragma warning disable CA1005
public interface IListOfKeyValuePairOfFor<in TIn, TKey, TValue> : IValueFor<TIn, List<KeyValuePair<TKey, TValue>>>;
#pragma warning restore CA1005