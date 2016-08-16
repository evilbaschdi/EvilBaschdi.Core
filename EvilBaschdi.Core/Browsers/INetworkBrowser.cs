using System.Collections;

namespace EvilBaschdi.Core.Browsers
{
    /// <summary>
    ///     Interface for NetworkBrowser.
    /// </summary>
    public interface INetworkBrowser
    {
        /// <summary>
        ///     Contains an ArrayList of computers found in the network.
        /// </summary>
        ArrayList GetNetworkComputers { get; }
    }
}