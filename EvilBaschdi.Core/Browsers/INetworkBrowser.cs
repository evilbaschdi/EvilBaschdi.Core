using System.Collections;
using EvilBaschdi.Core.DotNetExtensions;

namespace EvilBaschdi.Core.Browsers
{
    /// <inheritdoc />
    /// <summary>
    ///     Interface for NetworkBrowser.
    /// </summary>
    public interface INetworkBrowser : IValue<ArrayList>
    {
        /// <summary>
        ///     Contains an ArrayList of computers found in the network.
        /// </summary>
        ArrayList GetNetworkComputers { get; }
    }
}