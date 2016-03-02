using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EvilBaschdi.Core.MultiThreading
{
    public class MultiThreadingHelper : IMultiThreadingHelper
    {
        public void CallInParallelByProcessorCount(IList list, Action<Tuple<int, int>> worker)
        {
            if (list.Count > 0)
            {
                var partitionSitze = Math.Ceiling(list.Count/(decimal) Environment.ProcessorCount);

                Parallel.ForEach(Partitioner.Create(0, list.Count, (int) partitionSitze), range => { worker(range); });
            }
        }
    }

    public interface IMultiThreadingHelper
    {
        void CallInParallelByProcessorCount(IList list, Action<Tuple<int, int>> worker);
    }
}