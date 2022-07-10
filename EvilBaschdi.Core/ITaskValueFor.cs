namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface ITaskValueFor<in TIn> : IValueFor<TIn, Task>
{
}