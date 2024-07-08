namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> Value()
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskOfFromMethod<TResult> : IValueFromMethod<Task<TResult>>;