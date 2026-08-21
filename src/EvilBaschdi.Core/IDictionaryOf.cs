namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Dictionary{TKey,TValue}" /> Value
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IDictionaryOf<TKey, TValue> : IValue<Dictionary<TKey, TValue>>;