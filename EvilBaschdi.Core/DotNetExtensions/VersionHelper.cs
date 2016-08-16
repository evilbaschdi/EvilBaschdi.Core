using System;
using Microsoft.Win32;

namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Get if Windows Version is vista or higher.
    /// </summary>
    public class VersionHelper
    {
        /// <summary>
        ///     OS is at least Windows Vista.
        /// </summary>
        public static bool IsVista => GetWindowsClientVersion() == "Vista";

        /// <summary>
        ///     OS is Windows 7 or higher.
        /// </summary>
        public static bool IsWindows7 => GetWindowsClientVersion() == "Win7";

        /// <summary>
        ///     OS is Windows 10.
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows10()
        {
            var currentVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            // ReSharper disable once PossibleNullReferenceException
            var productName = currentVersion.GetValue("ProductName").ToString();

            return productName.StartsWith("Windows 10");
        }

        /// <summary>
        ///     Gets the real OS Version.
        ///     Application has to contain a app.manifest supporting windows 10.
        /// </summary>
        /// <returns></returns>
        public static string GetWindowsClientVersion()
        {
            var major = Environment.OSVersion.Version.Major;
            var minor = Environment.OSVersion.Version.Minor;
            var build = Environment.OSVersion.Version.Build;


            if (major == 4 && minor == 0 && build == 950)
            {
                return "Win95 Release 1";
            }
            if (major == 4 && minor == 0 && build == 1111)
            {
                return "Win95 Release 2";
            }
            if (major == 4 && minor == 3 && (build == 1212 || build == 1213 || build == 1214))
            {
                return "Win95 Release 2.1";
            }
            if (major == 4 && minor == 10 && build == 1998)
            {
                return "Win98";
            }
            if (major == 4 && minor == 10 && build == 2222)
            {
                return "Win98 Second Edition";
            }
            if (major == 4 && minor == 90)
            {
                return "WinMe";
            }
            if (major == 5 && minor == 0)
            {
                return "Win2000";
            }
            if (major == 5 && minor == 1 && build == 2600)
            {
                return "WinXP";
            }
            if (major == 6 && minor == 0)
            {
                return "Vista";
            }
            if (major == 6 && minor == 1)
            {
                return "Win7";
            }
            if (major == 6 && minor == 2 && build == 9200)
            {
                return "Win8 | Win8.1";
            }
            if (major == 6 && minor == 2 && build == 9600)
            {
                return "Win8.1 Update 1";
            }
            if (major == 10 && minor == 0 && build >= 10240)
            {
                return "Win10";
            }
            return "Can not find os version.";
        }
    }
}