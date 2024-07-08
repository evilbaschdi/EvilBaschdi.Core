namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfWithInjection<in TIn, TResult> : IValueFor<TIn, Task<TResult>>;