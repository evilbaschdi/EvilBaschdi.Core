using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent bag of a specific type based on an input value.
///     <see cref="ConcurrentBag{TResult}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TResult"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentBagOfFor<in TIn, TResult> : IValueFor<TIn, ConcurrentBag<TResult>>;