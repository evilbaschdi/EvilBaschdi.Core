namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Dictionary{TKey,TValue}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
// ReSharper disable once UnusedType.Global
#pragma warning disable CA1005
public interface IDictionaryOfFor<in TIn, TKey, TValue> : IValueFor<TIn, Dictionary<TKey, TValue>>;
#pragma warning restore CA1005