namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task{TResult}" /> ValueFor(TIn value)
/// </summary>
public interface ITaskWithResultValueFor<in TIn, TResult> : IValueFor<TIn, Task<TResult>>
{
}