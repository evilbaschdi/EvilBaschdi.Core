using System.Collections.Concurrent;

namespace EvilBaschdi.Core;

/// <summary>
///     Defines a contract for a concurrent stack of a specific type based on an input value.
///     <see cref="ConcurrentStack{TResult}" /> ValueFor(TIn value)
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TResult"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IConcurrentStackOfFor<in TIn, TResult> : IValueFor<TIn, ConcurrentStack<TResult>>;