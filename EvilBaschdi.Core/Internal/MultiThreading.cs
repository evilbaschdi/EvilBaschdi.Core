using System.Collections;
using System.Collections.Concurrent;

namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <summary>
///     Class to provide multi threading execution.
/// </summary>
// ReSharper disable once UnusedType.Global
public class MultiThreading : IMultiThreading
{
    /// <inheritdoc />
    /// <summary>
    ///     Calls actions by processor count.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="worker"></param>
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="list" /> is <see langword="null" />.
    ///     <paramref name="worker" /> is <see langword="null" />.
    /// </exception>
    public void RunFor(IList list, Action<Tuple<int, int>> worker)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (worker == null)
        {
            throw new ArgumentNullException(nameof(worker));
        }

        if (list.Count <= 0)
        {
            return;
        }

        var partitionSize = Math.Ceiling(list.Count / (decimal)Environment.ProcessorCount);

        Parallel.ForEach(Partitioner.Create(0, list.Count, (int)partitionSize), worker);
    }
}