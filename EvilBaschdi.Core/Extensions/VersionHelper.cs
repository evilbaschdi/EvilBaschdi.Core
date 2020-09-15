using System;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     Get if Windows Version is vista or higher.
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        ///     OS is Windows Vista.
        /// </summary>
        public static bool IsVista => GetWindowsClientVersion() == "Vista";

        /// <summary>
        ///     OS is Windows 10.
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows10 => GetWindowsClientVersion().StartsWith("Win10");

        /// <summary>
        ///     OS is Windows 7.
        /// </summary>
        public static bool IsWindows7 => GetWindowsClientVersion() == "Win7";

        /// <summary>
        ///     OS is Windows 8 or 8.1.
        /// </summary>
        public static bool IsWindows8 => GetWindowsClientVersion().StartsWith("Win8");

        //{
        //    var currentVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

        //    // ReSharper disable once PossibleNullReferenceException
        //    var productName = currentVersion.GetValue("ProductName").ToString();

        //    return productName.StartsWith("Windows 10") || productName.StartsWith("Windows Server 2016");
        //}

        /// <summary>
        ///     Gets the real OS Version.
        ///     Application has to contain a app.manifest supporting windows 10.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static string GetWindowsClientVersion()
        {
            var major = Environment.OSVersion.Version.Major;
            var minor = Environment.OSVersion.Version.Minor;
            var build = Environment.OSVersion.Version.Build;


            // ReSharper disable once SwitchStatementMissingSomeCases
            return major switch
            {
                4 when minor == 0 && build == 950 => "Win95 Release 1",
                4 when minor == 0 && build == 1111 => "Win95 Release 2",
                4 when minor == 3 && (build == 1212 || build == 1213 || build == 1214) => "Win95 Release 2.1",
                4 when minor == 10 && build == 1998 => "Win98",
                4 when minor == 10 && build == 2222 => "Win98 Second Edition",
                4 when minor == 90 => "WinMe",
                5 when minor == 0 => "Win2000",
                5 when minor == 1 && build == 2600 => "WinXP",
                6 when minor == 0 => "Vista",
                6 when minor == 1 => "Win7",
                6 when minor == 2 && build == 9200 => "Win8",
                6 when minor == 3 && build == 9600 => "Win8.1",
                10 when minor == 0 && build >= 10240 => "Win10",
                _ => "Can not find os version.",
            };
        }
    }
}