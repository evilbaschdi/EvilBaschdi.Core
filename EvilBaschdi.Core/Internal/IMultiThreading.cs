using System;
using System.Collections;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    /// <summary>
    ///     Interface to provide multi-threading execution.
    ///     Calls actions by processor count.
    /// </summary>
    public interface IMultiThreading : IRunFor2<IList, Action<Tuple<int, int>>>
    {
    }
}