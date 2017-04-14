using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.Threading
{
    /// <summary>
    ///     Class to provide multi threading execution.
    /// </summary>
    public class MultiThreadingHelper : IMultiThreadingHelper
    {
        /// <summary>
        ///     Calls actions by processor count.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="worker"></param>
        /// <exception cref="ArgumentNullException">
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