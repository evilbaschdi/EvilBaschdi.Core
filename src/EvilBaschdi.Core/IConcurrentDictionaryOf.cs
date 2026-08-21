using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent dictionary of a specific key-value pair type.
///     <see cref="ConcurrentDictionary{TKey,TValue}" />
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentDictionaryOf<TKey, TValue> : IValue<ConcurrentDictionary<TKey, TValue>>;