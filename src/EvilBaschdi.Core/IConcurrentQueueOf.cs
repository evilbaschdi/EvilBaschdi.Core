using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent queue of a specific type.
///     <see cref="ConcurrentQueue{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentQueueOf<T> : IValue<ConcurrentQueue<T>>;