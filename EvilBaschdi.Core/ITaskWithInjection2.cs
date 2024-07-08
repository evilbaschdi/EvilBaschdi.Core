namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Task" /> ValueFor(TIn1 value1, TIn2 value2)
/// </summary>
public interface ITaskWithInjection2<in TIn1, in TIn2> : IValueFor2<TIn1, TIn2, Task>;