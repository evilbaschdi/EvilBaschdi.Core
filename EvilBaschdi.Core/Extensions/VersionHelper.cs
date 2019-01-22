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
        ///     OS is Windows 7.
        /// </summary>
        public static bool IsWindows7 => GetWindowsClientVersion() == "Win7";

        /// <summary>
        ///     OS is Windows 8 or 8.1.
        /// </summary>
        public static bool IsWindows8 => GetWindowsClientVersion().StartsWith("Win8");

        /// <summary>
        ///     OS is Windows 10.
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows10 => GetWindowsClientVersion().StartsWith("Win10");

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
            switch (major)
            {
                case 4 when minor == 0 && build == 950:
                    return "Win95 Release 1";
                case 4 when minor == 0 && build == 1111:
                    return "Win95 Release 2";
                case 4 when minor == 3 && (build == 1212 || build == 1213 || build == 1214):
                    return "Win95 Release 2.1";
                case 4 when minor == 10 && build == 1998:
                    return "Win98";
                case 4 when minor == 10 && build == 2222:
                    return "Win98 Second Edition";
                case 4 when minor == 90:
                    return "WinMe";
                case 5 when minor == 0:
                    return "Win2000";
                case 5 when minor == 1 && build == 2600:
                    return "WinXP";
                case 6 when minor == 0:
                    return "Vista";
                case 6 when minor == 1:
                    return "Win7";
                case 6 when minor == 2 && build == 9200:
                    return "Win8";
                case 6 when minor == 3 && build == 9600:
                    return "Win8.1";
                case 10 when minor == 0 && build >= 10240:
                    return "Win10";
            }

            return "Can not find os version.";
        }
    }
}