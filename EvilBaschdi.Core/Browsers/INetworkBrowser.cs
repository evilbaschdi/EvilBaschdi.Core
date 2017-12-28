using System;
using System.Collections;
using System.Collections.Generic;
using EvilBaschdi.Core.DotNetExtensions;

namespace EvilBaschdi.Core.Browsers
{
    /// <inheritdoc />
    /// <summary>
    ///     Interface for NetworkBrowser.
    /// </summary>
    public interface INetworkBrowser : IValue<List<string>>
    {
        /// <summary>
        ///     Contains an ArrayList of computers found in the network.
        /// </summary>
        ArrayList GetNetworkComputers { get; }

        /// <summary>
        ///     Contains an Exception if Value has thrown some.
        /// </summary>
        Exception Exception { get; }
    }
}