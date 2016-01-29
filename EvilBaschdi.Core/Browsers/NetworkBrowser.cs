using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

namespace EvilBaschdi.Core.Browsers
{
    public sealed class NetworkBrowser : INetworkBrowser
    {
        [DllImport("Netapi32", CharSet = CharSet.Auto,
            SetLastError = true),
         SuppressUnmanagedCodeSecurity]
        public static extern int NetServerEnum(
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

        [DllImport("Netapi32", SetLastError = true),
         SuppressUnmanagedCodeSecurity]
        public static extern int NetApiBufferFree(
            IntPtr pBuf);

        public ArrayList GetNetworkComputers
        {
            get
            {
                var networkComputers = new ArrayList();
                const int maxPreferredLength = -1;
                const int svTypeWorkstation = 1;
                const int svTypeServer = 2;
                var buffer = IntPtr.Zero;
                var sizeofInfo = Marshal.SizeOf(typeof (ServerInfo));

                try
                {
                    int entriesRead;
                    int totalEntries;
                    int resHandle;
                    var ret = NetServerEnum(null, 100, ref buffer,
                        maxPreferredLength,
                        out entriesRead,
                        out totalEntries, svTypeWorkstation |
                                          svTypeServer, null, out
                                              resHandle);
                    if (ret == 0)
                    {
                        for (var i = 0; i < totalEntries; i++)
                        {
                            var tmpBuffer = new IntPtr((int) buffer + (i*sizeofInfo));
                            var svrInfo = (ServerInfo) Marshal.PtrToStructure(tmpBuffer, typeof (ServerInfo));
                            networkComputers.Add(svrInfo.svName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem with acessing " +
                                    "network computers in NetworkBrowser " +
                                    "\r\n\r\n\r\n" + ex.Message,
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                finally
                {
                    NetApiBufferFree(buffer);
                }

                return networkComputers;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServerInfo
        {
            internal int svPlatformId;

            [MarshalAs(UnmanagedType.LPWStr)] internal string svName;
        }
    }
}