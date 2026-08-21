using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent dictionary of a specific key-value pair type based on an input value.
///     <see cref="ConcurrentDictionary{TKey,TValue}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentDictionaryOfFor<in TIn, TKey, TValue> : IValueFor<TIn, ConcurrentDictionary<TKey, TValue>>;