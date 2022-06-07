namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task" /> ValueFor(TIn value)
/// </summary>
public interface ITaskValueFor<in TIn> : IValueFor<TIn, Task>
{
}