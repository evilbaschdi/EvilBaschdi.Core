namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> Value()
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskWithResultValue<TResult> : IValueFromMethod<Task<TResult>>
{
}