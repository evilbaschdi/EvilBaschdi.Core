using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EvilBaschdi.Core_new.Threading
{
    /// <inheritdoc />
    /// <summary>
    ///     Class to provide multi threading execution.
    /// </summary>
    public class MultiThreadingHelper : IMultiThreadingHelper
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
        public void CallInParallelByProcessorCount(IList list, Action<Tuple<int, int>> worker)
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
            var partitionSitze = Math.Ceiling(list.Count / (decimal) Environment.ProcessorCount);

            Parallel.ForEach(Partitioner.Create(0, list.Count, (int) partitionSitze), worker);
        }
    }
}