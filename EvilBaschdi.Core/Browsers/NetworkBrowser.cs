using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace EvilBaschdi.Core.Browsers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for NetworkBrowser.
    /// </summary>
    public sealed class NetworkBrowser : INetworkBrowser
    {
        /// <inheritdoc />
        /// <summary>
        ///     Contains an ArrayList of computers found in the network.
        /// </summary>
        public List<string> Value
        {
            get
            {
                var networkComputers = new List<string>();
                const int maxPreferredLength = -1;
                const int svTypeWorkstation = 1;
                const int svTypeServer = 2;
                var buffer = IntPtr.Zero;
                var sizeofInfo = Marshal.SizeOf(typeof(ServerInfo));

                try
                {
                    // ReSharper disable UnusedVariable
                    var ret = NetServerEnum(null, 100, ref buffer, maxPreferredLength, out var entriesRead, out var totalEntries, svTypeWorkstation | svTypeServer, null,
                        out var resHandle);
                    // ReSharper restore UnusedVariable
                    if (ret == 0)
                    {
                        for (var i = 0; i < totalEntries; i++)
                        {
                            var tmpBuffer = new IntPtr((int) buffer + i * sizeofInfo);
                            var svrInfo = (ServerInfo) Marshal.PtrToStructure(tmpBuffer, typeof(ServerInfo));
                            networkComputers.Add(svrInfo.svName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }
                finally
                {
                    NetApiBufferFree(buffer);
                }

                return networkComputers;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Contains an ArrayList of computers found in the network.
        /// </summary>
        [Obsolete("replaced with 'Value'")]
        public ArrayList GetNetworkComputers => new ArrayList(Value);

        /// <inheritdoc />
        /// <summary>
        ///     Contains an Exception if Value has thrown some.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        ///     NetServerEnum.
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dwLevel"></param>
        /// <param name="pBuf"></param>
        /// <param name="dwPrefMaxLen"></param>
        /// <param name="dwEntriesRead"></param>
        /// <param name="dwTotalEntries"></param>
        /// <param name="dwServerType"></param>
        /// <param name="domain"></param>
        /// <param name="dwResumeHandle"></param>
        /// <returns></returns>
        [DllImport("Netapi32", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern int NetServerEnum(
            string serverName,
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            int dwServerType,
            string domain,
            out int dwResumeHandle
        );

        /// <summary>
        ///     NetApiBufferFree.
        /// </summary>
        /// <param name="pBuf"></param>
        /// <returns></returns>
        [DllImport("Netapi32", SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern int NetApiBufferFree(IntPtr pBuf);

        /// <summary>
        ///     ServerInfo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct ServerInfo
        {
            private readonly int svPlatformId;

            [MarshalAs(UnmanagedType.LPWStr)] internal readonly string svName;
        }
    }
}