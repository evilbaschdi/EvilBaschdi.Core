using System;
using System.Collections;

namespace EvilBaschdi.Core.Threading
{
    /// <summary>
    ///     Interface to provide multi threading execution.
    /// </summary>
    public interface IMultiThreadingHelper
    {
        /// <summary>
        ///     Calls actions by processor count.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="worker"></param>
        void CallInParallelByProcessorCount(IList list, Action<Tuple<int, int>> worker);
    }
}