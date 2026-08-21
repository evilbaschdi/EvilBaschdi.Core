using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent queue of a specific type based on an input value.
///     <see cref="ConcurrentQueue{TResult}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TResult"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentQueueOfFor<in TIn, TResult> : IValueFor<TIn, ConcurrentQueue<TResult>>;