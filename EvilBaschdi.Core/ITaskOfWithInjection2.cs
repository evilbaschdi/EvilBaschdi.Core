namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> ValueFor(TIn1 value1, TIn2 value2)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfWithInjection2<in TIn1, in TIn2, TResult> : IValueFor2<TIn1, TIn2, Task<TResult>>;