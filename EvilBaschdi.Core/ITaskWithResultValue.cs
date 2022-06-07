namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> Value()
/// </summary>
public interface ITaskWithResultValue<TResult> : IValueFromMethod<Task<TResult>>
{
}