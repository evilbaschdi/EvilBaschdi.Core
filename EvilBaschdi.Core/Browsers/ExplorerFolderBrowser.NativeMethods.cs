using System;
using System.Runtime.InteropServices;

namespace EvilBaschdi.Core.Browsers
{
    public sealed partial class ExplorerFolderBrowser : IFolderBrowser
    {
        #region Types

        // ReSharper disable once ClassNeverInstantiated.Local
        private class NativeMethods
        {
            [DllImport("shell32.dll")]
            public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

            [DllImport("shell32.dll")]
            public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

            [DllImport("user32.dll")]
            public static extern IntPtr GetActiveWindow();
        }

        #endregion
    }
}